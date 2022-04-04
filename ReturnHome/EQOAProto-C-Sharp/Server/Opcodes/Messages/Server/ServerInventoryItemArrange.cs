using System;
using ReturnHome.Server.Network;
using ReturnHome.Utilities;

namespace ReturnHome.Server.Opcodes.Messages.Server
{
    public static class ServerInventoryItemArrange
    {
		public static void InventoryItemArrange(Session session, byte clientItem1, byte clientItem2)
        {
            //Define Memory span
            Memory<byte> temp = new byte[4];
            Span<byte> Message = temp.Span;

            BufferWriter writer = new BufferWriter(Message);

            //Write arrangeop code back to memory span
            writer.Write((ushort)GameOpcode.ArrangeItem);

            //send slot swap back to player to confirm
            writer.Write(clientItem1);
            writer.Write(clientItem2);

            //Send arrange op code back to player
            SessionQueueMessages.PackMessage(session, temp, MessageOpcodeTypes.ShortReliableMessage);
        }
    }
}
