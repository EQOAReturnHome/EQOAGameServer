using System;
using System.Collections.Generic;
using System.Text;
using ReturnHome.Utilities;

namespace ReturnHome.Server.Entity.Actor
{
    public class Item
    {
        //Should these be public? How would we handle adding stats to character overall stats without 100 methods?
        public int StackLeft { get; private set; }
        public int RemainingHP { get; private set; }
        public int Charges { get; private set; }
        public int EquipLocation { get; private set; }
        public byte Location { get; private set; } //inventory, bank auction etc
        public int InventoryNumber { get; private set; } //Location in inventory, would location in List suffice for this?
        public int ItemID { get; private set; }
        public int ItemCost { get; private set; }
        public int Unk1 { get; private set; }
        public int ItemIcon { get; private set; }
        public int Unk2 { get; private set; }
        public int Equipslot { get; private set; }
        public int Unk3 { get; private set; }
        public int Trade { get; private set; }
        public int Rent { get; private set; }
        public int Unk4 { get; private set; }
        public int Attacktype { get; private set; }
        public int Weapondamage { get; private set; }
        public int Unk5 { get; private set; }
        public int Levelreq { get; private set; }
        public int Maxstack { get; private set; }
        public int Maxhp { get; private set; }
        public int Duration { get; private set; }
        public int Classuse { get; private set; }
        public int Raceuse { get; private set; }
        public int Procanim { get; private set; }
        public int Lore { get; private set; }
        public int Unk6 { get; private set; }
        public int Craft { get; private set; }
        public string ItemName { get; private set; }
        public string ItemDesc { get; private set; }
        public int Model { get; private set; }
        public uint Color { get; private set; }

        //Gear stats
        public int Str { get; private set; }
        public int Sta { get; private set; }
        public int Agi { get; private set; }
        public int Dex { get; private set; }
        public int Wis { get; private set; }
        public int Int { get; private set; }
        public int Cha { get; private set; }
        public int HPMax { get; private set; }
        public int POWMax { get; private set; }
        public int PoT { get; private set; }
        public int HoT { get; private set; }
        public int AC { get; private set; }
        public int PR { get; private set; }
        public int DR { get; private set; }
        public int FR { get; private set; }
        public int CR { get; private set; }
        public int LR { get; private set; }
        public int AR { get; private set; }
        private List<byte> ourMessage = new List<byte> { };
        private List<byte> ourStats = new List<byte> { };

        //Stat counter
        private int Counter;

        //default constructor
        public Item()
        { }

        //This will instantiate an inventory object
        //Should we be able to instantiate a normal item and gear seperately? Seems the better choice
        //This is to instantiate 
        public Item(int thisStacksLeft, int thisCharges, byte thisLocation, int thisInventoryNumber, int thisItemID, int thisItemCost, int thisItemIcon, 
                         int thisTrade, int thisRent, int thisCraft, int thisLore, int thisLevelreq, int thisMaxStack, string thisItemName, string thisItemDesc)
        {
            StackLeft = thisStacksLeft;
            Charges = thisCharges;
            Location = thisLocation;
            InventoryNumber = thisInventoryNumber;
            ItemID = thisItemID;
            ItemCost = thisItemCost;
            ItemIcon = thisItemIcon;
            Trade = thisTrade;
            Rent = thisRent;
            Craft = thisCraft;
            Lore = thisLore;
            Levelreq = thisLevelreq;
            Maxstack = thisMaxStack;
            ItemName = thisItemName;
            ItemDesc = thisItemDesc;

        }

