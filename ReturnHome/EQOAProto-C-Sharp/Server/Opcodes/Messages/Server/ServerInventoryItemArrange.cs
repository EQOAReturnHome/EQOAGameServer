using System;
using ReturnHome.Server.Network;
using ReturnHome.Utilities;

namespace ReturnHome.Server.Opcodes.Messages.Client
{
    public static class ServerInventoryItemArrange
    {
		public static void InventoryItemArrange(Session session, byte clientItem1, byte clientItem2)
        {
            int offset = 0;

            //Define Memory span
            Memory<byte> temp = new byte[4];
            Span<byte> Message = temp.Span;

            //Write arrangeop code back to memory span
            Message.Write((ushort)GameOpcode.ArrangeItem, ref offset);

            //send slot swap back to player to confirm
            Message.Write(clientItem1, ref offset);
            Message.Write(clientItem2, ref offset);

            //Send arrange op code back to player
            SessionQueueMessages.PackMessage(session, temp, MessageOpcodeTypes.ShortReliableMessage);
        }
    }
}
