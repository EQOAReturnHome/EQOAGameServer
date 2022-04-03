// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the MIT license.

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ReturnHome.Server.Network;
using ReturnHome.Utilities;

namespace ReturnHome.Server.Opcodes.Messages.Server
{
    class ServerPlayerTarget
    {
        public static void PlayerTarget(Session session, byte faceColor, byte conColor, uint target, uint targetCounter)
        {
            int offset = 0;
            Memory<byte> message = new byte[0x0109];
            Span<byte> temp = message.Span;

            temp.Write((ushort)GameOpcode.TargetInformation, ref offset);
            temp.Write(faceColor, ref offset); // 0/1 = red face 2/3 = neutral face 4/5 = blue face //Perform Calculations to check for 
            temp.Write(conColor, ref offset); // 0 = red con 1 = yellow con 2 = white con 3 = Dark Blue con 4 = Light Blue Con 5 = Green con 6 = Yellowish/white con? 7 = no con at all? But can still target? 14 = faded yellow con? 15 = faded orange con? 60 = yellowish/green con?

            offset = 124;
            temp.Write(target, ref offset);

            offset = 261;
            temp.Write(targetCounter, ref offset);
            SessionQueueMessages.PackMessage(session, message, MessageOpcodeTypes.ShortReliableMessage);
        }
    }
}
