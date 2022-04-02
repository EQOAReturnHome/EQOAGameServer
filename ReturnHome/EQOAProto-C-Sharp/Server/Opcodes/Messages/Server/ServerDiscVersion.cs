using System;
using ReturnHome.Server.Network;
using ReturnHome.Utilities;

namespace ReturnHome.Server.Opcodes.Messages.Server
{
    class ServerDiscVersion
    {
        public static void DiscVersion(Session session, int GameVersion)
        {
            int offset = 0;

            Memory<byte> temp2 = new byte[6];
            Span<byte> Message = temp2.Span;
            ///Need to send this back to client
            Message.Write(BitConverter.GetBytes((ushort)GameOpcode.DiscVersion), ref offset);
            Message.Write(BitConverter.GetBytes((uint)GameVersion), ref offset);

            ///Handles packing message into outgoing packet
            SessionQueueMessages.PackMessage(session, temp2, MessageOpcodeTypes.ShortReliableMessage);
        }
    }
}
