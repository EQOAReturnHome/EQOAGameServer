using System;
using ReturnHome.Server.Network;
using ReturnHome.Utilities;

namespace ReturnHome.Server.Opcodes.Messages.Server
{
    public class ServerTime
    {
        public static void Time(Session session)
        {
            Memory<byte> temp = new byte[18];
            Span<byte> Message = temp.Span;

            BufferWriter writer = new BufferWriter(Message);

            //Get our timestamp opcode in queue
            writer.Write((ushort)GameOpcode.Time);
            writer.Write(DNP3Creation.CreateDNP3TimeStamp());

            SessionQueueMessages.PackMessage(session, temp, MessageOpcodeTypes.ShortReliableMessage);
        }
    }
}
