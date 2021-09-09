﻿using System;
using System.Net;
using System.Threading.Tasks;
using System.Linq;

using ReturnHome.Utilities;

namespace ReturnHome.Server.Network.Managers
{
    public static class SessionManager
    {
        ///Our IDUP Starter for now
        private static uint InstanceIDUpStarter = 220760;

        ///This is our sessionList
        public static readonly ConcurrentHashSet<Session> SessionHash = new ConcurrentHashSet<Session>();

        /// <summary>
        /// Handles packets from clients, creating/removing sessions, or sending established connections data to process
        /// </summary>
        public static void ProcessPacket(ServerListener listener, ClientPacket packet, IPEndPoint ClientIPEndPoint)
        ///public static void ProcessSession(List<byte> myPacket, bool NewSession)
        {
			Session ClientSession;

            //Remove session
            if (packet.Header.CancelSession)
            {
                findSession(ClientIPEndPoint, packet.Header.InstanceID, out ClientSession);
                if (SessionHash.TryRemove(ClientSession))
                    Logger.Info("Session Successfully removed from Session List");
                return;
            }

            //Create a new session
            if(packet.Header.NewInstance)
			{
                // Create New Session
                ClientSession = new Session(listener, ClientIPEndPoint, packet.Header.SessionID, packet.Header.InstanceID, packet.Header.ClientEndPoint, listener.serverEndPoint, false);

                //Try adding session to hashset
                if (SessionHash.TryAdd(ClientSession))
                {
                    Logger.Info($"{ClientSession.ClientEndpoint.ToString("X")}: Processing new session");

                    //Success, keep processing data
                    ClientSession.ProcessPacket(packet);
                }
			}

            else
            {
				//Find session, if it returns true, outputs session
                if (findSession(ClientIPEndPoint, packet.Header.InstanceID, out ClientSession))
                {
					//Checks if IP/Port matches expected session to incoming packet
					//This might not be needed?
                    if (ClientSession.MyIPEndPoint.Equals(ClientIPEndPoint))
                        ClientSession.ProcessPacket(packet);

                    else
                    {
                        //Somehow got the wrong session? Def. Needs a log to notate this
                        ClientSession = null;
                        Logger.Info($"Session for Id {packet.Header.ClientEndPoint} has IP {ClientSession.MyIPEndPoint} but packet has IP {ClientIPEndPoint}");
                    }
                }

                else
                {
                    Logger.Info($"Unsolicited Packet from {ClientIPEndPoint} with Id { packet.Header.ClientEndPoint}");
                }
            }
        }

        /// <summary>
        /// Finds a session and returns it
        /// </summary>
        public static bool findSession(IPEndPoint ClientIPEndPoint, uint InstanceID, out Session actualSession)
        {
            foreach (Session ClientSession in SessionHash)
            {
                if (ClientSession.MyIPEndPoint.Equals(ClientIPEndPoint) && ClientSession.InstanceID == InstanceID)
                {
                    actualSession = ClientSession;
                    return true;
                }
            }

            //Need logging to indicate actual session was not found
            actualSession = default;
            return false;
        }
		
        public static uint ObtainIDUp()
        {
            ///Eventually would need some checks to make sure it isn't taken in bigger scale
            uint NewID = InstanceIDUpStarter;
            ///Increment by 2 every pull
            ///Client will Transition into InstanceIDUpStarter + 1
            InstanceIDUpStarter += 2;

            return NewID;
        }

        public static void CreateMasterSession(Session MySession)
        {
            Session NewMasterSession = new Session(MySession.listener, MySession.MyIPEndPoint, ObtainIDUp(), DNP3Creation.DNP3Session(), MySession.ClientEndpoint, MySession.rdpCommIn.serverID, true);
            NewMasterSession.AccountID = MySession.AccountID;
            NewMasterSession.SessionAck = true;

            if (SessionHash.TryAdd(NewMasterSession))
            {
                //Start memory dump here.
                GenerateClientContact(NewMasterSession);
            }

        }

        public static void CreateMemoryDumpSession(Session MySession)
        {
            //Start new session 
            Session NewMasterSession = new Session(MySession.listener, MySession.MyIPEndPoint, MySession.SessionID++, DNP3Creation.DNP3Session(), MySession.ClientEndpoint, MySession.rdpCommIn.serverID, true);
            NewMasterSession.ClientEndpoint = MySession.ClientEndpoint;
            NewMasterSession.AccountID = MySession.AccountID;
            NewMasterSession.MyCharacter = MySession.MyCharacter;
            NewMasterSession.SessionAck = true;

            if (SessionHash.TryAdd(NewMasterSession))
            {
                Console.WriteLine("Ready For Memory Dump");
                //ProcessOpcode.ProcessMemoryDump(NewMasterSession.queueMessages, thisSession, this);
            }
        }

        ///Used when starting a master session with client.
        public static void GenerateClientContact(Session MySession)
        {
            /*
            MySession.queueMessages.messageCreator.MessageWriter(BitConverter.GetBytes((ushort)GameOpcode.Camera1));
            MySession.queueMessages.messageCreator.MessageWriter(new byte[] { 0x03, 0x00, 0x00, 0x00 });
            ///Handles packing message into outgoing packet
            MySession.queueMessages.PackMessage(MySession, MessageOpcodeTypes.ShortReliableMessage);

            MySession.queueMessages.messageCreator.MessageWriter(BitConverter.GetBytes((ushort)GameOpcode.Camera2));
            MySession.queueMessages.messageCreator.MessageWriter(new byte[] { 0x1B, 0x00, 0x00, 0x00 });
            ///Handles packing message into outgoing packet
            MySession.queueMessages.PackMessage(MySession, MessageOpcodeTypes.ShortReliableMessage);
            */
        }

        public static async Task CheckClientTimeOut()
        {
            //Approximately every 30 seconds, check if a session is in a timedout state and disconnect it
            do
            {
                foreach (Session MySession in SessionHash)
                {
                    if ((DateTimeOffset.Now.ToUnixTimeMilliseconds() - MySession.elapsedTime) > 60000)
                    {
                        //Send a disconnect from server to client, then remove the session
                        //For now just remove the session
                        if (SessionHash.TryRemove(MySession))
                        {
                            Console.WriteLine("Removed a Session");
                        }
                    }
                }
                await Task.Delay(30000);
            }
            while (true);
        }
		
		/// <summary>
        /// Dispatches all outgoing messages.<para />
        /// Removes dead sessions.
        /// </summary>
        public static int BeginSessionWork()
        {
            int sessionCount = 0;

            // The session tick outbound processes pending actions and handles outgoing messages
            Parallel.ForEach(SessionHash, s => s?.TickOutbound());

            // Removes sessions in the NetworkTimeout state, including sessions that have reached a timeout limit.
            foreach (var session in SessionHash.Where(k => !Equals(null, k)))
            {
                if (session.PendingTermination)
                {
                    session.DropSession();
                }

                sessionCount++;
            }
			
            return sessionCount;
        }
		
    }
}