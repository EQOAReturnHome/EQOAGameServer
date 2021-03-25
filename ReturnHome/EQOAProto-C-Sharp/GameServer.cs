using ServerSelect;
using System.Threading.Channels;
using System.Threading.Tasks;
using System;
using ReturnHome.PacketProcessing;

namespace ReturnHome
{
    /*
    Keep this in main.cs
    Will also start a thread for packet processing
    */
    public class GameServer
    {
        public static void Main(string[] args)
        {
            ///Shared Channel between udpServer and commManager
            var channel = Channel.CreateUnbounded<UdpPacketStruct>();

            ///Load in config for Server Select stuff
            SelectServer.ReadConfig();

            //Consider rmeoving this and building it into outbound packet sender? Could be a simple bool/counter check
            SessionManager sessionManager = new();
            Task.Run(sessionManager.CheckClientTimeOut);

            try
            {
                HandleIncPacket handle = new(sessionManager);
                Task.Run(() => handle.AcceptPacket(channel.Reader));
            }

            catch
            {
                Console.WriteLine("PIncoming Packet Hanldler crashed");
            }
            UDPListener udpListener = new();

            GameTick gameTick = new(udpListener, sessionManager);
            Task.Run(gameTick.GameLoop);

            // Start UDP Server Last
            //Start UDPServer async
            udpListener.StartServer(channel.Writer);
            string stuff = Console.ReadLine();
        }
    }
}

