using Characters;
using DNP3;
using EQLogger;
using MessageStruct;
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
        //Message List
        public List<Message> MyMessageList = new List<Message> { };

        ///SessionList Objects, probably need bundle information here too?
        public bool RemoteMaster;
        public ushort ClientEndpoint;
        public uint InstanceID;
        public uint SessionID;

        ///Client IPEndPoint
        public IPEndPoint MyIPEndPoint;

        ///Game version information
        public int GameVersion; // Code be useful later on if implementing code to support vanilla or other versions.
        public bool GameVersionAck = false;

        public bool CreateMasterSession = false;

        ///Our Received RDP Information
        public ushort ClientBundleNumber = 1;
        public ushort ClientMessageNumber = 0;
        ///Our Received RDP Information
        public ushort ClientRecvBundleNumber = 1;
        public ushort ClientRecvMessageNumber = 1;

        ///Our RDP Information, should always start with 1...
        public ushort ServerBundleNumber = 1;
        public ushort ServerMessageNumber = 1;

        ///Our RDP Information, should always start with 1...
        public ushort ServerRecvBundleNumber = 1;
        public ushort ServerRecvMessageNumber = 1;

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
        public bool ServerSelect = false;
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
        public int AccountID;

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

        /// <summary>
        /// Create a Simple Client Session
        /// </summary>
        /// <param name="MyIPEndPoint"></param>
        public Session(IPEndPoint myIPEndPoint, uint instanceID)
        {
            MyIPEndPoint = myIPEndPoint;
            InstanceID = instanceID;
            //StartTimer(2000);
        }

        ///Allows us to update last known Client Bundle Number
        public ushort clientBundleNumber
        {

            get { return ClientBundleNumber; }
            set { ClientBundleNumber = value;
                if (ClientBundleNumber == 17)
                    BundleTypeTransition = true;
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
                    BundleTypeTransition = true;
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
                    BundleTypeTransition = true;
            }
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

        //Override the GetHashcodeMethod so that Hashset works properly as our SessionHolder
        public override int GetHashCode()
        {
            unchecked
            {
                int hash = (int)2166136261;
                hash = (hash ^ MyIPEndPoint.GetHashCode()) * 16777619;
                hash = (hash ^ InstanceID.GetHashCode()) * 16777619;
                return hash;
            }
        }

    }
}
