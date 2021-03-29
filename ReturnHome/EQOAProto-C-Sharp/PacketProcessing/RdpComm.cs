using System;
using System.Collections.Generic;
using ReturnHome.Actor;
using ReturnHome.Opcodes;
using ReturnHome.SQL;
using ReturnHome.Utilities;
using ServerSelect;

namespace ReturnHome.PacketProcessing
{
    /// <summary>
    /// This is user to start processing incoming and outgoing packets
    /// </summary>
    public class RdpCommIn
    {
        private readonly SessionManager _sessionManager;
        private readonly ProcessOpcode _processOpcode;
        public readonly SessionQueueMessages queueMessages = new();
        private readonly ProcessUnreliable _processUnreliable;

        public RdpCommIn(SessionManager sessionManager)
        {
            _sessionManager = sessionManager;
            _processOpcode = new(queueMessages, sessionManager);
            _processUnreliable = new();
        }
        public void ProcessBundle(Session MySession, ReadOnlyMemory<byte> ClientPacket, int offset)
        {
            //At this point we have received contact from client, reset it's timer
            MySession.ClientTimerReset();

            ///Perform a check to find what switch statement is true
            switch (BinaryPrimitiveWrapper.GetLEByte(ClientPacket, ref offset))
            {
                case BundleOpcode.ProcessAll:

                    ProcessSessionAck(MySession, ClientPacket, ref offset);
                    ProcessRdpReport(MySession, ClientPacket, ref offset);
                    ProcessMessageBundle(MySession, ClientPacket, offset);
                    Logger.Info("Processing Session Ack, Rdp Report and Message Bundle");
                    break;

                case BundleOpcode.NewProcessReport:
                    ProcessRdpReport(MySession, ClientPacket, ref offset);
                    ProcessMessageBundle(MySession, ClientPacket, offset);
                    Logger.Info("Processing Rdp Report and messages");
                    break;

                case BundleOpcode.ProcessReport:
                    ProcessRdpReport(MySession, ClientPacket, ref offset);
                    Logger.Info("Processing Rdp Report");
                    break;

                case BundleOpcode.NewProcessMessages:
                case BundleOpcode.ProcessMessages:

                    MySession.clientBundleNumber = BinaryPrimitiveWrapper.GetLEUShort(ClientPacket, ref offset);

                    //2 bytes for Bundle #, is it relevant to track?
                    ProcessMessageBundle(MySession, ClientPacket, offset);

                    Logger.Info("Processing Message Bundle");
                    break;

                case BundleOpcode.ProcessMessageAndReport:
                    ProcessRdpReport(MySession, ClientPacket, ref offset);
                    ProcessMessageBundle(MySession, ClientPacket, offset);
                    Logger.Info("Processing unreliable Rdp Report and messages");
                    break;

                default:
                    Logger.Err("Unable to identify Bundle Type");
                    break;
            }
        }

        public void ProcessMessageBundle(Session MySession, ReadOnlyMemory<byte> ClientPacket, int offset)
        {
            ///Need to consider how many messages could be in here, and message types
            ///FB/FA/40/F9
            ///
            while (ClientPacket.Length > (offset + 4))
            {
                ///Get our Message Type
                ushort MessageTypeOpcode = GrabMessageType(ClientPacket, ref offset);
                switch (MessageTypeOpcode)
                {
                    //This signifies the end of the unreliable ack's for this client
                    case MessageOpcodeTypes.UnknownMessage:
                        break;

                    //reliable message type
                    case MessageOpcodeTypes.ShortReliableMessage:

                        _processOpcode.ProcessOpcodes(MySession, MessageTypeOpcode, ClientPacket, ref offset);
                        break;

                    //Client ping request, process accordingly
                    case MessageOpcodeTypes.PingMessage:
                        _processOpcode.ProcessPingRequest(queueMessages, MySession, ClientPacket, ref offset);
                        break;

                    default:
                        //This will process our unreliable message, or unreliable ack's
                        _processUnreliable.ProcessUnreliables(MySession, MessageTypeOpcode, ClientPacket, ref offset);
                        break;
                }  
            }
            MySession.RdpReport = true;
            Logger.Info("Done processing messages in packet");
            ///Should we just initiate responses to clients through here for now?
            ///Ultimately we want to have a seperate thread with a server tick, 
            ///that may handle initiating sending messages at timed intervals, and initiating data collection such as C9's
        }

