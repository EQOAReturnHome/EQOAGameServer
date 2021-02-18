using Characters;
using DNP3;
using EQLogger;
using OpcodeOperations;
using Opcodes;
using RdpComm;
using ServerSelect;
using SessManager;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Timers;
using MessageStruct;
using System.Text;

namespace Sessions
{
    /// This is individual client session object
    public class Session
    {
        //Session dependant timer
        Timer myTimer = new Timer();

        //Coordinate Debugger
        Timer myTimer2 = new Timer();

        //Message List
        public List<Message> MyMessageList = new List<Message> { };

        ///SessionList Objects, probably need bundle information here too?
        private bool RemoteMaster;
        private ushort ClientEndpoint;
        private uint SessionIDBase;
        private uint SessionIDUp;

        ///Client IPEndPoint
        public IPEndPoint MyIPEndPoint;

        ///Game version information
        private int GameVersion; // Code be useful later on if implementing code to support vanilla or other versions.
        private bool GameVersionAck = false;

        public bool CreateMasterSession = false;

        ///Our Received RDP Information
        private ushort ClientBundleNumber = 1;
        private ushort ClientMessageNumber = 0;

        ///Our Received RDP Information
        private ushort ClientRecvBundleNumber = 1;
        private ushort ClientRecvMessageNumber = 0;

        ///Our RDP Information, should always start with 1...
        private ushort ServerBundleNumber = 1;
        private ushort ServerMessageNumber = 1;

        ///Our RDP Information, should always start with 1...
        private ushort ServerRecvBundleNumber = 1;
        private ushort ServerRecvMessageNumber = 1;

        //Should these be stored here?
        public ushort Channel0Message = 1;
        public bool Channel0Ack = false;
        public ushort Channel1Message = 0;
        public bool Channel1Ack = false;
        public ushort Channel2Message = 0;
        public bool Channel2Ack = false;
        public ushort Channel3Message = 0;
        public bool Channel3Ack = false;
        public ushort Channel4Message = 0;
        public bool Channel4Ack = false;
        public ushort Channel5Message = 0;
        public bool Channel5Ack = false;
        public ushort Channel6Message = 0;
        public bool Channel6Ack = false;
        public ushort Channel7Message = 0;
        public bool Channel7Ack = false;
        public ushort Channel8Message = 0;
        public bool Channel9Ack = false;
        public ushort Channel10Message = 0;
        public bool Channel10Ack = false;
        public ushort Channel11Message = 0;
        public bool Channel11Ack = false;
        public ushort Channel12Message = 0;
        public bool Channel12Ack = false;
        public ushort Channel13Message = 0;
        public bool Channel13Ack = false; 
        public ushort Channel14Message = 0;
        public bool Channel14Ack = false; 
        public ushort Channel15Message = 0;
        public bool Channel15Ack = false; 
        public ushort Channel16Message = 0;
        public bool Channel16Ack = false; 
        public ushort Channel17Message = 0;
        public bool Channel17Ack = false; 
        public ushort Channel18Message = 0;
        public bool Channel18Ack = false; 
        public ushort Channel19Message = 0;
        public bool Channel19Ack = false; 
        public ushort Channel20Message = 0;
        public bool Channel20Ack = false; 
        public ushort Channel21Message = 0;
        public bool Channel21Ack = false; 
        public ushort Channel22Message = 0;
        public bool Channel22Ack = false; 
        public ushort Channel23Message = 0;
        public bool Channel23Ack = false;
        public bool Channel40Ack = false;
        public ushort Channel40Message = 0;
        public Message Channel40Base;
        public List<Message> Channel40BaseList = new List<Message> { }; 
        public ushort Channel42Message = 0;
        public bool Channel42Ack = false;
        public ushort Channel43Message = 0;
        public bool Channel43Ack = false;


        ///Server Select trigger
        private bool ServerSelect = false;
        public bool CharacterSelect = false;

        /// Indicates if session is "in game"
        private bool _InGame = false;

        //Server always has instance... eventually, once in world awhile, we could remove this and save on packet length
        public bool hasInstance = true;

        //Marks client ack'ing the instance
        public bool Instance = false;

        ///hacky for now... makes it so we wait to process both message before adding rdp report and creating outgoing packet
        public bool ClientFirstConnect = false;

        public bool ClientAck = false;
        ///BundleType Transition
        public bool BundleTypeTransition = false;

        public bool RdpReport = false;
        public bool RdpMessage = false;
        public bool SessionAck = false;

        ///Once we receive account ID, this should never change...
        private int _AccountID;

        ///Session outgoing packet
        public List<byte> SessionInformation = new List<byte>();
        public List<byte> SessionMessages = new List<byte>();

        ///Clients IP Information
        public IPAddress MyIPInfo;

        //Character List and dump stuff
        public List<byte> MyDumpData = new List<byte> { };
        public bool Dumpstarted = false;
        public List<Character> CharacterData;
        public Character MyCharacter = null;

