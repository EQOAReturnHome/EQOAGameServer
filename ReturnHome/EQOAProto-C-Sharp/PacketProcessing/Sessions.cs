using System.Collections.Generic;
using System.Net;
using System;
using ReturnHome.Utilities;
using ReturnHome.Actor;
using System.IO.Pipelines;
using ReturnHome.Opcodes;
using System.Text;

namespace ReturnHome.PacketProcessing
{
    /// This is individual client session object
    public class Session : IDisposable
    {
        //Message List
        public EQOASessionQueue sessionQueue;
        private Memory<byte> sessionMemory;
        private PipeWriter pipeWriter;
        private PipeReader pipeReader;
        public readonly SessionQueueMessages queueMessages = new();
        private int bytesRead;
        private byte ping = 0;
        public PacketCreator packetCreator = new();

        ///SessionList Objects, probably need bundle information here too?
        public bool RemoteMaster;
        public ushort ClientEndpoint;
        public uint InstanceID;
        public uint SessionID;

        ///Client IPEndPoint
        public IPEndPoint MyIPEndPoint;

        ///Our Received RDP Information
        public ushort ClientBundleNumber = 1;
        public ushort ClientMessageNumber = 0;
        ///Our Received RDP Information
        public ushort ClientRecvBundleNumber = 1;
        public ushort ClientRecvMessageNumber = 1;

        ///Our RDP Information, should always start with 1
        public ushort ServerBundleNumber = 1;
        public ushort ServerMessageNumber = 1;

        ///Our RDP Information, should always start with 1
        public ushort ServerRecvBundleNumber = 1;
        public ushort ServerRecvMessageNumber = 1;

        public bool Channel40Ack = false;
        public Memory<byte> Channel40Base = new byte[41];
        public ushort Channel40MessageNumber;
        public ushort ActorUpdatMessageCount = 1;

        public long elapsedTime = DateTimeOffset.Now.ToUnixTimeMilliseconds();

        public bool hasInstance = true;
        public bool Instance = false;

        ///BundleType Transition
        public bool BundleTypeTransition = false;

        public bool serverSelect;

        public bool RdpReport = false;
        public bool RdpMessage = false;
        public bool SessionAck = false;
        public bool inGame = false;
        public bool coordToggle = false;
        public bool unkOpcode = true;
        ///Once we receive account ID, this should never change...
        public int AccountID;

        //Character List and dump stuff
        public List<Character> CharacterData;
        public Character MyCharacter;

        /// <summary>
        /// Create a Simple Client Session
        /// </summary>
        /// <param name="MyIPEndPoint"></param>
        public Session(IPEndPoint myIPEndPoint, uint instanceID)
        {
            MyIPEndPoint = myIPEndPoint;
            InstanceID = instanceID;
            sessionQueue = new(this);
        }

        ///Allows us to update last known Client Bundle Number
        public ushort clientBundleNumber
        {
            get { return ClientBundleNumber; }
            set
            {
                ClientBundleNumber = value;
                //if (ClientBundleNumber == 17)
                //BundleTypeTransition = true;
            }
        }

        public void UnreliablePing()
        {
            if (ping == 20)
            {
                sessionQueue.Add(new MessageStruct(new ReadOnlyMemory<byte>(new byte[] { 0xFC, 0x02, 0xD0, 0x07 })));
                ping = 0;
                return;
            }

            ping += 1;
            return;
        }

        public void CoordinateUpdate()
        {
            string theMessage = $"Coordinates: X-{MyCharacter.XCoord} Y-{MyCharacter.YCoord} Z-{MyCharacter.ZCoord}";
            queueMessages.messageCreator.MessageWriter(BitConverter.GetBytes((ushort)GameOpcode.ClientMessage));
            queueMessages.messageCreator.MessageWriter(BitConverter.GetBytes(theMessage.Length));
            queueMessages.messageCreator.MessageWriter(Encoding.Unicode.GetBytes(theMessage));

            //Send Message
            queueMessages.PackMessage(this, MessageOpcodeTypes.ShortReliableMessage);
            coordToggle = false;
        }

        public void ResetPing()
        {
            ping = 0;
        }

        ///Allows us to update last known Client Bundle Number
        public ushort clientMessageNumber
        {
            get { return ClientMessageNumber; }
            set { ClientMessageNumber = value; }
        }

        ///Allows us to update last known Server Bundle Number
        public ushort serverBundleNumber
        {
            get { return ServerBundleNumber; }
            set { ServerBundleNumber = value; }
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

        public void StartPipe(int size = 0)
        {
            Pipe pipe = new();
            pipeWriter = pipe.Writer;
            pipeReader = pipe.Reader;
            sessionMemory = pipeWriter.GetMemory(size);
        }

        public void WriteMessage(byte[] ourBuffer)
        {
            pipeWriter.WriteAsync(ourBuffer);
            bytesRead += ourBuffer.Length;
        }

        public ReadOnlyMemory<byte> ReadMessage()
        {
            pipeWriter.Complete();
            if (pipeReader.TryRead(out ReadResult OurResult))
            {
                try
                {
                    return OurResult.Buffer.First;
                }

                finally
                {
                    pipeReader.Complete();
                    pipeReader = null;
                    pipeWriter = null;
                }
            }
            return default;
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

        public void ClientTimerReset()
        {
            elapsedTime = DateTimeOffset.Now.ToUnixTimeMilliseconds();
        }

        ~Session()
        {
            Dispose(false);
        }
        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }

        private void Dispose(bool FreeManagedObjects)
        {

            CharacterData = null;
            MyCharacter = null;

            if (FreeManagedObjects)
            {

            }
        }
    }
}
