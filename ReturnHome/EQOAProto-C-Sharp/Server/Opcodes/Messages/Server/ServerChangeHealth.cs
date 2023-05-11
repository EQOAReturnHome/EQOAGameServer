using ReturnHome.Server.Network;
using ReturnHome.Utilities;
using System.Diagnostics;


namespace ReturnHome.Server.Opcodes.Messages.Server
{
    class ServerChangeHealth
    {
        public static void Damage(Session session, int damage, uint caster, uint target)
        {
            //Send Damage Packet with Target to Client
            Message message = Message.Create(MessageType.ReliableMessage, GameOpcode.Damage);
            BufferWriter writer = new BufferWriter(message.Span);
            writer.Write(message.Opcode);
            writer.Write(caster); // ID of Caster
            writer.Write7BitEncodedInt64(damage*-1); // Damage Number
            writer.Write(target); // ID of Target
            message.Size = writer.Position;
            session.sessionQueue.Add(message);
        }

        public static void Heal(Session session, int health, uint caster, uint target)
        {
            //Send Damage Packet with Target to Client
            Message message = Message.Create(MessageType.ReliableMessage, GameOpcode.Damage);
            BufferWriter writer = new BufferWriter(message.Span);
            writer.Write(message.Opcode);
            writer.Write(caster); // ID of Caster
            writer.Write7BitEncodedInt64(health); // Damage Number
            writer.Write(target); // ID of Target
            message.Size = writer.Position;
            session.sessionQueue.Add(message);
        }
    }
}
