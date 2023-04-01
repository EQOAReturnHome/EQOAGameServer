// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the MIT license.

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ReturnHome.Server.Network;
using ReturnHome.Server.Opcodes.Messages.Server;
using ReturnHome.Utilities;

namespace ReturnHome.Server.Opcodes.Messages.Client
{
    public static class ClientMemorizeSpell
    {
        public static void MemorizeSpell(Session session, Message clientPacket)
        {
            BufferReader reader = new(clientPacket.message.Span);

            //First 4 bytes is targeting counter, just discarding for now
            _ = reader.Read<uint>();
            int spell = reader.Read<byte>(); // WhereOnHotBar Value
            int hotbarSlot = reader.Read<byte>();


            Console.WriteLine($"Spell in slot {spell} of spellbook on hotbar slot {hotbarSlot}");

            session.MyCharacter.MySpellBook.AddSpellToHotbar(hotbarSlot, session.MyCharacter.MySpellBook.GetSpellFromBook(spell));
            ServerMemorizeSpell.MemorizeSpell(session, session.MyCharacter.MySpellBook.GetSpellFromBook(spell));
        }
    }
}
