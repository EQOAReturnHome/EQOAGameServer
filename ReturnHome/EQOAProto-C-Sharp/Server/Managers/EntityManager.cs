using System.Collections.Generic;
using ReturnHome.Server.EntityObject;

namespace ReturnHome.Server.Managers
{
    public static class EntityManager
    {
        private static List<Entity> entityList = new();

        public static bool AddEntity(Entity entity)
        {
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
                return false;
            entityList.Remove(entity);
            return true;   
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

        public static List<Entity> QueryForAllEntitys()
        {
            return entityList;
        }
    }
}
