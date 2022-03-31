using ReturnHome.Server.EntityObject;
using ReturnHome.Server.Managers;
using ReturnHome.Server.Network;

namespace ReturnHome.Server.Opcodes.Chat
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

                message = $"Found character: {c.CharName}";
                ChatMessage.GenerateClientSpecificChat(MySession, message);

                switch(changes[1])
                {
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
