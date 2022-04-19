// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the MIT license.

using System;
using System.Collections.Generic;
using System.Text;
using ReturnHome.Server.EntityObject;
using ReturnHome.Server.EntityObject.Player;
using ReturnHome.Server.Managers;
using ReturnHome.Server.Network;
using ReturnHome.Utilities;

namespace ReturnHome.Server.Opcodes.Chat
{
    public static class SayChat
    {

        public static float Radius = 200.0f;

        //TODO: Consider reworking player query to send the say message?
        ///This method queries for nearby objects to player, then iterates over list, looking if any are players and distributing the message
        public static void ProcessSay(Session MySession, string message)
        {
            message = $"{MySession.MyCharacter.CharName}: " + message;

            //Query for nearby objects
            List<Entity> entityList = MapManager.QueryNearbyObjects(MySession.MyCharacter, Radius);

            Memory<byte> temp = new Memory<byte>(new byte[6 + (message.Length * 2)]);
            BufferWriter writer = new(temp.Span);
            writer.Write((ushort)GameOpcode.ClientMessage);
            writer.WriteString(Encoding.Unicode, message);

            //Loop over entity list, if any are players, distribute the message
            foreach (Entity e in entityList)
            {
                if (e.isPlayer)
                {
                    SessionQueueMessages.PackMessage(((Character)e).characterSession, temp, MessageOpcodeTypes.ShortReliableMessage);
                }
            }
        }
    }
}
