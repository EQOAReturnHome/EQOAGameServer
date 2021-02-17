using CRC32Calc;
using EQLogger;
using Opcodes;
using System.Net;
using SessManager;
using System;
using System.Collections.Generic;
using System.Linq;
using Sessions;
using System.Net.Sockets;
using System.Threading.Channels;
using System.Threading.Tasks;

namespace EQOAProto
{
    public class HandleIncPacket
    {
        ///private static readonly int AsyncDelay      = 200;


        /*
        Eventually become it's own class
        We will also need to create an endpoint class to 
        store each connecting clients endpoint, IP, Port
        */
        public static async Task AcceptPacket(ChannelReader<UdpReceiveResult> ChannelReader)
        {
            while (await ChannelReader.WaitToReadAsync())
                while (ChannelReader.TryRead(out UdpReceiveResult item))
                    ProcPacket(item);
        }

        public static void ProcPacket(UdpReceiveResult MyObject)
        {
            int offset = 0;
            ReadOnlySpan<byte> ClientPacket = MyObject.Buffer.AsSpan();
            
            ///List<byte> myPacket = new List<byte>(udpServer.IncomingQueue.Dequeue());
            Logger.Info("Grabbed item from queue");

            ///Grab Client Endpoint
            ushort ClientEndpoint = BinaryPrimitiveWrapper.GetBEUShort(ClientPacket, ref offset);

            ///Grab destination endpoint
            ushort EPDest = BinaryPrimitiveWrapper.GetBEUShort(ClientPacket, ref offset);
                
            ///Check if server transfer
            if (MessageOpcodeTypes.serverTransfer == EPDest)
            {
                ///If server transfer, do server transfer stuff
                Logger.Info("Received server Transfer");
                return;
            }

            ///Is not server transfer, let's check CRC
            else
            {
                Logger.Info("No Server transfer, processing");

                ///If CRC passes, continue
                if (ClientPacket[(ClientPacket.Length-4)..ClientPacket.Length].SequenceEqual(CRC.calculateCRC(ClientPacket[0..(ClientPacket.Length - 4)])))
                {
                    Logger.Info("CRC Passed, processing");

                    SessionManager.ProcessSession(ClientPacket, offset, MyObject.RemoteEndPoint, ClientEndpoint);
                    ///SessionManager.ProcessSession(myPacket, false);
                }

                ///CRC Failed, drop packet
                else
                {
                    ///Break for now
                    ///Probably need to log for time being
                    Logger.Err("Dropping Packet, CRC failed");
                    return;
                }
            }
        }
    }

    ///Outbound CommManager
    public class HandleOutPacket
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
