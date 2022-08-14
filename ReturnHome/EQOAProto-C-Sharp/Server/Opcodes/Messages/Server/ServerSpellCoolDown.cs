using ReturnHome.Server.Network;
using ReturnHome.Utilities;
using System.Diagnostics;


namespace ReturnHome.Server.Opcodes.Messages.Server
{
    class ServerSpellCoolDown
    {
        public static void SpellCoolDown(Session session, uint addedOrder, int timer)
        {
            Debug.WriteLine("start cooldown");
            //Send Spell Effect Packet with Target to Client
            Message message = Message.Create(MessageType.ReliableMessage, GameOpcode.SpellCoolDown);
            BufferWriter writer = new BufferWriter(message.Span);
            writer.Write(message.Opcode);
            writer.Write(addedOrder); // Location in SpellBook
            writer.Write(0x00000001);
            writer.Write7BitEncodedInt64(timer); // recast time
            message.Size = writer.Position;
            session.sessionQueue.Add(message);
        }
    }
}
