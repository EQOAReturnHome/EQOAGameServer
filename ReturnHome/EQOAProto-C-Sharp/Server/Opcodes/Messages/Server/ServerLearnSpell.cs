﻿// Licensed to the .NET Foundation under one or more agreements.
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
            writer.Write(message.Opcode);
            writer.Write(1);
            writer.Write(1);
            spell.DumpSpell(session, ref writer);

            message.Size = writer.Position;
            session.sessionQueue.Add(message);
        }
    }
}