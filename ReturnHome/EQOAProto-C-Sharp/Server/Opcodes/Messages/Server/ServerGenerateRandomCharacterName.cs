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

            int offset = 0;
            //Maybe a check here to verify name isn't taken in database before sending to client?

            Memory<byte> temp = new Memory<byte>(new byte[2 + 4 + (Name.Length * 2)]);
            Span<byte> Message = temp.Span;

            Message.Write((ushort)GameOpcode.RandomName, ref offset);
            Message.Write(Name.Length, ref offset);
            Message.Write(Encoding.Default.GetBytes(Name), ref offset);
            //Send Message
            SessionQueueMessages.PackMessage(MySession, temp, MessageOpcodeTypes.ShortReliableMessage);
        }
    }
}
