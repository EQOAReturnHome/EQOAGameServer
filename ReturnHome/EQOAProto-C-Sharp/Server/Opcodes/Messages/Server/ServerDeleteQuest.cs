using System;
using ReturnHome.Server.Network;
using ReturnHome.Utilities;

namespace ReturnHome.Server.Opcodes.Messages.Server
{
    class ServerDeleteQuest
    {
        public static void DeleteQuest(Session mySession, byte questNumber)
        {
            Memory<byte> temp = new byte[8];
            Span<byte> Message = temp.Span;

            BufferWriter writer = new BufferWriter(Message);

            writer.Write((ushort)GameOpcode.DeleteQuest);
            writer.Write(0);
            writer.Write(questNumber);


            SessionQueueMessages.PackMessage(mySession, temp, MessageOpcodeTypes.ShortReliableMessage);
        }
    }
}
