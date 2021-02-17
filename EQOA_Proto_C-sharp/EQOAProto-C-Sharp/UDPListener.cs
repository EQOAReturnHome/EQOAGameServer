using EQLogger;
using System;
using System.Collections.Generic;
using System.Net;
using System.Net.Sockets;
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

        ///Instantiate our udp Listener for any ip address and specified port
        private static UdpClient listener = new UdpClient(listenPort);
        private static IPEndPoint remEndpoint = new IPEndPoint(IPAddress.Any, listenPort);

        public static void StartListener(ChannelWriter<UdpReceiveResult> channelWriter)
        {
            ///Instantiate our udp Listener for any ip address and specified port
            Logger.Info($"Listening on {remEndpoint}...");

            _ = ReceiveAsync(channelWriter);
        }

        private static async ValueTask ReceiveAsync(ChannelWriter<UdpReceiveResult> channelWriter)
        { 
            while (true)
            {
                UdpReceiveResult result = await listener.ReceiveAsync();
                channelWriter.TryWrite(result);
                channelWriter.Complete();
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
