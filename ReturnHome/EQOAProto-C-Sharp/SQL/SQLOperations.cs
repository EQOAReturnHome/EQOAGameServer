using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Data;
using System.Configuration;
using System.Linq;
using ReturnHome.Actor;
using ReturnHome.PacketProcessing;
using ReturnHome.Playercharacter.Actor;
using ReturnHome.Utilities;
using ReturnHome.Opcodes;

namespace ReturnHome.SQL
{
    //Class to handle all SQL Operations
    class SQLOperations
    {

        //Holds list of characters for whole class
        private static List<Character> characterData = new List<Character>();

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
                Character newCharacter = new Character
                (
                    //charName
                    rdr.GetString(0),
                    //serverid
                    rdr.GetInt32(1),
                    //modelid
                    rdr.GetInt32(2),
                    //tclass
                    rdr.GetInt32(3),
                    //race
                    rdr.GetInt32(4),
                    //humType
                    rdr.GetString(5),
                    //level
                    rdr.GetInt32(6),
                    //haircolor
                    rdr.GetInt32(7),
                    //hairlength
                    rdr.GetInt32(8),
                    //hairstyle
                    rdr.GetInt32(9),
                    //faceoption
                    rdr.GetInt32(10),
                    //classIcon
                    rdr.GetInt32(11),
                    //totalXP
                    rdr.GetInt32(12),
                    //debt
                    rdr.GetInt32(13),
                    //breath
                    rdr.GetInt32(14),
                    //tunar
                    rdr.GetInt32(15),
                    //bankTunar
                    rdr.GetInt32(16),
                    //unusedTP
                    rdr.GetInt32(17),
                    //totalTP
                    rdr.GetInt32(18),
                    //world
                    rdr.GetInt32(19),
                    //x
                    rdr.GetFloat(20),
                    //y
                    rdr.GetFloat(21),
                    //z
                    rdr.GetFloat(22),
                    //facing
                    rdr.GetFloat(23),
                    //strength
                    rdr.GetInt32(24),
                    //stamina
                    rdr.GetInt32(25),
                    //agility
                    rdr.GetInt32(26),
                    //dexterity
                    rdr.GetInt32(27),
                    //wisdom
                    rdr.GetInt32(28),
                    //intel
                    rdr.GetInt32(29),
                    //charisma
                    rdr.GetInt32(30),
                    //currentHP
                    rdr.GetInt32(31),
                    //maxHP
                    rdr.GetInt32(32),
                    //currentPower
                    rdr.GetInt32(33),
                    //maxPower
                    rdr.GetInt32(34),
                    //healot
                    rdr.GetInt32(35),
                    //powerot
                    rdr.GetInt32(36),
                    //ac
                    rdr.GetInt32(37),
                    //poisonr
                    rdr.GetInt32(38),
                    //diseaser
                    rdr.GetInt32(39),
                    //firer
                    rdr.GetInt32(40),
                    //coldr
                    rdr.GetInt32(41),
                    //lightningr
                    rdr.GetInt32(42),
                    //arcaner
                    rdr.GetInt32(43),
                    //fishing
                    rdr.GetInt32(44),
                    //baseStr
                    rdr.GetInt32(45),
                    //baseSta
                    rdr.GetInt32(46),
                    //baseAgi
                    rdr.GetInt32(47),
                    //baseDex
                    rdr.GetInt32(48),
                    //baseWisdom
                    rdr.GetInt32(49),
                    //baseIntel
                    rdr.GetInt32(50),
                    //baseCha
                    rdr.GetInt32(51),
                    //currentHP2
                    rdr.GetInt32(52),
                    //baseHp
                    rdr.GetInt32(53),
                    //currentPower2
                    rdr.GetInt32(54),
                    //basePower
                    rdr.GetInt32(55),
                    //healot2
                    rdr.GetInt32(56),
                    //powerot2
                    rdr.GetInt32(57));

