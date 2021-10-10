using System;
using System.Collections.Generic;
using System.IO;
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

        public Session characterSession;

        //Store latest character update directly to character for other characters to pull
        public Memory<byte> characterUpdate = new Memory<byte> ( new byte[0xC9]);

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

        public void DumpCharacter(MemoryStream memStream)
        {
            //Start pulling data together
            memStream.WriteByte(0);
            memStream.Write(BitConverter.GetBytes(Tunaria.Length));
            memStream.Write(Encoding.UTF8.GetBytes(Tunaria));
            memStream.Write(Utility_Funcs.DoublePack(ServerID));
            memStream.Write(BitConverter.GetBytes(CharName.Length));
            memStream.Write(Encoding.UTF8.GetBytes(CharName));
            memStream.Write(Utility_Funcs.DoublePack(TClass));
            memStream.Write(Utility_Funcs.DoublePack(Race));
            memStream.Write(Utility_Funcs.DoublePack(Level));
            memStream.Write(Utility_Funcs.DoublePack(TotalXP));
            memStream.Write(Utility_Funcs.DoublePack(Debt));
            memStream.WriteByte((byte)Breath);
            memStream.Write(Utility_Funcs.DoublePack(Tunar));
            memStream.Write(Utility_Funcs.DoublePack(BankTunar));
            memStream.Write(Utility_Funcs.DoublePack(UnusedTP));
            memStream.Write(Utility_Funcs.DoublePack(TotalAssignableTP));
            memStream.Write(Utility_Funcs.DoublePack(World));
            memStream.Write(BitConverter.GetBytes(XCoord));
            memStream.Write(BitConverter.GetBytes(YCoord));
            memStream.Write(BitConverter.GetBytes(ZCoord));
            memStream.Write(BitConverter.GetBytes(Facing));
            //Two unknown values must be packed on the end, can be updated later to
            //include database values when we figure out what it does.
            memStream.Write(BitConverter.GetBytes(0.0f));
            memStream.Write(BitConverter.GetBytes(0.0f));
        }

        public void EquipGear()
        {
            ///Start processing MyItem
            foreach (Item MyItem in InventoryItems)
            {
                ///Use a switch to sift through MyItem and add them properly
                switch (MyItem.EquipLocation)
                {
                    ///Helm
                    case 1:
                        Helm = (byte)MyItem.Model;
                        HelmColor = MyItem.Color;
                        break;

                    ///Robe
                    case 2:
                        Robe = (byte)MyItem.Model;
                        RobeColor = MyItem.Color;
                        break;

                    ///Gloves
                    case 19:
                        Gloves = (byte)MyItem.Model;
                        GlovesColor = MyItem.Color;
                        break;

                    ///Chest
                    case 5:
                        Chest = (byte)MyItem.Model;
                        ChestColor = MyItem.Color;
                        break;

                    ///Bracers
                    case 8:
                        Bracer = (byte)MyItem.Model;
                        BracerColor = MyItem.Color;
                        break;

                    ///Legs
                    case 10:
                        Legs = (byte)MyItem.Model;
                        LegsColor = MyItem.Color;
                        break;

                    ///Feet
                    case 11:
                        Boots = (byte)MyItem.Model;
                        BootsColor = MyItem.Color;
                        break;

                    ///Primary
                    case 12:
                        Primary = MyItem.Model;
                        break;

                    ///Secondary
                    case 14:

                        ///If we have a secondary equipped already, puts next secondary into primary slot
                        if (Secondary > 0)
                        {
                            Primary = MyItem.Model;
                        }

                        ///If no secondary, add to secondary slot
                        else
                        {
                            Secondary = MyItem.Model;
                        }
                        break;

                    ///2 Hand
                    case 15:
                        Primary = MyItem.Model;
                        break;

                    ///Shield
                    case 13:
                        Shield = MyItem.Model;
                        break;

                    ///Bow
                    case 16:
                        Primary = MyItem.Model;
                        break;

                    ///Thrown
                    case 17:
                        Primary = MyItem.Model;
                        break;

                    ///Held
                    case 18:
                        ///If we have a secondary equipped already, puts next secondary into primary slot
                        if (Secondary > 0)
                        {
                            Primary = MyItem.Model;
                        }

                        ///If no secondary, add to secondary slot
                        else
                        {
                            Secondary = MyItem.Model;
                        }
                        break;

                    default:
                        Logger.Err("Equipment not in list, this may need to be changed");
                        break;
                }
            }
        }
        
        public void DistributeUpdates()
        {

        }
    }
}
