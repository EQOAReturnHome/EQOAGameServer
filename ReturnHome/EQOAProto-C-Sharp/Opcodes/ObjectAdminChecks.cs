// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the MIT license.

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ReturnHome.Server.EntityObject.Player;
using ReturnHome.Server.Managers;
using ReturnHome.Server.Network;

namespace ReturnHome.Opcodes.Chat
{
    public static class ObjectAdminChecks
    {
        public static bool ProcessChanges(Session MySession, string[] changes)
        {
            string message;
            if(PlayerManager.QueryForPlayer(changes[1], out Character c))
            {
                if (c == null)
                    return false;

                message = $"Found character: {changes[1]}";
                ChatMessage.GenerateClientSpecificChat(MySession, message);

                switch(changes[2])
                {
                    case "animation":
                        message = $"Changing character: {changes[1]}, {changes[2]} to {changes[3]}";
                        c.Animation = byte.Parse(changes[3]);
                        c.characterSession.objectUpdate = true;
                        ChatMessage.GenerateClientSpecificChat(MySession, message);
                        break;

                    case "size":
                        message = $"Changing character: {changes[1]}, {changes[2]} to {changes[3]}";
                        c.ModelSize = float.Parse(changes[3]);
                        c.characterSession.objectUpdate = true;
                        ChatMessage.GenerateClientSpecificChat(MySession, message);
                        break;

                    case "face":
                        message = $"Changing character: {changes[1]}, {changes[2]} to {changes[3]}";
                        c.UpdateFacing(byte.Parse(changes[3]), 0);
                        c.characterSession.objectUpdate = true;
                        ChatMessage.GenerateClientSpecificChat(MySession, message);
                        break;

                    case "face2":
                        message = $"Changing character: {changes[1]}, {changes[2]} to {changes[3]}";
                        c.FacingF = float.Parse(changes[3]);
                        c.characterSession.objectUpdate = true;
                        ChatMessage.GenerateClientSpecificChat(MySession, message);
                        break;

                    case "model":
                        message = $"Changing character: {changes[1]}, {changes[2]} to {changes[3]}";
                        c.ModelID = int.Parse(changes[3]);
                        c.characterSession.objectUpdate = true;
                        ChatMessage.GenerateClientSpecificChat(MySession, message);
                        break;

                    case "vy":
                        message = $"Changing character: {changes[1]}, {changes[2]} to {changes[3]}";
                        c.VelocityY = float.Parse(changes[3]);
                        c.characterSession.objectUpdate = true;
                        ChatMessage.GenerateClientSpecificChat(MySession, message);
                        break;

                    case "vx":
                        message = $"Changing character: {changes[1]}, {changes[2]} to {changes[3]}";
                        c.VelocityX = float.Parse(changes[3]);
                        c.characterSession.objectUpdate = true;
                        ChatMessage.GenerateClientSpecificChat(MySession, message);
                        break;

                    case "vz":
                        message = $"Changing character: {changes[1]}, {changes[2]} to {changes[3]}";
                        c.VelocityZ = float.Parse(changes[3]);
                        c.characterSession.objectUpdate = true;
                        ChatMessage.GenerateClientSpecificChat(MySession, message);
                        break;

                    case "move":
                        message = $"Changing character: {changes[1]}, {changes[2]} to {changes[3]}";
                        c.Movement = byte.Parse(changes[3]);
                        c.characterSession.objectUpdate = true;
                        ChatMessage.GenerateClientSpecificChat(MySession, message);
                        break;

                    case "ns":
                        message = $"Changing character: {changes[1]}, {changes[2]} to {changes[3]}";
                        c.NorthToSouth = byte.Parse(changes[3]);
                        c.characterSession.objectUpdate = true;
                        ChatMessage.GenerateClientSpecificChat(MySession, message);
                        break;

                    case "ew":
                        message = $"Changing character: {changes[1]}, {changes[2]} to {changes[3]}";
                        c.EastToWest = byte.Parse(changes[3]);
                        c.characterSession.objectUpdate = true;
                        ChatMessage.GenerateClientSpecificChat(MySession, message);
                        break;

                    case "lat":
                        message = $"Changing character: {changes[1]}, {changes[2]} to {changes[3]}";
                        c.LateralMovement = byte.Parse(changes[3]);
                        c.characterSession.objectUpdate = true;
                        ChatMessage.GenerateClientSpecificChat(MySession, message);
                        break;

                    case "turn":
                        message = $"Changing character: {changes[1]}, {changes[2]} to {changes[3]}";
                        c.Turning = byte.Parse(changes[3]);
                        c.characterSession.objectUpdate = true;
                        ChatMessage.GenerateClientSpecificChat(MySession, message);
                        break;

                    case "spin":
                        message = $"Changing character: {changes[1]}, {changes[2]} to {changes[3]}";
                        c.SpinDown = byte.Parse(changes[3]);
                        c.characterSession.objectUpdate = true;
                        ChatMessage.GenerateClientSpecificChat(MySession, message);
                        break;
                    default: 
                        break;

                }

                return true;
            }

            return false;

        }
    }
}
