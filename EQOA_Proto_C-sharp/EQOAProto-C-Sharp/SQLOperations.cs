using MySql.Data.MySqlClient;
using SessManager;
using System;
using EQLogger;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using Utility;
using RdpComm;
using Opcodes;
using System.Threading;
using System.Timers;
using System.Threading.Tasks;
using Timer = System.Timers.Timer;
using System.Security.Policy;
using System.Linq.Expressions;
using System.ComponentModel;
using System.Configuration;

namespace EQOASQL
{

    public class Character
    {
        //List to hold gear values for model, color, and equip location
        public List<Tuple<uint, uint, byte>> GearList = new List<Tuple<uint, uint, byte>>();
        //Attributes a character may have
        public string CharName { get; set; }
        public int ServerID { get; set; }
        public long ModelID { get; set; }
        public int TClass { get; set; }
        public int Race { get; set; }
        public String HumType { get; set; }
        public int Level { get; set; }
        public int HairColor { get; set; }
        public int HairLength { get; set; }
        public int HairStyle { get; set; }
        public int FaceOption { get; set; }
        public int ClassIcon { get; set; }
        public int TotalXP { get; set; }
        public int Debt { get; set; }
        public int Breath { get; set; }
        public int Tunar { get; set; }
        public int BankTunar { get; set; }
        public int UnusedTP { get; set; }
        public int TotalTP { get; set; }
        public float XCoord { get; set; }
        public float YCoord { get; set; }
        public float Facing { get; set; }
        public int Strength { get; set; }
        public int Stamina { get; set; }
        public int Agility { get; set; }
        public int Dexterity { get; set; }
        public int Wisdom { get; set; }
        public int Intelligence { get; set; }
        public int Charisma { get; set; }
        public int CurrentHP { get; set; }
        public int MaxHP { get; set; }
        public int CurrentPower { get; set; }
        public int MaxPower { get; set; }
        public float ZCoord { get; set; }
        public int HealOT { get; set; }
        public int PowerOT { get; set; }
        public int Ac { get; set; }
        public int PoisonResist { get; set; }
        public int DiseaseResist { get; set; }
        public int FireResist { get; set; }
        public int ColdResist { get; set; }
        public int LightningResist { get; set; }
        public int ArcaneResist { get; set; }
        public int Fishing { get; set; }
        public int BaseStrength { get; set; }
        public int BaseStamina { get; set; }
        public int BaseAgility { get; set; }
        public int BaseDexterity { get; set; }
        public int BaseWisdom { get; set; }
        public int BaseIntelligence { get; set; }
        public int BaseCharisma { get; set; }
        public int CurrentHP2 { get; set; }
        public int BaseHP { get; set; }
        public int CurrentPower2 { get; set; }
        public int BasePower { get; set; }
        public int HealOT2 { get; set; }
        public int PowerOT2 { get; set; }

        ///Armor
        public byte Helm = 0;
        public byte Chest = 0;
        public byte Gloves = 0;
        public byte Bracer = 0;
        public byte Legs = 0;
        public byte Boots = 0;
        public uint Robe = 0xFFFFFFFF;
        public uint Primary = 0;
        public uint Secondary = 0;
        public uint Shield = 0;

        ///Armor color
        public uint HelmColor = 0xFFFFFFFF;
        public uint ChestColor = 0xFFFFFFFF;
        public uint GlovesColor = 0xFFFFFFFF;
        public uint BracerColor = 0xFFFFFFFF;
        public uint LegsColor = 0xFFFFFFFF;
        public uint BootsColor = 0xFFFFFFFF;
        public uint RobeColor = 0xFFFFFFFF;

        //Dictionary mapping the client value to the string value expected in the DB
        //Eastern Human = 1, Western Human = 2, All non humans = 0
        public readonly Dictionary<int, string> HumTypeDict = new Dictionary<int, string>
        {
            {0, "Other" },
            {1, "Freeport" },
            {2, "Qeynos" }
        };

        //Dictionary mapping the client value to the string value expected in the DB for Player Class
        public readonly Dictionary<int, string> CharClassDict = new Dictionary<int, string>
        {
            {0, "WAR" },  {1, "RAN" }, {2, "PAL" }, {3, "SK" }, {4, "MNK" },{5, "BRD" }, {6, "RGE" },
            {7, "DRD" }, {8, "SHA" }, {9, "CL" }, {10, "MAG" }, {11, "NEC" },{12, "ENC" }, {13, "WIZ" }, {14, "ALC" }
        };

