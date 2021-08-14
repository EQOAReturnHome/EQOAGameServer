using System;
using System.Buffers;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Linq;
using System.Net.Sockets;
using System.Text;
using System.Timers;
using ReturnHome.Server.Managers;
using ReturnHome.Server.Network.Enum;
using ReturnHome.Server.Network.GameMessages;
using ReturnHome.Server.Network.GameMessages.Messages;
using ReturnHome.Server.Network.Handlers;
//using ReturnHome.Server.Network.Handlers;
using ReturnHome.Server.Network.Managers;

namespace ReturnHome.Server.Network
{
    public class NetworkSession
    {
        private const int minimumTimeBetweenBundles = 5; // 5ms
        private const int timeBetweenTimeSync = 20000; // 20s
        //private const int timeBetweenAck = 300; // 300ms

        private readonly Session session;
        private readonly ServerListener connectionListener;

        private readonly Object[] currentBundleLocks = new Object[(int)GameMessageGroup.QueueMax];
        private readonly NetworkBundle[] currentBundles = new NetworkBundle[(int)GameMessageGroup.QueueMax];

        private ConcurrentDictionary<ushort, ClientMessage> outOfOrderMessages = new ConcurrentDictionary<ushort, ClientMessage>();

        private DateTime nextSend = DateTime.UtcNow;

        // Resync will be started after ConnectResponse, and should immediately be sent then, so no delay here.
        // Fun fact: even though we send the server time in the ConnectRequest, client doesn't seem to use it?  Therefore we must TimeSync early so client doesn't see a skew when we send it later.
        public bool sendResync;
        private DateTime nextResync = DateTime.UtcNow;

        public readonly SessionConnectionData ConnectionData = new SessionConnectionData();

        //If an Ack is needed to client
        public bool sendAck = false;

        //Ack's client session, or makes sure client ack's ours.
        //Probably need checks to verify client has ack'd a session we created before continuing
        public bool sendSessionAck = false;
        public bool sessionApproved = false;

        //private DateTime nextAck = DateTime.UtcNow.AddMilliseconds(timeBetweenAck);

        public ushort lastReceivedPacketSequence = 0;
        public ushort lastReceivedMessageSequence = 0;

        /// <summary>
        /// This is referenced from many threads:<para />
        /// ConnectionListener.OnDataReceieve()->Session.HandlePacket()->This.HandlePacket(packet), This path can come from any client or other thinkable object.<para />
        /// WorldManager.UpdateWorld()->Session.Update(lastTick)->This.Update(lastTick)
        /// </summary>
        private readonly ConcurrentDictionary<ushort /*seq*/, ServerPacketMessage> cachedMessages = new ConcurrentDictionary<ushort /*seq*/, ServerPacketMessage>();

        private static readonly TimeSpan cachedMessageResendInterval = TimeSpan.FromSeconds(2);
        private DateTime lastCachedMessageResendTime;
        /// <summary>
        /// Number of seconds to retain cachedPackets
        /// </summary>
        private const int cachedPacketRetentionTime = 120;

        /// <summary>
        /// This is referenced by multiple thread:<para />
        /// [ConnectionListener Thread + 0] WorldManager.ProcessPacket()->SendLoginRequestReject()<para />
        /// [ConnectionListener Thread + 0] WorldManager.ProcessPacket()->Session.ProcessPacket()->NetworkSession.ProcessPacket()->DoRequestForRetransmission()<para />
        /// [ConnectionListener Thread + 1] WorldManager.ProcessPacket()->Session.ProcessPacket()->NetworkSession.ProcessPacket()-> ... AuthenticationHandler<para />
        /// [World Manager Thread] WorldManager.UpdateWorld()->Session.Update(lastTick)->This.Update(lastTick)<para />
        /// </summary>
        private readonly ConcurrentQueue<ServerPacket> packetQueue = new ConcurrentQueue<ServerPacket>();


        /// <summary>
        /// Stores the tick value for the when an active session will timeout. If this value is in the past, the session is dead/inactive.
        /// </summary>
        public long TimeoutTick { get; set; }

        public ushort ClientId { get; }
        public ushort ServerId { get; }

        public NetworkSession(Session session, ServerListener connectionListener, ushort clientId, ushort serverId)
        {
            this.session = session;
            this.connectionListener = connectionListener;

            ClientId = clientId;
            ServerId = serverId;

            // New network auth session timeouts will always be low.
            //For now hardcode 30 seconds, once we enter world it needs to be like... 2 seconds to ping clients, maybe 60 seconds to disconnect
            //Maybe this would get set by the session location. Pre-memory dump = 30 seconds, memory dump > is 2 seconds
            TimeoutTick = DateTime.UtcNow.AddSeconds(30000).Ticks;

            for (int i = 0; i < currentBundles.Length; i++)
            {
                currentBundleLocks[i] = new object();
                currentBundles[i] = new NetworkBundle();
            }
        }


