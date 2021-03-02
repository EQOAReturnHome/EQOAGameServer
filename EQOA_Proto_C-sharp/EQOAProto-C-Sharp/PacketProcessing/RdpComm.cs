using EQLogger;
using EQOAProto;
using EQOASQL;
using OpcodeOperations;
using Opcodes;
using ServerSelect;
using SessManager;
using System;
using System.Collections.Generic;
using Utility;
using Characters;
using Sessions;
using Unreliable;
using Packet;

namespace RdpComm
{
    /// <summary>
    /// This is user to start processing incoming and outgoing packets
    /// </summary>
    public class RdpCommIn
    {
        private readonly SessionManager _sessionManager;
        private readonly ProcessOpcode _processOpcode;
        private readonly SessionQueueMessages _queueMessages;

        public RdpCommIn(SessionManager sessionManager, SessionQueueMessages queueMessages)
        {
            _sessionManager = sessionManager;
            _processOpcode = new(sessionManager);
            _queueMessages = queueMessages;
        }
        public void ProcessBundle(Session MySession, ReadOnlyMemory<byte> ClientPacket, int offset)
        {
            ///Grab our bundle type
            byte BundleType = BinaryPrimitiveWrapper.GetLEByte(ClientPacket, ref offset);
            Logger.Info($"BundleType is {BundleType}");

            ///Perform a check to find what switch statement is true
            switch (BundleType)
            {
                case BundleOpcode.ProcessAll:

                    ProcessSessionAck(MySession, ClientPacket, ref offset);
                    ProcessRdpReport(MySession, ClientPacket, ref offset);
                    //ProcessMessageBundle(MySession, ClientPacket, offset);
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

                    //2 bytes for Bundle #, is it relevant to track?
                    ProcessMessageBundle(MySession, ClientPacket, offset);

                    MySession.clientBundleNumber = BinaryPrimitiveWrapper.GetLEUShort(ClientPacket, ref offset);

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
                        _processOpcode.ProcessPingRequest(MySession, ClientPacket, ref offset);
                        break;

                    default:
                        //This will process our unreliable message, or unreliable ack's
                        ProcessUnreliable.ProcessUnreliables(MySession, MessageTypeOpcode, ClientPacket, ref offset);
                        break;
                }  
            }
            MySession.RdpReport = true;
            MySession.ClientFirstConnect = true;
            if(MySession.StartMemoryDump)
            {
                _sessionManager.CreateMemoryDumpSession(MySession);
            }

            //Reset ack timer
            //MySession.ResetTimer();

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

            //Check our list  of reliable messages to remove
            for (int i = 0; i < MySession.ResendMessageQueue.Count; i++)
            {
                //If our message stored is less then client ack, need to remove it!
                if (MySession.ResendMessageQueue.TryPeek(out MessageStruct ResendMessage))
                {
                    //if this message# in queue < client ack'd message# can safely remove from resend
                    if (ResendMessage.Messagenumber <= LastRecvMessageNumber)
                    {
                        MySession.ResendMessageQueue.TryDequeue(out MessageStruct DelResendMessage);
                        continue;
                    }
                    break;
                }

                //Possibly empty queue, move on
                else
                    break;
            }

            ///Triggers creating master session
            if (((MySession.ClientEndpoint + 1) == (MySession.InstanceID & 0x0000FFFF)) && MySession.SessionAck && MySession.serverSelect == false)
            {
                _sessionManager.CreateMasterSession(MySession);
            }

            ///Trigger Server Select with this?
            else if ((MySession.ClientEndpoint == (MySession.InstanceID & 0x0000FFFF)) && MySession.SessionAck && !MySession.serverSelect)
            {
                SelectServer.GenerateServerSelect(_queueMessages, MySession);
            }

            ///Triggers Character select
            else if (MySession.CharacterSelect && (MySession.ClientEndpoint != MySession.InstanceID))
            {
                MySession.CharacterSelect = false;
                List<Character> MyCharacterList = new List<Character>();
                Logger.Info("Generating Character Select");
                MyCharacterList = SQLOperations.AccountCharacters(MySession);

                //Assign to our session
                MySession.CharacterData = MyCharacterList;

                ProcessOpcode.CreateCharacterList(_sessionManager, MyCharacterList, MySession);
            }

            else if (MySession.Dumpstarted)
            {/*
                //Let client know more data is coming
                if (MySession.MyDumpData.Count > 500)
                {
                    List<byte> ThisChunk = MySession.MyDumpData.GetRange(0, 500);
                    MySession.MyDumpData.RemoveRange(0, 500);

                    ///Handles packing message into outgoing packet
                    RdpCommOut.PackMessage(MySession, ThisChunk, MessageOpcodeTypes.MultiShortReliableMessage);
                }

                //End of dump
                else
                {
                    List<byte> ThisChunk = MySession.MyDumpData.GetRange(0, MySession.MyDumpData.Count);
                    MySession.MyDumpData.Clear();
                    ///Handles packing message into outgoing packet
                    RdpCommOut.PackMessage(MySession, ThisChunk, MessageOpcodeTypes.ShortReliableMessage);
                    //turn dump off
                    MySession.Dumpstarted = false;

                    //Gather remaining data to send in dump
                    //ignore list
                    ProcessOpcode.IgnoreList(MySession);
                    //Randommessage
                    //RandomFriend
                    //characterSpeed
                    ProcessOpcode.ActorSpeed(MySession);
                    //Some opcode
                }*/
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
                MySession.ClientAck = true;
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
            ushort Opcode = BinaryPrimitiveWrapper.GetLEByte(ClientPacket, ref offset);
            return Opcode;
        }
    }

