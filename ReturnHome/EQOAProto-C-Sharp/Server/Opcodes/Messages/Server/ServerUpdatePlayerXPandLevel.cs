using System;
using ReturnHome.Server.Network;
using ReturnHome.Utilities;

namespace ReturnHome.Server.Opcodes.Messages.Server
{
    class ServerUpdatePlayerXPandLevel
    {
        public static void UpdatePlayerXPandLevel(Session session, int Level, int totalXPEarned)
        {
            int offset = 0;
            Memory<byte> temp = new Memory<byte>(new byte[Utility_Funcs.DoubleVariableLengthIntegerLength(Level) + Utility_Funcs.DoubleVariableLengthIntegerLength(totalXPEarned) + 2]);
            Span<byte> Message = temp.Span;

            Message.Write((ushort)GameOpcode.GrantXP, ref offset);
            Message.Write7BitDoubleEncodedInt(Level, ref offset);
            Message.Write7BitDoubleEncodedInt(totalXPEarned, ref offset);
            SessionQueueMessages.PackMessage(session, temp, MessageOpcodeTypes.ShortReliableMessage);
        }
    }
}
