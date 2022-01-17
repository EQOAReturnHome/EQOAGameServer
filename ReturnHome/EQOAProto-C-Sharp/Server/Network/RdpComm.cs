using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Net.Sockets;

using ReturnHome.Database.SQL;
using ReturnHome.Opcodes;
using ReturnHome.Server.EntityObject.Player;
using ReturnHome.Server.Managers;
using ReturnHome.Server.Network.Managers;
using ReturnHome.Utilities;

namespace ReturnHome.Server.Network
{
    /// <summary>
    /// This is user to start processing incoming and outgoing packets
    /// </summary>
    public class RdpCommIn
    {
		public readonly SessionConnectionData connectionData;
		
		private readonly ConcurrentDictionary<ushort, ReadOnlyMemory<byte>> _outOfOrderMessages = new();
		
		private readonly Session _session;
		private readonly SessionQueue _sessionQueue;
		
		public ushort clientID { get; private set; }
        public ushort serverID { get; private set; }
		public long TimeoutTick { get; set; }
		
        public RdpCommIn(Session session, ushort ClientID, ushort ServerID)
        {
			_session = session;
            _sessionQueue = new(_session);
			clientID = ClientID;
			serverID = ServerID;
            connectionData = new(_session);

            // New network auth session timeouts will always be high.
            //For now hardcode 30 seconds, once we enter world it needs to be like... 2 seconds to ping clients, maybe 60 seconds to disconnect
            //Maybe this would get set by the session location. Pre-memory dump = 30 seconds, memory dump > is 2 seconds
            if (_session.inGame)
                TimeoutTick = DateTime.UtcNow.AddSeconds(2).Ticks;
            else
                TimeoutTick = DateTime.UtcNow.AddSeconds(45).Ticks;
        }
		
        public void ProcessPacket(ClientPacket packet)
        {
            if (_session.inGame)
                TimeoutTick = DateTime.UtcNow.AddSeconds(2).Ticks;
            else
                TimeoutTick = DateTime.UtcNow.AddSeconds(45).Ticks;
            _session.ResetPing();
            //Let's make sure this isn't a delayed packet etc.
            if (packet.Header.ClientBundleNumber <= connectionData.lastReceivedPacketSequence)
                //If this bundle number is 0x0000, we are just recycling through the counter, so don't return
                if (!(packet.Header.ClientBundleNumber == 0x0000))
                    return;
            
			
			connectionData.lastReceivedPacketSequence = packet.Header.ClientBundleNumber;
			
            if (packet.Header.RDPReport)
                ProcessRdpReport(packet);

            if (packet.Header.ProcessMessage)
                ProcessMessageBundle(packet);

            if (packet.Header.SessionAck)
                ProcessSessionAck(packet);
        }

        public void ProcessMessageBundle(ClientPacket packet)
        {
            //Check to see if packet contains next message #, if it doesn't... add them to out of order?
			//Will need to check against unreliables first
			if (packet.Messages.ContainsKey((ushort)(connectionData.lastReceivedMessageSequence + 1)))
			{
				while(true)
				{
                    if (packet.Messages.TryRemove((ushort)(connectionData.lastReceivedMessageSequence + 1), out PacketMessage message))
                    {
                        connectionData.lastReceivedMessageSequence++;
                        Logger.Info($"Working with message {message.Header.MessageNumber}");

                        switch (message.Header.messageType)
                        {
                            case (byte)MessageType.PingMessage:
                                ProcessOpcode.ProcessPingRequest(_session, message);
                                _session.RdpReport = true;
                                break;

                            case (byte)MessageType.ReliableMessage:
                                ProcessOpcode.ProcessOpcodes(_session, message);
                                _session.RdpReport = true;
                                break;

                            case (byte)MessageType.UnreliableMessage:
                                Logger.Info("!!!Received FC type message from client!!!");
                                break;

                            default:
                                Logger.Info("Processing Object update messages");
                                ProcessUnreliable.ProcessUnreliables(_session, message);
                                //Do stuff to process 0x40 type messages from client
                                break;
                        }
                    }

                    //Add remaining messages to out of order since
                    else
                    {
                        foreach (var i in packet.Messages)
                            _outOfOrderMessages.TryAdd(i.Key, i.Value.Data);

                        //Should be done working with messages now
                        break;
                    }

                    if (packet.Messages.Count == 0)
                        break;
                }
			}
						//Add all of these messages to out of order list
			else
			{
				foreach(var i in packet.Messages)
					_outOfOrderMessages.TryAdd(i.Key, i.Value.Data);
			}

			if(packet.Header.ChannelAcks != null)
            {
                Span<ServerObjectUpdate> temp = connectionData.serverObjects.Span;
                foreach (KeyValuePair<byte, ushort> ack in packet.Header.ChannelAcks)
                    temp[ack.Key].UpdateBaseXor(ack.Value);
            }

            //Check client update message
            if (packet.clientUpdate != null)
            {
                Logger.Info("Processing Object update messages");
                ProcessUnreliable.ProcessUnreliables(_session, packet.clientUpdate);
            }
            Logger.Info($"{_session.ClientEndpoint.ToString("X")}: Done processing messages in packet");
            ///Should we just initiate responses to clients through here for now?
            ///Ultimately we want to have a seperate thread with a server tick, 
            ///that may handle initiating sending messages at timed intervals, and initiating data collection such as C9's
        }

