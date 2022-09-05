using System.Text;
using ReturnHome.Server.EntityObject.Group;
using ReturnHome.Server.EntityObject.Player;
using ReturnHome.Server.Network;
using ReturnHome.Utilities;

namespace ReturnHome.Server.Opcodes.Messages.Server
{
    public static class ServerGroup
    {
        public static void CreateGroup(Session session, Group g)
        {
            Message message = Message.Create(MessageType.ReliableMessage, GameOpcode.CreateGroup);
            BufferWriter writer = new BufferWriter(message.Span);

            writer.Write(message.Opcode);
            writer.Write(g.GroupID);
            writer.Write(g.Size);

            foreach(Character c in g.GroupList)
                writer.Write(c.ObjectID);

            foreach (Character c in g.GroupList)
                writer.WriteString(Encoding.UTF8, c.CharName);

            message.Size = writer.Position;
            session.sessionQueue.Add(message);
        }

        public static void DisbandGroup(Session session, Group g)
        {
            Message message = Message.Create(MessageType.ReliableMessage, GameOpcode.CreateGroup);
            BufferWriter writer = new BufferWriter(message.Span);

            writer.Write(message.Opcode);
            writer.Write(0);
            writer.Write(4);
            for (int i = 0; i < 4; i++)
                writer.Write(0);

            for (int i = 0; i < 4; i++)
                writer.Write(0);

            //foreach (Character c in g.GroupList)
            //writer.Write(0);

            //foreach (Character c in g.GroupList)
            //writer.WriteString(Encoding.UTF8, "");

            message.Size = writer.Position;

            session.sessionQueue.Add(message);
        }

        public static void PlayerAcceptedInvite(Session session, Character c)
        {
            Message message = Message.Create(MessageType.ReliableMessage, GameOpcode.GroupInviteAcceptedMessage);
            BufferWriter writer = new BufferWriter(message.Span);

            writer.Write(message.Opcode);
            writer.WriteString(Encoding.UTF8, c.CharName);

            message.Size = writer.Position;
            session.sessionQueue.Add(message);
        }
    }
}
