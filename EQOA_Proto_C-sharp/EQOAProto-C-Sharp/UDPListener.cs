using EQLogger;
using System;
using System.Collections.Generic;
using System.Net;
using System.Net.Sockets;
using System.Threading;
using System.Threading.Channels;
using System.Threading.Tasks;

///Create a file revolved around UDP to place class into
///
namespace EQOAProto
{
    public class UDPListener
    {
        ///For now hard code port, config file eventually
        private const int listenPort = 10070;
        public const ushort ServerEndPoint = 0x73B0;
        public static UdpClient socket = null;
        ///Instantiate our udp Listener for any ip address and specified port
        //private static UdpClient listener = new UdpClient(listenPort);
        private static IPEndPoint remEndpoint = new IPEndPoint(IPAddress.Any, listenPort);
        ManualResetEvent completed = new ManualResetEvent(false);

        public static async Task StartServer(ChannelWriter<UdpReceiveResult> channelWriter)
        {
            socket = new UdpClient(listenPort);
            while (true)
            {
                UdpReceiveResult MyUDPResult = await socket.ReceiveAsync();
                channelWriter.TryWrite(MyUDPResult);
            }
        }

        public static void SendPacket(byte[] MyData, IPEndPoint Client)
        {
            try
            {
                ///Try to send data to client
                Logger.Info($"Sending to IP: {Client.Address}, Port: {Client.Port}");
                socket.Send(MyData, MyData.Length, Client);
            }
            
            ///If a socket error occurs, udp server closes
            catch (SocketException e)
            {
                Console.WriteLine($"Received socket error: {e}");
            }
        }
    }
}