        private void ProcessRdpReport(Session MySession, ReadOnlyMemory<byte> ClientPacket, ref int offset)
        {
            //Eventually incorporate UnreliableReport to cycle through unreliables and update accordingly

            //Read client bundle here. Accept client bundle as is because packets could be lost or dropped, client bundle# should not be "tracked"
            //considerations could be to track it for possible drop/lost rates
            MySession.clientBundleNumber = BinaryPrimitiveWrapper.GetLEUShort(ClientPacket, ref offset);
            ushort LastRecvBundleNumber = BinaryPrimitiveWrapper.GetLEUShort(ClientPacket, ref offset);
            ushort LastRecvMessageNumber = BinaryPrimitiveWrapper.GetLEUShort(ClientPacket, ref offset);

            //Check reliable resend queue
            MySession.sessionQueue.RemoveReliables(LastRecvMessageNumber);

            ///Triggers Character select
            if ((MySession.ClientEndpoint != MySession.InstanceID) && MySession.ServerMessageNumber == 3)
            {
                Logger.Info("Generating Character Select");
                List<Character> MyCharacterList = SQLOperations.AccountCharacters(MySession);

                //Assign to our session
                MySession.CharacterData = MyCharacterList;

                ProcessOpcode.CreateCharacterList(queueMessages, _sessionManager, MyCharacterList, MySession);
            }

            ///Triggers creating master session
            else if (((MySession.ClientEndpoint + 1) == (MySession.InstanceID & 0x0000FFFF)) && MySession.ServerMessageNumber == 2 && MySession.SessionAck)
            {
                _sessionManager.CreateMasterSession(MySession);
            }

            ///Trigger Server Select with this?
            else if ((MySession.ClientEndpoint == (MySession.InstanceID & 0x0000FFFF)) && MySession.ServerMessageNumber == 2 && MySession.SessionAck)
            {
                SelectServer.GenerateServerSelect(queueMessages, MySession);
            }

            else
            {
                Logger.Err($"Client received server message {LastRecvMessageNumber}, expected {MySession.ServerMessageNumber}");
            }
        }

        private void ProcessSessionAck(Session MySession, ReadOnlyMemory<byte> ClientPacket, ref int offset)
        {
            uint SessionAck = BinaryPrimitiveWrapper.GetLEUint(ClientPacket, ref offset);
            if (SessionAck == MySession.InstanceID)
            {
                MySession.Instance = true;
                Logger.Info("Beginning Character Select creation");
            }

            else
            {
                Console.WriteLine("Error");
                Logger.Err("Error occured with Session Ack Check...");
            }
        }

        ///This grabs the Message Type
        private static ushort GrabMessageType(ReadOnlyMemory<byte> ClientPacket, ref int offset)
        {
            byte Opcode = BinaryPrimitiveWrapper.GetLEByte(ClientPacket, ref offset);
            return Opcode;
        }
    }

    public class RdpCommOut
    {

        public PacketCreator packetCreator = new();
        private readonly HandleOutPacket _handleOutPacket;
        private readonly SessionManager _sessionManager;

        public RdpCommOut(UDPListener UdpListener, SessionManager sessionManager)
        {
            _sessionManager = sessionManager;
            _handleOutPacket = new(UdpListener);
        }
        public void PrepPacket()
        {
            foreach (Session MySession in _sessionManager.SessionHash)
            {
                MySession.UnreliablePing();
                if (MySession.sessionQueue.CheckQueue() || MySession.RdpReport)
                {
                     MySession.ResetPing();
                    ///Add Session Information
                    AddSession(MySession);

                    //Add bundle type first
                    AddBundleType(MySession);

                    ///Add session ack here if it has not been done yet
                    ///Lets client know we acknowledge session
                    ///Making sure remoteMaster is 1 (client) makes sure we have them ack our session
                    if (!MySession.SessionAck)
                    {
                        MySession.Instance = true;
                        MySession.SessionAck = true;
                        ///To ack session, we just repeat session information as an ack
                        AddSession(MySession);
                    }

                    AddRDPReport(MySession);
                    MySession.Reset();

                    MySession.sessionQueue.GatherMessages(packetCreator);

                    ///Bundle needs to be incremented after every sent packet, seems like a good spot?
                    MySession.IncrementServerBundleNumber();

                    ///Add the Session stuff here that has length built in with session stuff
                    byte[] SessionHeader = AddSessionHeader(MySession);

                    ///Done? Send to CommManagerOut
                    _handleOutPacket.AddEndPoints(packetCreator, MySession, SessionHeader);
                }
            }
        }

        ///Identifies if full RDPReport is needed or just the current bundle #
        public void AddRDPReport(Session MySession)
        {
            packetCreator.PacketWriter(BitConverter.GetBytes(MySession.ServerBundleNumber));
            ///If RDP Report == True, Current bundle #, Last Bundle received # and Last message received #
            if (MySession.RdpReport)
            {
                Logger.Info("Full RDP Report");

                packetCreator.PacketWriter(BitConverter.GetBytes(MySession.clientBundleNumber));
                packetCreator.PacketWriter(BitConverter.GetBytes(MySession.clientMessageNumber));
                /// Add them to packet in "reverse order" stated above
                /// 
                //If ingame, include 4029 ack's
                if (MySession.Channel40Ack)
                {
                    packetCreator.PacketWriter(new byte[] {0x40});
                    packetCreator.PacketWriter(BitConverter.GetBytes(MySession.Channel40Base.Messagenumber));
                    packetCreator.PacketWriter(new byte[] { 0xF8 });
                    MySession.Channel40Ack = false;
                    return;
                }
            }
        }

