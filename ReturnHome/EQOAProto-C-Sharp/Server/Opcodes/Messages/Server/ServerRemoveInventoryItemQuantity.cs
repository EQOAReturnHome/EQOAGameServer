using System;
using ReturnHome.Server.EntityObject.Player;
using ReturnHome.Server.Network;
using ReturnHome.Utilities;

namespace ReturnHome.Server.Opcodes.Messages.Client
{
    public static class ServerRemoveInventoryItemQuantity
    {
        /*<summary>
         * This method will send the opcode to update item quantity in inventory, including deleting it if entire quantity is removed.
         * </summary>
         */
		public static void RemoveInventoryItemQuantity(Session session, Item item, byte clientIndex)
        {
            int offset = 0;
            Memory<byte> temp = new byte[3 + Utility_Funcs.DoubleVariableLengthIntegerLength(item.StackLeft)];
            Span<byte> message = temp.Span;

            message.Write((ushort)GameOpcode.RemoveInvItem, ref offset);
            message[offset++] = clientIndex;
            message[offset++] = 1;
            message.Write7BitDoubleEncodedInt(item.StackLeft, ref offset);

            SessionQueueMessages.PackMessage(session, temp, MessageOpcodeTypes.ShortReliableMessage);
        }
    }
}
