using System;
using System.Collections.Generic;
using System.Text;
using ReturnHome.Playercharacter.Actor;
using ReturnHome.Server.Network;
using ReturnHome.Utilities;

namespace ReturnHome.Server.Entity.Actor
{
    public class Character
    {
        //List to hold gear values for model, color, and equip location
        public List<Tuple<uint, uint, byte>> GearList = new List<Tuple<uint, uint, byte>>();

        //Attributes a character may have
        public string CharName;
        private string Tunaria = "data\\tunaria.esf";
        public int ServerID;
        public int ModelID;
        public int TClass;
        public int Race;
        public string HumType;
        public int Level;
        public int HairColor;
        public int HairLength;
        public int HairStyle;
        public int FaceOption;
        public int ClassIcon;
        public int TotalXP;
        public int Debt;
        public int Breath;
        public int Tunar;
        public int BankTunar;
        public int UnusedTP;
        public int TotalAssignableTP;
        public int World;
        public float XCoord;
        public float YCoord;
        public float ZCoord;
        public float Facing;
        public byte FirstPerson = 0;
        public float ModelSize = 1.0f;
        public byte Turning = 0;
        public long killTime = 0;
        public short Animation;
        public int Target;
        public int Strength;
        public int Stamina;
        public int Agility;
        public int Dexterity;
        public int Wisdom;
        public int Intelligence;
        public int Charisma;
        public int CurrentHP;
        public int MaxHP;
        public int CurrentPower;
        public int MaxPower;
        public int HealOT;
        public int PowerOT;
        public int AC;
        public int PoisonResist;
        public int DiseaseResist;
        public int FireResist;
        public int ColdResist;
        public int LightningResist;
        public int ArcaneResist;
        public int Fishing;
        public int BaseStrength;
        public int BaseStamina;
        public int BaseAgility;
        public int BaseDexterity;
        public int BaseWisdom;
        public int BaseIntelligence;
        public int BaseCharisma;
        public int CurrentHP2;
        public int BaseHP;
        public int CurrentPower2;
        public int BasePower;
        public int HealOT2;
        public int PowerOT2;

        ///Armor
        public byte Helm = 0;
        public byte Chest = 0;
        public byte Gloves = 0;
        public byte Bracer = 0;
        public byte Legs = 0;
        public byte Boots = 0;
        public int Robe = -1;
        public int Primary = 0;
        public int Secondary = 0;
        public int Shield = 0;

        ///Armor color
        public uint HelmColor = 0xFFFFFFFF;
        public uint ChestColor = 0xFFFFFFFF;
        public uint GlovesColor = 0xFFFFFFFF;
        public uint BracerColor = 0xFFFFFFFF;
        public uint LegsColor = 0xFFFFFFFF;
        public uint BootsColor = 0xFFFFFFFF;
        public uint RobeColor = 0xFFFFFFFF;

        //Client Object updates
        public List<byte> BaseUpdate;
        //public List<List<byte>> Ourup

        //Our Lists for attributes of character
        public List<Item> InventoryItems = new List<Item> { };
        public List<Item> BankItems = new List<Item> { };
        public List<Item> AuctionItems = new List<Item> { };
        public List<WeaponHotbar> WeaponHotbars = new List<WeaponHotbar> { };
        public List<Hotkey> MyHotkeys = new List<Hotkey> { };
        public List<Spell> MySpells = new List<Spell> { };
        public List<Auction> MySellingAuctions = new List<Auction> { };
        public List<Auction> MyBuyingAuctions = new List<Auction> { };
        public List<Quest> MyQuests = new List<Quest> { };

        private List<byte> ourMessage = new List<byte> { };

        public Session characterSession;

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


