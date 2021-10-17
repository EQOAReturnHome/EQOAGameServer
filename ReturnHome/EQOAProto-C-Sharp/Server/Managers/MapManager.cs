// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the MIT license.

using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Numerics;
using QuadTrees;
using ReturnHome.Server.Entity.Actor;
using ReturnHome.Server.Network;

namespace ReturnHome.Server.Managers
{
    public static class MapManager
    {
        private static QuadTreePointF<Character> qtree = new QuadTreePointF<Character>(new RectangleF(25000.0f, 15000.0f, 2000.0f, 2000.0f));
        private static List<Character> treeQueue = new List<Character>();

        public static void AddPlayerToTree(Character character)
        {
            //Should this queue players to be added simulataneously, instead of individual add's? Probably.
            treeQueue.Add(character);
        }

        public static void BulkAddPlayers()
        {
            qtree.AddBulk(treeQueue);
            treeQueue.Clear();
        }

        public static void QueryNearbyPlayers()
        {
            
            List<Character> charList = new List<Character>();
            foreach (Character character in qtree.GetAllObjects())
            {
                qtree.GetObjects(new RectangleF(character.XCoord - 50.0f, character.ZCoord - 50.0f, 100.0f, 100.0f), charList);

                //Sort Character List
                charList = charList.OrderBy(x => Vector2.Distance(new Vector2(character.XCoord, character.ZCoord), new Vector2(x.XCoord, x.ZCoord))).ToList();

                //Test order
                //Console.Write($"List for {character.CharName}: ");
                //foreach(Character i in charList)
                    //Console.WriteLine($"{i.CharName} - Distance: {Vector2.Distance(new Vector2(character.XCoord, character.ZCoord), new Vector2(i.XCoord, i.ZCoord))}");
                //Console.WriteLine();

                //if(charList.Count > 23)
                    //charList.RemoveRange(23, charList.Count - 23);
                character.characterSession.rdpCommIn.connectionData.AddChannelObjects(charList);
                charList.Clear();

            }
            
        }

        public static void UpdatePosition(Character character)
        {
            qtree.Move(character);
        }


    }
}