        /// <summary>
        /// Enequeues a GameMessage for sending to this client.
        /// This may be called from many threads.
        /// </summary>
        /// <param name="messages">One or more GameMessages to send</param>
        public void EnqueueSend(params GameMessage[] messages)
        {
            if (isReleased) // Session has been removed
                return;

            foreach (var message in messages)
            {
                var grp = message.Group;
                var currentBundleLock = currentBundleLocks[(int)grp];
                lock (currentBundleLock)
                {
                    var currentBundle = currentBundles[(int)grp];
                    //packetLog.DebugFormat("[{0}] Enqueuing Message {1}", session.LoggingIdentifier, message.Opcode);
                    currentBundle.Enqueue(message);
                }
            }
        }

        /// <summary>
        /// Enqueues a ServerPacket for sending to this client.
        /// Currently this is only used publicly once during login.  If that changes it's thread safety should be re
        /// </summary>
        /// <param name="packets"></param>
        public void EnqueueSend(params ServerPacket[] packets)
        {
            if (isReleased) // Session has been removed
                return;

            foreach (var packet in packets)
            {
                //packetLog.DebugFormat("[{0}] Enqueuing Packet {1}", session.LoggingIdentifier, packet.GetHashCode());
                packetQueue.Enqueue(packet);
            }
        }

        /// <summary>
        /// Prunes the cachedPackets dictionary
        /// Checks if we should send the current bundle and then flushes all pending packets.
        /// </summary>
        public void Update()
        {
            if (isReleased) // Session has been removed
                return;

            if (DateTime.UtcNow - lastCachedMessageResendTime > cachedMessageResendInterval)
            {
                CachedMessageResend();
            }

            for (int i = 0; i < currentBundles.Length; i++)
            {
                NetworkBundle bundleToSend = null;

                var group = (GameMessageGroup)i;

                var currentBundleLock = currentBundleLocks[i];
                lock (currentBundleLock)
                {
                    var currentBundle = currentBundles[i];

                    if (group == GameMessageGroup.InvalidQueue)
                    {

                        if (currentBundle.NeedsSending && DateTime.UtcNow >= nextSend)
                        {
                            //packetLog.DebugFormat("[{0}] Swapping bundle", session.LoggingIdentifier);
                            // Swap out bundle so we can process it
                            bundleToSend = currentBundle;
                            currentBundles[i] = new NetworkBundle();
                        }
                    }

                    else
                    {
                        if (currentBundle.NeedsSending && DateTime.UtcNow >= nextSend)
                        {
                            //packetLog.DebugFormat("[{0}] Swapping bundle", session.LoggingIdentifier);
                            // Swap out bundle so we can process it
                            bundleToSend = currentBundle;
                            currentBundles[i] = new NetworkBundle();
                        }
                    }
                }

                // Send our bundle if we have one
                // We should be able to execute this outside the lock as Sending is single threaded
                // and all future writes from other threads will go to the new bundle
                if (bundleToSend != null)
                {
                    //Console.WriteLine("Sending bundle...");
                    SendBundle(bundleToSend, group);
                    nextSend = DateTime.UtcNow.AddMilliseconds(minimumTimeBetweenBundles);
                }
            }

            FlushPackets();
        }

        private void CachedMessageResend()
        {
            lastCachedMessageResendTime = DateTime.UtcNow;
            var currentTime = DateTime.UtcNow;
            // Make sure our comparison still works when ushort wraps every 18.2 hours.
            var resendList = cachedMessages.Values.Where(x => DateTime.UtcNow - x.Time > cachedMessageResendInterval).ToList();

            //Process messages to send out
            while (resendList.Count > 0)
            {
                //Create a packet to hold our message('s)
                ServerPacket packet = new ServerPacket();
                List<ServerPacketMessage> messageRemoveList = new();
                int availableSpace = ServerPacket.MaxPacketSize;

                foreach (ServerPacketMessage message in resendList)
                {
                    if (availableSpace >= message.Length)
                    {
                        packet.Messages.Add(message);
                        availableSpace -= message.Length;
                        messageRemoveList.Add(message);
                    }

                    else
                        break;
                }

                EnqueueSend(packet);
                // Remove all completed messages
                resendList.RemoveAll(x => messageRemoveList.Contains(x));
            }
        }

