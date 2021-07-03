using System;
using System.Collections.Generic;
using System.Threading.Channels;
using System.Threading.Tasks;
using System.Timers;
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
        private readonly ProcessUnreliable _processUnreliable = new();
        private readonly SelectServer _selectServer = new();
        public readonly ChannelWriter<Character> characterChanWriter;

        public RdpCommIn(SessionManager sessionManager, ChannelWriter<Character> chanWriter)
        {
            //ServerSelect stuff
            _selectServer.ReadConfig();
            Timer t = new Timer(5000);
            t.AutoReset = true;
            t.Elapsed += new ElapsedEventHandler((_, _) => _selectServer.GenerateServerSelect());
            t.Start();

            //The rest
            _sessionManager = sessionManager;
            characterChanWriter = chanWriter;

            _processOpcode = new(_sessionManager);
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
                    Logger.Info($"{MySession.ClientEndpoint.ToString("X")}: Processing Session Ack, Rdp Report and Message Bundle");
                    break;

                case BundleOpcode.NewProcessReport:
                    ProcessRdpReport(MySession, ClientPacket, ref offset);
                    ProcessMessageBundle(MySession, ClientPacket, offset);
                    Logger.Info($"{MySession.ClientEndpoint.ToString("X")}: Processing Rdp Report and messages");
                    break;

                case BundleOpcode.ProcessReport:
                    ProcessRdpReport(MySession, ClientPacket, ref offset);
                    Logger.Info($"{MySession.ClientEndpoint.ToString("X")}: Processing Rdp Report");
                    break;

                case BundleOpcode.NewProcessMessages:
                case BundleOpcode.ProcessMessages:

                    MySession.clientBundleNumber = BinaryPrimitiveWrapper.GetLEUShort(ClientPacket, ref offset);

                    //2 bytes for Bundle #, is it relevant to track?
                    ProcessMessageBundle(MySession, ClientPacket, offset);

                    Logger.Info($"{MySession.ClientEndpoint.ToString("X")}: Processing Message Bundle");
                    break;

                case BundleOpcode.ProcessMessageAndReport:
                    ProcessRdpReport(MySession, ClientPacket, ref offset);
                    ProcessMessageBundle(MySession, ClientPacket, offset);
                    Logger.Info($"{MySession.ClientEndpoint.ToString("X")}: Processing unreliable Rdp Report and messages");
                    break;

                default:
                    Logger.Err($"{MySession.ClientEndpoint.ToString("X")}: Unable to identify Bundle Type: {ClientPacket.Span[offset-1].ToString("X")}");
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
                        Logger.Info($"{MySession.ClientEndpoint.ToString("X")}: Processing Ping Request");
                        _processOpcode.ProcessPingRequest(MySession.queueMessages, MySession, ClientPacket, ref offset);
                        break;

                    default:

                        Logger.Info($"{MySession.ClientEndpoint.ToString("X")}: Processing Object Update");

                        //This will process our unreliable message, or unreliable ack's
                        _processUnreliable.ProcessUnreliables(MySession, MessageTypeOpcode, ClientPacket, ref offset);
                        break;
                }
            }

            MySession.RdpReport = true;
            Logger.Info($"{MySession.ClientEndpoint.ToString("X")}: Done processing messages in packet");
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
                Logger.Info($"{MySession.ClientEndpoint.ToString("X")}: Generating Character Select");
                List<Character> MyCharacterList = SQLOperations.AccountCharacters(MySession);

                //Assign to our session
                MySession.CharacterData = MyCharacterList;

                ProcessOpcode.CreateCharacterList(MySession.queueMessages, _sessionManager, MyCharacterList, MySession);
            }

            ///Triggers creating master session
            else if (((MySession.ClientEndpoint + 1) == (MySession.InstanceID & 0x0000FFFF)) && MySession.ServerMessageNumber == 2 && MySession.SessionAck)
            {
                _sessionManager.CreateMasterSession(MySession);
            }

            ///Trigger Server Select with this?
            else if ((MySession.ClientEndpoint == (MySession.InstanceID & 0x0000FFFF)) && MySession.ServerMessageNumber == 2 && MySession.SessionAck)
            {
                ListenForServerList(MySession);
            }

            else
            {
                Logger.Err($"{MySession.ClientEndpoint.ToString("X")}: Client received server message {LastRecvMessageNumber}, expected {MySession.ServerMessageNumber}");
            }
        }

        private async Task ListenForServerList(Session MySession)
        {
            MySession.serverSelect = true;
            ChannelReader<byte[]> serverList = _selectServer.getChannel(MySession);

            if (MySession.serverSelect)
            {
                //Wait for data in channel
                while (await serverList.WaitToReadAsync())
                {
                    //Grab the data
                    while (serverList.TryRead(out byte[] item))
                    {
                        MySession.queueMessages.messageCreator.MessageWriter(item);
                        MySession.queueMessages.PackMessage(MySession, MessageOpcodeTypes.ShortUnreliableMessage);
                    }
                }
            }
        }

        private void ProcessSessionAck(Session MySession, ReadOnlyMemory<byte> ClientPacket, ref int offset)
        {
            uint SessionAck = BinaryPrimitiveWrapper.GetLEUint(ClientPacket, ref offset);
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

        ///This grabs the Message Type
        private static ushort GrabMessageType(ReadOnlyMemory<byte> ClientPacket, ref int offset)
        {
            byte Opcode = BinaryPrimitiveWrapper.GetLEByte(ClientPacket, ref offset);
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
                if (MySession.inGame)
                {
                    MySession.UnreliablePing();
                    if (MySession.coordToggle)
                    {
                        MySession.CoordinateUpdate();
                    }
                }

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

                    MySession.sessionQueue.GatherMessages(MySession.packetCreator);

                    ///Bundle needs to be incremented after every sent packet, seems like a good spot?
                    MySession.IncrementServerBundleNumber();

                    ///Add the Session stuff here that has length built in with session stuff
                    byte[] SessionHeader = AddSessionHeader(MySession);

                    ///Done? Send to CommManagerOut
                    _handleOutPacket.AddEndPoints(MySession.packetCreator, MySession, SessionHeader);
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
                    MySession.packetCreator.PacketWriter(new byte[] { 0x40 });
                    MySession.packetCreator.PacketWriter(BitConverter.GetBytes(MySession.Channel40MessageNumber));
                    MySession.packetCreator.PacketWriter(new byte[] { 0xF8 });
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

            MySession.packetCreator.PacketWriter(new byte[] { segBody });
        }

        ///Add a session ack to send to client
        public void AddSession(Session MySession)
        {
            Logger.Info($"{MySession.ClientEndpoint.ToString("X")}: Adding Session Data");
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
            Logger.Info($"{MySession.ClientEndpoint.ToString("X")}: Adding Session Header");

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

            if (MySession.hasInstance) // Server always has instance ID, atleast untill we are in world awhile, then it can drop this and the 4 byte instance ID
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
