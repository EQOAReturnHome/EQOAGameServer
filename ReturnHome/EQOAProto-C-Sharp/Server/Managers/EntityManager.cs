using System;
using System.Collections.Generic;
using ReturnHome.Server.EntityObject;
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
                entity.canDespawn = true;
                entityList.RemoveAll(s => s.ObjectID == entity.ObjectID);
                entity = null;
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

        //TODO: Later this maybe should be folded into the state machine, or probably folded in to the shard server rewrite
        //As we don't need to query every entity for every zone nonstop when the zone is unoccupied, etc
        public static List<Entity> QueryForAllEntitys()
        {
            return entityList;
        }
    }
}
