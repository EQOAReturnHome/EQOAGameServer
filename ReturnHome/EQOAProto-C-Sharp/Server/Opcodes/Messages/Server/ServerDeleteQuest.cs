using System;
using ReturnHome.Server.Network;
using ReturnHome.Utilities;

namespace ReturnHome.Server.Opcodes.Messages.Server
{
    class ServerDeleteQuest
    {
        public static void DeleteQuest(Session mySession, byte questNumber)
        {
            int offset = 0;

            Memory<byte> temp = new Memory<byte>(new byte[8]);
            Span<byte> Message = temp.Span;

            Message.Write((ushort)GameOpcode.DeleteQuest, ref offset);
            Message.Write(new byte[4], ref offset);
            Message.Write(questNumber, ref offset);


            SessionQueueMessages.PackMessage(mySession, temp, MessageOpcodeTypes.ShortReliableMessage);
        }
    }
}
