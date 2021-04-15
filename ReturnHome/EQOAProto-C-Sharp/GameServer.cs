using System.Threading.Channels;
using System.Threading.Tasks;
using System;
using ReturnHome.PacketProcessing;
using ReturnHome.Actor;

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
            var Charchannel = Channel.CreateUnbounded<Character>();

            //Consider rmeoving this and building it into outbound packet sender? Could be a simple bool/counter check
            SessionManager sessionManager = new(Charchannel.Writer);
            Task.Run(sessionManager.CheckClientTimeOut);

            HandleIncPacket handle = new(sessionManager);
            Task.Run(() => handle.AcceptPacket(channel.Reader));
            UDPListener udpListener = new();
            WorldServer worldServer = new(Charchannel.Reader);
            GameTick gameTick = new(udpListener, sessionManager, worldServer);
            Task.Run(gameTick.GameLoop);

            // Start UDP Server Last
            //Start UDPServer async
            udpListener.StartServer(channel.Writer);
            string stuff = Console.ReadLine();
        }
    }
}

