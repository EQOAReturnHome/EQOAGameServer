using System;
using ReturnHome.Server.Network;
using ReturnHome.Utilities;

namespace ReturnHome.Server.Opcodes.Messages.Server
{
    class ServerUpdatePlayerXPandLevel
    {
        public static void UpdatePlayerXPandLevel(Session session, int Level, int totalXPEarned)
        {
            Memory<byte> temp = new Memory<byte>(new byte[Utility_Funcs.DoubleVariableLengthIntegerLength(Level) + Utility_Funcs.DoubleVariableLengthIntegerLength(totalXPEarned) + 2]);
            Span<byte> Message = temp.Span;

            BufferWriter writer = new BufferWriter(Message);

            writer.Write((ushort)GameOpcode.GrantXP);
            writer.Write7BitEncodedInt64(Level);
            writer.Write7BitEncodedInt64(totalXPEarned);

            SessionQueueMessages.PackMessage(session, temp, MessageOpcodeTypes.ShortReliableMessage);
        }
    }
}
