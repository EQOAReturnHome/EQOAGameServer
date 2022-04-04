using System;
using ReturnHome.Server.Network;
using ReturnHome.Utilities;

namespace ReturnHome.Server.Opcodes.Messages.Server
{
    public static class ServerUpdatePlayerTunar
    {
		public static void UpdatePlayerTunar(Session session, int tunar)
        {
            Memory<byte> temp = new byte[2 + Utility_Funcs.DoubleVariableLengthIntegerLength(tunar)];
            Span<byte> Message = temp.Span;

            BufferWriter writer = new BufferWriter(Message);

            writer.Write((ushort)GameOpcode.PlayerTunar);
            writer.Write7BitEncodedInt64(tunar);

            SessionQueueMessages.PackMessage(session, temp, MessageOpcodeTypes.ShortReliableMessage);
        }
    }
}
