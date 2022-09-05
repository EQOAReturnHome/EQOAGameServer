using System.Text;
using ReturnHome.Server.Managers;
using ReturnHome.Server.Network;
using ReturnHome.Utilities;

namespace ReturnHome.Server.Opcodes.Messages.Client
{
    public static class ClientAddCharacterToGroup
    {
        public static void AddCharacterToGroup(Session session, PacketMessage ClientPacket)
        {
            BufferReader reader = new(ClientPacket.Data.Span);

            uint playerID = reader.Read<uint>();
            string name = reader.ReadString(Encoding.UTF8, reader.Read<int>());

            GroupManager.InviteCharacterToGroup(session, playerID, name);
        }
    }
}
