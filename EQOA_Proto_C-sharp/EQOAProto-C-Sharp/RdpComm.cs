using EQLogger;
using EQOAProto;
using EQOASQL;
using OpcodeOperations;
using Opcodes;
using SessManager;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Timers;
using Utility;
using Characters;
using Sessions;
using Unreliable;
using System.Net.Sockets;

namespace RdpComm
{
    /// <summary>
    /// This is user to start processing incoming and outgoing packets
    /// </summary>
    class RdpCommIn
    {

        public static void ProcessBundle(Session MySession, ReadOnlySpan<byte> ClientPacket, int offset)
        {
            ///Grab our bundle type
            byte BundleType = BinaryPrimitiveWrapper.GetLEByte(ClientPacket, ref offset);
            Logger.Info($"BundleType is {BundleType}");

            ///Perform a check to find what switch statement is true
            switch (BundleType)
            {
                case BundleOpcode.ProcessAll:

                    ProcessSessionAck(MySession, ClientPacket, ref offset);
                    ProcessRdpReport(MySession, ClientPacket, ref offset, false);
                    ProcessMessageBundle(MySession, ClientPacket, offset);
                    Logger.Info("Processing Session Ack, Rdp Report and Message Bundle");
                    break;

                case BundleOpcode.NewProcessReport:
                    ProcessRdpReport(MySession, ClientPacket, ref offset, false);
                    ProcessMessageBundle(MySession, ClientPacket, offset);
                    Logger.Info("Processing Rdp Report and messages");
                    break;

                case BundleOpcode.ProcessReport:
                    ProcessRdpReport(MySession, ClientPacket, ref offset, false);
                    Logger.Info("Processing Rdp Report");
                    break;

                case BundleOpcode.NewProcessMessages:
                case BundleOpcode.ProcessMessages:

                    //2 bytes for Bundle #, is it relevant to track?
                    offset += 2;
                    
                    ProcessMessageBundle(MySession, ClientPacket, offset);
                    Logger.Info("Processing Message Bundle");
                    break;

                case BundleOpcode.ProcessMessageAndReport:
                    ProcessRdpReport(MySession, ClientPacket, ref offset, true);
                    ProcessMessageBundle(MySession, ClientPacket, offset);
                    Logger.Info("Processing unreliable Rdp Report and messages");
                    break;

                default:
                    Logger.Err("Unable to identify Bundle Type");
                    break;
            }
        }

        public static void ProcessMessageBundle(Session MySession, ReadOnlySpan<byte> ClientPacket, int offset)
        {
            ///Need to consider how many messages could be in here, and message types
            ///FB/FA/40/F9
            ///
            while (ClientPacket.Length > (offset + 4))
            {
                ///Get our Message Type
                ushort MessageTypeOpcode = GrabOpcode(ClientPacket, ref offset);
                switch (MessageTypeOpcode)
                {
                    //General opcodes (0xFB, 0xF9's)
                    case MessageOpcodeTypes.ShortReliableMessage:
                    case MessageOpcodeTypes.LongReliableMessage:
                    case MessageOpcodeTypes.UnknownMessage:
                        ///Work on processing this opcode
                        ProcessOpcode.ProcessOpcodes(MySession, MessageTypeOpcode, ClientPacket, ref offset);
                        break;

                    //Client Actor update (0x4029's)
                    case UnreliableTypes.ClientActorUpdate:
                        ProcessUnreliable.ProcessUnreliables(MySession, MessageTypeOpcode, ClientPacket, ref offset);
                        break;


                    default:
                        //Shouldn't get here?
                        Console.WriteLine($"Received unknown Message: {MessageTypeOpcode}");

                        //Should we consume the whole message here if it is unknown so we can keep processing?
                        break;
                }  
            }
            MySession.RdpReport = true;
            MySession.ClientFirstConnect = true;

            //Reset ack timer
            MySession.ResetTimer();

            Logger.Info("Done processing messages in packet");
            ///Should we just initiate responses to clients through here for now?
            ///Ultimately we want to have a seperate thread with a server tick, 
            ///that may handle initiating sending messages at timed intervals, and initiating data collection such as C9's
        }

