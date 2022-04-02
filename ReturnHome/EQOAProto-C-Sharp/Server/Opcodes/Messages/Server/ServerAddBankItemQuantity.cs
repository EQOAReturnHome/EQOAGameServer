using System;
using System.IO;
using ReturnHome.Server.EntityObject.Player;
using ReturnHome.Server.Network;

namespace ReturnHome.Server.Opcodes.Messages.Client
{
    public static class ServerAddBankItemQuantity
    {
		public static void AddBankItemQuantity(Session session, Item item)
        {
			Memory<byte> buffer;
            using (MemoryStream memStream = new())
            {

                memStream.Write(BitConverter.GetBytes((ushort)GameOpcode.AddBankItem));

                item.DumpItem(memStream);
                long pos = memStream.Position;
                buffer = new Memory<byte>(memStream.GetBuffer(), 0, (int)pos);

                SessionQueueMessages.PackMessage(session, buffer, MessageOpcodeTypes.ShortReliableMessage);
            }
        }
    }
}
