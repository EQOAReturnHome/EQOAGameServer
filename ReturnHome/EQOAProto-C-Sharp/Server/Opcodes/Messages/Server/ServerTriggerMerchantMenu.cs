// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the MIT license.

using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
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
