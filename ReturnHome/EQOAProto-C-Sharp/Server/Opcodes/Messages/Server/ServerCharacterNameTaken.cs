// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the MIT license.

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ReturnHome.Server.Network;
using ReturnHome.Utilities;

namespace ReturnHome.Server.Opcodes.Messages.Server
{
    class ServerCharacterNameTaken
    {
        public static void CharacterNameTaken(Session session)
        {
            int offset = 0;
            Memory<byte> temp2 = new byte[2];
            Span<byte> Message = temp2.Span;
            Message.Write((ushort)GameOpcode.NameTaken, ref offset);

            //Log character name taken and send out RDP message to pop up that name is taken.
            //Console.WriteLine("Character Name Already Taken");                //Send Message
            SessionQueueMessages.PackMessage(session, temp2, MessageOpcodeTypes.ShortReliableMessage);
        }
    }
}
