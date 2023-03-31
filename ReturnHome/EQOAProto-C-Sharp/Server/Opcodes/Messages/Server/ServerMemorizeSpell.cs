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
        public static void MemorizeSpell(Session session)
        {

            //0xB4 = int, byte, byte

            Message message = new Message(MessageType.SegmentReliableMessage, GameOpcode.LearnSpell);
            BufferWriter writer = new BufferWriter(message.Span);
            Spell testSpell = new Spell(10, 7, 0, 0, 1, 1, 23, 1, 1, 4, 1, 0, -1013573000, 638503769, 0, 0, 255,"Blowjob", "The best spell in the game");

            writer.Write(message.Opcode);
            writer.Write(1);
            writer.Write(1);
            testSpell.DumpSpell(ref writer);

            message.Size = writer.Position;
            session.sessionQueue.Add(message);
        }
    }
}
