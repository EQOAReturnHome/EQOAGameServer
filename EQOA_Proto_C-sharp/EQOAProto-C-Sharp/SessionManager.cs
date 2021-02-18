using EQLogger;
using EQOAProto;
using RdpComm;
using System;
using System.Collections.Generic;
using System.Net;
using Utility;
using Sessions;
using System.Linq;

namespace SessManager
{
    public class SessionManager
    {
        ///Our IDUP Starter for now
        private static uint SessionIDUpStarter = 220760;

        ///This is our sessionList
        public static Dictionary<IPEndPoint, Session> SessionDict = new Dictionary<IPEndPoint, Session>();

        ///When a new session is identified, we add this into our endpoint/session list
        public static void ProcessSession(ReadOnlySpan<byte> ClientPacket, int offset, IPEndPoint MyIPEndPoint, ushort ClientEndpoint)
        ///public static void ProcessSession(List<byte> myPacket, bool NewSession)
        {
            bool RemoteEndPoint = false;
            bool NewInstance = false;
            bool InstanceHeader = false;
            bool ResetInstance = false;

            uint val = Utility_Funcs.Unpack(ClientPacket, ref offset);

            //Get's this bundle length
            int BundleLength = (int)(val & 0x7FF);
            int value = (int)(val - (val & 0x7FF));

            if ((value & 0x80000) != 0) //Requesting instance ack, starting a new session/instance from client
            {
                NewInstance = true;
            }

            if ((value & 0x02000) != 0) //Has instance in header
            {
                InstanceHeader = true;
            }

            if ((value & 0x10000) != 0) // reset connection?
            {
                ResetInstance = true;
            }

            if ((value & 0x0800) != 0)
            {
                RemoteEndPoint = true;
            }

            //if false, means 4 byte instance header has been removed and only need to read the 3 byte character ID (If server is master
            //If client is "master", then goes straight into bundle type
            if (InstanceHeader)
            {

                uint SessionIDBase = BinaryPrimitiveWrapper.GetLEUint(ClientPacket, ref offset);

                //If not reset instance, start processing packet data
                if (ResetInstance)
                {
                    //Try to find the instance
                    try
                    {
                        ///This finds our session by utilizing endpoints and IPEndPoint info (IP and Port) and drops it
                        DropSession(SessionDict[MyIPEndPoint]);

                        //skip double session info here..
                        offset += 4;

                        if (RemoteEndPoint)
                        {
                            //Skip "Object ID"
                            offset += 3;
                        }

                        //Once we finish processing prior bundle, if the offset is still less then the Packet Length, must be another bundle?
                        //Add + 4 to offset on check to account for CRC at end
                        if(ClientPacket.Length > offset + 4)
                        {
                            //Process next bundle
                            ProcessSession(ClientPacket, offset, MyIPEndPoint, ClientEndpoint);
                        }
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
                    //Check if there is a session tied to this IP/Port
                    //if so... Ignore the request?
                    if(SessionDict.ContainsKey(MyIPEndPoint))
                    {
                        //Ignoring
                        return;
                    }

                    ///Create the session
                    Logger.Info("Creating new session for Client");

                    ///Create our session object instance
                    Session thisSession = new Session(ClientEndpoint, MyIPEndPoint, SessionIDBase);

                    ///Add this sesison to our session list
                    AddMasterSession(thisSession);

                    //Process remaining data
                    ProcessBundle(thisSession, ClientPacket, offset);
                }

                //Continue processing session
                else
                {
                    //Grab session
                    Session thisSession = SessionDict[MyIPEndPoint];

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
                        int SessionIDUp = Utility_Funcs.Untechnique(ClientPacket, ref offset);
                    }

                    //If remote master is 0, doesn't really matter... means client is "master" and will not have the 3 byte characterInstanceID
                    ProcessBundle(thisSession, ClientPacket, offset);

                }
            }

            //4 byte header has been removed
            else
            {
                //Assumption would be this must be an ongoing session, so should never expect a new session without the instance header
                //Grab session
                //Will this always be correct? Shouldn't have duplicate client endpoints so should be...
                Session thisSession = SessionDict[MyIPEndPoint];

                //If server is master/initiated the session we need to remove the 3 byte session Identifier from packet
                if (thisSession.remoteMaster)
                {
                    //Could always do an additional check to make sure this matches internally?
                    //Something like
                    //if (thisSession.SessionIDUp == Utility_Funcs.Untechnique(myPacket))
                    //{//Continue}
                    //else{ //Note exception and fail gracefully?}

                    //Will read our Session Identifier and remove it off the packet for us
                    int SessionIDUp = Utility_Funcs.Untechnique(ClientPacket, ref offset);
                }

                //If remote master is 0, doesn't really matter... means client is "master" and will not have the 3 byte characterInstanceID
                ProcessBundle(thisSession, ClientPacket, offset);
            }
        }

        ///Is this really needed?
        public static void ProcessBundle(Session MySession, ReadOnlySpan<byte> ClientPacket, int offset)
        {
            ///Send to RdpCommunicate

            ///SQLOperations.AccountCharacters(MySession);
            RdpCommIn.ProcessBundle(MySession, ClientPacket, offset);
        }

        public static void DropSession(Session MySession)
        {
            ///Simple enough, remove the session
            Logger.Info("Removing Session");

            //Stop the timers
            MySession.StopTimers();

            lock (SessionDict)
            {
                SessionDict.Remove(MySession.MyIPEndPoint);
            }
        }

        public static void AddMasterSession(Session MyMasterSession)
        {
            Logger.Info("Adding Master Session To List");
            lock (SessionDict)
            {
                SessionDict.Add(MyMasterSession.MyIPEndPoint, MyMasterSession);
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