        private void ProcessRdpReport(ClientPacket packet)
        {
            //Update our connection data object
            //Let's make sure the ack > current saved ack
            if (connectionData.clientLastReceivedMessage < packet.Header.ClientMessageAck)
                connectionData.clientLastReceivedMessage = packet.Header.ClientMessageAck;

            //Logging an old ack that was received here... should something else happen?
            else
            {
                Logger.Info($"Received an old ack, Expected: {connectionData.lastReceivedMessageSequence} Received: {packet.Header.ClientMessageAck}");
                return;
            }
            //Check reliable resend queue
            _sessionQueue.RemoveReliables();

            ///Triggers Character select, after client soft ack's the 2nd message we sent.
            if ((connectionData.clientLastReceivedMessage == 0x02) && _session.didServerInitiate)
            {
                CharacterSQL charList = new();
                Logger.Info($"{_session.rdpCommIn.clientID.ToString("X")}: Generating Character Select");
                List<Character> MyCharacterList = charList.AccountCharacters(_session);

				//May need to be fixed up
                ProcessOpcode.CreateCharacterList(MyCharacterList, _session);      
            }

            ///Triggers creating master session
            else if ((connectionData.lastReceivedMessageSequence == 0x02) && (clientID == ((_session.InstanceID - 1) & 0xFFFF)))
            {
                Console.WriteLine("Creating master session");
                SessionManager.CreateMasterSession(_session);
            }

            ///Trigger Server Select with this?
            else if ((connectionData.lastReceivedMessageSequence == 0x02) && (clientID == (_session.InstanceID & 0xFFFF)))
            {
                ServerListManager.AddSession(_session);
            }
        }

        
        private void ProcessSessionAck(ClientPacket packet)
        {
            if (_session.InstanceID == packet.Header.SessionAckID)
                //Session has been ack'd and no longer new
                _session.Instance = false;

                //Doesn't seem to really matter,we could use this as a check and balance and deny the session if this check fails?
                return;
        }
    }

    public class RdpCommOut
    {
        private readonly Session _session;
        public readonly ServerListener _listener;
        private int _offset;
        private int _value = 0;
		public int totalLength;
		private int _headerLength;
		public int maxSize = 1200;
		private Memory<byte> temp;

        public RdpCommOut(Session session, ServerListener listener)
        {
            _session = session;
			_offset = 0;
			totalLength = 0;
			_headerLength = 0;
            _listener = listener;
        }
		
        public void PrepPackets()
        {
            if (_session.sessionQueue.CheckQueue() || _session.RdpReport || _session.clientUpdateAck)
            {
                List<ReadOnlyMemory<byte>> messageList;

                (totalLength, messageList) = _session.sessionQueue.GatherMessages();
				//Calculate expected expected outgoing packet length
				// 4 bytes for endpoints,  , if has instance + 4, if RemoteMaster true + 3? Depends how we handle sessionID's, for now 3 works 
				GetHeaderLength();
				temp = new Memory<byte>(new byte[totalLength]);
                Span<byte> packet = temp.Span;
				
				//Jump offset past endpoints and bundle header information
				if (_value > 0x3000)// || _session.CancelConnection)
					_offset += 7;
			
				else
					_offset += 6;
				
                ///Add Session Information
                AddSession(packet);

                //Add bundle type first
                AddBundleType(packet);

                ///Add session ack here if it has not been done yet
                ///Lets client know we acknowledge session
                ///Making sure remoteMaster is 1 (client) makes sure we have them ack our session
                if (_session.SessionAck)
                {
                    //Change this to false then process
                    _session.SessionAck = false;

                    ///To ack session, we just repeat session information as an ack
                    AddSessionAck(packet);
                }

                AddRDPReport(packet);
                _session.Reset();
				
				AddMessages(packet, messageList);
				
				//Add session header data
				AddSessionHeader(packet);

                //Adjust offset to last 4 bytes
                _offset = packet.Length - 4;

                //Add CRC
                packet.Write(CRC.calculateCRC(packet[0..(packet.Length - 4)]), ref _offset);

                SocketAsyncEventArgs args = new();
                args.RemoteEndPoint = _session.MyIPEndPoint;
                args.SetBuffer(temp);

                //Send Packet
                _listener.socket.SendToAsync(args);

                _offset = 0;
                _headerLength = 0;
                totalLength = 0;
                _value = 0;
            }
        }
		
