using System;
using ReturnHome.Server.EntityObject.Items;
using ReturnHome.Server.Network;
using ReturnHome.Utilities;

namespace ReturnHome.Server.Opcodes.Messages.Server
{
    public static class ServerAddItemOrQuantity
    {
		public static void AddItemOrQuantity(Session session, GameOpcode opcode, Item item)
        {
            Message message = Message.Create(MessageType.ReliableMessage, opcode);
            BufferWriter writer = new BufferWriter(message.Span);

            writer.Write(message.Opcode);

            item.DumpItem(ref writer, item.ClientIndex);

            message.Size = writer.Position;
            session.sessionQueue.Add(message);
        }
    }
}
