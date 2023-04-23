using Microsoft.Win32.SafeHandles;
using System.Reflection.PortableExecutable;
using System.IO.Enumeration;
using System.IO;
using System.Security.AccessControl;
using System;
using System.IO;
using System.Threading.Tasks;
using AuthServer.Account;
using AuthServer.Server;
using Authserver.Utility;

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
