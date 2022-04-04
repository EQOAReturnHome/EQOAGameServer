using System;
using ReturnHome.Server.Network;
using ReturnHome.Utilities;

namespace ReturnHome.Server.Opcodes.Messages.Server
{
    class ServerPlayerSpeed
    {
        public static void PlayerSpeed(Session MySession)
        {
            Memory<byte> temp = new byte[6];
            Span<byte> Message = temp.Span;

            BufferWriter writer = new BufferWriter(Message);

            writer.Write((ushort)GameOpcode.PlayerSpeed);
            writer.Write(25.0f);

            //For now send a standard speed
            SessionQueueMessages.PackMessage(MySession, temp, MessageOpcodeTypes.ShortReliableMessage);
        }

        public static void PlayerSpeed(Session MySession, float speed)
        {
            Memory<byte> temp = new byte[6];
            Span<byte> Message = temp.Span;

            BufferWriter writer = new BufferWriter(Message);

            writer.Write((ushort)GameOpcode.PlayerSpeed);
            writer.Write(speed);

            //For now send a standard speed
            SessionQueueMessages.PackMessage(MySession, temp, MessageOpcodeTypes.ShortReliableMessage);
        }
    }
}