    public class RdpCommOut
    {
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
                if ((MySession.RdpReport || MySession.RdpMessage) && MySession.ClientFirstConnect)
                {
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

                    //First check resend queue for session, need to check message timestamp and see if it's been atleast 2 seconds
                    while (MySession.ResendMessageQueue.TryPeek(out MessageStruct ResendMessage))
                    {
                        if((ResendMessage.Time - DateTimeOffset.Now.ToUnixTimeMilliseconds()) > 2000)
                        {
                            if ((MySession.packetCreator.ReadBytes + ResendMessage.Message.Length) < 1800)
                            {
                                MySession.OutGoingMessageQueue.TryDequeue(out MessageStruct thisMessage);
                                MySession.packetCreator.PacketWriter(thisMessage.Message);
                                continue;
                            }
                            break;
                        }
                        break;
                    }

                    //check our new messages
                    while (MySession.OutGoingMessageQueue.TryPeek(out MessageStruct SendMessage))
                    {
                        if ((MySession.packetCreator.ReadBytes + SendMessage.Message.Length) < 1800)
                        {
                            MySession.OutGoingMessageQueue.TryDequeue(out MessageStruct thisMessage);
                            MySession.packetCreator.PacketWriter(thisMessage.Message);
                            continue;
                        }
                        break;
                    }

                    ///Bundle needs to be incremented after every sent packet, seems like a good spot?
                    MySession.IncrementServerBundleNumber();

                    ///Add the Session stuff here that has length built in with session stuff
                    byte[] SessionHeader = AddSessionHeader(MySession);

                    ///Done? Send to CommManagerOut
                    _handleOutPacket.AddEndPoints(MySession, SessionHeader);
                }
            }
        }

        ///Identifies if full RDPReport is needed or just the current bundle #
        public void AddRDPReport(Session MySession)
        {
            MySession.packetCreator.PacketWriter(BitConverter.GetBytes(MySession.ServerBundleNumber));
            ///If RDP Report == True, Current bundle #, Last Bundle received # and Last message received #
            if (MySession.RdpReport)
            {
                Logger.Info("Full RDP Report");

                MySession.packetCreator.PacketWriter(BitConverter.GetBytes(MySession.clientBundleNumber));
                MySession.packetCreator.PacketWriter(BitConverter.GetBytes(MySession.clientMessageNumber));
                /// Add them to packet in "reverse order" stated above
                /// 
                //If ingame, include 4029 ack's
                if (MySession.Channel40Ack)
                {
                    MySession.packetCreator.PacketWriter(BitConverter.GetBytes((byte)0x40));
                    MySession.packetCreator.PacketWriter(BitConverter.GetBytes(MySession.Channel40Message));
                    MySession.packetCreator.PacketWriter(BitConverter.GetBytes((byte)0xF8));
                    return;
                }
            }
        }

        ///Add our bundle type
        ///Consideration for in world or "certain packet" # is needed during conversion. For now something basic will work
        public void AddBundleType(Session MySession)
        {
            ///Should this be a big switch statement?
            ///Using if else for now
            if (MySession.BundleTypeTransition)
            {
                ///If all 3 are true
                if (MySession.Channel40Ack)
                {
                    Logger.Info("Adding Bundle Type 0x13");
                    MySession.packetCreator.PacketWriter(new byte[] { 0x13});
                    MySession.Channel40Ack = false;
                }

                ///Message only packet, no RDP Report
                else if (MySession.RdpMessage && !MySession.RdpReport && !MySession.Channel40Ack)
                {
                    Logger.Info("Adding Bundle Type 0x00");
                    MySession.packetCreator.PacketWriter(new byte[] { 0x00 });
                }

                ///RDP Report only
                else if (MySession.RdpReport)
                {
                    Logger.Info("Adding Bundle Type 0x03");
                    MySession.packetCreator.PacketWriter(new byte[] { 0x03 });
                }
            }

            else 
            {
                ///If Message and RDP report
                if (!MySession.SessionAck && MySession.RdpMessage && MySession.RdpReport)
                {
                    Logger.Info("Adding Bundle Type 0x63");
                    MySession.packetCreator.PacketWriter(new byte[] { 0x63 });
                }

                ///Message only packet, no RDP Report
                else if (MySession.SessionAck && MySession.RdpMessage && !MySession.RdpReport)
                {
                    Logger.Info("Adding Bundle Type 0x20");
                    MySession.packetCreator.PacketWriter(new byte[] { 0x20 });
                }

                ///RDP Report only
                else if (MySession.SessionAck && !MySession.RdpMessage && MySession.RdpReport)
                {
                    Logger.Info("Adding Bundle Type 0x23");
                    MySession.packetCreator.PacketWriter(new byte[] { 0x23 });
                }
            }
        }

        ///Add a session ack to send to client
        public void AddSession(Session MySession)
        {
            Logger.Info("Adding Session Data");
            if (!MySession.RemoteMaster)
            {
                ///Add first portion of session
                MySession.packetCreator.PacketWriter(BitConverter.GetBytes(MySession.InstanceID));
            }

            else
            {
                ///Add first portion of session
                MySession.packetCreator.PacketWriter(BitConverter.GetBytes(MySession.InstanceID));

                ///Ack session, first add 2nd portion of Session
                MySession.packetCreator.PacketWriter(Utility_Funcs.Pack(MySession.SessionID));
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
                value |= (uint)(MySession.packetCreator.ReadBytes - 4);
            }

            else
            {
                //Add bundle length in
                value |= (uint)(MySession.packetCreator.ReadBytes - 7);
            }

            return Utility_Funcs.Pack(value);
        }
    }
}
