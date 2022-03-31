// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the MIT license.

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ReturnHome.Server.Network;
using ReturnHome.Utilities;

namespace ReturnHome.Server.Opcodes.Messages.Client
{
    public static class TransferTunar
    {

        public static void UpdateBank(Session session, int tunar)
        {
            int offset = 0;
            Memory<byte> bankTemp = new byte[2 + Utility_Funcs.DoubleVariableLengthIntegerLength(tunar)];
            Span<byte> messageBank = bankTemp.Span;

            messageBank.Write((ushort)GameOpcode.ConfirmBankTunar, ref offset);
            messageBank.Write7BitDoubleEncodedInt(tunar, ref offset);

            SessionQueueMessages.PackMessage(session, bankTemp, MessageOpcodeTypes.ShortReliableMessage);
        }

        public static void UpdatePlayer(Session session, int tunar)
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
