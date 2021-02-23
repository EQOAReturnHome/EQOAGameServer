using EQOAProto;
using RdpComm;
using System;
using System.Net;
using Exodus.Collections.Concurrent;
using Utility;
using Sessions;

namespace SessManager
{
    public class SessionManager
    {
        ///Our IDUP Starter for now
        private static uint InstanceIDUpStarter = 220760;

        ///This is our sessionList
        public static ConcurrentHashSet<Session> SessionHash = new ConcurrentHashSet<Session>();

        private static Session ClientSession;
        private static uint InstanceID;
        private static uint SessionID;
        ///When a new session is identified, we add this into our endpoint/session list
        public static void ProcessSession(ReadOnlyMemory<byte> ClientPacket, int offset, ushort ClientEndPoint, IPEndPoint ClientIPEndPoint,  uint val)
        ///public static void ProcessSession(List<byte> myPacket, bool NewSession)
        {
            //Make sure this is null'd before continuing
            InstanceID = 0;
            SessionID = 0;
            ClientSession = null;

            //Get's this bundle length
            int BundleLength = (int)(val & 0x7FF);
            int value = (int)(val - (val & 0x7FF));

            if ((value & 0x02000) != 0) //Has instance in header
            {
                InstanceID = BinaryPrimitiveWrapper.GetLEUint(ClientPacket, ref offset);

                if ((value & 0x80000) != 0) //Requesting instance ack, starting a new session/instance from client
                {
                    // Create New Session
                    Session ClientSession = new Session(ClientIPEndPoint, InstanceID);

                    //Try adding session to hashset
                    if(SessionHash.TryAdd(ClientSession))
                    {
                        ClientSession.ClientEndpoint = ClientEndPoint;
                        //Success, keep processing data
                        RdpCommIn.ProcessBundle(ClientSession, ClientPacket, offset);
                        return;
                    }
                    //If it fails to add, return?
                    //This would mean there is already a connection for this endpoint
                    return;
                }

                //If true, should be a 3 byte SessionID to read/verify
                //Means client is "remote endpoint"
                if((value & 0x0800) != 0)
                {
                    //Utilize actual SessionID to help narrow down for correct results
                    SessionID = Utility_Funcs.Unpack(ClientPacket.Span, ref offset);
                    if (FindSession(ClientIPEndPoint, SessionID, out ClientSession))
                    {
                        if ((value & 0x10000) != 0) // reset connection?
                        {
                            SessionHash.TryRemove(ClientSession);
                            return;
                        }

                        RdpCommIn.ProcessBundle(ClientSession, ClientPacket, offset);
                        return;
                    }

                    //Ideally this won't be hit... should there be logging for this?
                    return;
                }

                else
                {
                    //Utilize 0'd SessionID to find correct result
                    if (FindSession(ClientIPEndPoint, SessionID, out ClientSession))
                    {
                        if ((value & 0x10000) != 0) // reset connection?
                        {
                            SessionHash.TryRemove(ClientSession);
                            return;
                        }

                        RdpCommIn.ProcessBundle(ClientSession, ClientPacket, offset);
                        return;
                    }

                    //Ideally this won't be hit... should there be logging for this?
                    return;
                }
            }

            //No instance header, means there is an established session
            else
            {
                //Utilize actual SessionID to help narrow down for correct results
                SessionID = Utility_Funcs.Unpack(ClientPacket.Span, ref offset);

                if (FindSession(ClientIPEndPoint, SessionID, out ClientSession))
                {
                    if ((value & 0x10000) != 0) // reset connection?
                    {
                        SessionHash.TryRemove(ClientSession);
                        return;
                    }

                    RdpCommIn.ProcessBundle(ClientSession, ClientPacket, offset);
                }
            }
        }

        public static bool FindSession(IPEndPoint ClientIPEndPoint, uint SessionID, out Session actualSession)
        {
            foreach(Session ClientSession in SessionHash)
            {
                if (ClientSession.MyIPEndPoint.Equals(ClientIPEndPoint) && (ClientSession.SessionID == SessionID))
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
    }
}