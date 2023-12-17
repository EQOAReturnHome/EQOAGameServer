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
using System.Drawing.Printing;

namespace ReturnHome.Server.Managers
{
    public static class WorldServer
    {

        private static Stopwatch gameTimer;
        private static int serverTick = 1000 / 10;
        public static SemaphoreSlim Sema = new SemaphoreSlim(1);
        public static long lastTick = DateTimeOffset.UtcNow.ToUnixTimeMilliseconds();
        public static long otTick = DateTimeOffset.UtcNow.ToUnixTimeMilliseconds();




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

                uint globalTick = 6000;
                uint attackTick = 3000;

                //Iterate through every entity
                for (int i = 0; i < entityList.Count; i++)
                {
                    //if the entity is not a player do non player checks
                    if (!entityList[i].isPlayer)
                    {
                        //If the entity has anything in its aggro table evaluate. Entities don't have aggroTables so cast to actor
                        if (((Actor)entityList[i]).aggroTable.Count > 0)
                        {
                            //If the current time is greater than their last attack time plus server tick or unset Evaluate table
                            //This may not be completely correct as we want to constantly evaluate aggro I think but only attack on cadence
                            if (DateTimeOffset.UtcNow.ToUnixTimeMilliseconds() >= ((Actor)entityList[i]).lastAtkTick + attackTick || ((Actor)entityList[i]).lastAtkTick == 0)
                            {
                                ((Actor)entityList[i]).EvaluateAggroTable();
                            }
                        }

                        //Respawns entity if they are dead and the current time is greater than their kill time plus respawn time
                        if (entityList[i].Dead is true && !((Actor)entityList[i]).corpse.CheckLoot() && DateTimeOffset.UtcNow.ToUnixTimeMilliseconds() >= (entityList[i]._killTime + entityList[i]._respawnTime))
                        {

                            //Set respawn boolean to true then reset the actor
                            entityList[i].respawn = true;
                            Respawn.ResetActor((Actor)entityList[i]);
                        }
                    }


                    //This section is for any entity type player or enemy
                    //Just for effects that need to be checked every 6s tick. Might be able to simplify effect check a bit nesting better
                    if (DateTimeOffset.UtcNow.ToUnixTimeMilliseconds() >= otTick + globalTick)
                    {
                        //lastTick = DateTimeOffset.UtcNow.ToUnixTimeSeconds();
                        otTick = DateTimeOffset.UtcNow.ToUnixTimeMilliseconds();
                        Console.WriteLine("Ticking " + DateTimeOffset.UtcNow.ToUnixTimeMilliseconds());

                        //if (entityList[i].CurrentHP > 0 && entityList[i].CurrentHP < entityList[i].GetMaxHP())
                        //{

                            entityList[i].CurrentHP += 1 + (entityList[i].GetMaxHP() / 50);
                            Console.WriteLine($"Ticking HoT {entityList[i].CharName}");
                       // }

                        if (entityList[i].CurrentPower > 0 && entityList[i].CurrentPower < entityList[i].GetMaxMP())
                        {

                            entityList[i].CurrentPower += 1 + (entityList[i].GetMaxMP() / 50);
                            Console.WriteLine($"Ticking PoT {entityList[i].CurrentPower}");
                        }
                    }


                    //If entity effects list is greater than 0 then try to iterate it
                    if (entityList[i].EntityStatusEffects.Count > 0)
                    {
                        //ticks the server every 6 seconds
                        if (DateTimeOffset.UtcNow.ToUnixTimeMilliseconds() >= lastTick + globalTick)
                        {
                            //updates the last tick to be now
                            lastTick = DateTimeOffset.UtcNow.ToUnixTimeMilliseconds();

                            //Iterates through each entities effects
                            Console.WriteLine(entityList[i].EntityStatusEffects.Count);
                            for (int j = 0; j < entityList[i].EntityStatusEffects.Count; j++)
                            {

                                if (DateTimeOffset.UtcNow.ToUnixTimeMilliseconds() >= entityList[i].EntityStatusEffects[j].castTime + entityList[i].EntityStatusEffects[j].duration)
                                {
                                    entityList[i].RemoveStatusEffect(entityList[i].EntityStatusEffects[j].name);
                                    SpellManager.OnEffectEnd(entityList[i]);
                                    break;
                                }

                                if (entityList[i].EntityStatusEffects[j].effectType == 1)
                                {
                                    Spell.TickEffect(entityList[i], entityList[i].EntityStatusEffects[j]);

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

