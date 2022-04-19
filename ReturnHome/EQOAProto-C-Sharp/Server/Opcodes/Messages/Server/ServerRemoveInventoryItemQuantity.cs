using System;
using ReturnHome.Server.EntityObject.Player;
using ReturnHome.Server.Network;
using ReturnHome.Utilities;

namespace ReturnHome.Server.Opcodes.Messages.Server
{
    public static class ServerRemoveInventoryItemQuantity
    {
        /*<summary>
         * This method will send the opcode to update item quantity in inventory, including deleting it if entire quantity is removed.
         * </summary>
         */
		public static void RemoveInventoryItemQuantity(Session session, Item item, byte clientIndex)
        {
            Memory<byte> temp = new byte[3 + Utility_Funcs.DoubleVariableLengthIntegerLength(item.StackLeft)];
            Span<byte> Message = temp.Span;

            BufferWriter writer = new BufferWriter(Message);

            writer.Write(GameOpcode.RemoveInvItem);
            writer.Write(clientIndex);
            writer.Write((byte)1);
            writer.Write7BitEncodedInt64(item.StackLeft);

            SessionQueueMessages.PackMessage(session, temp, MessageOpcodeTypes.ReliableMessage);
        }
    }
}
