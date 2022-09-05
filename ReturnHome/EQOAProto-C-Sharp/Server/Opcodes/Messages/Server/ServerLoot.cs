// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the MIT license.

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ReturnHome.Server.EntityObject;
using ReturnHome.Server.EntityObject.Items;
using ReturnHome.Server.EntityObject.Player;
using ReturnHome.Server.Managers;
using ReturnHome.Server.Network;
using ReturnHome.Utilities;

namespace ReturnHome.Server.Opcodes.Messages.Server
{
    public static class ServerLoot
    {
        public static void ServerLootBox(Session session, Entity npc)
        {
            Message message = new Message(MessageType.SegmentReliableMessage, GameOpcode.Loot);
            BufferWriter writer = new BufferWriter(message.Span);

            writer.Write(message.Opcode);
            writer.Write7BitEncodedInt64(npc.Inventory == null ? 0 : npc.Inventory.Count);
            writer.Write(npc.Inventory == null ? 0 : npc.Inventory.Count);
            if (npc.Inventory != null)
            {
                foreach (Item entry in npc.Inventory.itemContainer.Values)
                    entry.DumpItem(ref writer);
            }
            message.Size = writer.Position;
            session.sessionQueue.Add(message);
        }

        public static void ServerLootItem(Session session, byte clientIndex, int itemQty)
        {
            if (EntityManager.QueryForEntity(session.MyCharacter.Target, out Entity ent))
            {
                ent.Inventory.RetrieveItem(clientIndex, out Item item);



                Item newItem = item.AcquireItem(itemQty);

                Console.WriteLine(newItem.ItemName);
                session.MyCharacter.Inventory.AddItem(newItem);


                ServerAddInventoryItemQuantity.AddInventoryItemQuantity(session, newItem);
            }
            //Sends 
            Message message = Message.Create(MessageType.ReliableMessage, GameOpcode.ClientLoot);
            BufferWriter writer = new BufferWriter(message.Span);

            writer.Write(message.Opcode);
            writer.Write(clientIndex);
            writer.Write((byte)1);
            message.Size = writer.Position;
            session.sessionQueue.Add(message);
        }
    }
}