        //Dictionary mapping the client value to the string value expected in the DB for Player Race
        public readonly Dictionary<int, string> CharRaceDict = new Dictionary<int, string>
        {
            {0, "HUM" }, {1, "ELF" }, {2, "DELF" }, {3, "GNO" }, {4, "DWF" }, {5, "TRL" }, {6, "BAR" }, {7, "HLF" },
            {8, "ERU" }, {9, "OGR" }
        };

        //Dictionary mapping the client value to the string value expected in the DB for Player Gender
        public readonly Dictionary<int, string> CharSexDict = new Dictionary<int, string>
        {
            {0, "Male" }, {1, "Female"}
        };

        //Overwrite to ToString Method to allow for direct enumeration of the object for characterName. Could be expanded for additional attributes.
        public override string ToString()
        {
            return "Character Data: " + CharName;
        }

    }

    //Inherited from Base Character class to satisfy unique needs of character creation only
    public class NewCharacter : Character
    {

        public string TestCharName { get; set; }
        public int StartingClass { get; set; }
        public int Gender { get; set; }
        //Note this is for holding the HumType from the client that is an int and base Character has a string HumType
        public new int HumType { get; set; }
        //Addxxxx attributes of the class are to hold a new characters initial allocated stat points in each category
        public int AddStrength { get; set; }
        public int AddStamina { get; set; }
        public int AddAgility { get; set; }
        public int AddDexterity { get; set; }
        public int AddWisdom { get; set; }
        public int AddIntelligence { get; set; }
        public int AddCharisma { get; set; }
        //Defaultxxx attributes of the class pulled from the defaultClass table in the DB for new character creation
        public int DefaultStrength { get; set; }
        public int DefaultStamina { get; set; }
        public int DefaultAgility { get; set; }
        public int DefaultDexterity { get; set; }
        public int DefaultWisdom { get; set; }
        public int DefaultIntelligence { get; set; }
        public int DefaultCharisma { get; set; }

    }

