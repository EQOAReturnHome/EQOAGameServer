using System;
using ReturnHome.Server.Network;
using ReturnHome.Utilities;

namespace ReturnHome.Server.Opcodes.Messages.Client
{
    public static class ServerUpdateBankTunar
    {
		public static void UpdateBankTunar(Session session, int tunar)
        {
            int offset = 0;
            Memory<byte> bankTemp = new byte[2 + Utility_Funcs.DoubleVariableLengthIntegerLength(tunar)];
            Span<byte> messageBank = bankTemp.Span;

            messageBank.Write((ushort)GameOpcode.ConfirmBankTunar, ref offset);
            messageBank.Write7BitDoubleEncodedInt(tunar, ref offset);

            SessionQueueMessages.PackMessage(session, bankTemp, MessageOpcodeTypes.ShortReliableMessage);
        }
    }
}
