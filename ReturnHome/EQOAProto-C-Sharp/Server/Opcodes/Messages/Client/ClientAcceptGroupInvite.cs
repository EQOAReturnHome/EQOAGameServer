using ReturnHome.Server.Managers;
using ReturnHome.Server.Network;
using ReturnHome.Utilities;

namespace ReturnHome.Server.Opcodes.Messages.Client
{
    internal class ClientAcceptGroupInvite
    {
        public static void AcceptGroupInvite(Session session, PacketMessage ClientPacket)
        {
            BufferReader reader = new(ClientPacket.Data.Span);

            uint playerID = reader.Read<uint>();

            GroupManager.AcceptInviteToGroup(session, playerID);
        }
    }
}
