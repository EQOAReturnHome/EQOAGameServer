using System;
using ReturnHome.Server.Network;
using ReturnHome.Utilities;

namespace ReturnHome.Server.Opcodes.Messages.Server
{
    class ServerOpcode0x07D1
    {
        public static void Opcode0x07D1(Session session)
        {
            Memory<byte> temp = new byte[6];
            Span<byte> Message = temp.Span;

            BufferWriter writer = new BufferWriter(Message);

            writer.Write((ushort)GameOpcode.Camera1);
            writer.Write(0x03);

            ///Handles packing message into outgoing packet
            SessionQueueMessages.PackMessage(session, temp, MessageOpcodeTypes.ShortReliableMessage);
        }
    }
}
