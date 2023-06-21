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
using ReturnHome.Server.Items;

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

            Console.WriteLine("Collecting Mob Patterns...");
            List<MobPattern> myMobPatterns = npcList.GetMobPatterns();



            Console.WriteLine("Adding Mob Patterns...");
            for (int i = 0; i < myMobPatterns.Count; ++i)
                EntityManager.mobPatterns.Add(myMobPatterns[i]);

            Console.WriteLine("Total Item Pattern's Acquired: " + myItemPatterns.Count);
            Console.WriteLine("Adding Item Patterns...");
            for (int i = 0; i < myItemPatterns.Count; ++i)
                ItemManager.AddItem(myItemPatterns[i]);

            Console.WriteLine("Collecting Loot Tables...");
            List<LootTable> myLootTables = npcList.GetLootTables();

            Console.WriteLine("Adding Loot Tables...");
            for (int i = 0; i < myLootTables.Count; ++i)
            {
                ItemManager.lootTables.Add(myLootTables[i]);
                Console.WriteLine($"Adding loot table {ItemManager.lootTables[i]._itemid}");
            }

            Console.WriteLine("Adding Spell Patterns...");
            for (int i = 0; i < mySpellPatterns.Count; ++i)
                SpellManager.AddSpell(mySpellPatterns[i]);

            Console.WriteLine("Collecting Actors...");
            //Calls sql query function that fills list full of NPCs
            List<Actor> myNpcList = npcList.WorldActors();

            MapManager.Initialize();

            Console.WriteLine("Adding Spawn Points...");
            List<SpawnPoint> points = npcList.GetSpawnPoints();
            foreach (SpawnPoint sp in points)
            {
                EntityManager.spawnPoints.Add(sp);
            }
            Console.WriteLine("Done.");

            Console.WriteLine("Adding Spawn Groups...");
            List<SpawnGroup> groups = npcList.GetSpawnGroups();
            foreach (SpawnGroup group in groups)
            {
                EntityManager.spawnGroups.Add(group);
            }
            Console.WriteLine("Done.");

            Console.WriteLine("Adding Spawn Entries...");
            List<SpawnEntry> entries = npcList.GetSpawnEntries();
            foreach (SpawnEntry entry in entries)
            {
                EntityManager.spawnEntries.Add(entry);
            }
            Console.WriteLine("Done.");

            //Only used for pulling NPCs and shoving them into the spawn points table.
            //npcList.UpdateNPCs();

            npcList.CloseConnection();

            //Loops through each npc in list and sets their position, adds them to the entity manager, and mapmanager
            Console.WriteLine("Adding NPCs...");
            foreach (Actor myActor in myNpcList)
            {
                EntityManager.AddEntity(myActor);
                MapManager.Add(myActor);
            }

            foreach (SpawnPoint sp in EntityManager.spawnPoints)
            {
                EntityManager.SpawnMob(sp._pointID);
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
                                if (entityList[i].EntityStatusEffects[j].duration > 0)
                                {
                                    if ((DateTimeOffset.UtcNow.ToUnixTimeSeconds() >= entityList[i].EntityStatusEffects[j].lastTick + 6))
                                    {
                                        Console.WriteLine(entityList[i].CharName);
                                            Spell.TickEffect(entityList[i], entityList[i].EntityStatusEffects[j]);
                                            entityList[i].EntityStatusEffects[j].lastTick = DateTimeOffset.UtcNow.ToUnixTimeSeconds();
                                            entityList[i].EntityStatusEffects[j].duration -= globalTick;
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