        private static void ProcessRdpReport(Session MySession, ReadOnlySpan<byte> ClientPacket, ref int offset, bool UnreliableReport)
        {
            //Eventually incorporate UnreliableReport to cycle through unreliables and update accordingly

            //Read client bundle here. Accept client bundle as is because packets could be lost or dropped, client bundle# should not be "tracked"
            //considerations could be to track it for possible drop/lost rates
            MySession.clientBundleNumber = BinaryPrimitiveWrapper.GetLEUShort(ClientPacket, ref offset);
            ushort LastRecvBundleNumber = BinaryPrimitiveWrapper.GetLEUShort(ClientPacket, ref offset);
            ushort LastRecvMessageNumber = BinaryPrimitiveWrapper.GetLEUShort(ClientPacket, ref offset);

            //Check our list  of reliable messages to remove
            for (int i = 0; i < MySession.MyMessageList.Count(); i++)
            {
                //If our message stored is less then client ack, need to remove it!
                if(MySession.MyMessageList[i-i].ThisMessagenumber <= LastRecvMessageNumber)
                { MySession.MyMessageList.RemoveAt(0); }

                //Need to keep remaining messages, move on
                else { break; }
            }

            ///Only one that really matters, should tie into our packet resender stuff
            if (MySession.serverMessageNumber >= LastRecvMessageNumber)
            {

                ///This should be removing messages from resend mechanics.
                ///Update our last known message ack'd by client
                MySession.clientRecvMessageNumber = LastRecvMessageNumber;
            }

            ///Triggers creating master session
            if (((MySession.clientEndpoint + 1) == (MySession.sessionIDBase & 0x0000FFFF)) && MySession.SessionAck && MySession.serverSelect == false)
            {
                ///Key point here for character select is (MySession.clientEndpoint + 1 == MySession.sessionIDBase)
                ///SessionIDBase is 1 more then clientEndPoint
                ///Assume it's Create Master session?
                Session NewMasterSession = new Session(MySession.clientEndpoint, MySession.MyIPEndPoint, MySession.AccountID);

                SessionManager.AddMasterSession(NewMasterSession);
            }

            ///Trigger Server Select with this?
            else if ((MySession.clientEndpoint == (MySession.sessionIDBase & 0x0000FFFF)) && MySession.SessionAck && MySession.serverSelect == false)
            {
                MySession.serverSelect = true;
            }

            ///Triggers Character select
            else if (MySession.CharacterSelect && (MySession.clientEndpoint != MySession.sessionIDBase))
            {
                MySession.CharacterSelect = false;
                List<Character> MyCharacterList = new List<Character>();
                Logger.Info("Generating Character Select");
                MyCharacterList = SQLOperations.AccountCharacters(MySession);

                //Assign to our session
                MySession.CharacterData = MyCharacterList;

                ProcessOpcode.CreateCharacterList(MyCharacterList, MySession);
            }

            else if (MySession.Dumpstarted)
            {
                //Let client know more data is coming
                if (MySession.MyDumpData.Count() > 500)
                {
                    List<byte> ThisChunk = MySession.MyDumpData.GetRange(0, 500);
                    MySession.MyDumpData.RemoveRange(0, 500);

                    ///Handles packing message into outgoing packet
                    RdpCommOut.PackMessage(MySession, ThisChunk, MessageOpcodeTypes.MultiShortReliableMessage);
                }

                //End of dump
                else
                {
                    List<byte> ThisChunk = MySession.MyDumpData.GetRange(0, MySession.MyDumpData.Count());
                    MySession.MyDumpData.Clear();
                    ///Handles packing message into outgoing packet
                    RdpCommOut.PackMessage(MySession, ThisChunk, MessageOpcodeTypes.ShortReliableMessage);
                    //turn dump off
                    MySession.Dumpstarted = false;

                    //Gather remaining data to send in dump
                    //ignore list
                    ProcessOpcode.IgnoreList(MySession);
                    //Randommessage
                    //RandomFriend
                    //characterSpeed
                    ProcessOpcode.ActorSpeed(MySession);
                    //Some opcode
                }
            }

            else
            {
                Logger.Err($"Client received server message {LastRecvMessageNumber}, expected {MySession.serverMessageNumber}");
            }
        }

        private static void ProcessSessionAck(Session MySession, ReadOnlySpan<byte> ClientPacket, ref int offset)
        {
            ///We are here
            uint SessionAck = BinaryPrimitiveWrapper.GetLEUint(ClientPacket, ref offset);
            if (SessionAck == MySession.sessionIDBase)
            {
                MySession.ClientAck = true;
                MySession.Instance = true;
                /// Trigger Character select here
                Logger.Info("Beginning Character Select creation");
            }

            else
            {
                Console.WriteLine("Error");
                Logger.Err("Error occured with Session Ack Check...");
            }
        }

