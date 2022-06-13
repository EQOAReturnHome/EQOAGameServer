using System.Collections.Generic;
using System.IO;
using System.Text;
using System.Text.Json;
using Newtonsoft.Json;
using ReturnHome.Server.Network;
using ReturnHome.Utilities;

namespace ReturnHome.Server.EntityObject.Player
{  
    public partial class Character : Entity
    {
        private static string Tunaria = "data\\tunaria.esf";

        //Our Lists for attributes of character
        public ItemContainer Bank;
        public List<Item> AuctionItems = new List<Item> { };
        public List<WeaponHotbar> WeaponHotbars = new List<WeaponHotbar> { };
        public List<Hotkey> MyHotkeys = new List<Hotkey> { };
        public List<Spell> MySpells = new List<Spell> { };
        public List<Auction> MySellingAuctions = new List<Auction> { };
        public List<Auction> MyBuyingAuctions = new List<Auction> { };
        public List<Quest> MyQuests = new List<Quest> { };
        public Dialogue MyDialogue = new Dialogue();
        public Dictionary<string, bool> playerFlags = new Dictionary<string, bool>();

        public TrainingPoints PlayerTrainingPoints;

        public int ExpectedWorld;
        //this Reference helps keep these 2 objects tied together
        public Session characterSession;
        public int ServerID;

        public int XPEarnedInThisLevel;
        public long TotalXP = 0;
        public int totalDebt;

        //Default this should be 350, once player is 45+ should be changed to 400
        private int _maxTrainingPoints = 350;

        public int Breath;

        public int Fishing;

        public int Teleportcounter { get; internal set; } = 0;

        public Character() : base(true)
        {
            Inventory = new(0);
            Bank = new(0, false);
        }

        //Need instantiation, but needs some review because it's so big... 
        public Character(string charName, int serverID, int modelID, int tClass, int race, string humType, int level, int hairColor, int hairLength, int hairStyle, int faceOption, int earnedXP, int debt, int breath, int tunar, int bankTunar, int UnusedTrainingPoints, int TotalTrainingPoints,
                         int world, float xCoord, float yCoord, float zCoord, float facing, int strength, int stamina, int agility, int dexterity, int wisdom, int intelligence, int charisma, int currentHP, int maxHP, int currentPower, int maxPower, int healOT, int powerOT, int aC,
                         int poisonResist, int diseaseResist, int fireResist, int coldResist, int lightningResist, int arcaneResist, int fishing, int baseStrength, int baseStamina, int baseAgility, int baseDexterity, int baseWisdom, int baseIntelligence, int baseCharisma, int currentHP2,
                         int baseHP, int currentPower2, int basePower, int healOT2, int powerOT2, string playerFlags, Session MySession) : base(true)
        {
            //playerFlags.Add("Freeport", true);
            Target = 0xFFFFFFFF;
            CharName = charName;
            ServerID = serverID;
            ObjectID = MySession.SessionID;
            ModelID = modelID;
            Class = tClass;
            Race = race;
            HumType = humType;
            Level = level;
            HairColor = hairColor;
            HairLength = hairLength;
            HairStyle = hairStyle;
            FaceOption = faceOption;
            XPEarnedInThisLevel = earnedXP;

            //Calculate Total XP
            for (int i = 1; i < Level; i++)
                TotalXP += CharacterUtilities.CharXPDict[i];
            TotalXP += XPEarnedInThisLevel;

            totalDebt = debt;
            Breath = breath;
            Inventory = new(tunar);
            Bank = new(bankTunar, false);
            PlayerTrainingPoints = new(TotalTrainingPoints, UnusedTrainingPoints);
            World = world;
            ExpectedWorld = world;
            x = xCoord;
            y = yCoord;
            z = zCoord;
            Facing = (byte)(facing / 0.0245433693f);
            Strength = strength;
            Stamina = stamina;
            Agility = agility;
            Dexterity = dexterity;
            Wisdom = wisdom;
            Intelligence = intelligence;
            Charisma = charisma;
            HPFlag = true;
            CurrentHP = 300;
            HPMax = 500;
            CurrentPower = currentPower;
            PowerMax = maxPower;
            HealthOverTime = healOT;
            PowerOverTime = powerOT;
            CurrentAC = aC;
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
            if(playerFlags != null){
                this.playerFlags = JsonConvert.DeserializeObject<Dictionary<string, bool>>(playerFlags);
            }
            //CurrentPower2 = currentPower2;
            //BasePower = basePower;
            //HealOT2 = healOT2;
            //PowerOT2 = powerOT2;
            characterSession = MySession;

            //If Character level >= 45, change Max Training Points to 400
            if (Level >= 45)
                _maxTrainingPoints = 400;
        }

        public void UpdateFeatures(Session MySession, int hairColor, int hairLength, int hairStyle, int faceOption)
        {
            HairColor = hairColor;
            HairLength = hairLength;
            HairStyle = hairStyle;
            FaceOption = faceOption;
            MySession.MyCharacter = this;
        }

        public void DumpCharacter(ref BufferWriter writer)
        {
            //Start pulling data together
            writer.Write((byte)0);
            writer.WriteString(Encoding.UTF8, Tunaria);
            writer.Write7BitEncodedInt64(ServerID);
            writer.WriteString(Encoding.UTF8, CharName);
            writer.Write7BitEncodedInt64(Class);
            writer.Write7BitEncodedInt64(Race);
            writer.Write7BitEncodedInt64(Level);
            writer.Write7BitEncodedInt64(XPEarnedInThisLevel);
            writer.Write7BitEncodedInt64(totalDebt);
            writer.Write((byte)Breath);
            writer.Write7BitEncodedInt64(Inventory.Tunar);
            writer.Write7BitEncodedInt64(Bank.Tunar);
            writer.Write7BitEncodedInt64(PlayerTrainingPoints.RemainingTrainingPoints);
            writer.Write7BitEncodedInt64(_maxTrainingPoints);
            writer.Write7BitEncodedInt64(World);
            writer.Write(x);
            writer.Write(y);
            writer.Write(z);
            writer.Write(FacingF);

            //Two unknown values must be packed on the end, can be updated later to
            //include database values when we figure out what it does.
            writer.Write(0.0f);
            writer.Write(0.0f);
        }
        public bool GetPlayerFlags(Session mySession, string flagKey)
        {
            if (mySession.MyCharacter.playerFlags == null)
                return false;

            if (mySession.MyCharacter.playerFlags.ContainsKey(flagKey) && mySession.MyCharacter.playerFlags[flagKey])
                return true;

            return false;
        }

        public void SetPlayerFlag(Session mySession, string flagKey, bool flagValue)
        {
            //TODO: Fix for characters without flags? bypassing for now
            if (mySession.MyCharacter.playerFlags == null)
                return;

            if (!mySession.MyCharacter.playerFlags.ContainsKey(flagKey))
                mySession.MyCharacter.playerFlags.Add(flagKey, flagValue);

            else if (mySession.MyCharacter.playerFlags.ContainsKey(flagKey))
                mySession.MyCharacter.playerFlags[flagKey] = flagValue;
        }
    }
}
