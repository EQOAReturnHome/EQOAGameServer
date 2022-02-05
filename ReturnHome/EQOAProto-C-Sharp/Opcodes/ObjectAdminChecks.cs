using System;
using ReturnHome.Server.EntityObject;
using ReturnHome.Server.Managers;
using ReturnHome.Server.Network;

namespace ReturnHome.Opcodes.Chat
{
    public static class ObjectAdminChecks
    {
        public static bool ProcessChanges(Session MySession, string[] changes)
        {
            string message;
            /*Have this utilize players target as the source for change*/
            if(EntityManager.QueryForEntity(MySession.MyCharacter.Target, out Entity c))
            {
                if (c == null)
                    return false;

                int offset = 0;
                Memory<byte> temp;
                Span<byte> thisMessage;
                message = $"Found character: {c.CharName}";
                ChatMessage.GenerateClientSpecificChat(MySession, message);

                switch(changes[1])
                {
                    case "Zone":
                        message = $"Map: {MySession.MyCharacter.map.Name}";
                        ChatMessage.GenerateClientSpecificChat(MySession, message);
                        break;

                    case "Lava":
                        MapManager.Teleport(MySession, 3, 7906.09f, 75.2822f, 5914.65f, -2.40847f);
                        break;

                    case "Tun":
                        MapManager.Teleport(MySession, 0, 25273f, 54.125f, 15723.3f, -1.56333f);
                        break;

                    case "Arc":
                        MapManager.Teleport(MySession, 2, 7065.4609375f, 57.65724563598633f, 3499.9208984375f, -3.1183998584747314f);
                        break;

                    case "Rathe":
                        MapManager.Teleport(MySession, 1, 8949.796875f, 54.125919342041016f, 7189.22314453125f, -1.4557989835739136f);
                        break;

                    case "pos":
                        MapManager.Teleport(MySession, 4, 4823.9443359375f, 250.42288208007812f, 5504.09716796875f, -3.1277709007263184f);
                        break;

                    case "pod":
                        MapManager.Teleport(MySession, 5, 5063.4736328125f, -24.68804931640625f, 3880.777587890625f, 3.1250364780426025f);
                        break;

                    case "zp":
                        MapManager.Teleport(MySession, 5, 4872.84375f, 147.65725708007812f, 7047.947265625f, -1.588308334350586f);
                        break;

                    case "animation":
                        message = $"Changing character: {c.CharName}, {changes[1]} to {changes[2]}";
                        c.Animation = byte.Parse(changes[2]);
                        ChatMessage.GenerateClientSpecificChat(MySession, message);
                        break;

                    case "size":
                        message = $"Changing character: {c.CharName}, {changes[1]} to {changes[2]}";
                        c.ModelSize = float.Parse(changes[2]);
                        ChatMessage.GenerateClientSpecificChat(MySession, message);
                        break;

                    case "face":
                        message = $"Changing character: {c.CharName}, {changes[1]} to {changes[2]}";
                        c.UpdateFacing(byte.Parse(changes[2]), 0);
                        ChatMessage.GenerateClientSpecificChat(MySession, message);
                        break;

                    case "face2":
                        message = $"Changing character: {c.CharName}, {changes[1]} to {changes[2]}";
                        c.FacingF = float.Parse(changes[2]);
                        ChatMessage.GenerateClientSpecificChat(MySession, message);
                        break;

                    case "model":
                        message = $"Changing character: {c.CharName}, {changes[1]} to {changes[2]}";
                        c.ModelID = int.Parse(changes[2]);
                        ChatMessage.GenerateClientSpecificChat(MySession, message);
                        break;

                    case "vy":
                        message = $"Changing character: {c.CharName}, {changes[1]} to {changes[2]}";
                        c.VelocityY = float.Parse(changes[2]);
                        ChatMessage.GenerateClientSpecificChat(MySession, message);
                        break;

                    case "vx":
                        message = $"Changing character: {c.CharName}, {changes[1]} to {changes[2]}";
                        c.VelocityX = float.Parse(changes[2]);
                        ChatMessage.GenerateClientSpecificChat(MySession, message);
                        break;

                    case "vz":
                        message = $"Changing character: {c.CharName}, {changes[1]} to {changes[2]}";
                        c.VelocityZ = float.Parse(changes[2]);
                        ChatMessage.GenerateClientSpecificChat(MySession, message);
                        break;

                    case "move":
                        message = $"Changing character: {c.CharName}, {changes[1]} to {changes[2]}";
                        c.Movement = byte.Parse(changes[2]);
                        ChatMessage.GenerateClientSpecificChat(MySession, message);
                        break;

                    case "ns":
                        message = $"Changing character: {c.CharName}, {changes[1]} to {changes[2]}";
                        c.NorthToSouth = byte.Parse(changes[2]);
                        ChatMessage.GenerateClientSpecificChat(MySession, message);
                        break;

                    case "ew":
                        message = $"Changing character: {c.CharName}, {changes[1]} to {changes[2]}";
                        c.EastToWest = byte.Parse(changes[2]);
                        ChatMessage.GenerateClientSpecificChat(MySession, message);
                        break;

                    case "lat":
                        message = $"Changing character: {c.CharName}, {changes[1]} to {changes[2]}";
                        c.LateralMovement = byte.Parse(changes[2]);
                        ChatMessage.GenerateClientSpecificChat(MySession, message);
                        break;

                    case "turn":
                        message = $"Changing character: {c.CharName}, {changes[1]} to {changes[2]}";
                        c.Turning = byte.Parse(changes[2]);
                        ChatMessage.GenerateClientSpecificChat(MySession, message);
                        break;

                    case "spin":
                        message = $"Changing character: {c.CharName}, {changes[1]} to {changes[2]}";
                        c.SpinDown = byte.Parse(changes[2]);
                        ChatMessage.GenerateClientSpecificChat(MySession, message);
                        break;

                    case "namecolor":
                        message = $"Changing character: {c.CharName}, {changes[1]} to {changes[2]}";
                        c.ObjectUpdateNameColor(byte.Parse(changes[2]));
                        ChatMessage.GenerateClientSpecificChat(MySession, message);
                        break;

                    case "nameplate":
                        message = $"Changing character: {c.CharName}, {changes[1]} to {changes[2]}";
                        c.ObjectUpdateNamePlate(byte.Parse(changes[2]));
                        ChatMessage.GenerateClientSpecificChat(MySession, message);
                        break;

                    case "entity":
                        message = $"Changing character: {c.CharName}, {changes[1]} to {changes[2]}";
                        c.ObjectUpdateEntity(byte.Parse(changes[2]));
                        ChatMessage.GenerateClientSpecificChat(MySession, message);
                        break;

                    case "test":
                        message = $"Changing character: {c.CharName}, {changes[1]} to {changes[2]}";
                        c.ObjectUpdateUnknown();
                        ChatMessage.GenerateClientSpecificChat(MySession, message);
                        break;

                    case "status":
                        message = $"Changing character: {c.CharName}, {changes[1]} to {changes[2]}";
                        c.ObjectUpdateStatus(byte.Parse(changes[2]));
                        ChatMessage.GenerateClientSpecificChat(MySession, message);
                        break;

                    case "currenthp":
                        message = $"Changing character: {c.CharName}, {changes[1]} to {changes[2]}";
                        c.CurrentHP = int.Parse(changes[2]);
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
