using Characters;
using DNP3;
using EQLogger;
using OpcodeOperations;
using ServerSelect;
using SessManager;
using System.Collections.Generic;
using System.Net;

namespace Sessions
{
    /// This is individual client session object
    public class Session
    {
        ///SessionList Objects, probably need bundle information here too?
        private bool RemoteMaster;
        private ushort ClientEndpoint;
        private uint SessionIDBase;
        private uint SessionIDUp;

        ///Client IPEndPoint
        public IPEndPoint MyIPEndPoint;

        ///Game version information
        private int GameVersion;
        private bool GameVersionAck = false;

        ///Our Received RDP Information
        private ushort ClientBundleNumber = 0;
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

        ///Server Select trigger
        private bool ServerSelect = false;
        public bool CharacterSelect = false;

        /// Indicates if session is "in game"
        public bool InGame = false;

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
            this.Instance = true;
            this.ClientEndpoint = clientEndpoint;
            this.MyIPEndPoint = MyIPEndPoint;
            this.RemoteMaster = false;
            this.SessionIDBase = SessionIDBase;
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
        }

        public Session(ushort clientEndpoint, IPEndPoint MyIPEndPoint, bool RemoteMaster, int AccountID, uint SessionIDUp, Character MyCharacter)
        {
            Logger.Info("Creating Master Session");
            this.ClientEndpoint = clientEndpoint;
            this.RemoteMaster = RemoteMaster;
            this.MyIPEndPoint = MyIPEndPoint;

            ///Need to perform some leg work here to create this internal master session
            this.SessionIDBase = DNP3Creation.DNP3Session();

            ///This will now relate to Our ingame character ID. When we first create our master session, (character ingame ID / 2) - 1 should be our IDUp
            ///Once we start memory dump, IDUp + 1's to be half ingame ID
            /////This isn't quite right.. consider rework of some kind
            this.SessionIDUp = SessionIDUp + 1;
            this._AccountID = AccountID;

            this.CharacterSelect = false;

            ///We don't need to ack a session this Time, we do however need client to ack our's, should this be tracked? yes it should
            this.SessionAck = true;
            this.MyCharacter = MyCharacter;
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
            set { ClientBundleNumber = value; }
        }

        ///Allows us to update last known Client Bundle Number
        public ushort clientMessageNumber
        {

            get { return ClientMessageNumber; }
            set
            {
                ClientMessageNumber = value;
                if (ClientMessageNumber == 18)
                {
                    BundleTypeTransition = true;
                }
            }
        }

        ///Allows us to update last known Server Bundle Number
        public ushort serverBundleNumber
        {

            get { return ServerBundleNumber; }
            set { ServerBundleNumber = value; }
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
    }
}
