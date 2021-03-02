﻿using EQOAProto;
using RdpComm;
using System;
using System.Net;
using Exodus.Collections.Concurrent;
using Utility;
using Sessions;
using Characters;
using DNP3;
using Opcodes;

namespace SessManager
{
    public class SessionManager
    {
        ///Our IDUP Starter for now
        private uint InstanceIDUpStarter = 220760;

        ///This is our sessionList
        public ConcurrentHashSet<Session> SessionHash = new ConcurrentHashSet<Session>();

        private readonly RdpCommIn _rdpComm;
        public readonly SessionQueueMessages _queueMessages = new();

        public SessionManager()
        {
            _rdpComm = new(this, _queueMessages);
        }

        ///When a new session is identified, we add this into our endpoint/session list
        public void ProcessSession(ReadOnlyMemory<byte> ClientPacket, int offset, ushort ClientEndPoint, IPEndPoint ClientIPEndPoint,  uint val)
        ///public static void ProcessSession(List<byte> myPacket, bool NewSession)
        {
            //Make sure this is null'd before continuing
            uint InstanceID = 0;
            uint SessionID = 0;
            Session ClientSession = null;

            //Get's this bundle length
            int BundleLength = (int)(val & 0x7FF);
            int value = (int)(val - (val & 0x7FF));

            if ((value & 0x02000) != 0) //Has instance in header
            {
                InstanceID = BinaryPrimitiveWrapper.GetLEUint(ClientPacket, ref offset);

                if ((value & 0x80000) != 0) //Requesting instance ack, starting a new session/instance from client
                {
                    // Create New Session
                    ClientSession = new Session(ClientIPEndPoint, InstanceID);

                    //Try adding session to hashset
                    if(SessionHash.TryAdd(ClientSession))
                    {
                        ClientSession.ClientEndpoint = ClientEndPoint;
                        //Success, keep processing data
                        _rdpComm.ProcessBundle(ClientSession, ClientPacket, offset);
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
                    if (FindSession(ClientIPEndPoint, out ClientSession))
                    {
                        if ((value & 0x10000) != 0) // reset connection?
                        {
                            SessionHash.TryRemove(ClientSession);
                            return;
                        }

                        _rdpComm.ProcessBundle(ClientSession, ClientPacket, offset);
                        return;
                    }

                    //Ideally this won't be hit... should there be logging for this?
                    return;
                }

                else
                {
                    //Utilize 0'd SessionID to find correct result
                    if (FindSession(ClientIPEndPoint, out ClientSession))
                    {
                        if ((value & 0x10000) != 0) // reset connection?
                        {
                            SessionHash.TryRemove(ClientSession);
                            return;
                        }

                        _rdpComm.ProcessBundle(ClientSession, ClientPacket, offset);
                        return;
                    }

                    //Ideally this won't be hit... should there be logging for this?
                    return;
                }
            }

            //No instance header, means there is an established session
            else
            {
                //If true, should be a 3 byte SessionID to read/verify
                //Means client is "remote endpoint"
                if ((value & 0x0800) != 0)
                {
                    //Utilize actual SessionID to help narrow down for correct results
                    SessionID = Utility_Funcs.Unpack(ClientPacket.Span, ref offset);
                }

                if (FindSession(ClientIPEndPoint, out ClientSession))
                {
                    if ((value & 0x10000) != 0) // reset connection?
                    {
                        SessionHash.TryRemove(ClientSession);
                        return;
                    }

                    _rdpComm.ProcessBundle(ClientSession, ClientPacket, offset);
                }
            }
        }

        public bool FindSession(IPEndPoint ClientIPEndPoint, out Session actualSession)
        {
            foreach(Session ClientSession in SessionHash)
            {
                if (ClientSession.MyIPEndPoint.Equals(ClientIPEndPoint))
                {
                    actualSession = ClientSession;
                    return true;
                }
            }

            //Need logging to indicate actual session was not found
            actualSession = default;
            return false;
        }

        public uint ObtainIDUp()
        {
            ///Eventually would need some checks to make sure it isn't taken in bigger scale
            uint NewID = InstanceIDUpStarter;
            ///Increment by 2 every pull
            ///Client will Transition into InstanceIDUpStarter + 1
            InstanceIDUpStarter += 2;

            return NewID;
        }

        public void CreateMasterSession(Session MySession)
        {
            Session NewMasterSession = new Session(MySession.MyIPEndPoint, DNP3Creation.DNP3Session());
            NewMasterSession.SessionID = ObtainIDUp();
            NewMasterSession.AccountID = MySession.AccountID;
            NewMasterSession.ClientEndpoint = MySession.ClientEndpoint;
            NewMasterSession.RemoteMaster = true;
            NewMasterSession.SessionAck = true;
            NewMasterSession.CharacterSelect = true;

            if (SessionHash.TryAdd(MySession))
            {
                //Start memory dump here.
                GenerateClientContact(NewMasterSession);
            }

        }

        public void CreateMemoryDumpSession(Session MySession)
        {
            //Start new session 
            Session thisSession = new Session(MySession.MyIPEndPoint, DNP3Creation.DNP3Session());
            thisSession.ClientEndpoint = MySession.ClientEndpoint;
            thisSession.RemoteMaster = MySession.RemoteMaster;
            thisSession.AccountID = MySession.AccountID;
            thisSession.SessionID = MySession.SessionID + 1;
            thisSession.MyCharacter = MySession.MyCharacter;
            if (SessionHash.TryAdd(thisSession))
            {
                //Start memory dump here.
            }

        }

        ///Used when starting a master session with client.
        public void GenerateClientContact(Session MySession)
        {
            MySession.messageCreator.MessageWriter(BitConverter.GetBytes((ushort)GameOpcode.Camera1));
            MySession.messageCreator.MessageWriter(new byte[] { 0x03, 0x00, 0x00, 0x00 });
            ///Handles packing message into outgoing packet
            _queueMessages.PackMessage(MySession, MessageOpcodeTypes.ShortReliableMessage);

            MySession.messageCreator.MessageWriter(BitConverter.GetBytes((ushort)GameOpcode.Camera2));
            MySession.messageCreator.MessageWriter(new byte[] { 0x1B, 0x00, 0x00, 0x00 });
            ///Handles packing message into outgoing packet
            _queueMessages.PackMessage(MySession, MessageOpcodeTypes.ShortReliableMessage);
            MySession.ClientFirstConnect = true;
        }
    }
}
