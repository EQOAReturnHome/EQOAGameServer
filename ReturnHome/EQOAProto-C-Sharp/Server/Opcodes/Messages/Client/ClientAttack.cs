using System;
using ReturnHome.Server.EntityObject;
using ReturnHome.Server.EntityObject.Items;
using ReturnHome.Server.Managers;
using ReturnHome.Server.Network;
using ReturnHome.Server.Opcodes.Messages.Server;
using ReturnHome.Utilities;

namespace ReturnHome.Server.Opcodes.Messages.Client
{
    public static class ClientAttack
    {
        public static void ClientProcessAttack(Session session, PacketMessage ClientPacket)
        {
            BufferReader reader = new(ClientPacket.Data.Span);
            uint targetID = reader.Read<uint>();
            if (EntityManager.QueryForEntity(targetID, out Entity npc))
            {
                npc.takeDamage(500);
                /*if (npc.Inventory != null)
                {
                    foreach (Item entry in npc.Inventory.itemContainer.Values)
                    {
                        Console.WriteLine(entry.ItemName);
                        Console.WriteLine(entry.StackLeft);

                        
                    }
                }*/
                if (npc.CurrentHP <= 0)
                {
                    npc.Animation = 0x0e;
                  
                    
                    ServerLoot.ServerLootBox(session, npc);
                }


            }
        }
    }
}
