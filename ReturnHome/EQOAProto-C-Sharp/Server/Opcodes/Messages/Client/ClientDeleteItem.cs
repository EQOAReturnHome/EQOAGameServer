using ReturnHome.Server.Network;
using ReturnHome.Utilities;

namespace ReturnHome.Server.Opcodes.Messages.Client
{
    class ClientDeleteItem
    {
        public static void DeleteItem(Session MySession, PacketMessage ClientPacket)
        {
            BufferReader reader = new(ClientPacket.Data.Span);

            byte itemToDestroy = (byte)reader.Read<uint>();
            int quantityToDestroy = (int)reader.Read<uint>();

            MySession.MyCharacter.DestroyItem(itemToDestroy, quantityToDestroy);
        }
    }
}
