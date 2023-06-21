using System;
using System.Collections.Generic;
using System.Net.NetworkInformation;
using ReturnHome.Server.EntityObject;
using ReturnHome.Server.EntityObject.Actors;
using ReturnHome.Server.EntityObject.Items;
using ReturnHome.Server.EntityObject.Player;
using ReturnHome.Server.EntityObject.Spells;
using ReturnHome.Server.Network;

namespace ReturnHome.Server.Managers
{
    public static class EntityManager
    {
        private static List<Entity> entityList = new();
        private static List<Item> itemList = new();
        public static List<MobPattern> mobPatterns = new();
        public static List<SpawnGroup> spawnGroups = new();
        public static List<SpawnPoint> spawnPoints = new();
        public static List<SpawnEntry> spawnEntries = new();
        private static ObjectIDCreator _idCreator;

        static EntityManager()
        {
            //Create an ObjectID Creator Instance for NPC's
            _idCreator = new ObjectIDCreator(true);
        }
        public static bool AddEntity(Entity entity)
        {
            if (!entity.isPlayer)
            {
                _idCreator.GenerateID(entity, out uint ObjectID);
                entity.ObjectID = ObjectID;
            }

            else
            {
                if (!_idCreator.AddEntity(entity))
                    return false;

            }

            //Add entity to our tracking List
            if (entityList.Contains(entity))
                //Return false here? Boot in world entity and load new one?
                return false;

            entityList.Add(entity);
            return true;
        }

        public static bool RemoveEntity(Entity entity)
        {
            if (!entityList.Contains(entity))
            {
                Console.WriteLine("Entity not found in list");
                return false;
            }
            else
            {
                Console.WriteLine("Entity found in list");
                entity.despawn = true;
                return true;
            }
        }

        public static bool QueryForEntity(uint ObjectID, out Entity e)
        {
            if (_idCreator.QueryEntity(ObjectID, out Entity ent))
            {
                e = ent;
                return true;
            }

            e = default;
            return false;
        }

        public static bool QueryForEntityByServerID(uint ServerID, out Entity e)
        {
            if (_idCreator.QueryEntity(ServerID, out Entity ent))
            {
                e = ent;
                return true;

            }
            else
            {

                e = default;
                return false;
            }
        }

        public static bool QueryForEntity(string name, out Entity c)
        {
            foreach (Entity c2 in entityList)
            {
                if (c2.CharName == name)
                {
                    c = c2;
                    return true;
                }
            }
            c = default;
            return false;
        }

        public static Entity QueryForEntityLua(uint ObjectID)
        {
            Console.WriteLine("Trying");
            if (_idCreator.QueryEntity(ObjectID, out Entity ent))
            {
                return ent;
            }
            Console.WriteLine($"Entity ID is {((Actor)ent).CharName}");
            return ent;
        }

        //TODO: Later this maybe should be folded into the state machine, or probably folded in to the shard server rewrite
        //As we don't need to query every entity for every zone nonstop when the zone is unoccupied, etc
        public static List<Entity> QueryForAllEntitys()
        {
            return entityList;
        }

        public static void SpawnMob(int spawnPointID)
        {
            //Find SpawnPoint based on SpawnPoint ID
            SpawnPoint point = spawnPoints.Find(sp => sp._pointID == spawnPointID);
            //Find spawn group that belongs to the spawn point
            SpawnGroup group = spawnGroups.Find(g => g._groupID == point._groupID);
            if(group._groupID == 0)
            {
                return;
            }
            //Find the spawn entries that are part of that spawn group
            List<SpawnEntry> entries = spawnEntries.FindAll(e => e._spawnGroupID == group._groupID);
            

            //define totalWeight for randomizing spawner
            int totalWeight = 0;
            int mobID = 0;

            //Generate new instance of random
            Random _rnd = new Random();

            //For every entry in spawnentries for that group, total up the combined weights
            foreach (SpawnEntry entry in entries)
            {
                totalWeight += entry._chance;
            }

            //Find a random number between 0 and the total combined weight for the group
            int randomNumber = _rnd.Next(0, totalWeight);

            //For each entry in spawn entries for that group, determine which one is the one to spawn.
            //based on the randomized weight.
            foreach (SpawnEntry entry in entries)
            {
                if (randomNumber < entry._chance)
                {
                    //if the random number is smaller than the entries chance, select this for the mob to spawn
                    mobID = entry._npcID;
                    //break out out of the loop if an npc is chosen
                    break;
                }
                //deduct the entries chance from the random number every loop until one is chosen
                randomNumber = randomNumber - entry._chance;
            }

            //Find which actor needs to be updated based on the spawn point ID
            foreach (Actor a in entityList)
            {
                //when you find the matching actor, update the actors attributes
                if (a.spawnPointID == spawnPointID)
                {
                    RespawnActor(mobID, a);
                    return;
                }
            }
            SpawnActor(mobID, point);
        }

        public static void SpawnActor(int mobID, SpawnPoint point)
        {
            //Get pattern based on select mob spawn
            MobPattern mp = mobPatterns.Find(p => p._mobPatternID == mobID);
            //Create new random
            Random _rnd = new Random();
            //Randomize to be spawned mobs level
            int randLevel = _rnd.Next(mp._minLevel, mp._maxLevel);

            Actor newActor = new(mp.GetMobName(), point._x, point._y, point._z, point._facing, point._world, mp._modelID, mp._mobSize, 0, 0, 0, mp._hair_color,
                mp._hair_length, mp._hair_style, randLevel, 0, 0, 0, 0, 0, 0, 0, 0);
            newActor.lootTableID = mp._lootTableID;
            newActor.spawnPointID = point._pointID;
            AddEntity(newActor);
            MapManager.Add(newActor);
            Console.WriteLine(newActor.CharName);


        }

        public static void RespawnActor(int mobID, Actor a)
        {
            MobPattern mp = mobPatterns.Find(p => p._mobPatternID == mobID);

            Random _rnd = new Random();

            int randLevel = _rnd.Next(mp._minLevel, mp._maxLevel);

            a.lootTableID = mp._lootTableID;
            a.CharName = mp.GetMobName();
            a.Level = randLevel;
            a.ModelID = mp._modelID;

            Console.WriteLine($"Updating actor respawn - {mp.GetMobName()}");


        }
    }
}

