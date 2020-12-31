using CRC32Calc;
using EQLogger;
using Opcodes;
using System.Net;
using SessManager;
using System;
using System.Collections.Generic;
using System.Linq;


namespace EQOAProto
{
    public class CommManager
    {
        ///private static readonly int AsyncDelay      = 200;


        /*
        Eventually become it's own class
        We will also need to create an endpoint class to 
        store each connecting clients endpoint, IP, Port
        */
        public static void ProcPacket()
        {
            Logger.Info("commManager loop started");
            ///Catch method in a loop to receive packets
            while (true)
            {

                ///If UDPServer placed data, let's process it
                if (udpServer.IncomingQueue.Count > 0)
                {
                    Logger.Info("Queue count > 0");

                    ///Remove item from queue for now
                    var MyObject = udpServer.IncomingQueue.Dequeue();

                    List<byte> myPacket = MyObject.Item2;
                    IPEndPoint MyIPEndPoint = MyObject.Item1;

                    ///List<byte> myPacket = new List<byte>(udpServer.IncomingQueue.Dequeue());
                    Logger.Info("Grabbed item from queue");

                    ///Grab destination endpoint
                    short EPDest = (short)((myPacket[3] << 8) | myPacket[2]);

                    ///Check if server transfer
                    if (MessageOpcodeTypes.serverTransfer == EPDest)
                    {
                        ///If server transfer, do server transfer stuff
                        Logger.Info("Received server Transfer");
                        break;
                    }

                    ///Is not server transfer, let's check CRC
                    else
                    {
                        Logger.Info("No Server transfer, processing");


                        ///Make byte array for CRC
                        byte[] PacketCRC = new byte[4];

                        ///Save our CRC
                        myPacket.CopyTo(myPacket.Count() - 4, PacketCRC, 0, 4);

                        ///Remove CRC off our packet
                        myPacket.RemoveRange(myPacket.Count - 4, 4);

                        byte[] CRCCheck = new byte[myPacket.Count()];

                        ///Copy our list to array for Crc check
                        myPacket.CopyTo(CRCCheck);

                        ///If CRC passes, continue
                        if (PacketCRC.SequenceEqual(CRC.calculateCRC(CRCCheck)))
                        {
                            Logger.Info("CRC Passed, processing");
                            
                            
                            SessionManager.ProcessSession(myPacket, MyIPEndPoint);
                            ///SessionManager.ProcessSession(myPacket, false);
                            
                        }

                        ///CRC Failed, drop packet
                        else
                        {
                            ///Break for now
                            ///Probably need to log for time being
                            Logger.Err("Dropping Packet, CRC failed");
                            break;
                        }
                    }
                }

                ///No data in queue
                else
                {
                    /*
                    ///If no incoming packets, sleep task
                    await Task.Delay(200);
                    */
                }

            }
        }
    }

    ///Outbound CommManager
    public class CommManagerOut
    {
        ///Final touches
        ///Adds Endpoints to front and appends CRC
        public static void AddEndPoints(Session MySession, List<byte> OutGoingMessages)
        {
            ///Insert Client Endpoint
            OutGoingMessages.InsertRange(0, BitConverter.GetBytes(MySession.clientEndpoint));

            Logger.Info("Adding Client Endpoint");

            ///Insert Server Endpoint
            ///Hard Code Server endpoint for now...
            OutGoingMessages.InsertRange(0, BitConverter.GetBytes(UDPListener.ServerEndPoint));

            Logger.Info("Adding Server Endpoint");

            Logger.Info("Calculating CRC");

            ///Append CRC now
            byte[] PacketCRC = CRC.calculateCRC(OutGoingMessages.ToArray());

            Logger.Info("Appending CRC to packet");

            ///Append CRC to our packet
            OutGoingMessages.AddRange(PacketCRC);

            Logger.Info(OutGoingMessages);

            ///Packet should be done, send to UDP Listener out function
            byte[] MyArray = OutGoingMessages.ToArray();
            UDPListener.SendPacket(MyArray, MySession.MyIPEndPoint);
        }     
    }
}
