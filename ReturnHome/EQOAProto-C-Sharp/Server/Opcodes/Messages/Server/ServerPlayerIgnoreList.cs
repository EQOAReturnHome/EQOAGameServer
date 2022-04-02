using System;
using ReturnHome.Server.Network;
using ReturnHome.Utilities;

namespace ReturnHome.Server.Opcodes.Messages.Server
{
    class ServerPlayerIgnoreList
    {
        public static void PlayerIgnoreList(Session session)
        {
            int offset = 0;
            Memory<byte> temp = new byte[3];
            Span<byte> Message = temp.Span;
            Message.Write(BitConverter.GetBytes((ushort)GameOpcode.IgnoreList), ref offset);
            Message.Write(new byte[] { 0 }, ref offset);
            //For now send no ignored people
            SessionQueueMessages.PackMessage(session, temp, MessageOpcodeTypes.ShortReliableMessage);
        }
    }
}