    //Creates character listing for character select screen
    public class ProcessCharacterList
    {
        ///Create Character Packet here
        public static void CreateCharacterList(List<Character> MyCharacterList, Session MySession)
        {
            //Holds list of characters pulled from the DB for the AccountID
            List<byte> CharacterList = new List<byte>();

            ///Gets our character count and uses technique to double it
            CharacterList.AddRange(Utility_Funcs.Technique((byte)MyCharacterList.Count()));

            //Iterates through each charcter in the list and converts attribute values to packet values
            foreach (Character character in MyCharacterList)
            {
                ///Add the Character name length
                CharacterList.AddRange(BitConverter.GetBytes((uint)character.CharName.Length));

                ///Add character name
                CharacterList.AddRange(Encoding.ASCII.GetBytes(character.CharName));

                ///Add Server ID
                CharacterList.AddRange(Utility_Funcs.Technique(character.ServerID));

                ///Add Model
                CharacterList.AddRange(Utility_Funcs.Technique((int)character.ModelID));

                ///Add Class
                CharacterList.AddRange(Utility_Funcs.Technique(character.TClass));

                ///Add Race
                CharacterList.AddRange(Utility_Funcs.Technique(character.Race));

                ///Add Level
                CharacterList.AddRange(Utility_Funcs.Technique(character.Level));

                ///Add Hair color
                CharacterList.AddRange(Utility_Funcs.Technique(character.HairColor));

                ///Add Hair Length
                CharacterList.AddRange(Utility_Funcs.Technique(character.HairLength));

                ///Add Hair Style
                CharacterList.AddRange(Utility_Funcs.Technique(character.HairStyle));

                ///Add Face option
                CharacterList.AddRange(Utility_Funcs.Technique(character.FaceOption));

                ///Start processing gear
                foreach (var Gear in character.GearList)
                {
                    ///Use a switch to sift through gear and add them properly
                    switch (Gear.Item3)
                    {
                        ///Helm
                        case 1:
                            character.Helm = (byte)Gear.Item1;
                            character.HelmColor = Gear.Item2;
                            break;

                        ///Robe
                        case 2:
                            character.Robe = (byte)Gear.Item1;
                            character.RobeColor = Gear.Item2;
                            break;

                        ///Gloves
                        case 19:
                            character.Gloves = (byte)Gear.Item1;
                            character.GlovesColor = Gear.Item2;
                            break;

                        ///Chest
                        case 5:
                            character.Chest = (byte)Gear.Item1;
                            character.ChestColor = Gear.Item2;
                            break;

                        ///Bracers
                        case 8:
                            character.Bracer = (byte)Gear.Item1;
                            character.BracerColor = Gear.Item2;
                            break;

                        ///Legs
                        case 10:
                            character.Legs = (byte)Gear.Item1;
                            character.LegsColor = Gear.Item2;
                            break;

                        ///Feet
                        case 11:
                            character.Boots = (byte)Gear.Item1;
                            character.BootsColor = Gear.Item2;
                            break;

                        ///Primary
                        case 12:
                            character.Primary = Gear.Item1;
                            break;

                        ///Secondary
                        case 14:

                            ///If we have a secondary equipped already, puts next secondary into primary slot
                            if (character.Secondary > 0)
                            {
                                character.Primary = Gear.Item1;
                            }

                            ///If no secondary, add to secondary slot
                            else
                            {
                                character.Secondary = Gear.Item1;
                            }
                            break;

                        ///2 Hand
                        case 15:
                            character.Primary = Gear.Item1;
                            break;

                        ///Shield
                        case 13:
                            character.Shield = Gear.Item1;
                            break;

                        ///Bow
                        case 16:
                            character.Primary = Gear.Item1;
                            break;

                        ///Thrown
                        case 17:
                            character.Primary = Gear.Item1;
                            break;

                        ///Held
                        case 18:
                            ///If we have a secondary equipped already, puts next secondary into primary slot
                            if (character.Secondary > 0)
                            {
                                character.Primary = Gear.Item1;
                            }

                            ///If no secondary, add to secondary slot
                            else
                            {
                                character.Secondary = Gear.Item1;
                            }
                            break;

                        default:
                            Logger.Err("Equipment not in list, this may need to be changed");
                            break;
                    }
                }

                ///Add Robe
                CharacterList.AddRange(BitConverter.GetBytes(character.Robe));

                ///Add Primary
                CharacterList.AddRange(BitConverter.GetBytes(character.Primary));

                ///Add Secondary
                CharacterList.AddRange(BitConverter.GetBytes(character.Secondary));

                ///Add Shield
                CharacterList.AddRange(BitConverter.GetBytes(character.Shield));

                ///Add Character animation here, dumby for now
                CharacterList.AddRange(BitConverter.GetBytes((ushort)0x0003));

                ///unknown value?
                CharacterList.Add((byte)0);

                ///Chest Model
                CharacterList.Add(character.Chest);

                ///uBracer Model
                CharacterList.Add(character.Bracer);

                ///Glove Model
                CharacterList.Add(character.Gloves);

                ///Leg Model
                CharacterList.Add(character.Legs);

                ///Boot Model
                CharacterList.Add(character.Boots);

                ///Helm Model
                CharacterList.Add(character.Helm);

                ///unknown value?
                CharacterList.AddRange(BitConverter.GetBytes((uint)0));

                ///unknown value?
                CharacterList.AddRange(BitConverter.GetBytes((ushort)0));

                ///unknown value?
                CharacterList.AddRange(BitConverter.GetBytes(0xFFFFFFFF));

                ///unknown value?
                CharacterList.AddRange(BitConverter.GetBytes(0xFFFFFFFF));

                ///unknown value?
                CharacterList.AddRange(BitConverter.GetBytes(0xFFFFFFFF));

                ///Chest color
                CharacterList.AddRange(BitConverter.GetBytes(ByteSwaps.SwapBytes(character.ChestColor)));

                ///Bracer color
                CharacterList.AddRange(BitConverter.GetBytes(ByteSwaps.SwapBytes(character.BracerColor)));

                ///Glove color
                CharacterList.AddRange(BitConverter.GetBytes(ByteSwaps.SwapBytes(character.GlovesColor)));

                ///Leg color
                CharacterList.AddRange(BitConverter.GetBytes(ByteSwaps.SwapBytes(character.LegsColor)));

                ///Boot color
                CharacterList.AddRange(BitConverter.GetBytes(ByteSwaps.SwapBytes(character.BootsColor)));

                ///Helm color
                CharacterList.AddRange(BitConverter.GetBytes(ByteSwaps.SwapBytes(character.HelmColor)));

                ///Robe color
                CharacterList.AddRange(BitConverter.GetBytes(ByteSwaps.SwapBytes(character.RobeColor)));

                ///Reset gear values to 0 for defaults
                ProcessCharacterList.ClearGear(character);

                Logger.Info($"Processed {character.CharName}");
            }
           
            ///Character list is complete
            ///Handles packing message into outgoing packet
            RdpCommOut.PackMessage(MySession, CharacterList, MessageOpcodeTypes.ShortReliableMessage, GameOpcode.CharacterSelect);

        }

