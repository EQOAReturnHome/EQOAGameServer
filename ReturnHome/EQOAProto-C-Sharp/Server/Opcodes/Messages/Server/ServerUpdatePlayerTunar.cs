using System;
using ReturnHome.Server.Network;
using ReturnHome.Utilities;

namespace ReturnHome.Server.Opcodes.Messages.Client
{
    public static class ServerUpdatePlayerTunar
    {
		public static void UpdatePlayerTunar(Session session, int tunar)
        {
            int offset = 0;
            Memory<byte> playerTemp = new byte[2 + Utility_Funcs.DoubleVariableLengthIntegerLength(tunar)];
            Span<byte> messagePlayer = playerTemp.Span;

            messagePlayer.Write((ushort)GameOpcode.PlayerTunar, ref offset);
            messagePlayer.Write7BitDoubleEncodedInt(tunar, ref offset);

            SessionQueueMessages.PackMessage(session, playerTemp, MessageOpcodeTypes.ShortReliableMessage);
        }
    }
}
