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
        private static int serverTick = 1000 / 10;

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

        /*
        private async Task AddCharacters()
        {
            //Listen for new incoming connecting characters
            while (await _chanReader.WaitToReadAsync())
                while (_chanReader.TryRead(out Character item))
                {
                    _playerList.Add(item);
                    Console.WriteLine($"{item.CharName} added to Player List");
                }
        }

        public void CreateObjectUpdates()
        {
            foreach (Character i in _playerList)
            {
                i.DistributeUpdates();
            }
        }
        */

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