        ///Our Session object, when client makes the session we don't need AccopuntID
        public Session(ushort clientEndpoint, IPEndPoint MyIPEndPoint, uint SessionIDBase)
        ///public Session(ushort clientEndpoint, ushort RemoteMaster, ushort SessionPhase, uint SessionIDBase, uint SessionIDUp)
        {
            //Client initiated, so we are ack'ing and setting this to true
            Instance = true;
            ClientEndpoint = clientEndpoint;
            this.MyIPEndPoint = MyIPEndPoint;
            RemoteMaster = false;
            this.SessionIDBase = SessionIDBase;
            StartTimer(2000);
        }

        ///When server creates internal master sessions, key difference is AccountID atm.... Maybe need to be more complicated eventually
        public Session(ushort clientEndpoint, IPEndPoint MyIPEndPoint, int AccountID)
        ///public Session(ushort clientEndpoint, ushort RemoteMaster, ushort SessionPhase, uint SessionIDBase, uint SessionIDUp, uint AccountID)
        {
            Logger.Info("Creating Master Session");
            this.ClientEndpoint = clientEndpoint;
            this.RemoteMaster = true;
            this.MyIPEndPoint = MyIPEndPoint;

            ///Need to perform some leg work here to create this internal master session
            this.SessionIDBase = DNP3Creation.DNP3Session();

            ///This will now relate to Our ingame character ID. When we first create our master session, (character ingame ID / 2) - 1 should be our IDUp
            ///Once we start memory dump, IDUp + 1's to be half ingame ID
            this.SessionIDUp = SessionManager.ObtainIDUp() - 1;
            this._AccountID = AccountID;

            ///We don't need to ack a session this Time, we do however need client to ack our's, should this be tracked?
            this.SessionAck = true;
            this.CharacterSelect = true;

            ///Trigger contact with client inside session
            ProcessOpcode.GenerateClientContact(this);
            StartTimer(2000);
        }

        public Session(ushort clientEndpoint, IPEndPoint MyIPEndPoint, bool RemoteMaster, int AccountID, uint SessionIDUp, Character MyCharacter)
        {
            Logger.Info("Creating Master Session");
            ClientEndpoint = clientEndpoint;
            this.RemoteMaster = RemoteMaster;
            this.MyIPEndPoint = MyIPEndPoint;

            ///Need to perform some leg work here to create this internal master session
            SessionIDBase = DNP3Creation.DNP3Session();

            ///This will now relate to Our ingame character ID. When we first create our master session, (character ingame ID / 2) - 1 should be our IDUp
            ///Once we start memory dump, IDUp + 1's to be half ingame ID
            /////This isn't quite right.. consider rework of some kind
            this.SessionIDUp = SessionIDUp + 1;
            _AccountID = AccountID;

            CharacterSelect = false;

            ///We don't need to ack a session this Time, we do however need client to ack our's, should this be tracked? yes it should
            SessionAck = true;
            this.MyCharacter = MyCharacter;
            StartTimer(2000);
        }

        public int AccountID
        {
            get { return _AccountID; }
            set { _AccountID = value; }
        }

        ///Allows us to pull/change RemoteMaster if needed
        public ushort clientEndpoint
        {

            get { return ClientEndpoint; }
            set { ClientEndpoint = value; }
        }

        ///Allows us to pull/change RemoteMaster if needed
        public bool remoteMaster
        {

            get { return RemoteMaster; }
            set { RemoteMaster = value; }
        }

        ///Allows us to pull/change SessionIDBase if needed
        public uint sessionIDBase
        {

            get { return SessionIDBase; }
            set { SessionIDBase = value; }
        }

        ///Allows us to pull/change SessionIDUp if needed
        public uint sessionIDUp
        {

            get { return SessionIDUp; }
            set { SessionIDUp = value; }
        }

        ///Allows us to update last known Client Bundle Number
        public ushort clientBundleNumber
        {

            get { return ClientBundleNumber; }
            set { ClientBundleNumber = value;
                if (ClientBundleNumber == 17)
                {
                    BundleTypeTransition = true;
                }
            }
        }

        ///Allows us to update last known Client Bundle Number
        public ushort clientMessageNumber
        {

            get { return ClientMessageNumber; }
            set
            {
                ClientMessageNumber = value;
                if (ClientMessageNumber == 17)
                {
                    BundleTypeTransition = true;
                }
            }
        }

        ///Allows us to update last known Server Bundle Number
        public ushort serverBundleNumber
        {

            get { return ServerBundleNumber; }
            set
            {
                ServerBundleNumber = value;
                if (ServerBundleNumber == 17)
                {
                    BundleTypeTransition = true;
                }
            }
        }

        ///Allows us to update last known Server Message Number
        public ushort serverMessageNumber
        {

            get { return ServerMessageNumber; }
            set { ServerMessageNumber = value; }
        }

