using ReturnHome.Server.Network;
using ReturnHome.Server.Opcodes.Messages.Server;
using ReturnHome.Utilities;

namespace ReturnHome.Server.Opcodes.Messages.Client
{
    public static class ClientOptions
    {
        public static void ClientMessageOptions(Session session, PacketMessage ClientPacket)
        {
            BufferReader reader = new(ClientPacket.Data.Span);

            byte state = reader.Read<byte>();
            if (ClientPacket.Header.Opcode == (ushort)GameOpcode.LootMessages)
                session.MyCharacter.LootMessages = state == 0;

            if (ClientPacket.Header.Opcode == (ushort)GameOpcode.FactionMessages)
                session.MyCharacter.FactionMessages = state == 0;
        }
    }
}