                //Add character attribute data to charaterData List
                //Console.WriteLine(newCharacter.CharName);
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
                //Hold charactervalue so we have names to compare against 
                charName = SecondRdr.GetString(0);

                //Iterate through characterData list finding charnames that exist
                Character thisChar = characterData.Find(i => Equals(i.CharName, charName));

                Item ThisItem = new Item(
                  //Stacksleft
                  SecondRdr.GetInt32(1),
                  //RemainingHP
                  SecondRdr.GetInt32(2),
                  //Charges
                  SecondRdr.GetInt32(3),
                  //Equipment Location
                  SecondRdr.GetInt32(4),
                  //Location (Bank, self, auction etc)
                  SecondRdr.GetByte(5),
                  //Location in inventory
                  SecondRdr.GetInt32(6),
                  //ItemID
                  SecondRdr.GetInt32(7),
                  //Item cost 
                  SecondRdr.GetInt32(8),
                  //ItemIcon
                  SecondRdr.GetInt32(9),
                  //Itempattern equipslot
                  SecondRdr.GetInt32(10),
                  //Attack Type 
                  SecondRdr.GetInt32(11),
                  //WeaponDamage
                  SecondRdr.GetInt32(12),
                  //MaxHP of item 
                  SecondRdr.GetInt32(13),
                  //Tradeable?
                  SecondRdr.GetInt32(14),
                  //Rentable
                  SecondRdr.GetInt32(15),
                  //Craft Item
                  SecondRdr.GetInt32(16),
                  //Lore item 
                  SecondRdr.GetInt32(17),
                  //Level requirement 
                  SecondRdr.GetInt32(18),
                  //Max stack of item 
                  SecondRdr.GetInt32(19),
                  //ItemName
                  SecondRdr.GetString(20),
                  //Item Description
                  SecondRdr.GetString(21),
                  //Duration
                  SecondRdr.GetInt32(22),
                  //useable classes
                  SecondRdr.GetInt32(23),
                  //useable races
                  SecondRdr.GetInt32(24),
                  //Proc Animation
                  SecondRdr.GetInt32(25),
                  //Strength
                  SecondRdr.GetInt32(26),
                  //Stamina
                  SecondRdr.GetInt32(27),
                  //Agility
                  SecondRdr.GetInt32(28),
                  //Dexterity
                  SecondRdr.GetInt32(29),
                  //Wisdom
                  SecondRdr.GetInt32(30),
                  //Intelligence
                  SecondRdr.GetInt32(31),
                  //Charisma
                  SecondRdr.GetInt32(32),
                  //HPMax
                  SecondRdr.GetInt32(33),
                  //POWMax
                  SecondRdr.GetInt32(34),
                  //Powerot
                  SecondRdr.GetInt32(35),
                  //Healot
                  SecondRdr.GetInt32(36),
                  //Ac
                  SecondRdr.GetInt32(37),
                  //PR 
                  SecondRdr.GetInt32(38),
                  //DR 
                  SecondRdr.GetInt32(39),
                  //FR 
                  SecondRdr.GetInt32(40),
                  //CR 
                  SecondRdr.GetInt32(41),
                  //LR 
                  SecondRdr.GetInt32(42),
                  //AR 
                  SecondRdr.GetInt32(43),
                  //Model
                  SecondRdr.GetInt32(44),
                  //Color
                  SecondRdr.GetUInt32(45));

                //If this is 1, it needs to go to inventory
                if (ThisItem.Location == 1)
                {
                    thisChar.InventoryItems.Add(ThisItem);
                }

                //If this is 2, it needs to go to the Bank
                else if(ThisItem.Location == 2)
                {
                    thisChar.BankItems.Add(ThisItem);
                }

                //If this is 4, it needs to go to "Auction items". This should be items you are selling and still technically in your possession
                else if(ThisItem.Location == 4)
                {
                    thisChar.AuctionItems.Add(ThisItem);
                }
            }
            SecondRdr.Close();
            //foreach (Character character in characterData.OrderBy(newCharacter => newCharacter.CharName)) Console.WriteLine(character);

