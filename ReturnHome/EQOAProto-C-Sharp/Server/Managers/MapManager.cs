// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the MIT license.

using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Numerics;
using QuadTrees;
using ReturnHome.Server.EntityObject;
using ReturnHome.Server.EntityObject.Player;
using ReturnHome.Utilities;
using ReturnHome.Server.Zone;
using ReturnHome.Server.Network;
using ReturnHome.Opcodes;

namespace ReturnHome.Server.Managers
{
    public static class MapManager
    {
        private static ConcurrentDictionary<int, Map> _gameDict;
        //private static ObjectID class

        //Initializes all the map qudtree's
        public static void Initialize()
        {
            //make game map Dictionary
            _gameDict = new ConcurrentDictionary<int, Map>();
            _gameDict.TryAdd(0, new Map("Tunaria"));
            _gameDict.TryAdd(1, new Map("Rathe Mountains"));
            _gameDict.TryAdd(2, new Map("Odus"));
            _gameDict.TryAdd(3, new Map("Lava Storm"));
            _gameDict.TryAdd(4, new Map("Plane of Sky"));
            _gameDict.TryAdd(5, new Map("Secrets"));
            foreach (var m in _gameDict)
                m.Value.Initialize();
        }
        
        //Adds entity to world buffers, to be loaded in on next tick
        public static void Add(Entity e)
        {
          if(_gameDict.TryGetValue(e.World, out Map m))
            m.AddObject(e);

          else
            Logger.Err($"Error grabbing world: {e.World}");
        }

        public static void UpdatePosition(Entity e)
        {
            if (_gameDict.TryGetValue(e.World, out Map m))
                m.UpdatePosition(e);

            else
                Logger.Err($"Error Updating Object position, World: {e.World} Object: {e.CharName}");
        }

        public static void RemoveObject(Entity e)
        {
          if(_gameDict.TryGetValue(e.World, out Map m))
            m.RemoveObject(e);

          else
            Logger.Err($"Error grabbing world: {e.World}");
        }

        public static List<Entity> QueryObjects(Entity e, float Radius)
        {
            if (_gameDict.TryGetValue(e.World, out Map m))
                return m.Query(e, Radius);

            return default;
        }

        //Will force each map to do a bulk drop in, then distribute objects if needed
        public static void UpdateMaps()
        {
            foreach (var m in _gameDict)
            {
                m.Value.AddBulkObjects();
                m.Value.RemoveBulkObjects();
                m.Value.QueryObjectsForDistribution();
            }
        }

        //Method to teleport players, will check if new location != old location, and adjust appropriately in the quad tree
        //Make the assumption it is never a group teleport unless specified so
        public static void Teleport(Session session, byte world, float x, float y, float z, float facing, bool groupTeleport = false)
        {
            if (groupTeleport)
            {
                //Eventually we would check if the player is in a  group, and verify their distance from player to see if they teleport with them
                if (false)
                {

                }
            }

            //Teleport player
            Memory<byte> temp = new byte[31];
            Span<byte> thisMessage = temp.Span;
            int offset = 0;
            thisMessage.Write((ushort)0x07F6, ref offset);
            thisMessage.Write(world, ref offset);
            thisMessage.Write(x, ref offset);
            thisMessage.Write(y, ref offset);
            thisMessage.Write(z, ref offset);
            thisMessage.Write(facing, ref offset);
            offset += 8;
            thisMessage.Write(session.MyCharacter.Teleportcounter++, ref offset);
            SessionQueueMessages.PackMessage(session, temp, MessageOpcodeTypes.ShortReliableMessage);

            //Reset player channels
            session.rdpCommIn.connectionData.DisableChannels();

            //Check if player is changing maps, if so remove them from the current map
            //Set new map also
            if (session.MyCharacter.World != world)
            {
                RemoveObject(session.MyCharacter);
                session.MyCharacter.World = world;
            }
        }
    }
}
