using EQLogger;
using Opcodes;
using OpcodeOperations;
using DNP3;
using RdpComm;
using ServerSelect;
using System.Collections.Generic;
using System.Linq;
using System.Net;

namespace SessManager
{
    public class SessionManager
    {
        ///Our IDUP Starter for now
        private static uint SessionIDUpStarter = 100001;

        ///This is our sessionList
        public static List<Session> SessionList = new List<Session>();

        ///When a new session is identified, we add this into our endpoint/session list
        public static void ProcessSession(List<byte> myPacket, IPEndPoint MyIPEndPoint)
        ///public static void ProcessSession(List<byte> myPacket, bool NewSession)
        {
            ushort SessionAction = 0;
            uint SessionIDBase = 0;
            uint SessionIDUp = 0;

            ///This gets our possible session owner
            ///1 should be client master
            ///0 server is master
            ushort RemoteMaster = (ushort)((myPacket[5] << 8 | myPacket[4]) >> 15);

            ///Session phase
            ushort SessionPhase = (ushort)((((myPacket[5] << 8 | myPacket[4]) & 0xF000) >> 12) & 0x7);

            ///Gets our Packet Length
            ushort BundleLength = (ushort)((((myPacket[5] << 8 | myPacket[4]) & 0x0F00) >> 1) + (((myPacket[5] << 8 | myPacket[4]) & 0x00FF) & 0x7F));
            Logger.Info($"RemoteMaster {RemoteMaster}, SessionPhase {SessionPhase}, PacketLength {BundleLength}");

            ushort ClientEndpoint = (ushort)(myPacket[1] << 8 | myPacket[0]);
            myPacket.RemoveRange(0, 6);

            ///Normally is 6, but if so many packets are sent, it transitions to 2
            if (SessionPhase == 6 || SessionPhase == 2)
            {
                if (SessionPhase == 6)
                {
                    SessionAction = (ushort)(myPacket[0]);
                    SessionIDBase = (uint)(myPacket[2] << 8 | myPacket[1]);
                    SessionIDUp = (uint)(myPacket[4] << 8 | myPacket[3]);

                    ///Remove read bytes to this point, 5
                    myPacket.RemoveRange(0, 5);
                }

                ///*******Check this eventually****
                ///If Packet length calculations do not add up, drop it
                ///if (myPacket.Count() != BundleLength)
                if (false)
                {
                    ///This could be a way to identify there is mutliple bundles in this message (Rare scenario's this does happen)
                    ///Drop for now I guess?
                    //Logger.Err($"Bundle Length: {BundleLength}, does not match Actual Bundle Length{myPacket.Count()}");

                }

                ///Calculations add up, move on
                else
                {

                    Logger.Info($"Bundle Length: {BundleLength}, does match Actual Bundle Length{myPacket.Count()}");
                    
                    ///If new session, create it
                    if (SessionAction == SessionOpcode.NewSession)
                    {

                        Logger.Info("Checking if session \"exists\"");
                        bool SessionExistence = SessionList.Exists(i => Equals(i.clientEndpoint, ClientEndpoint) && Equals(i.MyIPEndPoint, MyIPEndPoint) && Equals(i.sessionIDBase, SessionIDBase));

                        ///If it exists, returns to drop
                        if (SessionExistence)
                        {
                            ///This "drops" the packet
                            ///If a client from x IPAddress tries to connect with the same endpoint and IP
                            ///and we will drop and wait for clients next endpoint
                            ///Client will do the leg work to "remove" the session
                            return;
                        }

                        ///Create the session
                        Logger.Info("Creating new session for Client");
                        ///Create our session object instance
                        Session thisSession = new Session(ClientEndpoint, MyIPEndPoint, RemoteMaster, SessionPhase, SessionIDBase, SessionIDUp);
                        ///Session thisSession = new Session(ClientEndpoint, RemoteMaster, SessionPhase, SessionIDBase, SessionIDUp);
                        lock (SessionList)
                        {
                            ///Add our session to Session List
                            SessionList.Add(thisSession);
                        }

                        ProcessSession(thisSession, myPacket);
                    }


                    ///Remove session
                    else if (SessionAction == SessionOpcode.CloseSession)
                    {
                        
                        ///This finds our session by utilizing endpoints and IPEndPoint info (IP and Port), subject to change
                        Session thisSession = SessionList.Find(i => Equals(i.clientEndpoint, ClientEndpoint) && Equals(i.MyIPEndPoint, MyIPEndPoint) && Equals(i.sessionIDBase, SessionIDBase));
                        DropSession(thisSession);
                    }

                    ///Continuing Session, grab it
                    else
                    {
                        Logger.Info("Continuing session for Client, locating...");
                        
                        ///This finds our session by utilizing endpoints and IPEndPoint info (IP and Port), subject to change
                        Session thisSession = SessionList.Find(i => Equals(i.clientEndpoint, ClientEndpoint) && Equals(i.MyIPEndPoint, MyIPEndPoint));

                        ///Process Session
                        ProcessSession(thisSession, myPacket);
                    }
                }
            }

            ///Server is master if 7/3. Phase transitions after ~17 bundles
            else if (SessionPhase == 7 || SessionPhase == 3)
            {
                if (SessionPhase == 7)
                {
                    SessionIDBase = (uint)(myPacket[3] << 24 | myPacket[2] << 16 | myPacket[1] << 8 | myPacket[0]);

                    ///Remove read bytes to this point, 6
                    myPacket.RemoveRange(0, 4);
                }


                SessionIDUp = (uint)(myPacket[6] << 16 | myPacket[5] << 8 | myPacket[4]);
                myPacket.RemoveRange(0, 3);

                ///*******Check this eventually****
                ///If Packet length calculations do not add up, drop it
                ///if (myPacket.Count() != BundleLength)
                if (false)
                {
                    ///This could be a way to identify there is mutliple bundles in this message (Rare scenario's this does happen)
                    ///Drop for now I guess?
                    //Logger.Err($"Bundle Length: {BundleLength}, does not match Actual Bundle Length{myPacket.Count()}");
                }

                ///Calculations add up, move on
                else
                { 
                    Logger.Info($"Bundle Length: {BundleLength}, does match Actual Bundle Length{myPacket.Count()}");

                    ///Remove session
                    if (SessionAction == SessionOpcode.CloseSession)
                    {

                        ///This finds our session by utilizing endpoints and IPEndPoint info (IP and Port), subject to change
                        Session thisSession = SessionList.Find(i => Equals(i.clientEndpoint, ClientEndpoint) && Equals(i.MyIPEndPoint, MyIPEndPoint) && Equals(i.sessionIDBase, SessionIDBase));
                        DropSession(thisSession);
                    }

                    ///Continuing Session, grab it
                    else
                    {
                        Logger.Info("Continuing session for Client, locating...");

                        ///This finds our session by utilizing endpoints and IPEndPoint info (IP and Port), subject to change
                        Session thisSession = SessionList.Find(i => Equals(i.clientEndpoint, ClientEndpoint) && Equals(i.MyIPEndPoint, MyIPEndPoint));

                        ///Process Session
                        ProcessSession(thisSession, myPacket);
                    }
                }
            }
        }

