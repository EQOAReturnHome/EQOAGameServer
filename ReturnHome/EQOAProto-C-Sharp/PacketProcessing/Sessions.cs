using System.Collections.Generic;
using System.Net;
using System;
using ReturnHome.Utilities;
using ReturnHome.Actor;
using System.IO.Pipelines;

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
        private int bytesRead;
        public byte ping = 0;

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
        public MessageStruct Channel40Base;
        public ushort ActorUpdatMessageCount = 1; 

        public long elapsedTime = DateTimeOffset.Now.ToUnixTimeMilliseconds();

        public bool hasInstance = true;
        public bool Instance = false;

        ///BundleType Transition
        public bool BundleTypeTransition = false;

        public bool RdpReport = false;
        public bool RdpMessage = false;
        public bool SessionAck = false;

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
            //StartTimer(2000);
        }

        ///Allows us to update last known Client Bundle Number
        public ushort clientBundleNumber
        {
            get { return ClientBundleNumber; }
            set { ClientBundleNumber = value;
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

        public void ResetPing()
        {
            ping = 0;
        }

        ///Allows us to update last known Client Bundle Number
        public ushort clientMessageNumber
        {
            get { return ClientMessageNumber; }
            set
            {
                ClientMessageNumber = value;
                //if (ClientMessageNumber == 17)
                   // BundleTypeTransition = true;
            }
        }

        ///Allows us to update last known Server Bundle Number
        public ushort serverBundleNumber
        {
            get { return ServerBundleNumber; }
            set
            {
                ServerBundleNumber = value;
                //if (ServerBundleNumber == 17)
                    //BundleTypeTransition = true;
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
