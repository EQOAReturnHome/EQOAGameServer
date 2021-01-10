using Spells;
using Hotkeys;
using WeaponHotbars;
using Items;
using System;
using System.Collections.Generic;
using System.Text;
using Utility;
using Auctions;
using Quests;
using EQOASQL;

namespace Characters
{
	public class Character
	{
		//List to hold gear values for model, color, and equip location
		public List<Tuple<uint, uint, byte>> GearList = new List<Tuple<uint, uint, byte>>();

		//Attributes a character may have
		public string CharName { get; set; }
		private string Tunaria = "data\\tunaria.esf";
		public int ServerID { get; set; }
		public int ModelID { get; set; }
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
		public int TotalAssignableTP { get; set; }
		public int World { get; set; }
		public float XCoord { get; set; }
		public float YCoord { get; set; }
		public float ZCoord { get; set; }
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
		public int HealOT { get; set; }
		public int PowerOT { get; set; }
		public int AC { get; set; }
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

		//Our Lists for attributes of character
		public List<Item> InventoryItems = new List<Item> { };
		public List<Item> BankItems = new List<Item> { };
		public List<WeaponHotbar> WeaponHotbars = new List<WeaponHotbar> { };
		public List<Hotkey> MyHotkeys = new List<Hotkey> { };
		public List<Spell> MySpells = new List<Spell> { };
		public List<Auction> MySellingAuctions = new List<Auction> { };
		public List<Auction> MyBuyingAuctions = new List<Auction> { };
		public List<Quest> MyQuests = new List<Quest> { };

		private List<byte> ourMessage = new List<byte> { };

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
		public string TestCharName { get; set; }
		public int StartingClass { get; set; }
		public int Gender { get; set; }
		//Note this is for holding the HumType from the client that is an int and base Character has a string HumType
		public int HumTypeNum { get; set; }
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
			int healOT2, int powerOT2)
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
        }

		public void UpdateFeatures(int hairColor, int hairLength, int hairStyle, int faceOption)
		{
			HairColor = hairColor;
			HairLength = hairLength;
			HairStyle = hairStyle;
			FaceOption = faceOption;
		}

		public List<byte> PullCharacter()
        {
			//Clear List
			ourMessage.Clear();

			//Start pulling data together
			ourMessage.Add(0);
			ourMessage.AddRange(Utility_Funcs.Technique(World));
			ourMessage.AddRange(BitConverter.GetBytes(Tunaria.Length));
			ourMessage.AddRange(Encoding.Unicode.GetBytes(Tunaria));
			ourMessage.AddRange(Utility_Funcs.Technique(ServerID));
			ourMessage.AddRange(BitConverter.GetBytes(CharName.Length));
			ourMessage.AddRange(Encoding.Unicode.GetBytes(CharName));
			ourMessage.AddRange(Utility_Funcs.Technique(TClass));
			ourMessage.AddRange(Utility_Funcs.Technique(Race));
			ourMessage.AddRange(Utility_Funcs.Technique(Level));
			ourMessage.AddRange(Utility_Funcs.Technique(TotalXP));
			ourMessage.AddRange(Utility_Funcs.Technique(Debt));
			ourMessage.Add((byte)Breath);
			ourMessage.AddRange(Utility_Funcs.Technique(Tunar));
			ourMessage.AddRange(Utility_Funcs.Technique(BankTunar));
			ourMessage.AddRange(Utility_Funcs.Technique(UnusedTP)); 
			ourMessage.AddRange(Utility_Funcs.Technique(World));
			ourMessage.AddRange(BitConverter.GetBytes(XCoord));
			ourMessage.AddRange(BitConverter.GetBytes(YCoord));
			ourMessage.AddRange(BitConverter.GetBytes(ZCoord));
			ourMessage.AddRange(BitConverter.GetBytes(Facing));
			//Two unknown values must be packed on the end, can be updated later to
			//include database values when we figure out what it does.
			ourMessage.AddRange(BitConverter.GetBytes(0.0f));
			ourMessage.AddRange(BitConverter.GetBytes(0.0f));

            

			return ourMessage;
		}
	}
}