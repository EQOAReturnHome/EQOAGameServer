// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the MIT license.

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ReturnHome.Server.Network;
using ReturnHome.Server.Opcodes.Messages.Server;
using ReturnHome.Utilities;

namespace ReturnHome.Server.Opcodes.Messages.Client
{
    public static class ClientLoot
    {

        public static void ClientLootItem(Session session, PacketMessage clientPacket)
        {
            BufferReader reader = new(clientPacket.Data.Span);

            byte itemSlot = (byte)reader.Read<int>();
            int itemQty = reader.Read<int>();

            Console.WriteLine(itemQty);

            ServerLoot.ServerLootItem(session, itemSlot, itemQty);

        }
        public static void ClientLootClose(Session session, PacketMessage clientPacket)
        {
            //Sends 
            Message message = Message.Create(MessageType.ReliableMessage, GameOpcode.ClientCloseLoot);
            BufferWriter writer = new BufferWriter(message.Span);

            writer.Write(message.Opcode);
            message.Size = writer.Position;
            session.sessionQueue.Add(message);
        }
    }
}