        //Constructor object for armour and weapons
        public Item(int thisStacksLeft, int thisRemainingHP, int thisCharges, int thisEquipLocation, byte thisLocation, int thisInventoryNumber, int thisItemID, int thisItemCost, int thisItemIcon, int thisEquipslot,
                         int thisAttackType, int thisWeaponDamage, int thisMaxHP, int thisTrade, int thisRent, int thisCraft, int thisLore, int thisLevelreq, int thisMaxStack, string thisItemName, string thisItemDesc,
                         int thisDuration, int thisClassuse, int thisRaceuse, int thisProcanim, int Strength, int Stamina, int Agility, int Dexterity, int Wisdom, int Intelligence, int Charisma, int HpMax, int PowMax, 
                         int pot, int hot, int ac, int pr, int dr, int fr, int cr, int lr, int ar, int model, uint color)
        {
            StackLeft = thisStacksLeft;
            Charges = thisCharges;
            Location = thisLocation;
            InventoryNumber = thisInventoryNumber;
            ItemID = thisItemID;
            ItemCost = thisItemCost;
            ItemIcon = thisItemIcon;
            Trade = thisTrade;
            Rent = thisRent;
            Craft = thisCraft;
            Lore = thisLore;
            Levelreq = thisLevelreq;
            Maxstack = thisMaxStack;
            ItemName = thisItemName;
            ItemDesc = thisItemDesc;

            //Null this for outgoing packet
            RemainingHP = thisRemainingHP;
            EquipLocation = thisEquipLocation;
            Equipslot = thisEquipslot;
            Attacktype = thisAttackType;
            Weapondamage = thisWeaponDamage;
            Maxhp = thisMaxHP;
            Duration = thisDuration;
            Classuse = thisClassuse;
            Raceuse = thisRaceuse;
            Procanim = thisProcanim;

            Color = color;
            Model = model;

            //Stats if any
            Str = Strength;
            Sta = Stamina;
            Agi = Agility;
            Dex = Dexterity;
            Wis = Wisdom;
            Int = Intelligence;
            Cha = Charisma;
            HPMax = HpMax;
            POWMax = PowMax;
            PoT = pot;
            HoT = hot;
            AC = ac;
            PR = pr;
            DR = dr;
            FR = fr;
            CR = cr;
            LR = lr;
            AR = ar;
        }

        
        public byte[] DumpItem()
        {
            ourMessage.Clear();

            //Start adding attributes to list for this item
            ourMessage.AddRange(Utility_Funcs.Technique(StackLeft));
            ourMessage.AddRange(Utility_Funcs.Technique(RemainingHP));
            ourMessage.AddRange(Utility_Funcs.Technique(Charges));
            ourMessage.AddRange(Utility_Funcs.Technique(EquipLocation));
            ourMessage.Add(Location);
            ourMessage.AddRange(BitConverter.GetBytes(InventoryNumber));
            ourMessage.AddRange(Utility_Funcs.Technique(ItemID));
            ourMessage.AddRange(Utility_Funcs.Technique(ItemCost));
            ourMessage.AddRange(Utility_Funcs.Technique(Unk1));
            ourMessage.AddRange(Utility_Funcs.Technique(ItemIcon));
            ourMessage.AddRange(Utility_Funcs.Technique(Unk2));
            ourMessage.AddRange(Utility_Funcs.Technique(Equipslot));
            ourMessage.AddRange(Utility_Funcs.Technique(Unk3));
            ourMessage.AddRange(Utility_Funcs.Technique(Trade));
            ourMessage.AddRange(Utility_Funcs.Technique(Rent));
            ourMessage.AddRange(Utility_Funcs.Technique(Unk4));
            ourMessage.AddRange(Utility_Funcs.Technique(Attacktype));
            ourMessage.AddRange(Utility_Funcs.Technique(Weapondamage));
            ourMessage.AddRange(Utility_Funcs.Technique(Unk5));
            ourMessage.AddRange(Utility_Funcs.Technique(Levelreq));
            ourMessage.AddRange(Utility_Funcs.Technique(Maxstack));
            ourMessage.AddRange(Utility_Funcs.Technique(Maxhp));
            ourMessage.AddRange(Utility_Funcs.Technique(Duration));
            ourMessage.AddRange(Utility_Funcs.Technique(Classuse));
            ourMessage.AddRange(Utility_Funcs.Technique(Raceuse));
            ourMessage.AddRange(Utility_Funcs.Technique(Procanim));
            ourMessage.AddRange(Utility_Funcs.Technique(Lore));
            ourMessage.AddRange(Utility_Funcs.Technique(Unk6));
            ourMessage.AddRange(Utility_Funcs.Technique(Craft));
            ourMessage.AddRange(BitConverter.GetBytes(ItemName.Length));
            ourMessage.AddRange(Encoding.Unicode.GetBytes(ItemName));
            ourMessage.AddRange(BitConverter.GetBytes(ItemDesc.Length));
            ourMessage.AddRange(Encoding.Unicode.GetBytes(ItemDesc));
            PullStats();

            return ourMessage.ToArray();
        }