        ///Clear gear here
        private static void ClearGear(Character character)
        {
            ///Armor
            character.Helm = 0;
            character.Chest = 0;
            character.Gloves = 0;
            character.Bracer = 0;
            character.Legs = 0;
            character.Boots = 0;
            character.Robe = 0;
            character.Primary = 0;
            character.Secondary = 0;
            character.Shield = 0;

            ///Armor color
            character.HelmColor = 0;
            character.ChestColor = 0;
            character.GlovesColor = 0;
            character.BracerColor = 0;
            character.LegsColor = 0;
            character.BootsColor = 0;
            character.RobeColor = 0;
        }

    }

    //Class to handle all SQL Operations
    class SQLOperations
    {
        //Holds list of characters for whole class
        private static List<Character> characterData = new List<Character>();

        //Connection string for SQL Queries. Needs to be moved to config file at some point
        //private static string cs = @"server=192.168.6.53;userid=fooUser;password=fooPass;database=localeqoa;Allow User Variables=True";

        //Class to pull characters from DB via serverid
        public static List<Character> AccountCharacters(Session MySession)
        {
            //Clears characterData previously queried
            characterData.Clear();
            var connectionString = ConfigurationManager.ConnectionStrings["DevLocal"].ConnectionString;

            //Set connection property from connection string and open connection
            using MySqlConnection con = new MySqlConnection(connectionString);
            con.Open();

            //Queries DB for all characters and their necessary attributes  to generate character select
            //Later should convert to a SQL stored procedure if possible.
            //Currently pulls ALL charcters, will pull characters based on accountID.
            using var cmd = new MySqlCommand("GetAccountCharacters", con);
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.AddWithValue("pAccountID", MySession.AccountID);
            using MySqlDataReader rdr = cmd.ExecuteReader();

            //string to hold local charcter Name
            string charName;

            //Read through results from query populating character data needed for character select
            while (rdr.Read())
            {
                //Instantiate new character object, not to be confused with a newly created character
                var newCharacter = new Character
                {

                    //Assign character variables from DB values
                    CharName = rdr.GetString(0),
                    ServerID = rdr.GetInt32(1),
                    ModelID = rdr.GetInt64(2),
                    TClass = rdr.GetInt32(3),
                    Race = rdr.GetInt32(4),
                    HumType = rdr.GetString(5),
                    Level = rdr.GetInt32(6),
                    HairColor = rdr.GetInt32(7),
                    HairLength = rdr.GetInt32(8),
                    HairStyle = rdr.GetInt32(9),
                    FaceOption = rdr.GetInt32(10)
                };

                //Add character attribute data to charaterData List
                Console.WriteLine(newCharacter.CharName);
                characterData.Add(newCharacter);
            }
            //Close first reader
            rdr.Close();

            //Second SQL command and reader
            using var SecondCmd = new MySqlCommand("GetCharacterGear", con);
            SecondCmd.CommandType = CommandType.StoredProcedure;
            SecondCmd.Parameters.AddWithValue("pAccountID", MySession.AccountID);
            using MySqlDataReader SecondRdr = SecondCmd.ExecuteReader();

            //Use second reader to iterate through character gear and assign to character attributes
            while (SecondRdr.Read())
            {
                //create newCharacter obbject to hold gear data
                var newCharacter = new Character();

                //Hold charactervalue so we have names to compare against 
                charName = SecondRdr.GetString(0);
                //Iterate through characterData list finding charnames that exist
                Character thisChar = characterData.Find(i => Equals(i.CharName, charName));

                //Add Character gear data here
                uint model = SecondRdr.GetUInt32(1);
                uint color = SecondRdr.GetUInt32(2);
                byte equipslot = SecondRdr.GetByte(3);

                //Append the previously pulled DB data to tuples in the gear list for each character with gear.
                thisChar.GearList.Add(Tuple.Create(model, color, equipslot));
            }
            SecondRdr.Close();
            //foreach (Character character in characterData.OrderBy(newCharacter => newCharacter.CharName)) Console.WriteLine(character);

            //return Character Data with characters and gear.
            return characterData;
        }