        // This is called from ConnectionListener.OnDataReceieve()->Session.ProcessPacket()->This
        /// <summary>
        /// Processes an incoming packet from a client.
        /// </summary>
        /// <param name="packet">The ClientPacket to process.</param>
        public void ProcessPacket(ClientPacket packet)
        {
            //Develop a way to see if session has been removed/disconnected
            if (isReleased) // Session has been removed
                return;

            if (packet.Header.CancelSession)
            {
                session.Terminate(SessionTerminationReason.PacketHeaderDisconnect);
                return;
            }

            //If packet# is less then expected, let's drop it. 
            //Packet ordering is not gauranteed and messages that get resent have a new packet#, implying only messages are reliable
            var desiredSeq = lastReceivedPacketSequence + 1;
            if (packet.Header.ClientBundleNumber < desiredSeq)
            {
                //Delayed/lost packet, drop it
                return;
            }

            //Set our sequence# to our most recent, and accepted, packet.
            lastReceivedPacketSequence = packet.Header.ClientBundleNumber;

            // Processing stage
            // If we reach here, this is a packet we should proceed with processing.
            HandleOrderedPacket(packet);

            // Need to process messages in sequence
			//I believe for now we just drop out of order messages/packets, leading to forced packet order 
			//by client or eventually dropping the connection. Probably bad practice as this could lead to bad user experiences, but works for now.
            //CheckOutOfOrderPackets();
        }


        const uint MaxNumNakSeqIds = 115; //464 + header = 484;  (464 - 4) / 4

        private DateTime LastRequestForRetransmitTime = DateTime.MinValue;

        /// <summary>
        /// Handles a packet<para />
        /// Packets at this stage are already verified, "half processed", and reordered
        /// </summary>
        /// <param name="packet">ClientPacket to handle</param>
        private void HandleOrderedPacket(ClientPacket packet)
        {
            // Received an rudp report, flush out old packet up to ack
            if (packet.Header.RDPReport)
            {
                AcknowledgeSequence(packet.Header.ClientMessageAck);

                //Perform Server List actions here
                /*
                How do we know this is what we need?
                Client header information etc doesn't seem to change/indicate we should be here... So we go
                on the assumption that if client's endpoint == client's instanceid & 0x0000FFFF, it should be time to send the server list
                */
                if ((ConnectionData.MessageSequence == 0x02) && (ClientId == (session.SessionID & 0xFFFF)))
                {
                    ServerListManager.AddSession(session);
                }

                //We are at the point we are starting a master session with client
                else if ((ConnectionData.MessageSequence == 0x02) && (ClientId == ((session.SessionID - 1) & 0xFFFF)))
                {
                    //Need to randomly Generate the InstanceID some how, hardcoded for now...
                    Session newSession = new Session(connectionListener, session.EndPoint, Utilities.DNP3Creation.DNP3Session(), 220760, ClientId, ServerId, true);

                    //Try to add session to List, if it fails drop the session/packet, or process if it passes
                    if (NetworkManager.SessionHash.TryAdd(newSession))
                    {
                        //Session has been added, trigger communication to client inside new session and let client close this session/time out?
                        Console.WriteLine("Master Session created");
                    }
                    //What do we do if it fails...? Logging at minimum
                }

                else if ((lastReceivedMessageSequence == 0x00) && session.didServerInitiate)
                {
                    CharacterList charList = new CharacterList(session);
                    EnqueueSend(charList);
                }
				
				return;
            }

            // This should be set on the first packet to the server indicating the client is logging in.
            // This is the start of a three-way handshake between the client and server (LoginRequest, ConnectRequest, ConnectResponse)
            // Note this would be sent to each server a client would connect too (Login and each world).
            // In our current implimenation we handle all roles in this one server.
            if (packet.Header.HasHeaderFlag(PacketHeaderFlags.NewInstance))
            {
                //Need to trigger session ack here
                //packetLog.Debug($"[{session.LoggingIdentifier}] LoginRequest");
                sendSessionAck = true;
                sendAck = true;
                AuthenticationHandler.HandleLoginRequest(packet, session); //Revisit this, authentication/verification of sent data needs to happen

                //Assuming this went well, lets assume we got the 2 correct messages?
                lastReceivedMessageSequence = 0x02;
                return;
            }

            // Process all messages out of the packet
            foreach (ClientPacketMessage message in packet.Messages)
            {
                //Check if Message is a ping request, if it is, process it and ack
                if (message.Header.MessageType == (byte)MessageType.PingMessage)
					if (message.Header.MessageNumber == lastReceivedMessageSequence + 1)
                    {
                        //Process Ping
                        lastReceivedMessageSequence++;
                    }
				
                else
                    ProcessMessage(message);

                if (message.Header.MessageType == (byte)MessageType.ReliableMessage || message.Header.MessageType == (byte)MessageType.PingMessage)
                    sendAck = true;
            }
        }

