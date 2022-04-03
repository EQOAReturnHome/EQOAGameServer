using System;
using System.IO;
using ReturnHome.Server.EntityObject.Player;
using ReturnHome.Server.Network;

namespace ReturnHome.Server.Opcodes.Messages.Server
{
    public static class ServerAddInventoryItemQuantity
    {
		public static void AddInventoryItemQuantity(Session session, Item item)
        {
			Memory<byte> buffer;
            using (MemoryStream memStream = new())
            {

                memStream.Write(BitConverter.GetBytes((ushort)GameOpcode.AddInvItem));

                item.DumpItem(memStream);
                long pos = memStream.Position;
                buffer = new Memory<byte>(memStream.GetBuffer(), 0, (int)pos);

                SessionQueueMessages.PackMessage(session, buffer, MessageOpcodeTypes.ShortReliableMessage);
            }
        }
    }
}
