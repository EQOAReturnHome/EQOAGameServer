using System;
using ReturnHome.Server.EntityObject;
using ReturnHome.Server.EntityObject.Player;
using ReturnHome.Server.Network;
using ReturnHome.Utilities;

namespace ReturnHome.Server.Opcodes.Messages.Server
{
    class ServerTriggerMerchantMenu
    {
        public static void TriggerMerchantMenu(Session session, Entity npc)
        {
            int unknownInt = 200;

            Memory<byte> temp = new byte[5000];
            Span<byte> Message = temp.Span;

            BufferWriter writer = new BufferWriter(Message);

            writer.Write((ushort)GameOpcode.MerchantBox);
            writer.Write(npc.ObjectID);
            writer.Write7BitEncodedInt64(unknownInt);
            writer.Write7BitEncodedInt64(unknownInt);
            writer.Write7BitEncodedInt64(npc.Inventory.Count);
            writer.Write(npc.Inventory.Count);
            foreach (Item entry in npc.Inventory.itemContainer.Values)
                entry.DumpItem(ref writer);

            Memory<byte> buffer = temp.Slice(0, writer.Position);

            SessionQueueMessages.PackMessage(session, buffer, MessageOpcodeTypes.ShortReliableMessage);
        }
    }
}