        ///Add our bundle type
        ///Consideration for in world or "certain packet" # is needed during conversion. For now something basic will work
        public void AddBundleType(Session MySession)
        {
            byte segBody = 0;

            //Always has this
            segBody |= 0x20;

            if (MySession.RdpReport)
            {
                segBody |= 0x03;
            }

            if (MySession.Channel40Ack)
            {
                segBody |= 0x10;
            }

            if (!MySession.SessionAck)
            {
                segBody |= 0x40;
            }

            packetCreator.PacketWriter(new byte[] { segBody });
            ///Should this be a big switch statement?
            ///Using if else for now
            ///
            /*
            if (MySession.BundleTypeTransition)
            {
                if (MySession.RdpMessage && MySession.RdpReport)
                {
                    Logger.Info("Adding Bundle Type 0x13");
                    packetCreator.PacketWriter(new byte[] { 0x13});
                }

                ///Message only packet, no RDP Report
                else if (MySession.RdpMessage && !MySession.RdpReport && !MySession.Channel40Ack)
                {
                    Logger.Info("Adding Bundle Type 0x00");
                    packetCreator.PacketWriter(new byte[] { 0x00 });
                }

                ///RDP Report only
                else if (MySession.RdpReport)
                {
                    Logger.Info("Adding Bundle Type 0x03");
                    packetCreator.PacketWriter(new byte[] { 0x03 });
                }
            }

            else 
            {
                ///If Message and RDP report
                if (!MySession.SessionAck && MySession.RdpMessage && MySession.RdpReport)
                {
                    Logger.Info("Adding Bundle Type 0x63");
                    packetCreator.PacketWriter(new byte[] { 0x63 });
                }

                ///Message only packet, no RDP Report
                else if (MySession.SessionAck && MySession.RdpMessage && !MySession.RdpReport)
                {
                    Logger.Info("Adding Bundle Type 0x20");
                    packetCreator.PacketWriter(new byte[] { 0x20 });
                }
                else if(MySession.SessionAck  && MySession.Channel40Ack (()
                {
                    packetCreator.PacketWriter(BitConverter.GetBytes((byte)0x40));
                    packetCreator.PacketWriter(BitConverter.GetBytes(MySession.Channel40Message));
                    packetCreator.PacketWriter(BitConverter.GetBytes((byte)0xF8));

                    Logger.Info("Adding Bundle Type 0x33");
                    packetCreator.PacketWriter(new byte[] { 0x33 });

                    MySession.Channel40Ack = false;
                }

                ///RDP Report only
                else if (MySession.SessionAck && MySession.RdpReport)
                {
                    Logger.Info("Adding Bundle Type 0x23");
                    packetCreator.PacketWriter(new byte[] { 0x23 });
                }
            }
            */

        }

        ///Add a session ack to send to client
        public void AddSession(Session MySession)
        {
            Logger.Info("Adding Session Data");
            if (!MySession.RemoteMaster)
            {
                ///Add first portion of session
                packetCreator.PacketWriter(BitConverter.GetBytes(MySession.InstanceID));
            }

            else
            {
                ///Add first portion of session
                packetCreator.PacketWriter(BitConverter.GetBytes(MySession.InstanceID));

                ///Ack session, first add 2nd portion of Session
                packetCreator.PacketWriter(Utility_Funcs.Pack(MySession.SessionID));
            }
        }

        public byte[] AddSessionHeader(Session MySession)
        {
            uint value = 0;
            Logger.Info("Adding Session Header");

            if (!MySession.Instance) //When server initiates instance with the client, it will use this
            {
                value |= 0x80000;
            }

            //This would be if we want to cancel the connection... Client handles this in most cases? For now.
            //if (MySession.CancelConnection) // 
            //{
            //    value += 0x10000;
            //}

            if (MySession.RemoteMaster) // Purely a guess.... Something is 0x4000 in this and seems to correspond the initator of the session
            {
                value |= 0x04000;
            }

            else // Server is not master, seems to always have this when not in control
            {
                value |= 0x01000;
            }

            if(MySession.hasInstance) // Server always has instance ID, atleast untill we are in world awhile, then it can drop this and the 4 byte instance ID
            {
                value |= 0x02000;
            }

            if (!MySession.RemoteMaster)
            {
                //Add bundle length in
                value |= (uint)(packetCreator.ReadBytes - 4);
            }

            else
            {
                //Add bundle length in
                value |= (uint)(packetCreator.ReadBytes - 7);
            }

            return Utility_Funcs.Pack(value);
        }
    }
}