        //Method to delete character from player's account
        public static void DeleteCharacter(int serverid, Session MySession)
        {
            //Opens new Sql connection using connection parameters
            var connectionString = ConfigurationManager.ConnectionStrings["DevLocal"].ConnectionString;

            //Set connection property from connection string and open connection
            using MySqlConnection con = new MySqlConnection(connectionString);
            con.Open();
            //Creates var to store a MySQlcommand with the query and connection parameters.
            using var cmd = new MySqlCommand("DeleteCharacter", con);
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.AddWithValue("cServerID", serverid);

            //Executes a reader on the previous var.
            using MySqlDataReader rdr = cmd.ExecuteReader();

            //Log which character serverid was deleted
            Logger.Info($"Deleted Character with ServerID: {serverid}");

            //Create a new list of characters after deletion
            List<Character> MyCharacterList = new List<Character>();
            MyCharacterList = SQLOperations.AccountCharacters(MySession);

            //Send Fresh Character Listing
            ProcessCharacterList.CreateCharacterList(MyCharacterList, MySession);
        }

        //Method to check if characters name exist in the DB
        public static string CheckName(String CharName)
        {
            //Create local var to hold character name from DB
            String TestCharName = "";
            //Set and open SQL con
            var connectionString = ConfigurationManager.ConnectionStrings["DevLocal"].ConnectionString;

            //Set connection property from connection string and open connection
            using MySqlConnection CheckNameCon = new MySqlConnection(connectionString);
            CheckNameCon.Open();
            //SQL query to check if a name exists in the DB or not
            using var CheckNameCmd = new MySqlCommand("CheckName", CheckNameCon);
            CheckNameCmd.CommandType = CommandType.StoredProcedure;
            //Assigns variable to in line @Sql variable
            CheckNameCmd.Parameters.AddWithValue("@CharacterName", CharName);
            //Executes the SQL reader
            using MySqlDataReader CheckNameRdr = CheckNameCmd.ExecuteReader();

            //Reads through the returned rows(should only be 1 or none) and sets variable to returned value
            while (CheckNameRdr.Read())
            {
                TestCharName = CheckNameRdr.GetString(0);
            }
            //Close the DB connection
            CheckNameCon.Close();

            //Return the matched name if it existed in the DB.
            return TestCharName;
        }

