using ReturnHome.Server.Network;
using ReturnHome.Utilities;

namespace ReturnHome.Server.Opcodes.Messages.Client
{
    class ClientCastSpell
    {
        public static void CastSpell(Session session, PacketMessage clientPacket)
        {
            BufferReader reader = new(clientPacket.Data.Span);

            //First 4 bytes is targeting counter, just discarding for now
            _ = reader.Read<uint>();
            uint spell = reader.Read<byte>(); // WhereOnHotBar Value
            //uint spellTarget = reader.Read<uint>(); // Spell Target

            session.MyCharacter.Spell = spell;
            session.SpellCastUpdate();
        }
    }
}
