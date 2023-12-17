using ReturnHome.Server.Network;
using ReturnHome.Utilities;

namespace ReturnHome.Server.Opcodes.Messages.Server
{
    public static class ServerFaction
    {
        public static void ServerSendFaction(Session session, byte SubOpcode, int CMtoInspect)
        {
            Message message = Message.Create(MessageType.ReliableMessage, GameOpcode.ClassMasteryServer);
            BufferWriter writer = new BufferWriter(message.Span);

            writer.Write(message.Opcode);
            switch (SubOpcode)
            {
                //Server responser to Client CM% set request
                case 1:
                    writer.Write<byte>(3);
                    writer.Write(1); //counter?
                    writer.Write(100); //CM % to set, 0 - 100
                    break;

                case 3:
                    writer.Write<byte>(3);
                    writer.Write(1000); //Usable CM's
                    writer.Write(1001); //Total CM's
                    break;

                //Send Xp earned towards a CM
                case 4:
                    writer.Write<byte>(4);
                    writer.Write(0x00098850);
                    break;

                //CM Menu request response
                case 8:
                    break;

                //Response to inspecting/viewing CM Requirements
                case 9:
                    writer.Write<byte>(9);
                    InspectCM(session, ref writer, CMtoInspect);
                    break;

            }

            message.Size = writer.Position;
            session.sessionQueue.Add(message);
        }
        public static void InspectCM(Session session, ref BufferWriter writer, int CMtoInspect)
        {
            writer.Write<byte>(9);

            /*
            writer.Write(message.Opcode);
            writer.Write<byte>(4);
            writer.Write(0x00098850);
            */

            writer.Write(1); //message counter? Or CM identifier
            writer.Write7BitEncodedInt64(CMtoInspect - 10);
            writer.Write<byte>(0); //0 Buy 1 Remove
            writer.Write<byte>(0x1); //0 Can't buy 1 can buy
            writer.Write7BitEncodedInt64(CMtoInspect); //CM being inspect index as a packed value //0 = Inspect, -1 = Requirements to unlock for ??? CMs
            writer.Write7BitEncodedInt64(0x6a); //packed
            writer.Write<byte>(0); //0 No Masterclass 1 Masterclass set
            writer.Write<byte>(3); //byte
            writer.Write<byte>(9); //Level Requirement
            writer.Write7BitEncodedInt64(9); //packed

            //Related to Pre-bought CM's?
            writer.Write7BitEncodedInt64(4); //POINTS SPENT
            writer.Write7BitEncodedInt64(8); //BASE STR REQUIRED
            writer.Write7BitEncodedInt64(9); //BASE STA REQUIRED
            writer.Write7BitEncodedInt64(10); //BASE AGI REQUIRED
            writer.Write7BitEncodedInt64(11); //BASE DEX REQUIRED
            writer.Write7BitEncodedInt64(12); //BASE WIS REQUIRED
            writer.Write7BitEncodedInt64(13); //BASE INT REQUIRED
            writer.Write7BitEncodedInt64(14); //BASE CHA REQUIRED
                                              //END

            //Related to bought CM's?
            writer.WriteString(System.Text.Encoding.Unicode, ""); //String
            writer.Write7BitEncodedInt64(0); //packed
            writer.WriteString(System.Text.Encoding.Unicode, "stuff2"); //CM Title
            writer.WriteString(System.Text.Encoding.Unicode, "fudge"); // Description
            /*
             * 0 = STR
             * -1 = STA
             * 1 = AGI
             * -2 = DEX
             * 2 = WIS
             * -3 = INT
             * 3 = CHA
             * -4 = STR MAX
             * 4 = STA MAX
             * -5 = AGI MAX
             * 5 = DEX MAX
             * -6 = WIS MAX
             * 6 = INT MAX
             * -7 = CHA MAX
             * 7 = HP MAX
             * -8 = PWR MAX
             * 8 = HOT
             * -9 = POT
             * 9 = AC
             * -10 = OFF MOD
             * 10 = DEF MOD
             * -11 = HP FACTOR
             * 11 = FIRE RESIST
             * -12 = LIGHTNI(NG RESIST
             * 12 = COLD RESIST
             * -13 = ARCANE RESIST
             * 13 = POISON RESIST
             * -14 = DISEASE RESIST
             * 14 = MOVEMENT RATE
            */

            writer.Write7BitEncodedInt64(0); //Stat related entries
                                             //writer.Write7BitEncodedInt64(-10);
            writer.Write7BitEncodedInt64(2); //Add / Remove Spells
            writer.WriteString(System.Text.Encoding.Unicode, "Ultra Instinct 2");
            writer.Write<byte>(0); // 0 = Remove, 1 = Add
            writer.WriteString(System.Text.Encoding.Unicode, "Ultra Instinct 3");
            writer.Write<byte>(1);
            writer.Write<byte>(0);
        }
    }
}
