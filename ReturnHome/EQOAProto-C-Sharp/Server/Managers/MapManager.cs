// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the MIT license.

using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Numerics;
using QuadTrees;
using ReturnHome.Server.EntityObject;
using ReturnHome.Server.EntityObject.Player;

namespace ReturnHome.Server.Managers
{
    public static class MapManager
    {
        private static QuadTreePointF<Entity> qtree = new QuadTreePointF<Entity>(new RectangleF(3000.0f, 3000.0f, 24000.0f, 30000.0f));
        private static List<Entity> treeQueue = new List<Entity>();
        private static List<Entity> treeRemoveQueue = new List<Entity>();

        public static void AddObjectToTree(Entity entity)
        {
            //Should this queue players to be added simulataneously, instead of individual add's? Probably.
            
            treeQueue.Add(entity);
        }

        public static void BulkAddObjects()
        {
            //iterate over treeQueue and add players to the quad tree one by one
           for(int i = 0; i < treeQueue.Count; i++)
            {
                qtree.Add(treeQueue[i]);
            }
           
            //clear the tree queue so it doesn't continuosly try to add the same objects to the tree
            treeQueue.Clear();
        }

        public static void QueryObjectsForDistribution()
        {
            List<Entity> entityList = new();
            List<Character> charList = PlayerManager.RequestPlayerList();

            foreach (Character entity in charList)
            {
                qtree.GetObjects(new RectangleF(entity.x - 50.0f, entity.z - 50.0f, 100.0f, 100.0f), entityList);

                //Sort Character List
                entityList = entityList.OrderBy(x => Vector2.Distance(new Vector2(entity.x, entity.z), new Vector2(x.x, x.z))).ToList();

                entity.characterSession.rdpCommIn.connectionData.AddChannelObjects(entityList);



                entityList.Clear();
            }
        }

        ///Takes a "radius" float for query
        //Allows us to query for nearby objects to player, individual methods can handle cycling through the list for correctness
        public static List<Entity> QueryNearbyObjects(Character myCharacter, float radius)
        {
            List<Entity> entityList = new();
            foreach (Entity entity in qtree.GetAllObjects())
            {
                Console.WriteLine(entity.CharName);
            }
            qtree.GetObjects(new RectangleF(myCharacter.x - (radius / 2), myCharacter.z - (radius / 2), radius, radius), entityList);

            return entityList;
        }

        public static void UpdatePosition(Entity entity)
        {
            qtree.Move(entity);
        }

        /*
        public static void BulkRemovePlayers()
        {
            for(int i = 0; i < treeRemoveQueue.Count; i++)
        }
        */

        public static void RemoveObjectFromTree(Entity entity)
        {
            if (entity == null)
                return;

            qtree.Remove(entity);
            Console.WriteLine($"Removed: {entity.CharName}");
        }


    }
}
