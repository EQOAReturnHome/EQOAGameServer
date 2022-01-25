using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

using ReturnHome.Server.Network;
using ReturnHome.Utilities;

namespace ReturnHome.Server.EntityObject.Player
{
    public class Character : Entity
    {
        private static string Tunaria = "data\\tunaria.esf";

        //Our Lists for attributes of character
        public List<Item> BankItems = new List<Item> { };
        public List<Item> AuctionItems = new List<Item> { };
        public List<WeaponHotbar> WeaponHotbars = new List<WeaponHotbar> { };
        public List<Hotkey> MyHotkeys = new List<Hotkey> { };
        public List<Spell> MySpells = new List<Spell> { };
        public List<Auction> MySellingAuctions = new List<Auction> { };
        public List<Auction> MyBuyingAuctions = new List<Auction> { };
        public List<Quest> MyQuests = new List<Quest> { };
        public Dialogue MyDialogue = new Dialogue();

        //this Reference helps keep these 2 objects tied together
        public Session characterSession;
        public int ServerID;

        public int TotalXPEarned;
        public int totalDebt;

        public int Breath;

        public int Fishing;

        public int MaxAssignableTP;
        public int UnusedTP;
        public int Tunar;
        public int BankTunar;

        public Character() : base(true)
        {

        }

        //Need instantiation, but needs some review because it's so big... 
        public Character(string charName, int serverID, int modelID, int tClass, int race, string humType, int level, int hairColor, int hairLength, int hairStyle, int faceOption, int totalXP, int debt, int breath, int tunar, int bankTunar, int unusedTP, int totalAssignableTP,
                         int world, float xCoord, float yCoord, float zCoord, float facing, int strength, int stamina, int agility, int dexterity, int wisdom, int intelligence, int charisma, int currentHP, int maxHP, int currentPower, int maxPower, int healOT, int powerOT, int aC,
                         int poisonResist, int diseaseResist, int fireResist, int coldResist, int lightningResist, int arcaneResist, int fishing, int baseStrength, int baseStamina, int baseAgility, int baseDexterity, int baseWisdom, int baseIntelligence, int baseCharisma, int currentHP2,
                         int baseHP, int currentPower2, int basePower, int healOT2, int powerOT2, Session MySession) : base(true)
        {
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
            TotalXPEarned = totalXP;
            totalDebt = debt;
            Breath = breath;
            Tunar = tunar;
            BankTunar = bankTunar;
            UnusedTP = unusedTP;
            MaxAssignableTP = totalAssignableTP;
            World = world;
            x = xCoord;
            y = yCoord;
            z = zCoord;
            //set our base vactor 3 herw
            SetPosition();
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
            PWRMax = maxPower;
            HealthOverTime = healOT;
            PowerOverTime = powerOT;
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
            //CurrentHP2 = currentHP2;
            //BaseHP = baseHP;
            //CurrentPower2 = currentPower2;
            //BasePower = basePower;
            //HealOT2 = healOT2;
            //PowerOT2 = powerOT2;
            characterSession = MySession;
        }

        public void UpdatePosition()
        {
            if (waypoint == Position)
                return;
            else
            {
                Position = waypoint;
                VelocityX = WayPointVelocityX;
                VelocityY = WayPointVelocityY;
                VelocityZ = WayPointVelocityZ;
            }
            characterSession.objectUpdate = true;
        }


        public void UpdateFeatures(Session MySession, int hairColor, int hairLength, int hairStyle, int faceOption)
        {
            HairColor = hairColor;
            HairLength = hairLength;
            HairStyle = hairStyle;
            FaceOption = faceOption;
            MySession.MyCharacter = this;
        }

        public void DumpCharacter(MemoryStream memStream)
        {
            //Start pulling data together
            memStream.WriteByte(0);
            memStream.Write(BitConverter.GetBytes(Tunaria.Length));
            memStream.Write(Encoding.UTF8.GetBytes(Tunaria));
            memStream.Write(Utility_Funcs.DoublePack(ModelID));
            memStream.Write(BitConverter.GetBytes(CharName.Length));
            memStream.Write(Encoding.UTF8.GetBytes(CharName));
            memStream.Write(Utility_Funcs.DoublePack(Class));
            memStream.Write(Utility_Funcs.DoublePack(Race));
            memStream.Write(Utility_Funcs.DoublePack(Level));
            memStream.Write(Utility_Funcs.DoublePack(TotalXPEarned));
            memStream.Write(Utility_Funcs.DoublePack(totalDebt));
            memStream.WriteByte((byte)Breath);
            memStream.Write(Utility_Funcs.DoublePack(Tunar));
            memStream.Write(Utility_Funcs.DoublePack(BankTunar));
            memStream.Write(Utility_Funcs.DoublePack(UnusedTP));
            memStream.Write(Utility_Funcs.DoublePack(MaxAssignableTP));
            memStream.Write(Utility_Funcs.DoublePack(World));
            memStream.Write(BitConverter.GetBytes(x));
            memStream.Write(BitConverter.GetBytes(y));
            memStream.Write(BitConverter.GetBytes(z));
            memStream.Write(BitConverter.GetBytes(FacingF));

            //Two unknown values must be packed on the end, can be updated later to
            //include database values when we figure out what it does.
            memStream.Write(BitConverter.GetBytes(0.0f));
            memStream.Write(BitConverter.GetBytes(0.0f));
        }
    }
}
