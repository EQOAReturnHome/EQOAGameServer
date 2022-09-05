// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the MIT license.

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ReturnHome.Server.Managers;
using ReturnHome.Server.Network;

namespace ReturnHome.Server.Opcodes.Messages.Client
{
    public static class ClientDisbandGroup
    {
        public static void DisbandGroup(Session session, PacketMessage ClientPacket)
        {
            /*Shouldn't be anything to read, just the opcode*/
            //->
            GroupManager.DisbandGroup(session);
        }
    }
}
