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
        public static Socket socket = null;
        private static byte[] ReceiveBuffer = new byte[400];
        public const int SIO_UDP_CONNRESET = -1744830452;
        ///Instantiate our udp Listener for any ip address and specified port

        public static async Task StartServer(ChannelWriter<Tuple<byte[], IPEndPoint>> channelWriter)
        {
            socket = new Socket(AddressFamily.InterNetwork, SocketType.Dgram, ProtocolType.Udp);

            //Without this, when connections close ICMP's seem to cause forced close errors: https://stackoverflow.com/questions/38191968/c-sharp-udp-an-existing-connection-was-forcibly-closed-by-the-remote-host
            socket.IOControl((IOControlCode)SIO_UDP_CONNRESET, new byte[] { 0, 0, 0, 0 }, null);
            
            socket.SetSocketOption(SocketOptionLevel.Socket, SocketOptionName.ReuseAddress, true);
            socket.Bind(new IPEndPoint(IPAddress.Any, listenPort));
            EndPoint newClientEP = new IPEndPoint(IPAddress.Any, 0);
            socket.BeginReceiveFrom(ReceiveBuffer, 0, ReceiveBuffer.Length, SocketFlags.None, ref newClientEP,(iar) => DoReceiveFrom(iar, channelWriter), newClientEP);
        }

        private static void DoReceiveFrom(IAsyncResult iar, ChannelWriter<Tuple<byte[], IPEndPoint>> channelWriter)
        {
            EndPoint clientEP = new IPEndPoint(IPAddress.Any, 0);
            int dataLen = 0;
            byte[] data = null;
            try
            {
                dataLen = socket.EndReceiveFrom(iar, ref clientEP);
                //Console.WriteLine($"Received {dataLen} from {(IPEndPoint)clientEP}.");
                data = new byte[dataLen];
                Array.Copy(ReceiveBuffer, data, dataLen);
                //Place into Channel
                channelWriter.TryWrite(Tuple.Create(data, (IPEndPoint)clientEP));
            }

            finally
            {
                EndPoint newClientEP = new IPEndPoint(IPAddress.Any, 0);
                socket.BeginReceiveFrom(ReceiveBuffer, 0, ReceiveBuffer.Length, SocketFlags.None, ref newClientEP, (iar) => DoReceiveFrom(iar, channelWriter), newClientEP);
            }
        }

        public static void SendPacket(byte[] MyData, IPEndPoint Client)
        {
            try
            {
                ///Try to send data to client
                Logger.Info($"Sending to IP: {Client.Address}, Port: {Client.Port}");
                socket.BeginSendTo(MyData, 0, MyData.Length, SocketFlags.None, Client, new AsyncCallback(SendData), null);
            }
            
            catch (SocketException e)
            {
                Console.WriteLine($"Received socket error: {e}");
            }
        }

        private static void SendData(IAsyncResult ar)
        {
            socket.EndSend(ar);
        }
    }
}
