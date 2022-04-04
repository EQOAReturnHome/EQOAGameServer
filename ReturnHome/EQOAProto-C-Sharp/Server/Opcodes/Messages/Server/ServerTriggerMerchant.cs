using System;
using System.IO;
using ReturnHome.Server.EntityObject;
using ReturnHome.Server.EntityObject.Player;
using ReturnHome.Server.Network;
using ReturnHome.Utilities;

namespace ReturnHome.Server.Opcodes.Messages.Server
{
    class ServerTriggerMerchant
    {
        public static void TriggerMerchant(Session session, Entity npc) //uint targetNPC)
        {
            int unknownInt = 200;

            Memory<byte> buffer;
            using (MemoryStream memStream = new())
            {
                //memStream.Write(BitConverter.GetBytes((ushort)GameOpcode.MerchantBox));
                memStream.Write(BitConverter.GetBytes((ushort)GameOpcode.MerchantBox));
                memStream.Write(BitConverter.GetBytes(npc.ObjectID));
                memStream.Write(Utility_Funcs.DoublePack(unknownInt));
                memStream.Write(Utility_Funcs.DoublePack(unknownInt));
                memStream.Write(Utility_Funcs.DoublePack(npc.Inventory.Count));
                memStream.Write(BitConverter.GetBytes(npc.Inventory.Count));
                foreach (Item entry in npc.Inventory.itemContainer.Values)
                    entry.DumpItem(memStream);

                long pos = memStream.Position;
                buffer = new Memory<byte>(memStream.GetBuffer(), 0, (int)pos);

                SessionQueueMessages.PackMessage(session, buffer, MessageOpcodeTypes.ShortReliableMessage);
                memStream.Flush();
            }
        }
    }
}
