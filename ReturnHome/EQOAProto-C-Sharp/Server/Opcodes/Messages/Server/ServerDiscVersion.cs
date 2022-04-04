using System;
using ReturnHome.Server.Network;
using ReturnHome.Utilities;

namespace ReturnHome.Server.Opcodes.Messages.Server
{
    class ServerDiscVersion
    {
        public static void DiscVersion(Session session, int GameVersion)
        {
            Memory<byte> temp = new byte[6];
            Span<byte> Message = temp.Span;

            BufferWriter writer = new BufferWriter(Message);

            ///Need to send this back to client
            writer.Write((ushort)GameOpcode.DiscVersion);
            writer.Write(GameVersion);

            ///Handles packing message into outgoing packet
            SessionQueueMessages.PackMessage(session, temp, MessageOpcodeTypes.ShortReliableMessage);
        }
    }
}
