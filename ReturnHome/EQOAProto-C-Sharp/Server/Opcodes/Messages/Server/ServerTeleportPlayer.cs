using ReturnHome.Server.Network;
using ReturnHome.Utilities;

namespace ReturnHome.Server.Opcodes.Messages.Server
{
    class ServerTeleportPlayer
    {
        public static void TeleportPlayer(Session session, byte world, float x, float y, float z, float facing)
        {
            Message message = Message.Create(MessageType.ReliableMessage, GameOpcode.Teleport);
            BufferWriter writer = new BufferWriter(message.Span);

            writer.Write(message.Opcode);
            writer.Write(world); //world
            writer.Write(x); // x
            writer.Write(y); // y
            writer.Write(z); // z
            writer.Write(facing); //Facing
            writer.Position += 8;
            writer.Write(session.MyCharacter.Teleportcounter++); //counter

            message.Size = writer.Position;
            session.sessionQueue.Add(message);
        }
    }
}
