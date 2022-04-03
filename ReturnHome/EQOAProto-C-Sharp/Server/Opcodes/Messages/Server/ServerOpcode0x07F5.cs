using System;
using ReturnHome.Server.Network;
using ReturnHome.Utilities;

namespace ReturnHome.Server.Opcodes.Messages.Server
{
    class ServerOpcode0x07F5
    {
        public static void Opcode0x07F5(Session session)
        {
            int offset = 0;
            Memory<byte> temp2 = new byte[6];
            Span<byte> Message2 = temp2.Span;
            Message2.Write(BitConverter.GetBytes((ushort)GameOpcode.Camera2), ref offset);
            Message2.Write(new byte[] { 0x1B, 0x00, 0x00, 0x00 }, ref offset);
            ///Handles packing message into outgoing packet
            SessionQueueMessages.PackMessage(session, temp2, MessageOpcodeTypes.ShortReliableMessage);
        }
    }
}
