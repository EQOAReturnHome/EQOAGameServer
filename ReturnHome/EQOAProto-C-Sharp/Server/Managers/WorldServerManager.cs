using System;
using System.Diagnostics;
using System.Threading;
using System.Threading.Tasks;

using ReturnHome.Server.Network.Managers;

namespace ReturnHome.Server.Managers
{
    public static class WorldServer
    {

        private static Stopwatch gameTimer;
        private static int serverTick = 1000 / 5;

        static WorldServer()
        {

        }

        public static void Initialize()
        {
            var thread = new Thread(() =>
                        {
                            UpdateWorld();
                        });
            thread.Name = "World Manager";
            thread.Priority = ThreadPriority.AboveNormal;
            thread.Start();
        }

        public async static void UpdateWorld()
        {
            gameTimer = new Stopwatch();

            while (true)
            {
                gameTimer.Restart();

                SessionManager.BeginSessionWork();

                gameTimer.Stop();

                if (gameTimer.ElapsedMilliseconds > serverTick)
                {
                    Console.WriteLine("Server can't keep up");
                }

                await Task.Delay(Math.Max(0, serverTick - (int)gameTimer.ElapsedMilliseconds));
            }
        }
    }
}
