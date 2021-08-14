using System.Collections.Generic;
using System.Net;
using System;
using ReturnHome.Utilities;
using ReturnHome.Actor;
using System.IO.Pipelines;
using ReturnHome.Opcodes;
using System.Text;

namespace ReturnHome.Server.Network
{
    /// This is individual client session object
    public class Session
    {
        //Message List
		//New data to Session...
		public readonly RDPCommIn rdpCommIn { get; private set; } 
		public readonly RDPCommOut rdpCommOut { get; private set; }
        public readonly SessionQueue sessionQueue { get; private set; } 
		
		
        private byte _pingCount = 0;
        public uint InstanceID;
        public uint SessionID;
		public ushort ClientEndpoint;
		
		//Helps to identify master of session
		public bool didServerInitiate { get; private set; }
		
		///Client IPEndPoint
        public IPEndPoint MyIPEndPoint { get; private set; }
		
		//End
		
        private int bytesRead;
        public PacketCreator packetCreator = new();

        ///SessionList Objects, probably need bundle information here too?

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

        //Character 
        public Character MyCharacter { get; private set; }

        /// <summary>
        /// Create a Simple Client Session
        /// </summary>
        /// <param name="MyIPEndPoint"></param>
        public Session(ServerListener listener, IPEndPoint myIPEndPoint, uint sessionID, uint instanceID, ushort clientID, ushort serverID, bool DidServerInitiate)
        {
            didServerInitiate = DidServerInitiate;
            SessionID = sessionID;
            MyIPEndPoint = myIPEndPoint;
            InstanceID = instanceID;
            rdpCommIn = new(this, listener, clientID, serverID);
			rdpCommOut = new(this);
			sessionQueue = new(this);
			
			//Scrap this specific code?
            //If server initiated, have 2 messages to send client/initiate session
			/*
            if(didServerInitiate)
            {
                GameMessage[] gameList = new GameMessage[2];

                var camera1 = new Camera1();
                gameList[0] = camera1;

                var camera2 = new Camera2();
                gameList[1] = camera2;

                Network.EnqueueSend(gameList);
            }*/
        }

        public void ProcessPacket(ClientPacket packet)
        {
			//Eventually we would want to verify the sessions state in the game before continuing to process?
			//Would be effectively dropping the packet if this check fails
            //if (!CheckState(packet))
                //return;

            RdpCommIn.ProcessPacket(packet);
        }

        public void UnreliablePing()
        {
            if (_pingCount++ == 20)
            {
                sessionQueue.Add(new MessageStruct(new ReadOnlyMemory<byte>(new byte[] { 0xFC, 0x02, 0xD0, 0x07 })));
                _pingCount = 0;
            }
        }
		
		//This should get built into somewhere else, eventually
        public void CoordinateUpdate()
        {
            string theMessage = $"Coordinates: X-{MyCharacter.XCoord} Y-{MyCharacter.YCoord} Z-{MyCharacter.ZCoord}";
			
			//opcode length + string length definition
			int length = 2 + 4 + (theMessage.Length * 2);
			
			ReadOnlyMemory<byte> message = new ReadOnlyMemory<byte>(length);
			
            BitConverter.GetBytes((ushort)GameOpcode.ClientMessage).CopyTo(message[0..2]);
            BitConverter.GetBytes(theMessage.Length).CopyTo(message[2..6]);
            Encoding.Unicode.GetBytes(theMessage).CopyTo(message[6..message.Length]);
			
            //Send Message
			rdpCommIn.sessionQueueMessages.PackMessage(this, message, MessageOpcodeTypes.ShortReliableMessage);
            coordToggle = false;
        }

        public void ResetPing()
        {
            _pingCount = 0;
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
		
		/// <summary>
        /// This will send outgoing packets as well as the final logoff message.
        /// </summary>
        public void TickOutbound()
        {
			//Add some state stuff? To identify some stuff
            // Checks if the session has stopped responding.
            if (DateTime.UtcNow.Ticks >= Network.TimeoutTick)
            {
				//After 2 minutes... disconnect session
				if (inGame)
				{
					UnreliablePing();
                    if (MySession.coordToggle)
                    {
                        MySession.CoordinateUpdate();
                    }
				}
            }

            rdpCommOut.PrepPackets();
        }	
    }
}
