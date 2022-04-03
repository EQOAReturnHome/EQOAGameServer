using System;
using ReturnHome.Server.Network;
using ReturnHome.Utilities;

namespace ReturnHome.Server.Opcodes.Messages.Server
{
    class ServerTeleportPlayer
    {
        public static void TeleportPlayer(Session session, byte world, float x, float y, float z, float facing)
        {

            Memory<byte> temp;
            Span<byte> thisMessage;
            temp = new byte[31];
            thisMessage = temp.Span;
            int offset = 0;
            thisMessage.Write((ushort)0x07F6, ref offset);
            thisMessage.Write(world, ref offset); //world
            thisMessage.Write(x, ref offset); // x
            thisMessage.Write(y, ref offset); // y
            thisMessage.Write(z, ref offset); // z
            thisMessage.Write(facing, ref offset); //Facing
            offset += 8;
            thisMessage.Write(session.MyCharacter.Teleportcounter++, ref offset); //counter
            SessionQueueMessages.PackMessage(session, temp, MessageOpcodeTypes.ShortReliableMessage);
        }
    }
}
