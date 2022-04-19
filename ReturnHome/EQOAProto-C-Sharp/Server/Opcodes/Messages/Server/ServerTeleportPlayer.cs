using System;
using ReturnHome.Server.Network;
using ReturnHome.Utilities;

namespace ReturnHome.Server.Opcodes.Messages.Server
{
    class ServerTeleportPlayer
    {
        public static void TeleportPlayer(Session session, byte world, float x, float y, float z, float facing)
        {
            Memory<byte> temp = new byte[31];
            Span<byte> Message = temp.Span;

            BufferWriter writer = new BufferWriter(Message);

            writer.Write((ushort)0x07F6);
            writer.Write(world); //world
            writer.Write(x); // x
            writer.Write(y); // y
            writer.Write(z); // z
            writer.Write(facing); //Facing
            writer.Position += 8;
            writer.Write(session.MyCharacter.Teleportcounter++); //counter

            SessionQueueMessages.PackMessage(session, temp, MessageOpcodeTypes.ReliableMessage);
        }
    }
}