        ///Allows us to update last known Client Bundle Number
        public ushort clientRecvBundleNumber
        {

            get { return ClientRecvBundleNumber; }
            set { ClientRecvBundleNumber = value; }
        }

        ///Allows us to update last known Client Message Number
        public ushort clientRecvMessageNumber
        {

            get { return ClientRecvMessageNumber; }
            set { ClientRecvMessageNumber = value; }
        }

        ///Allows us to update last known Server Bundle Number
        public ushort serverRecvBundleNumber
        {

            get { return ServerRecvBundleNumber; }
            set { ServerRecvBundleNumber = value; }
        }

        ///Allows us to update last known Server Message Number
        public ushort serverRecvMessageNumber
        {

            get { return ServerRecvMessageNumber; }
            set { ServerRecvMessageNumber = value; }
        }

        ///Allows us to update Game version if needed
        public int gameVersion
        {

            get { return GameVersion; }
            set { GameVersion = value; }
        }

        ///Allows us update GameVersion Ack
        public bool gameVersionAck
        {

            get { return GameVersionAck; }
            set { GameVersionAck = value; }
        }

        ///Allows us to change ServerSelect Bool and trigger Server Select
        public bool serverSelect
        {

            get { return ServerSelect; }
            set
            {
                ServerSelect = value;

                ///Our Server Select trigger
                if (ServerSelect)
                {
                    ///Should generate our server list.
                    SelectServer.GenerateServerSelect(this);
                }
            }
        }

        public bool InGame
        {
            get { return _InGame; }
            set
            {
                _InGame = value;

                if(_InGame)
                {
                    //StartCoordinateDebugging(10000);
                }
            }
        }

        public void IncrementServerMessageNumber()
        {
            ServerMessageNumber += 1;
        }

        public void IncrementServerBundleNumber()
        {
            ServerBundleNumber += 1;
        }

        public void IncrementClientMessageNumber()
        {
            clientMessageNumber += 1;
        }

        public void IncrementClientBundleNumber()
        {
            clientBundleNumber += 1;
        }

        ///Resets our bool's for next message
        public void Reset()
        {
            RdpReport = false;
            RdpMessage = false;
        }

        private void StartTimer(int interval)
        {
            myTimer.Interval = interval;
            myTimer.AutoReset = true;
            myTimer.Elapsed += new ElapsedEventHandler(Timer_Elapsed);
            myTimer.Start();
        }

        private void StartCoordinateDebugging(int interval)
        {
            myTimer2.Interval = interval;
            myTimer2.AutoReset = true;
            myTimer2.Elapsed += new ElapsedEventHandler(Timer_Elapsed2);
            myTimer2.Start();
        }

        public void ResetTimer()
        {
            myTimer.Stop();
            myTimer.Start();
        }

        public void StopTimers()
        {
            try
            {
                myTimer.Stop();
                myTimer2.Stop();
            }

            catch
            {
                Console.WriteLine("Error occured stopping timers");
            }
        }
        //Utilize this timer per session to check for client responses.
        //Will include F9's and message check here
        private void Timer_Elapsed(Object source, ElapsedEventArgs e)
        {
            //Check and see if any messages need an ack
            if (MyMessageList.Count() > 0)
            {
                for (int i = 0; i < MyMessageList.Count(); i++)
                {
                    lock (SessionMessages)
                    {
                        //Should we verify these messages are not less then what client has ack'd?
                        SessionMessages.AddRange(MyMessageList[i].ThisMessage);
                    }
                    RdpMessage = true;
                }
            }

            
            else
            {
                if (!InGame)
                {
                    //Pack up a ping request and send to client
                    RdpCommOut.PackMessage(this, new List<byte> { 0x12 }, MessageOpcodeTypes.UnknownMessage);
                }

                else
                {
                    //Pack up a ping request and send to client
                    RdpCommOut.PackMessage(this, new List<byte> { 0x12 }, MessageOpcodeTypes.UnknownMessage);
                }
            }
        }

        private void Timer_Elapsed2(Object source, ElapsedEventArgs e)
        {
            string theMessage = $"Coordinate Update: X-{MyCharacter.XCoord} Y-{MyCharacter.YCoord} Z-{MyCharacter.ZCoord}";
            List<byte> MyMessage = new List<byte> { };
            MyMessage.AddRange(BitConverter.GetBytes(theMessage.Length));
            MyMessage.AddRange(Encoding.Unicode.GetBytes(theMessage));

            //Send Message
            RdpCommOut.PackMessage(this, MyMessage, MessageOpcodeTypes.ShortReliableMessage, (ushort)GameOpcode.ClientMessage);
        }

            public void AddMessage(ushort num, List<byte> MyMessage)
        {
            MyMessageList.Add(new Message(num, MyMessage));
        }
    }
}