        private void PullStats()
        {
            
            //Gather stats if any exist
            //Increment counter if if statement true, then add identifier int for stat and then technique and add stat value

            if (Str > 0)
            {
                Counter++;
                ourStats.Add(0);
                ourStats.AddRange(Utility_Funcs.Technique(Str));
            }

            if (Sta > 0)
            {
                Counter++;
                ourStats.Add(2);
                ourStats.AddRange(Utility_Funcs.Technique(Sta));
            }

            if (Agi > 0)
            {
                Counter++;
                ourStats.Add(4);
                ourStats.AddRange(Utility_Funcs.Technique(Agi));
            }

            if (Dex > 0)
            {
                Counter++;
                ourStats.Add(6);
                ourStats.AddRange(Utility_Funcs.Technique(Dex));
            }

            if (Wis > 0)
            {
                Counter++;
                ourStats.Add(8);
                ourStats.AddRange(Utility_Funcs.Technique(Wis));
            }

            if (Int > 0)
            {
                Counter++;
                ourStats.Add(10);
                ourStats.AddRange(Utility_Funcs.Technique(Int));
            }

            if (Cha > 0)
            {
                Counter++;
                ourStats.Add(12);
                ourStats.AddRange(Utility_Funcs.Technique(Cha));
            }

            if (HPMax > 0)
            {
                Counter++;
                ourStats.Add(16);
                ourStats.AddRange(Utility_Funcs.Technique(HPMax));
            }

            if (POWMax > 0)
            {
                Counter++;
                ourStats.Add(20);
                ourStats.AddRange(Utility_Funcs.Technique(POWMax));
            }

            if (PoT > 0)
            {
                Counter++;
                ourStats.Add(24);
                ourStats.AddRange(Utility_Funcs.Technique(PoT));
            }

            if (HoT > 0)
            {
                Counter++;
                ourStats.Add(26);
                ourStats.AddRange(Utility_Funcs.Technique(HoT));
            }

            if (AC > 0)
            {
                Counter++;
                ourStats.Add(28);
                ourStats.AddRange(Utility_Funcs.Technique(AC));
            }

            if (PR > 0)
            {
                Counter++;
                ourStats.Add(44);
                ourStats.AddRange(Utility_Funcs.Technique(PR));
            }

            if (DR > 0)
            {
                Counter++;
                ourStats.Add(46);
                ourStats.AddRange(Utility_Funcs.Technique(DR));
            }

            if (FR > 0)
            {
                Counter++;
                ourStats.Add(48);
                ourStats.AddRange(Utility_Funcs.Technique(FR));
            }

            if (CR > 0)
            {
                Counter++;
                ourStats.Add(50);
                ourStats.AddRange(Utility_Funcs.Technique(CR));
            }

            if (LR > 0)
            {
                Counter++;
                ourStats.Add(52);
                ourStats.AddRange(Utility_Funcs.Technique(LR));
            }

            if (AR > 0)
            {
                Counter++;
                ourStats.Add(54);
                ourStats.AddRange(Utility_Funcs.Technique(AR));
            }

            //Prepend the stat count
            ourStats.InsertRange(0, Utility_Funcs.Technique(Counter));

            //Add Stats to our List
            ourMessage.AddRange(ourStats);
        }