        ///Is this really needed?
        public static void ProcessSession(Session MySession, List<byte> myPacket)
        {

            ///Send to RdpCommunicate
            
            ///SQLOperations.AccountCharacters(MySession);
            RdpCommIn.ProcessBundle(MySession, myPacket);
        }

        public static void DropSession(Session MySession)
        {
            ///Simple enough, remove the session
            Logger.Info("Removing Session");
            lock (SessionList)
            {
                SessionList.Remove(MySession);
            }
        }

        public static void AddMasterSession(Session MyMasterSession)
        {
            Logger.Info("Adding Master Session To List");
            lock (SessionList)
            {
                SessionList.Add(MyMasterSession);
            }
        }

        public static uint ObtainIDUp()
        {
            ///Eventually would need some checks to make sure it isn't taken in bigger scale
            uint NewID = SessionIDUpStarter;
            ///Increment by 2 every pull
            ///Client will Transition into SessionIDUpStarter + 1
            SessionIDUpStarter += 2;

            return NewID;

        }
    }

    /// This is individual client session object
    public class Session
    {
        ///SessionList Objects, probably need bundle information here too?
        private ushort RemoteMaster;
        private ushort SessionPhase;
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

        /// Indicates if session is "in game"
        public bool InGame = false;

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

        ///Our Session object, when client makes the session we don't need AccopuntID
        public Session(ushort clientEndpoint, IPEndPoint MyIPEndPoint, ushort RemoteMaster, ushort SessionPhase, uint SessionIDBase, uint SessionIDUp)
        ///public Session(ushort clientEndpoint, ushort RemoteMaster, ushort SessionPhase, uint SessionIDBase, uint SessionIDUp)
        {
            this.ClientEndpoint = clientEndpoint;
            this.MyIPEndPoint = MyIPEndPoint;
            this.RemoteMaster = RemoteMaster;
            this.SessionPhase = SessionPhase;
            this.SessionIDBase = SessionIDBase;
            this.SessionIDUp = SessionIDUp;
        }

        ///When server creates internal master sessions, key difference is AccountID atm.... Maybe need to be more complicated eventually
        public Session(ushort clientEndpoint, IPEndPoint MyIPEndPoint, ushort RemoteMaster, ushort SessionPhase, int AccountID)
        ///public Session(ushort clientEndpoint, ushort RemoteMaster, ushort SessionPhase, uint SessionIDBase, uint SessionIDUp, uint AccountID)
        {
            Logger.Info("Creating Master Session");
            this.ClientEndpoint = clientEndpoint;
            this.RemoteMaster = RemoteMaster;
            this.MyIPEndPoint = MyIPEndPoint;
            this.SessionPhase = SessionPhase;

            ///Need to perform some leg work here to create this internal master session
            this.SessionIDBase = DNP3Creation.DNP3Session();

            ///This will now relate to Our ingame character ID. When we first create our master session, (character ingame ID / 2) - 1 should be our IDUp
            ///Once we start memory dump, IDUp + 1's to be half ingame ID
            this.SessionIDUp = SessionManager.ObtainIDUp();
            this._AccountID = AccountID;

            ///We don't need to ack a session this Time, we do however need client to ack our's, should this be tracked?
            this.SessionAck = true;

            ///Trigger contact with client inside session
            ProcessOpcode.GenerateClientContact(this);
        }

        public int AccountID
        {
            get { return _AccountID;  }
            set { _AccountID = value; }
        }

        ///Allows us to pull/change RemoteMaster if needed
        public ushort clientEndpoint
        {

            get { return ClientEndpoint; }
            set { ClientEndpoint = value; }
        }

        ///Allows us to pull/change RemoteMaster if needed
        public ushort remoteMaster
        {

            get { return RemoteMaster; }
            set { RemoteMaster = value; }
        }

        ///Allows us to pull/change SessionPhase if needed
        public ushort sessionPhase
        {

            get { return SessionPhase; }
            set { SessionPhase = value; }
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

