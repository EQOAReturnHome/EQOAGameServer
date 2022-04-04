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
            Memory<byte> temp = new Memory<byte>(new byte[11 + (questText.Length * 2)]);
            Span<byte> Message = temp.Span;

            BufferWriter writer = new BufferWriter(Message);

            writer.Write((ushort)GameOpcode.AddQuestLog);
            writer.Write(questNumber);
            writer.WriteString(Encoding.Unicode, questText);

            SessionQueueMessages.PackMessage(mySession, temp, MessageOpcodeTypes.ShortReliableMessage);
        }
    }
}
