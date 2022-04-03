using System;
using System.Text;
using ReturnHome.Server.Network;
using ReturnHome.Utilities;

namespace ReturnHome.Server.Opcodes.Messages.Server
{
    class ServerAddQuestLog
    {
        public static void AddQuestLog(Session mySession, uint questNumber, string questText)
        {
            int offset = 0;

            Memory<byte> temp = new Memory<byte>(new byte[11 + (questText.Length * 2)]);
            Span<byte> Message = temp.Span;

            Message.Write((ushort)GameOpcode.AddQuestLog, ref offset);
            Message.Write(questNumber, ref offset);
            Message.Write(questText.Length, ref offset);
            Message.Write(Encoding.Unicode.GetBytes(questText), ref offset);
            SessionQueueMessages.PackMessage(mySession, temp, MessageOpcodeTypes.ShortReliableMessage);
        }
    }
}
