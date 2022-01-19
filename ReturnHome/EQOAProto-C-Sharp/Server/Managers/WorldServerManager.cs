using System;
using System.Diagnostics;
using System.Threading;
using System.Threading.Tasks;
using System.Collections.Generic;
using ReturnHome.Database.SQL;
using ReturnHome.Server.EntityObject.Actor;
using ReturnHome.Server.EntityObject;
using QuadTrees;

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

            //Creates NPC List
            CharacterSQL npcList = new();
            //Calls sql query function that fills list full of NPCs
            List<Actor> myNpcList = npcList.WorldActors();
            //Loops through each npc in list and sets their position, adds them to the entity manager, and mapmanager
            Console.WriteLine("Adding NPCs...");
            uint objectID = 0;
            foreach (Actor myActor in myNpcList)
            {
                
                
                myActor.SetPosition();
                EntityManager.AddEntity(myActor);
                MapManager.AddObjectToTree(myActor);
            }
            Console.WriteLine("Done.");
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