        public int GetSize()
        {
            int size = 0;
            size += Utility_Funcs.DoubleVariableLengthIntegerLength(StackLeft);
            size += Utility_Funcs.DoubleVariableLengthIntegerLength(RemainingHP);
            size += Utility_Funcs.DoubleVariableLengthIntegerLength(Charges);
            size += Utility_Funcs.DoubleVariableLengthIntegerLength(EquipLocation);
            size += 5;
            size += Utility_Funcs.DoubleVariableLengthIntegerLength(ItemID);
            size += Utility_Funcs.DoubleVariableLengthIntegerLength(ItemCost);
            size += Utility_Funcs.DoubleVariableLengthIntegerLength(Unk1);
            size += Utility_Funcs.DoubleVariableLengthIntegerLength(ItemIcon);
            size += Utility_Funcs.DoubleVariableLengthIntegerLength(Unk2);
            size += Utility_Funcs.DoubleVariableLengthIntegerLength(Equipslot);
            size += Utility_Funcs.DoubleVariableLengthIntegerLength(Unk3);
            size += Utility_Funcs.DoubleVariableLengthIntegerLength(Trade);
            size += Utility_Funcs.DoubleVariableLengthIntegerLength(Rent);
            size += Utility_Funcs.DoubleVariableLengthIntegerLength(Unk4);
            size += Utility_Funcs.DoubleVariableLengthIntegerLength(Attacktype);
            size += Utility_Funcs.DoubleVariableLengthIntegerLength(Weapondamage);
            size += Utility_Funcs.DoubleVariableLengthIntegerLength(Unk5);
            size += Utility_Funcs.DoubleVariableLengthIntegerLength(Levelreq);
            size += Utility_Funcs.DoubleVariableLengthIntegerLength(Maxstack);
            size += Utility_Funcs.DoubleVariableLengthIntegerLength(Maxhp);
            size += Utility_Funcs.DoubleVariableLengthIntegerLength(Duration);
            size += Utility_Funcs.DoubleVariableLengthIntegerLength(Classuse);
            size += Utility_Funcs.DoubleVariableLengthIntegerLength(Raceuse);
            size += Utility_Funcs.DoubleVariableLengthIntegerLength(Procanim);
            size += Utility_Funcs.DoubleVariableLengthIntegerLength(Lore);
            size += Utility_Funcs.DoubleVariableLengthIntegerLength(Unk6);
            size += Utility_Funcs.DoubleVariableLengthIntegerLength(Craft);
            size += 8 + ItemName.Length + ItemDesc.Length;
            size += GetStatSize();
            return size;
        }

        private int GetStatSize()
        {
            int size = 1;
            if (Str > 0)
            {
                size++;
                Utility_Funcs.DoubleVariableLengthIntegerLength(Str);
            }

            if (Sta > 0)
            {
                size++;
                Utility_Funcs.DoubleVariableLengthIntegerLength(Sta);
            }

            if (Agi > 0)
            {
                size++;
                Utility_Funcs.DoubleVariableLengthIntegerLength(Agi);
            }

            if (Dex > 0)
            {
                size++;
                Utility_Funcs.DoubleVariableLengthIntegerLength(Dex);
            }

            if (Wis > 0)
            {
                size++;
                Utility_Funcs.DoubleVariableLengthIntegerLength(Wis);
            }

            if (Int > 0)
            {
                size++;
                Utility_Funcs.DoubleVariableLengthIntegerLength(Int);
            }

            if (Cha > 0)
            {
                size++;
                Utility_Funcs.DoubleVariableLengthIntegerLength(Cha);
            }

            if (HPMax > 0)
            {
                size++;
                Utility_Funcs.DoubleVariableLengthIntegerLength(HPMax);
            }

            if (POWMax > 0)
            {
                size++;
                Utility_Funcs.DoubleVariableLengthIntegerLength(POWMax);
            }

            if (PoT > 0)
            {
                size++;
                Utility_Funcs.DoubleVariableLengthIntegerLength(PoT);
            }

            if (HoT > 0)
            {
                size++;
                Utility_Funcs.DoubleVariableLengthIntegerLength(HoT);
            }

            if (AC > 0)
            {
                size++;
                Utility_Funcs.DoubleVariableLengthIntegerLength(AC);
            }

            if (PR > 0)
            {
                size++;
                Utility_Funcs.DoubleVariableLengthIntegerLength(PR);
            }

            if (DR > 0)
            {
                size++;
                Utility_Funcs.DoubleVariableLengthIntegerLength(DR);
            }

            if (FR > 0)
            {
                size++;
                Utility_Funcs.DoubleVariableLengthIntegerLength(FR);
            }

            if (CR > 0)
            {
                size++;
                Utility_Funcs.DoubleVariableLengthIntegerLength(CR);
            }

            if (LR > 0)
            {
                size++;
                Utility_Funcs.DoubleVariableLengthIntegerLength(LR);
            }

            if (AR > 0)
            {
                size++;
                Utility_Funcs.DoubleVariableLengthIntegerLength(AR);
            }

            return size;
        }
    }
}
