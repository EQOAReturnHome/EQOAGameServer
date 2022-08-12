using ReturnHome.Server.Network;
using ReturnHome.Utilities;
using System.Diagnostics;


namespace ReturnHome.Server.Opcodes.Messages.Server
{
    class ServerCastSpell
    {
        public static void CastSpell(Session session, uint spellFX, uint target)
        {
            //Send Spell Effect Packet with Target to Client
            Message message = Message.Create(MessageType.ReliableMessage, GameOpcode.SpellInformation);
            BufferWriter writer = new BufferWriter(message.Span);
            writer.Write(message.Opcode);
            writer.Write(session.SessionID); // ID of Caster
            writer.Write(target); // ID of Target
            writer.Position += 1;
            writer.Write(0x0BB8); // Cast time
            writer.Write(spellFX); // SpellFX
            message.Size = writer.Position;
            session.sessionQueue.Add(message);
        }
    }
}
