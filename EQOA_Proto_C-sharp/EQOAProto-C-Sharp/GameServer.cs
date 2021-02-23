using ServerSelect;
using System.Net;
using System.Threading.Channels;
using System.Threading.Tasks;
using System;
using System.Timers;

namespace EQOAProto
{
    /*
    Keep this in main.cs
    Will also start a thread for packet processing
    */
    public class GameServer
    {

        public static Timer OutBoundTimer = new Timer();

        public static async Task Main(string[] args)
        {
            ///Shared Channel between udpServer and commManager
            var channel = Channel.CreateUnbounded<Tuple<byte[], IPEndPoint>>();

            ///Load in config for Server Select stuff
            SelectServer.ReadConfig();

            //Start UDPServer async
            UDPListener.StartServer(channel.Writer);

            Task.Run(async() => HandleIncPacket.AcceptPacket(channel.Reader));

            OurGameServer.GameLoop();
            string stuff = Console.ReadLine();
        }
    }
}