            //return Character Data with characters and gear.
            return characterData;
        }

        public static void GetPlayerSpells(Session MySession)
        {
            var connectionString = ConfigurationManager.ConnectionStrings["DevLocal"].ConnectionString;

            //Set connection property from connection string and open connection
            using MySqlConnection con = new MySqlConnection(connectionString);
            con.Open();

            //Queries DB for all characters and their necessary attributes  to generate character select
            //Later should convert to a SQL stored procedure if possible.
            //Currently pulls ALL charcters, will pull characters based on accountID.
            using var cmd = new MySqlCommand("GetCharSpells", con);
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.AddWithValue("charID", MySession.MyCharacter.ServerID);
            using MySqlDataReader rdr = cmd.ExecuteReader();

            while (rdr.Read())
            {
                //Instantiate new character object, not to be confused with a newly created character
                Spell thisSpell = new Spell
                (
                    //SpellID
                     rdr.GetInt32(0),
                     //AddedOrder
                     rdr.GetInt32(1),
                     //OnHotBar
                     rdr.GetInt32(2),
                     //WhereOnHotBar
                     rdr.GetInt32(3),
                     //Unk1
                     rdr.GetInt32(4),
                     //ShowHide
                     rdr.GetInt32(5),
                     //AbilityLevel
                     rdr.GetInt32(6),
                     //Unk2
                     rdr.GetInt32(7),
                     //Unk3
                     rdr.GetInt32(8),
                     //SpellRange
                     rdr.GetInt32(9),
                     //CastTime
                     rdr.GetInt32(10),
                     //Power
                     rdr.GetInt32(11),
                     //IconColor
                     rdr.GetInt32(12),
                     //Icon
                     rdr.GetInt32(13),
                     //SpellScope
                     rdr.GetInt32(14),
                     //Recast
                     rdr.GetInt32(15),
                     //EqpRequirement
                     rdr.GetInt32(16),
                     //SpellName
                     rdr.GetString(17),
                     //SpellDesc
                     rdr.GetString(18)
                );

                //Add these spells to player book
                MySession.MyCharacter.MySpells.Add(thisSpell);
            }

            rdr.Close();
        }

        public static void GetPlayerHotkeys(Session MySession)
        {
            var connectionString = ConfigurationManager.ConnectionStrings["DevLocal"].ConnectionString;

            //Set connection property from connection string and open connection
            using MySqlConnection con = new MySqlConnection(connectionString);
            con.Open();

            //Queries DB for all characters and their necessary attributes  to generate character select
            //Later should convert to a SQL stored procedure if possible.
            //Currently pulls ALL charcters, will pull characters based on accountID.
            using var cmd = new MySqlCommand("GetCharHotkeys", con);
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.AddWithValue("charID", MySession.MyCharacter.ServerID);
            using MySqlDataReader rdr = cmd.ExecuteReader();

            while (rdr.Read())
            {
                //Instantiate new character object, not to be confused with a newly created character
                Hotkey thisHotkey= new Hotkey
                (
                     //direction
                     rdr.GetString(0),
                     //Nlabel
                     rdr.GetString(1),
                     //Nmessage
                     rdr.GetString(2),
                     //Wlabel
                     rdr.GetString(3),
                     //Wmessage
                     rdr.GetString(4),
                     //Elabel
                     rdr.GetString(5),
                     //Emessage
                     rdr.GetString(6),
                     //Slabel
                     rdr.GetString(7),
                     //Smessage
                     rdr.GetString(8)
                );

                //Add these spells to player book
                MySession.MyCharacter.MyHotkeys.Add(thisHotkey);
            }
            rdr.Close();
        }

        public static void GetPlayerWeaponHotbar(Session MySession)
        {
            var connectionString = ConfigurationManager.ConnectionStrings["DevLocal"].ConnectionString;

            //Set connection property from connection string and open connection
            using MySqlConnection con = new MySqlConnection(connectionString);
            con.Open();

            //Queries DB for all characters and their necessary attributes  to generate character select
            //Later should convert to a SQL stored procedure if possible.
            //Currently pulls ALL charcters, will pull characters based on accountID.
            using var cmd = new MySqlCommand("GetCharWeaponHotbar", con);
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.AddWithValue("charID", MySession.MyCharacter.ServerID);
            using MySqlDataReader rdr = cmd.ExecuteReader();

            while (rdr.Read())
            {
                //Instantiate new character object, not to be confused with a newly created character
                WeaponHotbar thisHotbar = new WeaponHotbar
                (
                     //Hotbar name
                     rdr.GetString(0),
                     //primaryID
                     rdr.GetInt32(1),
                     //secondaryID
                     rdr.GetInt32(2)
                );

                //Add these weapon hotbars
                MySession.MyCharacter.WeaponHotbars.Add(thisHotbar);
            }
            rdr.Close();

            //If less then 4 weapon hotbars, need constructor dummies untill 4 total
            for (int i = MySession.MyCharacter.WeaponHotbars.Count(); i < 4; i++)
            {
                MySession.MyCharacter.WeaponHotbars.Add(new WeaponHotbar());
            }
        }

        //Method to delete character from player's account
        public static void DeleteCharacter(SessionQueueMessages queueMessages, SessionManager sessionManager, int serverid, Session MySession)
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
                List<Character> MyCharacterList = AccountCharacters(MySession);

                //Send Fresh Character Listing
                ProcessOpcode.CreateCharacterList(queueMessages, sessionManager, MyCharacterList, MySession);
            }

            //Method to check if characters name exist in the DB
            public static string CheckName(string CharName)
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
            public static void CreateCharacter(SessionQueueMessages queueMessages, SessionManager sessionManager, Session MySession, Character charCreation)
            {

                //Instantiate new list of Characters to return new character listing


                //Local variables to get string values to store in the DB from dictionary keys received from client
                string humType = charCreation.HumTypeDict[charCreation.HumTypeNum];
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
                    charCreation.TotalAssignableTP = rdr.GetInt32(8);
                    charCreation.XCoord = rdr.GetFloat(9);
                    charCreation.YCoord = rdr.GetFloat(10);
                    charCreation.ZCoord = rdr.GetFloat(11);
                    charCreation.Facing = rdr.GetFloat(12);
                    charCreation.DefaultStrength = rdr.GetInt32(14);
                    charCreation.DefaultStamina = rdr.GetInt32(15);
                    charCreation.DefaultAgility = rdr.GetInt32(16);
                    charCreation.DefaultDexterity = rdr.GetInt32(17);
                    charCreation.DefaultWisdom = rdr.GetInt32(18);
                    charCreation.DefaultIntelligence = rdr.GetInt32(19);
                    charCreation.DefaultCharisma = rdr.GetInt32(20);
                    charCreation.ModelID = rdr.GetInt32(21);
                }
                rdr.Close();
                con.Close();

                //Calculate Unused TP still available to character upon entering world.
                charCreation.UnusedTP = charCreation.UnusedTP - UsedTP;

                //Add total strength from default plus added TP to each category. Not sure this is correct, may need to still add the TP from client
                charCreation.Strength = charCreation.DefaultStrength + charCreation.AddStrength;
                charCreation.Stamina = charCreation.DefaultStamina + charCreation.AddStamina;
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
                SecondCmd.Parameters.AddWithValue("TotalTP", 350);
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
                //Console.WriteLine($"Created Character with Name: {charCreation.CharName}");

                List<Character> MyCharacterList = SQLOperations.AccountCharacters(MySession);

                //Send Fresh Character Listing
                ProcessOpcode.CreateCharacterList(queueMessages, sessionManager, MyCharacterList, MySession);
            }
        }
    }


