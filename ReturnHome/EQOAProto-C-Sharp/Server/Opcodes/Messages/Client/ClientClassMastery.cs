using System;
using ReturnHome.Server.Network;
using ReturnHome.Server.Opcodes.Messages.Server;
using ReturnHome.Utilities;

namespace ReturnHome.Server.Opcodes.Messages.Client
{
    public static class ClientClassMastery
    {
        public static void ProcessClassMastery(Session session, Message ClientPacket)
        {
            BufferReader reader = new(ClientPacket.message.Span);

            byte subOpcode = reader.Read<byte>();
            int counter = reader.Read<int>();
            Console.WriteLine($"SubOpcode: {subOpcode}");
            switch (subOpcode)
            {
                //set cm xp%
                case 1:
                    reader.Read<int>(); //CM % set
                    break;

                //Buy CM
                case 2:
                    int CMToBuy = reader.Read<int>(); //Assuming this should relate to the CM we are buying, only getting 0's so far. PCAP's show values here
                    Console.WriteLine($"Buying CM: {CMToBuy}");
                    break;

                //Remove CM
                case 3:
                    int CMToRemove = reader.Read<int>(); //Assuming this should relate to the CM we are removing, only getting 0's so far
                    Console.WriteLine($"Removing CM: {CMToRemove}");
                    break;
                //request cm menu
                case 4:
                    ServerCMMenu.SendCMMenu(session);
                    break;

                //Inspect CM / View Requirements to unlock CM
                case 5:
                    int CMToInspect = reader.Read<int>();
                    Console.WriteLine($"Inspecting CM: {CMToInspect}");
                    ServerFaction.ServerSendFaction(session, 5, CMToInspect);
                    break;

                default:
                    Console.WriteLine($"SubOpcode: {subOpcode}");
                    break;
            }
            //Ensures that we don't get locked into anything in the CM menu
            /*Message message = Message.Create(MessageType.ReliableMessage, GameOpcode.ClassMasteryServer);
            BufferWriter writer = new BufferWriter(message.Span);

            writer.Write(message.Opcode);
            writer.Write<byte>(2);

            message.Size = writer.Position;
            session.sessionQueue.Add(message);*/
        }
    }
}

