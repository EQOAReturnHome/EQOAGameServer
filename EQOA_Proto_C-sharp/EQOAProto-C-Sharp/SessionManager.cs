using EQLogger;
using RdpComm;
using System;
using System.Collections.Generic;
using System.Net;
using Utility;
using Sessions;

namespace SessManager
{
    public class SessionManager
    {
        ///Our IDUP Starter for now
        private static uint SessionIDUpStarter = 220760;

        ///This is our sessionList
        public static List<Session> SessionList = new List<Session>();

        ///When a new session is identified, we add this into our endpoint/session list
        public static void ProcessSession(List<byte> myPacket, IPEndPoint MyIPEndPoint)
        ///public static void ProcessSession(List<byte> myPacket, bool NewSession)
        {
            ushort ClientEndpoint = (ushort)(myPacket[1] << 8 | myPacket[0]);
            myPacket.RemoveRange(0, 4);

            bool NewInstance = false;
            bool InstanceHeader = false;
            bool ResetInstance = false;

            uint val = Utility_Funcs.Unpack(myPacket);

            //Get's this bundle length
            int BundleLength = (int)(val & 0x7FF);
            int value = (int)(val - (val & 0x7FF));

            if ((value & 0x80000) != 0)//Requesting instance ack, starting a new session/instance from client
            {
                NewInstance = true;
            }

            if ((value & 0x02000) != 0)//Has instance in header
            {
                InstanceHeader = true;
            }

            if ((value & 0x10000) != 0)// reset connection?
            {
                ResetInstance = true;
            }

            //if false, means 4 byte instance header has been removed and only need to read the 3 byte character ID (If server is master
            //If client is "master", then goes straight into bundle type
            if (InstanceHeader)
            {

                uint SessionIDBase = (uint)(myPacket[3] << 24 | myPacket[2] << 16 | myPacket[1] << 8 | myPacket[0]);
                myPacket.RemoveRange(0, 4);

                //If not reset instance, start processing packet data
                if (ResetInstance)
                {
                    //Try to find the instance
                    try
                    {
                        ///This finds our session by utilizing endpoints and IPEndPoint info (IP and Port) and drops it
                        DropSession(SessionList.Find(i => Equals(i.clientEndpoint, ClientEndpoint) && Equals(i.MyIPEndPoint, MyIPEndPoint) && Equals(i.sessionIDBase, SessionIDBase)));
                    }

                    catch
                    {
                        //Just let it die gracefully if not in list
                        // possible for a session to not be in the list if a new connection
                    }

                }

                //If new instance, client is initiating this and we know bytes to read
                else if (NewInstance)
                {
                    Logger.Info("Checking if session \"exists\"");
                    bool SessionExistence = SessionList.Exists(i => Equals(i.clientEndpoint, ClientEndpoint) && Equals(i.MyIPEndPoint, MyIPEndPoint) && Equals(i.sessionIDBase, SessionIDBase));

                    ///If it exists, returns to drop this request
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
                    Session thisSession = new Session(ClientEndpoint, MyIPEndPoint, SessionIDBase);

                    ///Add this sesison to our session list
                    AddMasterSession(thisSession);

                    //Process remaining data
                    ProcessSession(thisSession, myPacket);
                }

                //Continue processing session
                else
                {
                    //Grab session
                    Session thisSession = SessionList.Find(i => Equals(i.clientEndpoint, ClientEndpoint) && Equals(i.MyIPEndPoint, MyIPEndPoint) && Equals(i.sessionIDBase, SessionIDBase));

                    if(thisSession == null)
                    {
                        Console.WriteLine("Error Finding Session, maybe transition to memory dump? Ignore for now");
                        return;
                    }
                    //If server is master/initiated the session we need to remove the 3 byte session Identifier from packet
                    if (thisSession.remoteMaster)
                    {
                        //Could always do an additional check to make sure this matches internally?
                        //Something like
                        //if (thisSession.SessionIDUp == Utility_Funcs.Untechnique(myPacket))
                        //{//Continue}
                        //else{ //Note exception and fail gracefully?}

                        //Will read our Session Identifier and remove it off the packet for us
                        int SessionIDUp = Utility_Funcs.Untechnique(myPacket);
                    }

                    //If remote master is 0, doesn't really matter... means client is "master" and will not have the 3 byte characterInstanceID
                    ProcessSession(thisSession, myPacket);

                }
            }

            //4 byte header has been removed
            else
            {
                //Assumption would be this must be an ongoing session, so should never expect a new session without the instance header
                //Grab session
                //Will this always be correct? Shouldn't have duplicate client endpoints so should be...
                Session thisSession = SessionList.Find(i => Equals(i.clientEndpoint, ClientEndpoint) && Equals(i.MyIPEndPoint, MyIPEndPoint));

                //If server is master/initiated the session we need to remove the 3 byte session Identifier from packet
                if (thisSession.remoteMaster)
                {
                    //Could always do an additional check to make sure this matches internally?
                    //Something like
                    //if (thisSession.SessionIDUp == Utility_Funcs.Untechnique(myPacket))
                    //{//Continue}
                    //else{ //Note exception and fail gracefully?}

                    //Will read our Session Identifier and remove it off the packet for us
                    int SessionIDUp = Utility_Funcs.Untechnique(myPacket);
                }

                //If remote master is 0, doesn't really matter... means client is "master" and will not have the 3 byte characterInstanceID
                ProcessSession(thisSession, myPacket);
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
}