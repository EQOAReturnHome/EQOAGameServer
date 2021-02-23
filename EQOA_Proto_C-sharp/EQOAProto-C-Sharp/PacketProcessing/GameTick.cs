using System;
using System.Diagnostics;
using System.Threading;
using RdpComm;

namespace EQOAProto
{
    class OurGameServer
    {
        private static Stopwatch gameTimer;
        private static int serverTick = 1000 / 10;

        public static void GameLoop()
        {
            gameTimer = new Stopwatch();

            while (true)
            {
                gameTimer.Restart();
                
                ///Loop over Sessions and send updates if available
                RdpCommOut.PrepPacket();

                gameTimer.Stop();

                if (gameTimer.ElapsedMilliseconds > serverTick)
                    Console.WriteLine("Server can't keep up");

                Thread.Sleep(Math.Max(0, serverTick - (int)gameTimer.ElapsedMilliseconds));
            }
        }

    }
}