		private int GetHeaderLength()
		{
			//Client/Server endpoints
			totalLength += 4;
			_headerLength += 4;

            if (_session.Instance) //When server initiates instance with the client, it will use this
            {
                _value |= 0x80000;
            }

            if (_session.didServerInitiate) // Purely a guess.... Something is 0x4000 in this and seems to correspond the initator of the session
            {
                //This needs to be more dynamic, technically the sessionID length can vary when packed but 3 is super common
                totalLength += 3;
                _headerLength += 3;
                _value |= 0x04000;
            }

            else // Server is not master, seems to always have this when not in control
            {
                _value |= 0x01000;
            }

            if (_session.hasInstance) // Server always has instance ID, atleast untill we are in world awhile, then it can drop this and the 4 byte instance ID
            {
                totalLength += 4;
                _headerLength += 4;
                _value |= 0x02000;
            }

            //If _value is over 0x3000, means the header info should be 3 bytes. is _value is 0, header will be the packet length using variable length integer.... So will have to rethink this a little bit eventually
            byte temp = _value > 0x3000 ? (byte)3 : (byte)2;
            totalLength += temp;
            _headerLength += temp;
			
			//Bundle Type & Bundle #
			totalLength += 3;
			
			if(_session.SessionAck)
				totalLength += 4;
			
			if (_session.RdpReport)
				totalLength += 4;
			
			if (_session.clientUpdateAck)
				totalLength += 4;
			
			//Add 4 more for CRC upfront
			//If we ever implement server transfers, may need to rethink this as those do not use crc
			totalLength += 4;

            return totalLength;
		}

		private void AddMessages(Span<byte> packet, List<ReadOnlyMemory<byte>> messageList)
		{
            foreach( ReadOnlyMemory<byte> message in messageList)
            {
                packet.Write(message, ref _offset);
            }
		}

        ///Identifies if full RDPReport is needed or just the current bundle #
        private void AddRDPReport(Span<byte> packet)
        {
            packet.Write(_session.rdpCommIn.connectionData.lastSentPacketSequence++, ref _offset);
            ///If RDP Report == True, Current bundle #, Last Bundle received # and Last message received #
            if (_session.RdpReport)
            {
                Logger.Info("Full RDP Report");

                packet.Write(_session.rdpCommIn.connectionData.lastReceivedPacketSequence, ref _offset);
                packet.Write(_session.rdpCommIn.connectionData.lastReceivedMessageSequence, ref _offset);
                /// Add them to packet in "reverse order" stated above
            }

            //If ingame, include 4029 ack's
            if (_session.clientUpdateAck)
            {
                packet.Write((byte)0x40, ref _offset);
                packet.Write(_session.rdpCommIn.connectionData.client.BaseXorMessage, ref _offset);
                packet.Write((byte)0xF8, ref _offset);
                _session.clientUpdateAck = false;
                return;
            }
        }

        ///Add our bundle type
        ///Consideration for in world or "certain packet" # is needed during conversion. For now something basic will work
        private void AddBundleType(Span<byte> packet)
        {
            byte segBody = 0;

            //Always has this
            segBody |= 0x20;

            if (_session.RdpReport)
            {
                segBody |= 0x03;
            }

            if (_session.clientUpdateAck)
            {
                segBody |= 0x10;
            }

            if (_session.SessionAck)
            {
                segBody |= 0x40;
            }

            packet.Write(segBody, ref _offset);
        }

        ///Add a session ack to send to client
        private void AddSession(Span<byte> packet)
        {
            Logger.Info($"{_session.ClientEndpoint.ToString("X")}: Adding Session Data");
            if (_session.hasInstance)
            {
                packet.Write(_session.InstanceID, ref _offset);
            }
			
            if (_session.didServerInitiate)
            {
				packet.Write7BitEncodedInt(_session.SessionID, ref _offset);
            }
        }
		
		private void AddSessionAck(Span<byte> packet)
        {
            packet.Write(_session.InstanceID, ref _offset);
        }

        private void AddSessionHeader(Span<byte> packet)
        {
			_offset = 0;
            Logger.Info($"{_session.ClientEndpoint.ToString("X")}: Adding Session Header");

            packet.Write(_session.rdpCommIn.serverID, ref _offset);
            packet.Write(_session.rdpCommIn.clientID, ref _offset);

			//Subtract CRC length from total count also
			packet.Write7BitEncodedInt((uint)(_value + (totalLength - _headerLength - 4)), ref _offset);
        }
    }
}
