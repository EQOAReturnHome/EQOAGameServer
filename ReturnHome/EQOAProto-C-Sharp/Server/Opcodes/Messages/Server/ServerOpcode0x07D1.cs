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
    class ServerOpcode0x07D1
    {
        public static void Opcode0x07D1(Session session)
        {
            int offset = 0;
            Memory<byte> temp1 = new byte[6];
            Span<byte> Message = temp1.Span;
            Message.Write(BitConverter.GetBytes((ushort)GameOpcode.Camera1), ref offset);
            Message.Write(new byte[] { 0x03, 0x00, 0x00, 0x00 }, ref offset);
            ///Handles packing message into outgoing packet
            SessionQueueMessages.PackMessage(session, temp1, MessageOpcodeTypes.ShortReliableMessage);
        }
    }
}
