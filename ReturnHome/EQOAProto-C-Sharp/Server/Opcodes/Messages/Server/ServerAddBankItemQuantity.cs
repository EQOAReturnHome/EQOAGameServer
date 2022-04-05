using System;
using ReturnHome.Server.EntityObject.Player;
using ReturnHome.Server.Network;
using ReturnHome.Utilities;

namespace ReturnHome.Server.Opcodes.Messages.Server
{
    public static class ServerAddBankItemQuantity
    {
		public static void AddBankItemQuantity(Session session, Item item)
        {
			Memory<byte> temp = new byte[500];
            Span<byte> Message = temp.Span;

            BufferWriter writer = new BufferWriter(Message);

            writer.Write((ushort)GameOpcode.AddBankItem);

            item.DumpItem(ref writer);

            Memory<byte> buffer = temp.Slice(0, writer.Position); 

            SessionQueueMessages.PackMessage(session, buffer, MessageOpcodeTypes.ShortReliableMessage);
        }
    }
}