        /* These are all values for character creation, likely don't need to be attributes of the character object at all*/
        public string TestCharName;
        public int StartingClass;
        public int Gender;
        //Note this is for holding the HumType from the client that is an int and base Character has a string HumType
        public int HumTypeNum;
        //Addxxxx attributes of the class are to hold a new characters initial allocated stat points in each category
        public int AddStrength;
        public int AddStamina;
        public int AddAgility;
        public int AddDexterity;
        public int AddWisdom;
        public int AddIntelligence;
        public int AddCharisma;
        //Defaultxxx attributes of the class pulled from the defaultClass table in the DB for new character creation
        public int DefaultStrength;
        public int DefaultStamina;
        public int DefaultAgility;
        public int DefaultDexterity;
        public int DefaultWisdom;
        public int DefaultIntelligence;
        public int DefaultCharisma;

        //Overwrite to ToString Method to allow for direct enumeration of the object for characterName. Could be expanded for additional attributes.
        public override string ToString()
        {
            return "Character Data: " + CharName;
        }

        public Character()
        {

        }

        public Character(string charName, int serverID, int modelID, int tClass, int race, string humType, int level, int hairColor,
            int hairLength, int hairStyle, int faceOption, int classIcon, int totalXP, int debt, int breath, int tunar, int bankTunar,
            int unusedTP, int totalAssignableTP, int world, float xCoord, float yCoord, float zCoord, float facing, int strength,
            int stamina, int agility, int dexterity, int wisdom, int intelligence, int charisma, int currentHP, int maxHP, int currentPower,
            int maxPower, int healOT, int powerOT, int aC, int poisonResist, int diseaseResist, int fireResist, int coldResist,
            int lightningResist, int arcaneResist, int fishing, int baseStrength, int baseStamina, int baseAgility, int baseDexterity,
            int baseWisdom, int baseIntelligence, int baseCharisma, int currentHP2, int baseHP, int currentPower2, int basePower,
            int healOT2, int powerOT2, Session MySession)
        {
            CharName = charName;
            ServerID = serverID;
            ModelID = modelID;
            TClass = tClass;
            Race = race;
            HumType = humType;
            Level = level;
            HairColor = hairColor;
            HairLength = hairLength;
            HairStyle = hairStyle;
            FaceOption = faceOption;
            ClassIcon = classIcon;
            TotalXP = totalXP;
            Debt = debt;
            Breath = breath;
            Tunar = tunar;
            BankTunar = bankTunar;
            UnusedTP = unusedTP;
            TotalAssignableTP = totalAssignableTP;
            World = world;
            XCoord = xCoord;
            YCoord = yCoord;
            ZCoord = zCoord;
            Facing = facing;
            Strength = strength;
            Stamina = stamina;
            Agility = agility;
            Dexterity = dexterity;
            Wisdom = wisdom;
            Intelligence = intelligence;
            Charisma = charisma;
            CurrentHP = currentHP;
            MaxHP = maxHP;
            CurrentPower = currentPower;
            MaxPower = maxPower;
            HealOT = healOT;
            PowerOT = powerOT;
            AC = aC;
            PoisonResist = poisonResist;
            DiseaseResist = diseaseResist;
            FireResist = fireResist;
            ColdResist = coldResist;
            LightningResist = lightningResist;
            ArcaneResist = arcaneResist;
            Fishing = fishing;
            BaseStrength = baseStrength;
            BaseStamina = baseStamina;
            BaseAgility = baseAgility;
            BaseDexterity = baseDexterity;
            BaseWisdom = baseWisdom;
            BaseIntelligence = baseIntelligence;
            BaseCharisma = baseCharisma;
            CurrentHP2 = currentHP2;
            BaseHP = baseHP;
            CurrentPower2 = currentPower2;
            BasePower = basePower;
            HealOT2 = healOT2;
            PowerOT2 = powerOT2;
            characterSession = MySession;
        }

        public void UpdateFeatures(Session MySession, int hairColor, int hairLength, int hairStyle, int faceOption)
        {
            HairColor = hairColor;
            HairLength = hairLength;
            HairStyle = hairStyle;
            FaceOption = faceOption;
            MySession.MyCharacter = this;

            //Need to add a Database push here also
        }

