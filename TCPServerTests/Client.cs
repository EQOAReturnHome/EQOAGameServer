using System;
using EQOAAccount;
using EQOAUtilities;
using EQOAAccountMessageTypes;
using System.Net;
using System.Collections.Generic;
using System.Linq;
using System.Net.Sockets;

namespace EQOAClientSpace
{
    static public class EQOAClientTracker 
    {
        //Store connected client sessions here
        static public List<EQOAClient> EQOAClientList = new List<EQOAClient>();
        
        //Add new connecting clients here
        static public void AddEQOAClient(EQOAClient NewClient){ EQOAClientList.Add(NewClient); }

        //Remove Client from connected list
        static public void RemoveEQOAClient(EQOAClient DisconnectedClient) { EQOAClientList.Remove(DisconnectedClient); }
        
        //Gets total connected clients
        static public uint ConnectedEQOAClientCount() { return (uint)EQOAClientList.Count; }
    }
    
    public class EQOAClient : AccountManagement
    {
        
        //Incoming and Outgoing Message
        public List<byte> ClientPacket = new List<byte>();
        public List<byte> ResponsePacket = new List<byte>();
        
        //Message Data
        public int MessageLength;
        public int MessageType;
        
        //Username
        public string Username;

        //Encryoted Password
        public string EncPassword;
        
        //Socket
        public Socket Handler;

        public bool AccountVerified = false;

        //Client Information
        //Standard this for now... In the future would draw results from Database
        public int AccountID = 1;
        public short Result = 0;
        public short AccountStatus = 1;
        public int Subtime = 2592000;
        public int Partime = 2592000;
        public int Subfeatures = 4;
        public int Gamefeatures = 3;
        
        public EQOAClient(){ }
        
        public EQOAClient(IPEndPoint ThisIPEndPoint, Socket handler)
        {
            //Our socket
            Handler = handler;

            //This Clients IPAddress
            IPEndPoint ClientIPEndPoint = ThisIPEndPoint;

            //Add client to tracker
            EQOAClientTracker.AddEQOAClient(this);
        }
        
        public void ClearMessages()
        {
            //Clear the incoming packet
            ClientPacket.Clear();

            //Clear the outgoing packet
            ResponsePacket.Clear();
        }

        public void CheckClientRequest()
        {
            int MessageLength = Utilities.ReturnInt(ClientPacket);
            int MessageType = Utilities.ReturnInt(ClientPacket);
            Console.WriteLine($"Received Message with Length {MessageLength} and Message Type {MessageType}");
            
            switch (MessageType)
            {
                case AccountMessageTypes.LOGIN_REQUEST:
                    Console.WriteLine("Processing Character Request...");
                    LoginRequest(this);
                    break;

                case AccountMessageTypes.SUBMIT_ACCT_CREATE:
                    Console.WriteLine("Attempting to create an account...");
                    CreateAccount(this);
                    break;

                case AccountMessageTypes.CHANGE_PASSWORD:
                    Console.WriteLine("Attempting to change client Password...");
                    ChangePassword(this);
                    break;

                case AccountMessageTypes.CONSUME_GAMECARD:
                case AccountMessageTypes.REQUEST_ACCT_KEY:
                case AccountMessageTypes.REQUEST_CANCEL_ACCT:
                case AccountMessageTypes.REQUEST_GAME_CARD:
                case AccountMessageTypes.REQUEST_SUBSCRIPTION:
                case AccountMessageTypes.REQUEST_UPDATE_ACCT:
                case AccountMessageTypes.FORGOT_PASSWORD:
                    break;

                default:
                    break;

            }
        }
    }
}

