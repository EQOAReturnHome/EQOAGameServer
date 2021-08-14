using System;
using System.Timers;
using ReturnHome.Server.Managers;
using ReturnHome.Server.Network.Managers;

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
			//Load config for ServerListManager and start it
			ServerListManager.ReadConfig(); 
			Timer t = new(5000);
            t.AutoReset = true;
            t.Elapsed += new ElapsedEventHandler((_, _) => ServerListManager.DistributeServerList());
            t.Start();
			
			//Load GameManager
			
			
            // Start SocketManager
			SocketManager.Initialize();
			
            string stuff = Console.ReadLine();
        }
    }
}

