using System;
using System.Text;
using ReturnHome.Server.EntityObject;
using ReturnHome.Server.Managers;
using ReturnHome.Server.Network;
using ReturnHome.Utilities;

namespace ReturnHome.Server.Opcodes.Messages.Server
{
    public static class ServerFaction
    {
        public static void ServerSendFaction(Session session)
        {
            Message message = Message.Create(MessageType.ReliableMessage, GameOpcode.BuddyListStuff);
            BufferWriter writer = new BufferWriter(message.Span);

            writer.Write(message.Opcode);

            writer.Write<byte>(3);
            writer.Write<byte>(0x88);
            writer.Write<byte>(2);
            writer.Write<ushort>(0);

            message.Size = writer.Position;
            session.sessionQueue.Add(message);
        }
    }
}
