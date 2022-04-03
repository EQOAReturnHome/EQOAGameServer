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
    class ServerPlayerSpeed
    {
        public static void PlayerSpeed(Session MySession)
        {
            int offset = 0;
            Memory<byte> temp = new byte[6];
            Span<byte> Message = temp.Span;

            Message.Write(BitConverter.GetBytes((ushort)GameOpcode.PlayerSpeed), ref offset);
            Message.Write(BitConverter.GetBytes(25.0f), ref offset);
            //For now send a standard speed
            SessionQueueMessages.PackMessage(MySession, temp, MessageOpcodeTypes.ShortReliableMessage);
        }

        public static void PlayerSpeed(Session MySession, float speed)
        {
            int offset = 0;
            Memory<byte> temp = new byte[6];
            Span<byte> Message = temp.Span;

            Message.Write(BitConverter.GetBytes((ushort)GameOpcode.PlayerSpeed), ref offset);
            Message.Write(BitConverter.GetBytes(speed), ref offset);
            //For now send a standard speed
            SessionQueueMessages.PackMessage(MySession, temp, MessageOpcodeTypes.ShortReliableMessage);
        }
    }
}
