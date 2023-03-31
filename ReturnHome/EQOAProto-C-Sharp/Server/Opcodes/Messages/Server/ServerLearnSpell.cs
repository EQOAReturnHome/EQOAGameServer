// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the MIT license.

using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ReturnHome.Server.EntityObject.Items;
using ReturnHome.Server.EntityObject.Spells;
using ReturnHome.Server.Network;
using ReturnHome.Utilities;

namespace ReturnHome.Server.Opcodes.Messages.Server
{
    public static class ServerLearnSpell
    {

        public static void LearnSpell(Session session, SpellPattern spell)
        {
            Message message = new Message(MessageType.SegmentReliableMessage, GameOpcode.LearnSpell);
            BufferWriter writer = new BufferWriter(message.Span);
            //Spell testSpell = new Spell(179, 1, 0, 0, 1, 1, 13, 1, 1, 1, 1, 0, 486870879, 638503769, 0, 0, 255,"Kick", "An attack that allows you to kick your enemy.");
            //Spell testSpell = new Spell(2, 3, 0, 0, 1, 1, 1, 1, 1, 1, 1, 0, -1809115846, -568605204, 0, 0, 255, "Werewolf Form", "Assume the form of the werewolf.");
            //Spell testSpell = new Spell(3, 4, 0, 0, 1, 1, 11, 1, 1, 2, 1, 0, 486870879, -49534593, 1, 0, 255, 0, "Provoke", "Allows you to gain the attention of your enemy.");
            writer.Write(message.Opcode);
            writer.Write(1);
            writer.Write(1);
            spell.DumpSpell(session, ref writer);

            message.Size = writer.Position;
            session.sessionQueue.Add(message);
        }
    }
}
