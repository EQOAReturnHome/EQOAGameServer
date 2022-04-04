using System;
using ReturnHome.Server.EntityObject.Player;
using ReturnHome.Server.Network;
using ReturnHome.Utilities;

namespace ReturnHome.Server.Opcodes.Messages.Server
{
    public static class ServerRemoveBankItemQuantity
    {
		public static void RemoveBankItemQuantity(Session session, Item item, byte clientIndex)
        {
            Memory<byte> temp = new byte[3 + Utility_Funcs.DoubleVariableLengthIntegerLength(item.StackLeft)];
            Span<byte> Message = temp.Span;

            BufferWriter writer = new BufferWriter(Message);

            writer.Write((ushort)GameOpcode.RemoveBankItem);
            writer.Write((byte)clientIndex);
            writer.Write7BitEncodedInt64(item.StackLeft);

            SessionQueueMessages.PackMessage(session, temp, MessageOpcodeTypes.ShortReliableMessage);
        }
    }
}