        //Method to create new character for player's account
        public static void CreateCharacter(Session MySession, NewCharacter charCreation)
        {

            //Instantiate new list of Characters to return new character listing
            

            //Local variables to get string values to store in the DB from dictionary keys received from client
            string humType = charCreation.HumTypeDict[charCreation.HumType];
            string classType = charCreation.CharClassDict[charCreation.StartingClass];
            string raceType = charCreation.CharRaceDict[charCreation.Race];
            string sexType = charCreation.CharSexDict[charCreation.Gender];

            //Calculate total TP used among all stats for DB storage
            int UsedTP = charCreation.AddStrength + charCreation.AddStamina + charCreation.AddAgility + charCreation.AddDexterity + charCreation.AddWisdom + charCreation.AddIntelligence
                             + charCreation.AddCharisma;

            //Create and Open new Sql connection using connection parameters
            var connectionString = ConfigurationManager.ConnectionStrings["DevLocal"].ConnectionString;

            //Set connection property from connection string and open connection
            using MySqlConnection con = new MySqlConnection(connectionString);
            con.Open();

            //Assign query string and connection to commands
            using var cmd = new MySqlCommand("GetCharModel", con);
            cmd.CommandType = CommandType.StoredProcedure;

            //Add parameter values for parameterized string.
            cmd.Parameters.AddWithValue("@RaceType", raceType);
            cmd.Parameters.AddWithValue("@ClassType", classType);
            cmd.Parameters.AddWithValue("@HumType", humType);
            cmd.Parameters.AddWithValue("@SexType", sexType);

            //Execute reader on SQL command
            using MySqlDataReader rdr = cmd.ExecuteReader();

            //Iterate through default character values for class and race and assign to new character
            while (rdr.Read())
            {
                charCreation.Tunar = rdr.GetInt32(5);
                charCreation.UnusedTP = rdr.GetInt32(7);
                charCreation.TotalTP = rdr.GetInt32(8);
                charCreation.XCoord = rdr.GetFloat(9);
                charCreation.ZCoord = rdr.GetFloat(10);
                charCreation.YCoord = rdr.GetFloat(11);
                charCreation.Facing = rdr.GetFloat(12);
                charCreation.DefaultStrength = rdr.GetInt32(14);
                charCreation.DefaultStamina = rdr.GetInt32(15);
                charCreation.DefaultAgility = rdr.GetInt32(16);
                charCreation.DefaultDexterity = rdr.GetInt32(17);
                charCreation.DefaultWisdom = rdr.GetInt32(18);
                charCreation.DefaultIntelligence = rdr.GetInt32(19);
                charCreation.DefaultCharisma = rdr.GetInt32(20);
                charCreation.ModelID = rdr.GetInt64(21);
            }
            rdr.Close();
            con.Close();

            //Calculate totalTP maximum for new character
            charCreation.TotalTP = charCreation.DefaultStrength + charCreation.DefaultStamina + charCreation.DefaultAgility + charCreation.DefaultDexterity + charCreation.DefaultWisdom +
                                    charCreation.DefaultIntelligence + charCreation.DefaultCharisma + charCreation.UnusedTP;

            //Calculate Unused TP still available to character upon entering world.
            charCreation.UnusedTP = charCreation.UnusedTP - UsedTP;

            //Add total strength from default plus added TP to each category. Not sure this is correct, may need to still add the TP from client
            charCreation.Strength = charCreation.DefaultStrength + charCreation.AddStrength;
            charCreation.Stamina = charCreation.DefaultStamina + charCreation.AddStrength;
            charCreation.Agility = charCreation.DefaultAgility + charCreation.AddAgility;
            charCreation.Dexterity = charCreation.DefaultDexterity + charCreation.AddDexterity;
            charCreation.Wisdom = charCreation.DefaultWisdom + charCreation.AddWisdom;
            charCreation.Intelligence = charCreation.DefaultIntelligence + charCreation.AddIntelligence;
            charCreation.Charisma = charCreation.DefaultCharisma + charCreation.AddCharisma;

            //Open second connection using query string params

            //Set connection property from connection string and open connection
            using MySqlConnection SecondCon = new MySqlConnection(connectionString);
            SecondCon.Open();

            //Create second command using second connection and char insert query string
            using var SecondCmd = new MySqlCommand("CreateCharacter", SecondCon);
            SecondCmd.CommandType = CommandType.StoredProcedure;

            //Add all character attributes for new character creation to parameterized values
            SecondCmd.Parameters.AddWithValue("@charName", charCreation.CharName);
            //Needs to be MySession.AccountID once CharacterSelect shows characters off true AccountID.
            SecondCmd.Parameters.AddWithValue("AccountID", MySession.AccountID);
            SecondCmd.Parameters.AddWithValue("ModelID", charCreation.ModelID);
            SecondCmd.Parameters.AddWithValue("TClass", charCreation.StartingClass);
            SecondCmd.Parameters.AddWithValue("Race", charCreation.Race);
            SecondCmd.Parameters.AddWithValue("HumType", humType);
            SecondCmd.Parameters.AddWithValue("Level", charCreation.Level);
            SecondCmd.Parameters.AddWithValue("HairColor", charCreation.HairColor);
            SecondCmd.Parameters.AddWithValue("HairLength", charCreation.HairLength);
            SecondCmd.Parameters.AddWithValue("HairStyle", charCreation.HairStyle);
            SecondCmd.Parameters.AddWithValue("FaceOption", charCreation.FaceOption);
            SecondCmd.Parameters.AddWithValue("classIcon", charCreation.StartingClass);
            //May need other default values but these hard set values are placeholders for now
            SecondCmd.Parameters.AddWithValue("TotalXP", 0);
            SecondCmd.Parameters.AddWithValue("Debt", 0);
            SecondCmd.Parameters.AddWithValue("Breath", 255);
            SecondCmd.Parameters.AddWithValue("Tunar", charCreation.Tunar);
            SecondCmd.Parameters.AddWithValue("BankTunar", charCreation.BankTunar);
            SecondCmd.Parameters.AddWithValue("UnusedTP", charCreation.UnusedTP);
            SecondCmd.Parameters.AddWithValue("TotalTP", charCreation.TotalTP);
            SecondCmd.Parameters.AddWithValue("X", charCreation.XCoord);
            SecondCmd.Parameters.AddWithValue("Y", charCreation.YCoord);
            SecondCmd.Parameters.AddWithValue("Z", charCreation.ZCoord);
            SecondCmd.Parameters.AddWithValue("Facing", charCreation.Facing);
            SecondCmd.Parameters.AddWithValue("Strength", charCreation.Strength);
            SecondCmd.Parameters.AddWithValue("Stamina", charCreation.Stamina);
            SecondCmd.Parameters.AddWithValue("Agility", charCreation.Agility);
            SecondCmd.Parameters.AddWithValue("Dexterity", charCreation.Dexterity);
            SecondCmd.Parameters.AddWithValue("Wisdom", charCreation.Wisdom);
            SecondCmd.Parameters.AddWithValue("Intelligence", charCreation.Intelligence);
            SecondCmd.Parameters.AddWithValue("Charisma", charCreation.Charisma);
            //May need other default or calculated values but these hard set values are placeholders for now
            SecondCmd.Parameters.AddWithValue("CurrentHP", 1000);
            SecondCmd.Parameters.AddWithValue("MaxHP", 1000);
            SecondCmd.Parameters.AddWithValue("CurrentPower", 500);
            SecondCmd.Parameters.AddWithValue("MaxPower", 500);
            SecondCmd.Parameters.AddWithValue("Healot", 20);
            SecondCmd.Parameters.AddWithValue("Powerot", 10);
            SecondCmd.Parameters.AddWithValue("Ac", 0);
            SecondCmd.Parameters.AddWithValue("PoisonR", 10);
            SecondCmd.Parameters.AddWithValue("DiseaseR", 10);
            SecondCmd.Parameters.AddWithValue("FireR", 10);
            SecondCmd.Parameters.AddWithValue("ColdR", 10);
            SecondCmd.Parameters.AddWithValue("LightningR", 10);
            SecondCmd.Parameters.AddWithValue("ArcaneR", 10);
            SecondCmd.Parameters.AddWithValue("Fishing", 0);
            SecondCmd.Parameters.AddWithValue("Base_Strength", charCreation.DefaultStrength);
            SecondCmd.Parameters.AddWithValue("Base_Stamina", charCreation.DefaultStamina);
            SecondCmd.Parameters.AddWithValue("Base_Agility", charCreation.DefaultAgility);
            SecondCmd.Parameters.AddWithValue("Base_Dexterity", charCreation.DefaultDexterity);
            SecondCmd.Parameters.AddWithValue("Base_Wisdom", charCreation.DefaultWisdom);
            SecondCmd.Parameters.AddWithValue("Base_Intelligence", charCreation.DefaultIntelligence);
            SecondCmd.Parameters.AddWithValue("Base_Charisma", charCreation.DefaultCharisma);
            //See above comments regarding hard set values
            SecondCmd.Parameters.AddWithValue("CurrentHP2", 1000);
            SecondCmd.Parameters.AddWithValue("BaseHP", 1000);
            SecondCmd.Parameters.AddWithValue("CurrentPower2", 500);
            SecondCmd.Parameters.AddWithValue("BasePower", 500);
            SecondCmd.Parameters.AddWithValue("Healot2", 20);
            SecondCmd.Parameters.AddWithValue("Powerot2", 10);

            //Execute parameterized statement entering it into the DB
            //using MySqlDataReader SecondRdr = SecondCmd.ExecuteReader();
            SecondCmd.ExecuteNonQuery();
            SecondCon.Close();

            ///Close DB connection
            SecondCon.Close();

            //Log which character serverid was created
            Console.WriteLine($"Created Character with Name: {charCreation.CharName}");

            List<Character> MyCharacterList = new List<Character>();
            MyCharacterList = SQLOperations.AccountCharacters(MySession);

            //Send Fresh Character Listing
            ProcessCharacterList.CreateCharacterList(MyCharacterList, MySession);
        }

    }
}
