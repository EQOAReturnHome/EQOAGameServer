using System;
using ReturnHome.Server.Network;
using ReturnHome.Utilities;

namespace ReturnHome.Server.Opcodes.Messages.Server
{
    class ServerPlayerIgnoreList
    {
        public static void PlayerIgnoreList(Session session)
        {
            Memory<byte> temp = new byte[3];
            Span<byte> Message = temp.Span;

            BufferWriter writer = new BufferWriter(Message);

            writer.Write((ushort)GameOpcode.IgnoreList);
            writer.Write((byte)0);

            //For now send no ignored people
            SessionQueueMessages.PackMessage(session, temp, MessageOpcodeTypes.ShortReliableMessage);
        }
    }
}