        ///This grabs the full Message Type. Checks for FF, if FF is present, then grab proceeding byte (FA or FB)
        private static ushort GrabOpcode(ReadOnlySpan<byte> ClientPacket, ref int offset)
        {
            ushort Opcode = BinaryPrimitiveWrapper.GetLEByte(ClientPacket, ref offset);
            ///If Message is > 255 bytes, Message type is prefixed with FF to indeificate this
            if (Opcode == 255)
            {
                Logger.Info("Received Long Message type (> 255 bytes)");
                //Read an additional byte and flip them
                Opcode = (ushort)(BinaryPrimitiveWrapper.GetLEByte(ClientPacket, ref offset) << 8 | Opcode);

                return Opcode;
            }

            ///Message type should be < 255 bytes
            else
            {
                Logger.Info("Received Normal Message type (< 255 bytes)");
                return Opcode;
            }
        }
    }

    class RdpCommOut
    {

        ///Message processing for outbound section
        public static void PackMessage(Session MySession, List<byte> myMessage, ushort MessageOpcodeType, ushort Opcode)
        {
            ///0xFB/FA type Message type
            if ((MessageOpcodeType == MessageOpcodeTypes.ShortReliableMessage) || (MessageOpcodeType == MessageOpcodeTypes.MultiShortReliableMessage))
            {
                ///Add our opcode
                myMessage.InsertRange(0, BitConverter.GetBytes(Opcode));

                ///Add Message #
                myMessage.InsertRange(0, BitConverter.GetBytes(MySession.serverMessageNumber));

                ///Pack Message here into MySession.SessionMessages
                ///Check message length first
                if ((myMessage.Count()) > 255)
                {
                    ///Add Message Length
                    myMessage.InsertRange(0, BitConverter.GetBytes((ushort)(myMessage.Count() - 2)));

                    ///Add out MessageType
                    myMessage.InsertRange(0, BitConverter.GetBytes((ushort)(0xFF00 ^ MessageOpcodeType)));
                }

                ///Message is < 255
                else
                {
                    ///Add Message Length
                    myMessage.Insert(0, (byte)(myMessage.Count() - 2));

                    ///Add out MessageType
                    myMessage.Insert(0, (byte)MessageOpcodeType);
                }

                //Add reliable Message to reliablemessage ack list
                MySession.AddMessage(MySession.serverMessageNumber, myMessage);

                //Increment server message #
                MySession.IncrementServerMessageNumber();
            }

            ///0xFC Message type
            else if (MessageOpcodeType == MessageOpcodeTypes.ShortUnreliableMessage)
            {
                ///Add our opcode
                myMessage.InsertRange(0, BitConverter.GetBytes(Opcode));

                ///Check message length first
                if ((myMessage.Count()) > 255)
                {
                    ///Add Message Length
                    myMessage.InsertRange(0, BitConverter.GetBytes((ushort)(myMessage.Count())));

                    ///Add out MessageType
                    myMessage.InsertRange(0, BitConverter.GetBytes(0xFF00 ^ MessageOpcodeType));
                }

                ///Message is < 255
                else
                {
                    ///Add Message Length
                    myMessage.Insert(0, (byte)(myMessage.Count()));

                    ///Add out MessageType
                    myMessage.Insert(0, (byte)MessageOpcodeType);
                }
            }

            AddMessage(MySession, myMessage);
        }

        public static void PackMessage(Session MySession, List<byte> myMessage, ushort MessageOpcodeType)
        {
            //Technically shouldn't need this if statement? But to be safe
            ///0xFB/FA/F9 Message type
            if ((MessageOpcodeType == MessageOpcodeTypes.ShortReliableMessage) || (MessageOpcodeType == MessageOpcodeTypes.MultiShortReliableMessage) || (MessageOpcodeType == MessageOpcodeTypes.UnknownMessage))
            {
                ///Add Message #
                myMessage.InsertRange(0, BitConverter.GetBytes(MySession.serverMessageNumber));

                ///Pack Message here into MySession.SessionMessages
                ///Check message length first
                if ((myMessage.Count()) > 255)
                {
                    ///Add Message Length
                    ///Swap endianness, then convert to bytes
                    myMessage.InsertRange(0, BitConverter.GetBytes((ushort)(myMessage.Count() - 2)));

                    ///Add our MessageType
                    myMessage.InsertRange(0, BitConverter.GetBytes((ushort)(0xFF00 ^ MessageOpcodeType)));
                }

                ///Message is < 255
                else
                {

                    ///Add Message Length (Remove the message #)
                    myMessage.Insert(0, (byte)(myMessage.Count() - 2));

                    ///Add out MessageType
                    myMessage.Insert(0, (byte)MessageOpcodeType);
                }

                //Add reliable Message to reliablemessage ack list
                MySession.AddMessage(MySession.serverMessageNumber, myMessage);

                ///Increment our internal message #
                MySession.IncrementServerMessageNumber();
            }

            AddMessage(MySession, myMessage);
        }

