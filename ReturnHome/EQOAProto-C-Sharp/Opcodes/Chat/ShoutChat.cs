// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the MIT license.

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ReturnHome.Server.EntityObject;
using ReturnHome.Server.EntityObject.Player;
using ReturnHome.Server.Managers;
using ReturnHome.Server.Network;
using ReturnHome.Utilities;

namespace ReturnHome.Opcodes.Chat
{
    public static class ShoutChat
    {

        public static float Radius = 500.0f;

        /*EVENTUALLY: This method queries for nearby objects to player, then iterates over list, looking if any are players and distributing the message*/
        //This method is treated as global shat for now
        public static void ProcessShout(Session MySession, string message)
        {

            message = $"{MySession.MyCharacter.CharName} shouts: " + message;

            /*
            List<Entity> entityList = MapManager.QueryNearbyObjects(MySession.Character, Radius);
            */

            List<Character> entityList = PlayerManager.QueryForAllPlayers();

            int offset = 0;

            //Add Colored message Opcode to list
            Memory<byte> temp = new Memory<byte>(new byte[6 + (message.Length * 2) + 4]);
            Span<byte> Message = temp.Span;
            Message.Write((ushort)GameOpcode.ColoredChat, ref offset);
            Message.Write(message.Length, ref offset);
            Message.Write(Encoding.Unicode.GetBytes(message), ref offset);

            //This needs to be dynamic eventually, allow the color to inject a color for the text? Or maybe needs an overload for this to be for specific players to receive
            Message.Write(new byte[] { 0x00, 0x3F, 0xFF, 0x3F }, ref offset);

            foreach (Entity e in entityList)
            {
                if (e.isPlayer)
                {
                    //Good to leave as is for now... will just adapt over if shout is changed to more of a local chat instead of global
                    SessionQueueMessages.PackMessage(((Character)e).characterSession, temp, MessageOpcodeTypes.ShortReliableMessage);
                }
            }
        }
    }
}
