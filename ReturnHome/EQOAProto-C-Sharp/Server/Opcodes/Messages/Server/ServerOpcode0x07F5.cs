using System;
using ReturnHome.Server.Network;
using ReturnHome.Utilities;

namespace ReturnHome.Server.Opcodes.Messages.Server
{
    class ServerOpcode0x07F5
    {
        public static void Opcode0x07F5(Session session)
        {
            Memory<byte> temp = new byte[6];
            Span<byte> Message = temp.Span;

            BufferWriter writer = new BufferWriter(Message);

            writer.Write((ushort)GameOpcode.Camera2);
            writer.Write(0x1B);
            ///Handles packing message into outgoing packet
            SessionQueueMessages.PackMessage(session, temp, MessageOpcodeTypes.ShortReliableMessage);
        }
    }
}