        ///Message processing for outbound section
        public static void PackMessage(Session MySession, ushort MessageOpcodeType, ushort Opcode)
        {
            List<byte> myMessage = new List<byte> { };

            ///Add our opcode
            myMessage.InsertRange(0, BitConverter.GetBytes(Opcode));

            ///0xFB Message type
            if (MessageOpcodeType == MessageOpcodeTypes.ShortReliableMessage)
            {
                ///Add Message #
                myMessage.InsertRange(0, BitConverter.GetBytes(MySession.serverMessageNumber));

                //Add Message Length
                myMessage.Insert(0, 2);

                ///Add out MessageType
                myMessage.Insert(0, (byte)MessageOpcodeType);

                //Add reliable Message to reliablemessage ack list
                MySession.AddMessage(MySession.serverMessageNumber, myMessage);

                ///Increment our internal message #
                MySession.IncrementServerMessageNumber();
            }

            ///0xFC Message type
            else if (MessageOpcodeType == MessageOpcodeTypes.ShortUnreliableMessage)
            {
                //Add Message Length
                myMessage.Insert(0, 2);

                ///Add our MessageType
                myMessage.Insert(0, (byte)MessageOpcodeType);
            }

            AddMessage(MySession, myMessage);
        }

        private static void AddMessage(Session MySession, List<byte> MyMessage)
        {
            lock (MySession.SessionMessages)
            {
                MySession.SessionMessages.AddRange(MyMessage);
                ///We are packing to send a message, set MySession.RdpMessage to true

                MySession.RdpMessage = true;
            }
        }

        public static void PrepPacket(object source, ElapsedEventArgs e)
        {
            lock (SessionManager.SessionDict)
            {
                foreach (Session MySession in SessionManager.SessionDict.Values)
                {
                    if ((MySession.RdpReport || MySession.RdpMessage) && MySession.ClientFirstConnect)
                    {
                        ///If creating outgoing packet, write this data to new list to minimize writes to session
                        List<byte> OutGoingMessage = new List<byte>();

                        lock (MySession.SessionMessages)
                        {
                            ///Add our SessionMessages to this list
                            OutGoingMessage.AddRange(MySession.SessionMessages);

                            ///Clear client session Message List
                            MySession.SessionMessages.Clear();
                        }
                        Logger.Info("Packing header into packet");
                        ///Add RDPReport if applicable
                        AddRDPReport(MySession, OutGoingMessage);

                        ///Bundle needs to be incremented after every sent packet, seems like a good spot?
                        MySession.IncrementServerBundleNumber();

                        ///Add session ack here if it has not been done yet
                        ///Lets client know we acknowledge session
                        ///Making sure remoteMaster is 1 (client) makes sure we have them ack our session
                        if (!MySession.remoteMaster)
                        {
                            if (MySession.SessionAck == false)
                            {
                                ///To ack session, we just repeat session information as an ack
                                AddSession(MySession, OutGoingMessage);
                            }
                        }

                        ///Adds bundle type
                        AddBundleType(MySession, OutGoingMessage);

                        ///Get Packet Length
                        ushort PacketLength = (ushort)OutGoingMessage.Count();

                        ///Add Session Information
                        AddSession(MySession, OutGoingMessage);

                        ///Add the Session stuff here that has length built in with session stuff
                        AddSessionHeader(MySession, OutGoingMessage, PacketLength);

                        ///Done? Send to CommManagerOut
                        HandleOutPacket.AddEndPoints(MySession, OutGoingMessage);
                    }

                    ///No packet needed to respond to client
                    else
                    {
                        Logger.Info("No Packet needed to respond to last message from client");
                    }
                }
            }
        }

        ///Identifies if full RDPReport is needed or just the current bundle #
        public static void AddRDPReport(Session MySession, List<byte> OutGoingMessage)
        {
            ///If RDP Report == True, Current bundle #, Last Bundle received # and Last message received #
            if (MySession.RdpReport == true)
            {
                Logger.Info("Full RDP Report");
                /// Add them to packet in "reverse order" stated above
                ///This swaps endianness of our Message received, then converts to bytes
                OutGoingMessage.InsertRange(0, BitConverter.GetBytes(MySession.clientMessageNumber));

                ///This swaps endianness of our Bundle received, then converts to bytes
                OutGoingMessage.InsertRange(0, BitConverter.GetBytes(MySession.clientBundleNumber));

                ///This swaps endianness of our Bundle, then converts to bytes
                OutGoingMessage.InsertRange(0, BitConverter.GetBytes(MySession.serverBundleNumber));
                
                //If ingame, include 4029 ack's
                if (MySession.Channel40Ack)
                {
                    OutGoingMessage.Insert(6, 0x40);
                    OutGoingMessage.InsertRange(7, BitConverter.GetBytes(MySession.Channel40Message));
                    OutGoingMessage.Insert(9, 0xF8);
                }
            }

            ///We should only add Servers current Bundle #, only when no messages received from client
            else
            {
                Logger.Info("Partial Rdp Report (Bundle #)");
                ///This swaps endianness of our Bundle, then converts to bytes
                OutGoingMessage.InsertRange(0, BitConverter.GetBytes(MySession.serverBundleNumber));
            }
        }

