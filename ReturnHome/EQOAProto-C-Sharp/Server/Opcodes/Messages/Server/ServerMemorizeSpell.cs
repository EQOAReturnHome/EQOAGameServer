// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the MIT license.

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ReturnHome.Server.EntityObject.Spells;
using ReturnHome.Server.Network;
using ReturnHome.Utilities;

namespace ReturnHome.Server.Opcodes.Messages.Server
{
    public static class ServerMemorizeSpell
    {
        public static void MemorizeSpell(Session session, uint counter, byte bookSlot, byte hotbarSlot)
        {

            //0xB4 = int, byte, byte

            Message message = new Message(MessageType.SegmentReliableMessage, GameOpcode.ServerMemorizeSpell);
            BufferWriter writer = new BufferWriter(message.Span);

            writer.Write(message.Opcode);
            writer.Write(counter);
            writer.Write(bookSlot);
            writer.Write(hotbarSlot);

            message.Size = writer.Position;
            session.sessionQueue.Add(message);
        }
    }
}