        /// <summary>
        /// Processes a message, combining split messages as needed, then handling them
        /// </summary>
        /// <param name="message">ClientPacketMessage to process</param>
        private void ProcessMessage(ClientPacketMessage packetMessage)
        {
            ClientMessage message = null;

            message = new ClientMessage(packetMessage.Data);

            // If message is not null, we have a complete message to handle
            // when would it be null...?
            if (message != null)
            {
                // First check if this message is the next sequence, if it is not, add it to our outOfOrderMessages
                if (packetMessage.Header.MessageNumber == lastReceivedMessageSequence + 1)
                {
                    HandleMessages(message);

                }
				
                else
                {
                    outOfOrderMessages.TryAdd(packetMessage.Header.MessageNumber, message);
                }
            }
        }

        /// <summary>
        /// Handles a ClientMessage by calling InboundMessageManager
        /// </summary>
        /// <param name="message">ClientMessage to process</param>
        private void HandleMessages(ClientMessage message)
        {
            InboundMessageManager.HandleClientMessage(message, session);
            lastReceivedMessageSequence++;
        }

        /// <summary>
        /// Checks for received messages from Client that may be out of order, and if order has resumed, process them
        /// </summary>
        private void CheckOutOfOrderMessages()
        {
            while (outOfOrderMessages.TryRemove((ushort)(lastReceivedMessageSequence + 1), out var message))
            {
                HandleMessages(message);
            }
        }

        private void AcknowledgeSequence(ushort messageSequence)
        {
            //Remove stored messages here that the client ack's

            var removalList = cachedMessages.Keys.Where(x => x <= messageSequence);

            foreach (var key in removalList)
            {
                cachedMessages.TryRemove(key, out ServerPacketMessage serverMessage);
                Console.WriteLine($"Removed Message #{serverMessage.Sequence}");
            }
        }

        /* Client can request a message to be resent, eventually this would incorporate that resend method once we figure out how it works.
        private bool Retransmit(ushort sequence)
        {
            if (cachedMessages.TryGetValue(sequence, out var cachedMessage))
            {
                //Need to construct a packet with resend messages I suppose?
                EnqueueSend(cachedMessage);
                return true;
            }

            if (cachedMessages.Count > 0)
            {
                // This is to catch a race condition between .Count and .Min() and .Max()
                try
                {
                    //log.Error($"Session {session.Network?.ClientId}\\{session.EndPoint} ({session.Account}:{session.Player?.Name}) retransmit requested packet {sequence} not in cache. Cache range {cachedPackets.Keys.Min()} - {cachedPackets.Keys.Max()}.");
                }
                catch
                {
                    //log.Error($"Session {session.Network?.ClientId}\\{session.EndPoint} ({session.Account}:{session.Player?.Name}) retransmit requested packet {sequence} not in cache. Cache is empty. Race condition threw exception.");
                }
            }
            else
                //log.Error($"Session {session.Network?.ClientId}\\{session.EndPoint} ({session.Account}:{session.Player?.Name}) retransmit requested packet {sequence} not in cache. Cache is empty.");
            return false;
        }*/

        private void FlushPackets()
        {
            while (packetQueue.TryDequeue(out var packet))
            {
                //Packet should be fully formed at this point... We need to cache messages for resend, not packets
                SendPacket(packet);
            }

            if (sendAck)
                SendPacket(new ServerPacket());
        }

        private void SendPacket(ServerPacket packet)
        {
            //Console.WriteLine("Sending Packet...");
            SendPacketRaw(packet);
        }

