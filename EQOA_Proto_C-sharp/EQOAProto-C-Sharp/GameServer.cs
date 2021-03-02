using ServerSelect;
using System.Net;
using System.Threading.Channels;
using System.Threading.Tasks;
using System;
using SessManager;
using System.Threading;

namespace EQOAProto
{
    /*
    Keep this in main.cs
    Will also start a thread for packet processing
    */
    public class GameServer
    {
        public static async Task Main(string[] args)
        {
            ///Shared Channel between udpServer and commManager
            var channel = Channel.CreateUnbounded<UdpPacketStruct>();

            ///Load in config for Server Select stuff
            SelectServer.ReadConfig();

            SessionManager sessionManager = new();

            HandleIncPacket handle = new(sessionManager);
            Thread th = new Thread((_) => handle.AcceptPacket(channel.Reader));
            th.Start();

            UDPListener udpListener = new();

            GameTick gameTick = new(udpListener, sessionManager);
            Thread th2 = new Thread(gameTick.GameLoop);
            th2.Start();

            // Start UDP Server Last
            //Start UDPServer async
            udpListener.StartServer(channel.Writer);
            string stuff = Console.ReadLine();
        }
    }
}

