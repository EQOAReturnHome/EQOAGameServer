using System;
using System.Diagnostics;
using System.Threading;
using System.Threading.Tasks;
using System.Collections.Generic;

using ReturnHome.Database.SQL;
using ReturnHome.Server.EntityObject.Actors;
using ReturnHome.Server.Network.Managers;
using ReturnHome.Server.EntityObject.Items;
using ReturnHome.Server.EntityObject.Spells;
using ReturnHome.Server.EntityObject;

namespace ReturnHome.Server.Managers
{
    public static class WorldServer
    {

        private static Stopwatch gameTimer;
        private static int serverTick = 1000 / 10;
        public static SemaphoreSlim Sema = new SemaphoreSlim(1);


        static WorldServer()
        {

        }

        public static void Initialize()
        {
            //Creates NPC List
            Console.WriteLine("Collecting Item Patterns...");
            CharacterSQL npcList = new();

            List<ItemPattern> myItemPatterns = npcList.ItemPatterns();

            Console.WriteLine("Collecting Spell Patterns...");
            List<SpellPattern> mySpellPatterns = npcList.SpellPatterns();


            npcList.CloseConnection();
            Console.WriteLine("Total Item Pattern's Acquired: " + myItemPatterns.Count);
            Console.WriteLine("Adding Item Patterns...");
            for (int i = 0; i < myItemPatterns.Count; ++i)
                ItemManager.AddItem(myItemPatterns[i]);


            Console.WriteLine("Adding Spell Patterns...");
            for (int i = 0; i < mySpellPatterns.Count; ++i)
                SpellManager.AddSpell(mySpellPatterns[i]);

            npcList = new();
            Console.WriteLine("Collecting Actors...");
            //Calls sql query function that fills list full of NPCs
            List<Actor> myNpcList = npcList.WorldActors();

            //Closing DB connection
            npcList.CloseConnection();
            MapManager.Initialize();

            Console.WriteLine("Done.");
            //Loops through each npc in list and sets their position, adds them to the entity manager, and mapmanager
            Console.WriteLine("Adding NPCs...");

            foreach (Actor myActor in myNpcList)
            {
                EntityManager.AddEntity(myActor);
                MapManager.Add(myActor);

            }

            Console.WriteLine("Done.");
            Console.WriteLine("Getting itemID seed.");
            CharacterSQL itemIDs = new();
            itemIDs.GetMaxItemID();
            itemIDs.CloseConnection();

            Console.WriteLine("Loading Default character options");
            CharacterSQL LoadDefaultCharacters = new();
            LoadDefaultCharacters.CollectDefaultCharacters();
            LoadDefaultCharacters.CloseConnection();


            Console.WriteLine("Done...");
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

                await Sema.WaitAsync();

                SessionManager.BeginSessionWork();

                Sema.Release();

                gameTimer.Stop();

                if (gameTimer.ElapsedMilliseconds > serverTick)
                {
                    Console.WriteLine("Server can't keep up");
                }

                List<Entity> entityList = EntityManager.QueryForAllEntitys();

                uint globalTick = 6;
                uint attackTick = 3;
                //Console.WriteLine(entityList.Count);
                for (int i = 0; i < entityList.Count; i++)
                {
                    if (!entityList[i].isPlayer)
                    {
                        if (((Actor)entityList[i]).aggroTable.Count > 0)
                        {
                            if (DateTimeOffset.UtcNow.ToUnixTimeSeconds() >= ((Actor)entityList[i]).lastAtkTick + attackTick || ((Actor)entityList[i]).lastAtkTick == 0)
                            {
                                ((Actor)entityList[i]).EvaluateAggroTable();
                            }
                        }
                    }

                    if (entityList[i].Dead is true && DateTimeOffset.UtcNow.ToUnixTimeSeconds() >= (entityList[i]._killTime + entityList[i]._respawnTime))
                    {
                        entityList[i].respawn = true;
                        Respawn.ResetActor((Actor)entityList[i]);
                    }


                    if (entityList[i].EntityStatusEffects.Count > 0)
                    {
                        for (int j = 0; j < entityList[i].EntityStatusEffects.Count; j++)
                        {
                            {
                                //Console.WriteLine(effect.name);
                                if (entityList[i].EntityStatusEffects[j].duration > 0)
                                {
                                    if ((DateTimeOffset.UtcNow.ToUnixTimeSeconds() >= entityList[i].EntityStatusEffects[j].lastTick + 6))
                                    {
                                        Console.WriteLine(entityList[i].EntityStatusEffects[j].lastTick);
                                        if (entityList[i].isPlayer)
                                        {
                                            //entityList[i].MySpellBook.TickEffect(((Character)entityList[i]).characterSession, entityList[i].EntityStatusEffects[j]);
                                            entityList[i].MySpellBook.TickEffect(entityList[i].EntityStatusEffects[j]);
                                            entityList[i].EntityStatusEffects[j].lastTick = DateTimeOffset.UtcNow.ToUnixTimeSeconds();
                                            entityList[i].EntityStatusEffects[j].duration -= globalTick;
                                        }

                                    }
                                }
                                else
                                {
                                    entityList[i].RemoveStatusEffect(entityList[i].EntityStatusEffects[j].name);
                                }
                            }
                        }
                    }
                }
                await Task.Delay(Math.Max(0, serverTick - (int)gameTimer.ElapsedMilliseconds));
            }
        }
    }
}