        private void SendPacketRaw(ServerPacket packet)
        {
            try
            {
                var socket = connectionListener.Socket;

                byte[] buffer = packet.CreateReadyToSendPacket(session);

                try
                {
                    SocketAsyncEventArgs e = new();
                    e.SetBuffer(buffer);
                    e.RemoteEndPoint = session.EndPoint;

                    socket.SendToAsync(e);
                }

                catch (SocketException ex)
                {
                    // Unhandled Exception: System.Net.Sockets.SocketException: A message sent on a datagram socket was larger than the internal message buffer or some other network limit, or the buffer used to receive a datagram into was smaller than the datagram itself
                    // at System.Net.Sockets.Socket.UpdateStatusAfterSocketErrorAndThrowException(SocketError error, String callerName)
                    // at System.Net.Sockets.Socket.SendTo(Byte[] buffer, Int32 offset, Int32 size, SocketFlags socketFlags, EndPoint remoteEP)

                    var listenerEndpoint = (System.Net.IPEndPoint)socket.LocalEndPoint;
                    var sb = new StringBuilder();
                    sb.AppendLine(ex.ToString());
                    sb.AppendLine(String.Format("[{5}] Sending Packet (Len: {0}) [{1}:{2}=>{3}:{4}]", buffer.Length, listenerEndpoint.Address, listenerEndpoint.Port, session.EndPoint.Address, session.EndPoint.Port, session.Network.ClientId));
                    //log.Error(sb.ToString());

                    session.Terminate(SessionTerminationReason.SendToSocketException, null, null, ex.Message);
                }
            }

            finally
            {

            }
        }

        /// <summary>
        /// This function handles turning a bundle of messages (representing all messages accrued in a timeslice),
        /// into 1 or more packets, combining multiple messages into one packet or spliting large message across
        /// several packets as needed.
        /// </summary>
        /// <param name="bundle"></param>
        private void SendBundle(NetworkBundle bundle, GameMessageGroup group)
        {
            List<ServerMessage> messages = new List<ServerMessage>();

            // Pull all messages out and create MessageFragment objects
            while (bundle.HasMoreMessages)
            {
                var thisMessage = bundle.Dequeue();

                //Message sequence numbers don't matter here, just add all of the messages
                var message = new ServerMessage(thisMessage);
                messages.Add(message);
            }

            // Loop through while we have messages
            while (messages.Count > 0)
            {
                // Create a list to remove completed messages after iterator
                List<ServerMessage> removeList = new List<ServerMessage>();
                List<ServerPacketMessage> PacketMessages = new();

                foreach (ServerMessage message in messages)
                {
                    //Process unreliable message seperately, does not have a sequence #
                    if (message.Message.Messagetype == 0xFC)
                    {
                        //packetLog.DebugFormat("[{0}] Sending tail fragment", session.LoggingIdentifier);
                        ServerPacketMessage spf = message.GetNextMessage(0);
                        PacketMessages.Add(spf);
                        //Add to removeList
                        removeList.Add(message);
                        continue;
                    }

                    //Reliable type message
                    while (true)
                    {

                        //Do some math to break the message down
                        for (int i = 0; i < (Math.Ceiling((double)message.DataLength / PacketMessage.MaxMessageSize)); i++)
                        {
                            //packetLog.DebugFormat("[{0}] Sending tail fragment", session.LoggingIdentifier);
                            ServerPacketMessage spf = message.GetNextMessage(ConnectionData.MessageSequence++);
                            PacketMessages.Add(spf);

                            //Store resend Messages here since these are not FC types?
                            Console.WriteLine($"Storing message {message.Sequence}");
                            if (!cachedMessages.TryAdd(spf.Sequence, spf))
                                Console.WriteLine($"Error Adding Message {message.Sequence} to resend cache...");
                        }

                        removeList.Add(message);
                        //Should be done processing message, break out
                        break;
                    }
                }

                // Remove all completed messages
                messages.RemoveAll(x => removeList.Contains(x));

                //Process messages to send out
                while (PacketMessages.Count > 0)
                {
                    //Create a packet to hold our message('s)
                    ServerPacket packet = new ServerPacket();
                    List<ServerPacketMessage> messageRemoveList = new();
                    int availableSpace = ServerPacket.MaxPacketSize;

                    foreach (ServerPacketMessage message in PacketMessages)
                    {
                        if (availableSpace >= message.Length)
                        {
                            packet.Messages.Add(message);
                            availableSpace -= message.Length;
                            messageRemoveList.Add(message);
                        }

                        else
                            break;
                    }

                    EnqueueSend(packet);
                    // Remove all completed messages
                    PacketMessages.RemoveAll(x => messageRemoveList.Contains(x));
                }
            }
        }

        private bool isReleased;

        /// <summary>
        /// This will empty out arrays, collections and dictionaries, and mark the object as released.
        /// Any further work assigned to this object will be ignored.
        /// </summary>
        public void ReleaseResources()
        {
            isReleased = true;

            //Remove from server list queue, if possible there
            ServerListManager.RemoveSession(session);
            for (int i = 0; i < currentBundles.Length; i++)
                currentBundles[i] = null;

            //partialFragments.Clear();
            outOfOrderMessages.Clear();

            cachedMessages.Clear();

            packetQueue.Clear();


        }
    }
}
