// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the MIT license.

using System;
using System.Buffers.Binary;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ReturnHome.Server.Network;
using ReturnHome.Utilities;

namespace ReturnHome.Opcodes
{
    public static class ChatMessage
    {

        public static void ProcessClientChat(Session MySession, PacketMessage ClientPacket)
        {

            int offset = 0;

            int messageLength = BinaryPrimitives.ReadInt32LittleEndian(ClientPacket.Data.Span[0..]);
            string message = Encoding.Unicode.GetString(ClientPacket.Data.Span[4..(4 + messageLength * 2)]);

            if (message == "!c")
            {
                MySession.CoordinateUpdate();
            }

            if(message == "!t")
            {
                if(ObjectAdminChecks.ProcessChanges(MySession, message.Split(' ')))
                {
                    //string[] words = message.Split(' ');
                    message = "Completed processing request";
                    GenerateClientSpecificChat(MySession, message);
                }

                else
                {
                    //string[] words = message.Split(' ');
                    message = "Error Occured";
                    GenerateClientSpecificChat(MySession, message);
                }
            }
            if (message == "!o")
            {
                MySession.unkOpcode ^= true;
                if (MySession.unkOpcode)
                {
                    message = "Unknown opcode display is now on.";
                }

                else
                {
                    message = "Unknown opcode display is now off.";
                }

                GenerateClientSpecificChat(MySession, message);
            }

            if (message == "!s")
            {
                float speed;
                try
                {
                    speed = float.Parse(message.Substring(3, messageLength - 3));
                    ProcessOpcode.ActorSpeed(MySession, speed);
                }

                catch
                {
                    message = "Not a valid value for speed";
                    GenerateClientSpecificChat(MySession, message);
                }
            }
        }

        public static void GenerateClientSpecificChat(Session MySession, string message)
        {
            int offset = 0;

            Memory<byte> temp = new Memory<byte>(new byte[6 + (message.Length * 2)]);
            Span<byte> Message = temp.Span;
            Message.Write((ushort)GameOpcode.ClientMessage, ref offset);
            Message.Write(message.Length, ref offset);
            Message.Write(Encoding.Unicode.GetBytes(message), ref offset);

            //Send Message
            SessionQueueMessages.PackMessage(MySession, temp, MessageOpcodeTypes.ShortReliableMessage);
        }

        public static void ProcessSayChat(Session MySession, PacketMessage ClientPacket)
        {
            //This would process messages designated for "say" from a client, which we could add some pre-processing here... but should distribute to players within x range technically
        }

        public static void ProcessShoutChat(Session MySession, PacketMessage ClientPacket)
        {
            //Would Process shout chat, and query the quad tree for the range to distribute the chat too
        }

        public static void ProcessTells(Session MySession, PacketMessage ClientPacket)
        {
            //Would take client message designated for a specific player, sending them a message if they are online
        }

        public static void ProcessGuildChat(Session MySession, PacketMessage ClientPacket)
        {
            //Processes client chat bound for their guild
        }

        public static void ProcessGroupChat(Session MySession, PacketMessage ClientPacket)
        {
            //Processes client messages bound for group chat
        }

        public static void DistributeSpecificMessageAndColor(Session MySession, PacketMessage ClientPacket)
        {
            //This method could
        }
    }
}