        ///Add our bundle type
        ///Consideration for in world or "certain packet" # is needed during conversion. For now something basic will work
        public static void AddBundleType(Session MySession, List<byte> OutGoingMessage)
        {
            ///Should this be a big switch statement?
            ///Using if else for now
            if (MySession.BundleTypeTransition)
            {
                ///If all 3 are true
                if (MySession.Channel40Ack)
                {
                    Logger.Info("Adding Bundle Type 0x13");
                    OutGoingMessage.Insert(0, 0x13);
                    MySession.Channel40Ack = false;
                }

                ///Message only packet, no RDP Report
                else if (MySession.RdpMessage && !MySession.RdpReport && !MySession.Channel40Ack)
                {
                    Logger.Info("Adding Bundle Type 0x00");
                    OutGoingMessage.Insert(0, 0x00);
                }

                ///RDP Report only
                else if (MySession.RdpReport)
                {
                    Logger.Info("Adding Bundle Type 0x03");
                    OutGoingMessage.Insert(0, 0x03);
                }
            }

            else 
            {
                ///If Message and RDP report
                if (MySession.SessionAck == false && MySession.RdpMessage && MySession.RdpReport)
                {
                    Logger.Info("Adding Bundle Type 0x63");
                    OutGoingMessage.Insert(0, 0x63);
                    MySession.SessionAck = true;
                }

                ///Message only packet, no RDP Report
                else if (MySession.SessionAck == true && MySession.RdpMessage && MySession.RdpReport == false)
                {
                    Logger.Info("Adding Bundle Type 0x20");
                    OutGoingMessage.Insert(0, 0x20);
                }

                ///RDP Report only
                else if ((MySession.SessionAck == true && MySession.RdpMessage == false && MySession.RdpReport) || (MySession.SessionAck == true && MySession.RdpMessage && MySession.RdpReport))
                {
                    Logger.Info("Adding Bundle Type 0x23");
                    OutGoingMessage.Insert(0, 0x23);
                }
            }

            ///Reset our bools for next message to get proper Bundle Type
            MySession.Reset();

        }

        ///Add a session ack to send to client
        public static void AddSession(Session MySession, List<byte> OutGoingMessage)
        {
            Logger.Info("Adding Session Data");
            if (!MySession.remoteMaster)
            {
                ///Add first portion of session
                OutGoingMessage.InsertRange(0, BitConverter.GetBytes(MySession.sessionIDBase));
            }

            else
            {
                ///Ack session, first add 2nd portion of Session
                OutGoingMessage.InsertRange(0, Utility_Funcs.Pack(MySession.sessionIDUp));

                ///Add first portion of session
                OutGoingMessage.InsertRange(0, BitConverter.GetBytes(MySession.sessionIDBase));
            }

        }

        public static void AddSessionHeader(Session MySession, List<byte> OutGoingMessage, ushort PacketLength)
        {
            uint value = 0;
            Logger.Info("Adding Session Header");

            if (!MySession.Instance) //When server initiates instance with the client, it will use this
            {
                value |= 0x80000;
            }

            //This would be if we want to cancel the connection... Client handles this in most cases? For now.
            //if (MySession.CancelConnection) // 
            //{
            //    value += 0x10000;
            //}

            if (MySession.remoteMaster) // Purely a guess.... Something is 0x4000 in this and seems to correspond the initator of the session
            {
                value |= 0x04000;
            }

            else // Server is not master, seems to always have this when not in control
            {
                value |= 0x01000;
            }

            if(MySession.hasInstance) // Server always has instance ID, atleast untill we are in world awhile, then it can drop this and the 4 byte instance ID
            {
                value |= 0x02000;
            }

            //Add bundle length in
            value |= PacketLength;

            ///Combine, switch endianness and place into Packet
            OutGoingMessage.InsertRange(0, Utility_Funcs.Pack(value));
        }
    }
}