        public int calcDumpSize()
        {
            int size = 3;
            size += 4 + Tunaria.Length;
            size += Utility_Funcs.DoubleVariableLengthIntegerLength(ServerID);
            size += 4 + CharName.Length;
            size += Utility_Funcs.DoubleVariableLengthIntegerLength(TClass);
            size += Utility_Funcs.DoubleVariableLengthIntegerLength(Race);
            size += Utility_Funcs.DoubleVariableLengthIntegerLength(Level);
            size += Utility_Funcs.DoubleVariableLengthIntegerLength(TotalXP);
            size += Utility_Funcs.DoubleVariableLengthIntegerLength(Debt);
            size += 1;
            size += Utility_Funcs.DoubleVariableLengthIntegerLength(Tunar);
            size += Utility_Funcs.DoubleVariableLengthIntegerLength(BankTunar);
            size += Utility_Funcs.DoubleVariableLengthIntegerLength(UnusedTP);
            size += Utility_Funcs.DoubleVariableLengthIntegerLength(TotalAssignableTP);
            size += Utility_Funcs.DoubleVariableLengthIntegerLength(World);
            size += 24;
            size += Utility_Funcs.DoubleVariableLengthIntegerLength(MyHotkeys.Count);
            foreach (Hotkey hk in MyHotkeys)
                size += hk.GetSize();

            //2 unknown 4 byte sections and 4 bytes for quest count
            size += 12;
            foreach (Quest q in MyQuests)
                size += q.GetSize();

            size += 4 + Utility_Funcs.DoubleVariableLengthIntegerLength(InventoryItems.Count);
            foreach (Item i in InventoryItems)
                size += i.GetSize();

            foreach (WeaponHotbar wb in WeaponHotbars)
                size += wb.GetSize();

            size += 4 + Utility_Funcs.DoubleVariableLengthIntegerLength(BankItems.Count);
            foreach (Item i in BankItems)
                size += i.GetSize();

            size += 2;
            foreach (Auction sa in MySellingAuctions)
                size += sa.GetSize();

            foreach (Auction ba in MyBuyingAuctions)
                size += ba.GetSize();

            size += Utility_Funcs.DoubleVariableLengthIntegerLength(MySpells.Count);
            foreach (Spell s in MySpells)
                size += s.GetSize();

            size += 165;

            return size;
        }

        public byte[] DumpCharacter()
        {
            //Clear List
            ourMessage.Clear();

            //Start pulling data together
            ourMessage.Add(0);
            ourMessage.AddRange(BitConverter.GetBytes(Tunaria.Length));
            ourMessage.AddRange(Encoding.UTF8.GetBytes(Tunaria));
            ourMessage.AddRange(Utility_Funcs.Technique(ServerID));
            ourMessage.AddRange(BitConverter.GetBytes(CharName.Length));
            ourMessage.AddRange(Encoding.UTF8.GetBytes(CharName));
            ourMessage.AddRange(Utility_Funcs.Technique(TClass));
            ourMessage.AddRange(Utility_Funcs.Technique(Race));
            ourMessage.AddRange(Utility_Funcs.Technique(Level));
            ourMessage.AddRange(Utility_Funcs.Technique(TotalXP));
            ourMessage.AddRange(Utility_Funcs.Technique(Debt));
            ourMessage.Add((byte)Breath);
            ourMessage.AddRange(Utility_Funcs.Technique(Tunar));
            ourMessage.AddRange(Utility_Funcs.Technique(BankTunar));
            ourMessage.AddRange(Utility_Funcs.Technique(UnusedTP));
            ourMessage.AddRange(Utility_Funcs.Technique(TotalAssignableTP));
            ourMessage.AddRange(Utility_Funcs.Technique(World));
            ourMessage.AddRange(BitConverter.GetBytes(XCoord));
            ourMessage.AddRange(BitConverter.GetBytes(YCoord));
            ourMessage.AddRange(BitConverter.GetBytes(ZCoord));
            ourMessage.AddRange(BitConverter.GetBytes(Facing));
            //Two unknown values must be packed on the end, can be updated later to
            //include database values when we figure out what it does.
            ourMessage.AddRange(BitConverter.GetBytes(0.0f));
            ourMessage.AddRange(BitConverter.GetBytes(0.0f));

            return ourMessage.ToArray(); ;
        }
        
        public void DistributeUpdates()
        {

        }
    }
}
