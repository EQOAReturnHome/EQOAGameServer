using ReturnHome.Server.EntityObject.Items;
using ReturnHome.Server.Network;
using ReturnHome.Utilities;

namespace ReturnHome.Server.Opcodes.Messages.Server
{
    public static class ServerRemoveItemOrQuantity
    {
		public static void RemoveItemOrQuantity(Session session, GameOpcode opcode, int StackLeft, byte clientIndex)
        {
            Message message = Message.Create(MessageType.ReliableMessage, opcode);
            BufferWriter writer = new BufferWriter(message.Span);

            writer.Write(message.Opcode);
            writer.Write(clientIndex);
            writer.Write7BitEncodedInt64(StackLeft);

            message.Size = writer.Position;
            session.sessionQueue.Add(message);
        }
    }
}
