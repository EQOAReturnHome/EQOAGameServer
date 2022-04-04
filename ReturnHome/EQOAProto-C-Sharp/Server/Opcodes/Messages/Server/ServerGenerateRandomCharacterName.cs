using System;
using System.Text;
using ReturnHome.Server.Network;
using ReturnHome.Utilities;

namespace ReturnHome.Server.Opcodes.Messages.Server
{
    class ServerGenerateRandomCharacterName
    {
        public static void GenerateRandomCharacterName(Session MySession)
        {
            string Name = RandomName.GenerateName();

            //Maybe a check here to verify name isn't taken in database before sending to client?

            Memory<byte> temp = new Memory<byte>(new byte[2 + 4 + (Name.Length * 2)]);
            Span<byte> Message = temp.Span;

            BufferWriter writer = new BufferWriter(Message);

            writer.Write((ushort)GameOpcode.RandomName);
            writer.Write(Name.Length);
            writer.WriteString(Encoding.UTF8, Name);

            //Send Message
            SessionQueueMessages.PackMessage(MySession, temp, MessageOpcodeTypes.ShortReliableMessage);
        }
    }
}
