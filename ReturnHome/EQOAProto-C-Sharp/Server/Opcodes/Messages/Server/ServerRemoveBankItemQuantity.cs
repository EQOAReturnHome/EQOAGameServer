using System;
using ReturnHome.Server.EntityObject.Player;
using ReturnHome.Server.Network;
using ReturnHome.Utilities;

namespace ReturnHome.Server.Opcodes.Messages.Client
{
    public static class ServerRemoveBankItemQuantity
    {
		public static void RemoveBankItemQuantity(Session session, Item item, byte clientIndex)
        {
            int offset = 0;
            Memory<byte> temp = new byte[3 + Utility_Funcs.DoubleVariableLengthIntegerLength(item.StackLeft)];
            Span<byte> message = temp.Span;

            message.Write((ushort)GameOpcode.RemoveBankItem, ref offset);
            message[offset++] = clientIndex;
            message.Write7BitDoubleEncodedInt(item.StackLeft, ref offset);

            SessionQueueMessages.PackMessage(session, temp, MessageOpcodeTypes.ShortReliableMessage);
        }
    }
}
