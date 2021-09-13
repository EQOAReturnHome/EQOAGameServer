using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Net.Sockets;
using ReturnHome.Opcodes;
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
		public readonly SessionConnectionData connectionData = new();
		
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
			
			// New network auth session timeouts will always be high.
            //For now hardcode 30 seconds, once we enter world it needs to be like... 2 seconds to ping clients, maybe 60 seconds to disconnect
            //Maybe this would get set by the session location. Pre-memory dump = 30 seconds, memory dump > is 2 seconds
            TimeoutTick = DateTime.UtcNow.AddSeconds(30000).Ticks;
        }
		
        public void ProcessPacket(ClientPacket packet)
        {
            //At this point we have received contact from client, reset it's timer
            TimeoutTick = DateTime.UtcNow.AddSeconds(30000).Ticks;
			
			//Let's make sure this isn't a delayed packet etc.
			if(packet.Header.ClientBundleNumber <= connectionData.lastReceivedPacketSequence)
				return;
			
			connectionData.lastReceivedPacketSequence = packet.Header.ClientBundleNumber;
			
            if (packet.Header.RDPReport)
                ProcessRdpReport(packet);

            if (packet.Header.ProcessMessage)
                ProcessMessageBundle(packet);
			
			/*Probably don't even need this...
            if (packet.ClientAck) 
			*/
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
                        ProcessOpcode.ProcessOpcodes(_session, message);
						_session.RdpReport = true;
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
			
			/*else if ()
			{
				
			}*/
			
			//Add all of these messages to out of order list
			else
			{
				foreach(var i in packet.Messages)
					_outOfOrderMessages.TryAdd(i.Key, i.Value.Data);
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

            ///Triggers Character select
            if ((connectionData.lastReceivedMessageSequence == 0x00) && _session.didServerInitiate)
            {
                /*
                Logger.Info($"{_session.ClientEndpoint.ToString("X")}: Generating Character Select");
                Character[] MyCharacterList = SQLOperations.AccountCharacters(_session);

				//May need to be fixed up
                ProcessOpcode.CreateCharacterList(sessionQueueMessages, MyCharacterList, _session);
                */
            }

            ///Triggers creating master session
            else if ((connectionData.lastReceivedMessageSequence == 0x02) && (_session.ClientEndpoint == ((_session.SessionID - 1) & 0xFFFF)))
            {
                SessionManager.CreateMasterSession(_session);
            }

            ///Trigger Server Select with this?
            else if ((connectionData.lastReceivedMessageSequence == 0x02) && (_session.ClientEndpoint == (_session.SessionID & 0xFFFF)))
            {
                ServerListManager.AddSession(_session);
            }
        }

        /*
        private void ProcessSessionAck(Session MySession, ClientPacket packet, ref int offset)
        {
            if (SessionAck == MySession.InstanceID)
            {
                MySession.Instance = true;
                Logger.Info($"{MySession.ClientEndpoint.ToString("X")}: Beginning Character Select creation");
            }

            else
            {
                Logger.Err($"{MySession.ClientEndpoint.ToString("X")}: Error occured with Session Ack Check...");
            }
        }
        */
    }

    public class RdpCommOut
    {
        private readonly Session _session;
        private readonly ServerListener _listener;
        private int _offset;
		public int totalLength;
		private int _headerLength;
		public int maxSize = 1024;
		private Memory<byte> packet;

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
            if (_session.sessionQueue.CheckQueue() || _session.RdpReport)
            {
                _session.ResetPing();

                List<ReadOnlyMemory<byte>> messageList;

                (totalLength, messageList) = _session.sessionQueue.GatherMessages();
				//Calculate expected expected outgoing packet length
				// 4 bytes for endpoints,  , if has instance + 4, if RemoteMaster true + 3? Depends how we handle sessionID's, for now 3 works 
				GetHeaderLength();
				packet = new Memory<byte>(new byte[totalLength]);
				
				//Jump offset past endpoints and bundle header information
				if (_session.Instance)// || _session.CancelConnection)
					_offset += 7;
			
				else
					_offset += 6;
				
                ///Add Session Information
                AddSession();

                //Add bundle type first
                AddBundleType();

                ///Add session ack here if it has not been done yet
                ///Lets client know we acknowledge session
                ///Making sure remoteMaster is 1 (client) makes sure we have them ack our session
                if (!_session.SessionAck)
                {
                    _session.Instance = true;
                    _session.SessionAck = true;
                    ///To ack session, we just repeat session information as an ack
                    AddSessionAck();
                }

                AddRDPReport();
                _session.Reset();
				
				AddMessages(messageList);
				
				//Add session header data
				AddSessionHeader();

                //Adjust offset to last 4 bytes

                _offset = packet.Length - 4;

                //Add CRC
                packet.Write(CRC.calculateCRC(packet.Span[0..(packet.Length - 4)]), ref _offset);

                SocketAsyncEventArgs args = new();
                args.RemoteEndPoint = _session.MyIPEndPoint;
                args.SetBuffer(packet);

                //Send Packet
                //_listener.socket.SendToAsync(args);
            }
        }
		
		private int GetHeaderLength()
		{
			//Client/Server endpoints
			totalLength += 4;
			_headerLength += 4;
			
			//If new instance
			if (_session.Instance)// || _session.CancelConnection)
			{
				_headerLength += 3;
				totalLength += 3;
			}
			
			else
			{
				totalLength += 2;
				_headerLength += 2;
			}
			
			if(_session.hasInstance)
			{
				totalLength += 4;
				_headerLength += 4;
			}	
			
			if(_session.didServerInitiate)
			{
				totalLength += 3;
				_headerLength += 3;
			}
			
			//Bundle Type & Bundle #
			totalLength += 3;
			
			if(!_session.SessionAck)
				totalLength += 4;
			
			if (_session.RdpReport)
				totalLength += 4;
			
			if (_session.Channel40Ack)
				totalLength += 4;
			
			//Add 4 more for CRC upfront
			//If we ever implement server transfers, may need to rethink this as those do not use crc
			totalLength += 4;

            return totalLength;
		}

		private void AddMessages(List<ReadOnlyMemory<byte>> messageList)
		{
            foreach( ReadOnlyMemory<byte> message in messageList)
            {
                packet.Write(message, ref _offset);
            }
		}

        ///Identifies if full RDPReport is needed or just the current bundle #
        private void AddRDPReport()
        {
            packet.Write(_session.rdpCommIn.connectionData.lastSentPacketSequence++, ref _offset);
            ///If RDP Report == True, Current bundle #, Last Bundle received # and Last message received #
            if (_session.RdpReport)
            {
                Logger.Info("Full RDP Report");

                packet.Write(_session.rdpCommIn.connectionData.lastReceivedPacketSequence, ref _offset);
                packet.Write(_session.rdpCommIn.connectionData.lastReceivedMessageSequence, ref _offset);
                /// Add them to packet in "reverse order" stated above
                /// 
                //If ingame, include 4029 ack's
                if (_session.Channel40Ack)
                {
                    packet.Write(0x40, ref _offset);
                    packet.Write(_session.Channel40MessageNumber, ref _offset);
                    packet.Write(0xF8, ref _offset);
                    _session.Channel40Ack = false;
                    return;
                }
            }
        }

        ///Add our bundle type
        ///Consideration for in world or "certain packet" # is needed during conversion. For now something basic will work
        private void AddBundleType()
        {
            byte segBody = 0;

            //Always has this
            segBody |= 0x20;

            if (_session.RdpReport)
            {
                segBody |= 0x03;
            }

            if (_session.Channel40Ack)
            {
                segBody |= 0x10;
            }

            if (!_session.SessionAck)
            {
                segBody |= 0x40;
            }

            packet.Write(segBody, ref _offset);
        }

        ///Add a session ack to send to client
        private void AddSession()
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
		
		private void AddSessionAck()
        {
            packet.Write(_session.InstanceID, ref _offset);
        }

        private void AddSessionHeader()
        {
			_offset = 0;
            uint value = 0;
            Logger.Info($"{_session.ClientEndpoint.ToString("X")}: Adding Session Header");

            packet.Write(_listener.serverEndPoint, ref _offset);
            packet.Write(_session.rdpCommIn.clientID, ref _offset);
			
            if (!_session.Instance) //When server initiates instance with the client, it will use this
            {
                value |= 0x80000;
            }

            //This would be if we want to cancel the connection... Client handles this in most cases? For now.
            //if (MySession.CancelConnection) // 
            //{
            //    value += 0x10000;
            //}

            if (_session.didServerInitiate) // Purely a guess.... Something is 0x4000 in this and seems to correspond the initator of the session
            {
                value |= 0x04000;
            }

            else // Server is not master, seems to always have this when not in control
            {
                value |= 0x01000;
            }

            if (_session.hasInstance) // Server always has instance ID, atleast untill we are in world awhile, then it can drop this and the 4 byte instance ID
            {
                value |= 0x02000;
            }

			//Subtract CRC length from total count also
			packet.Write7BitEncodedInt((uint)(value + (totalLength - _headerLength - 4)), ref _offset);
        }
    }
}
