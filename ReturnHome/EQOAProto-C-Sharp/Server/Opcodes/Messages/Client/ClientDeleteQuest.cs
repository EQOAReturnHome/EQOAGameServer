using System;
using ReturnHome.Server.EntityObject.Player;
using ReturnHome.Server.Network;
using ReturnHome.Utilities;

namespace ReturnHome.Server.Opcodes.Messages.Client
{
    class ClientDeleteQuest
    {
        public static void DeleteQuest(Session session, PacketMessage clientPacket)
        {
            BufferReader reader = new(clientPacket.Data.Span);

            uint unknownSection2 = reader.Read<uint>();
            int questIndex = (int)reader.Read7BitEncodedInt64();

            Quest.DeleteQuest(session, questIndex);
        }
    }
}
