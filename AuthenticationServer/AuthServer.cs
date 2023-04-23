using System.IO;
using System;
using System.Threading.Tasks;
using AuthServer.Account;
using AuthServer.Server;
using AuthServer.Utility;

namespace AuthServer
{
    class AuthServer
    {
        static async Task Main(string[] args)
        {
            var root = Directory.GetCurrentDirectory();
            var dotenv = Path.Combine(root, ".env");
            DotEnv.Load(dotenv);

            EQOAClientTracker eqoaClientTracker = new();
            //Start TCP Server

            AccountManagement.Initialize();

            AsynchronousSocketListener TcpServer = new(eqoaClientTracker);
            await TcpServer.StartListener();

            string stuff = Console.ReadLine();
        }
    }
}
