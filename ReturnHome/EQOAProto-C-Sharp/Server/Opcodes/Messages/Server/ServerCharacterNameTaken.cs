using System;
using ReturnHome.Server.Network;
using ReturnHome.Utilities;

namespace ReturnHome.Server.Opcodes.Messages.Server
{
    class ServerCharacterNameTaken
    {
        public static void CharacterNameTaken(Session session)
        {
            Memory<byte> temp = new byte[2];
            Span<byte> Message = temp.Span;

            BufferWriter writer = new BufferWriter(Message);

            writer.Write((ushort)GameOpcode.NameTaken);

            //Log character name taken and send out RDP message to pop up that name is taken.
            //Console.WriteLine("Character Name Already Taken");                //Send Message
            SessionQueueMessages.PackMessage(session, temp, MessageOpcodeTypes.ShortReliableMessage);
        }
    }
}
