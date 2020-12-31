using EQLogger;
using System;
using System.Collections.Generic;
using System.Net;
using System.Net.Sockets;

///Create a file revolved around UDP to place class into
///
namespace EQOAProto
{
    public class UDPListener
    {
        ///For now hard code port, config file eventually
        private const int listenPort = 10070;
        public const ushort ServerEndPoint = 0x73B0;

        ///Instantiate our udp Listener for any ip address and specified port
        private static UdpClient listener = new UdpClient(listenPort);
        private static IPEndPoint remEndpoint = new IPEndPoint(IPAddress.Any, listenPort);

        public static void StartListener(Queue<(IPEndPoint, List<byte>)> IncomingQueue)
        {
            ///Instantiate our udp Listener for any ip address and specified port
            Logger.Info($"Listening on {remEndpoint}...");

            ///For now catch incoming packets within a try statement
            try
            {
                ///Check for incoming packets within the While loop, helps to receive multiple packets
                while (true)
                {
                    ///UdpClient listener = new UdpClient();
                    ///Once a packet comes in, saved to bytes variable and immediately placed into queue
                    List<byte> MyPacket = new List<byte>(listener.Receive(ref remEndpoint));

                    IncomingQueue.Enqueue((remEndpoint, MyPacket));
                    ///IncomingQueue.Enqueue(MyPacket);

                    //Console.WriteLine($"Received broadcast from {remEndpoint} :");
                    //Console.WriteLine($" {Encoding.ASCII.GetString(bytes, 0, bytes.Length)}");
                }
            }

            ///If a socket error occurs, udp server closes
            catch (SocketException e)
            {
                Console.WriteLine($"Received socket error: {e}");
            }

            finally
            {
                listener.Close();
            }
        }

        public static void SendPacket(byte[] MyData, IPEndPoint Client)
        {
            try
            {
                ///Try to send data to client
                Logger.Info($"Sending to IP: {Client.Address}, Port: {Client.Port}");
                listener.Send(MyData, MyData.Length, Client);
            }
            
            ///If a socket error occurs, udp server closes
            catch (SocketException e)
            {
                Console.WriteLine($"Received socket error: {e}");
            }

        }
    }
}
