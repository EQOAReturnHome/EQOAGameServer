using System.Net;
using System;

using ReturnHome.Utilities;
using ReturnHome.Opcodes.Chat;
using ReturnHome.Server.Managers;
using ReturnHome.Server.Network.Managers;
using ReturnHome.Server.EntityObject.Player;

namespace ReturnHome.Server.Network
{
    /// This is individual client session object
    public class Session
    {
        //Message List
		//New data to Session...
		public RdpCommIn rdpCommIn { get; private set; } 
		public RdpCommOut rdpCommOut { get; private set; }
        public SessionQueue sessionQueue { get; private set; }
        public ServerListener listener { get; private set; }
		
		
        private short _pingCount = 0;
        public uint InstanceID;
        public uint SessionID;
		public ushort ClientEndpoint;
		
		//Helps to identify master of session
		public bool didServerInitiate { get; private set; }
        public bool PendingTermination { get; set; } = false;
		
		///Client IPEndPoint
        public IPEndPoint MyIPEndPoint { get; private set; }
		
		//End
		
        public PacketCreator packetCreator = new();

        ///SessionList Objects, probably need bundle information here too?
        public ushort ActorUpdatMessageCount = 1;

        public bool hasInstance = true;
        public bool Instance = false;

        ///BundleType Transition
        public bool BundleTypeTransition = false;

        public bool serverSelect;

        public bool characterInWorld = false;
        public bool RdpReport = false;
        public bool RdpMessage = false;
        public bool SessionAck = false;
        public bool clientUpdateAck = false;
        public bool inGame = false;
        public bool objectUpdate = false;
        public bool unkOpcode = true;
        ///Once we receive account ID, this should never change...
        public int AccountID;

        //Character 
        public Character MyCharacter { get; set; }

        /// <summary>
        /// Create a Simple Client Session
        /// </summary>
        /// <param name="MyIPEndPoint"></param>
        public Session(ServerListener listener, IPEndPoint myIPEndPoint, uint instanceID, uint sessionID, ushort clientID, ushort serverID, bool DidServerInitiate)
        {
            didServerInitiate = DidServerInitiate;
            SessionID = sessionID;
            MyIPEndPoint = myIPEndPoint;
            InstanceID = instanceID;
            rdpCommIn = new(this, clientID, serverID);
			rdpCommOut = new(this, listener);
			sessionQueue = new(this);
        }

        public void ProcessPacket(ClientPacket packet)
        {
			//Eventually we would want to verify the sessions state in the game before continuing to process?
			//Would be effectively dropping the packet if this check fails
            //if (!CheckState(packet))
                //return;

            rdpCommIn.ProcessPacket(packet);
        }

        public void UnreliablePing()
        {
            sessionQueue.Add(new MessageStruct(new ReadOnlyMemory<byte>(new byte[] { 0xFC, 0x02, 0xD0, 0x07 })));
            _pingCount++;
        }

        //This should get built into somewhere else, eventually
        public void CoordinateUpdate()
        {
            string message = $"Coordinates: X-{MyCharacter.x} Y-{MyCharacter.y} Z-{MyCharacter.z}";

            ChatMessage.GenerateClientSpecificChat(this, message);
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

        public void UpdateClientObject()
        {
            MyCharacter?.UpdatePosition();
        }

        /// <summary>
        /// This will send outgoing packets as well as the final logoff message.
        /// </summary>
        public void TickOutbound()
        {
            //Add some state stuff? To identify some stuff
            // Checks if the session has stopped responding.
            if (DateTime.UtcNow.Ticks >= rdpCommIn.TimeoutTick)
            {
                UnreliablePing();
                if (inGame)
                    rdpCommIn.TimeoutTick = DateTime.UtcNow.AddSeconds(2).Ticks;
                else
                    rdpCommIn.TimeoutTick = DateTime.UtcNow.AddSeconds(45).Ticks;
            }

            if (characterInWorld)
            {
                //Check for updates on current objects
                foreach (ServerObjectUpdate i in rdpCommIn.connectionData.serverObjects.Span)
                    i.GenerateUpdate();
            }

            PendingTermination = inGame ? _pingCount >= 50 ? true : false : _pingCount >= 10 ? true : false;
                //Send a disconnect from server to client, then remove the session
                //For now just remove the session

            rdpCommOut.PrepPackets();
        }

        public void DropSession()
        {
            if (!PendingTermination) return;
            //Eventually this would kick the player out of the world and save data/free resources
            // Remove character from Character List
            MapManager.RemoveObjectFromTree(MyCharacter);
            PlayerManager.RemovePlayer(MyCharacter);
            EntityManager.RemoveEntity(MyCharacter);
            SessionManager.SessionHash.TryRemove(this);
        }
    }
}
