using System;
using ReturnHome.Server.Network;
using ReturnHome.Utilities;

namespace ReturnHome.Server.Opcodes.Messages.Client
{
    class ClientCastSpell
    {
        public static void CastSpell(Session session, Message clientPacket)
        {
            BufferReader reader = new(clientPacket.message.Span);

            //First 4 bytes is targeting counter, just discarding for now
            _ = reader.Read<uint>();
            int spell = reader.Read<byte>(); // WhereOnHotBar Value
            Console.WriteLine($"Got spell from client in hotbar slot {spell}");
            //uint spellTarget = reader.Read<uint>(); // Spell Target

            session.MyCharacter.MySpellBook.UseSpell(spell);
            session.SpellCastUpdate();
        }
    }
}
