using System.Text;
using ReturnHome.Database.SQL;
using ReturnHome.Server.EntityObject.Player;
using ReturnHome.Server.Network;
using ReturnHome.Server.Opcodes.Messages.Server;
using ReturnHome.Utilities;

namespace ReturnHome.Server.Opcodes.Messages.Client
{
    class ClientCreateCharacter
    {
        //Method to create new character when new character opcode is received
        public static void CreateCharacter(Session session, PacketMessage ClientPacket)
        {
            CharacterSQL createCharacter = new CharacterSQL();
            BufferReader reader = new(ClientPacket.Data.Span);

            //Get length of characters name expected in packet
            int nameLength = reader.Read<int>();

            //Get Character Name
            string CharName = reader.ReadString(Encoding.UTF8, nameLength);

            //Before processing a full character creation check if the characters name already exists in the DB.
            //Later this will need to include a character/world combination if additional servers are spun up.
            if (CharName == createCharacter.CheckName(CharName))
            {
                //Close SQL connection
                createCharacter.CloseConnection();
                ServerCharacterNameTaken.CharacterNameTaken(session);
            }

            //If name not found continue to actually create character
            else
            {
                //Create NewCharacter object
                Character charCreation = new Character();

                charCreation.CharName = CharName;
                //Get starting level
                charCreation.Level = (int)reader.Read7BitEncodedInt64();

                //Divide startLevel by 2 because client doubles it
                //Get single byte attributes
                charCreation.Race = (int)reader.Read7BitEncodedInt64();
                charCreation.StartingClass = (int)reader.Read7BitEncodedInt64();
                charCreation.Gender = (int)reader.Read7BitEncodedInt64();
                charCreation.HairColor = (int)reader.Read7BitEncodedInt64();
                charCreation.HairLength = (int)reader.Read7BitEncodedInt64();
                charCreation.HairStyle = (int)reader.Read7BitEncodedInt64();
                charCreation.FaceOption = (int)reader.Read7BitEncodedInt64();
                charCreation.HumTypeNum = (int)reader.Read7BitEncodedInt64();

                //Get player attributes from packet and remove bytes after reading into variable
                charCreation.AddStrength = reader.Read<int>();
                charCreation.AddStamina = reader.Read<int>();
                charCreation.AddAgility = reader.Read<int>();
                charCreation.AddDexterity = reader.Read<int>();
                charCreation.AddWisdom = reader.Read<int>();
                charCreation.AddIntelligence = reader.Read<int>();
                charCreation.AddCharisma = reader.Read<int>();

                //Call SQL method for character creation
                createCharacter.CreateCharacter(session, charCreation);

                //CLose SQL connection
                createCharacter.CloseConnection();
            }
        }
    }
}
