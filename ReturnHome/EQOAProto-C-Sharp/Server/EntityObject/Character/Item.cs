using System;
using System.IO;
using System.Text;

using ReturnHome.Utilities;

namespace ReturnHome.Server.EntityObject.Player
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

        //Stat counter
        private int Counter;

        //default constructor
        public Item()
        { }

        //This will instantiate an inventory object
        //Should we be able to instantiate a normal item and gear seperately? Seems the better choice
        //This is to instantiate 
        public Item(int thisStacksLeft, int thisCharges, byte thisLocation, int thisInventoryNumber, int thisItemID, int thisItemCost, int thisItemIcon, int thisTrade, int thisRent, int thisCraft, int thisLore, int thisLevelreq, int thisMaxStack, string thisItemName, string thisItemDesc)
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
        //Alot of this could be managed by scripting as there is a huge portion that is static
        //Varis: int thisStacksLeft, int thisRemainingHP, int thisCharges, int thisEquipLocation, byte thisLocation, int thisInventoryNumber, int thisItemID<- use this in scripting to get right gear?
        public Item(int thisStacksLeft, int thisRemainingHP, int thisCharges, int thisEquipLocation, byte thisLocation, int thisInventoryNumber, int thisItemID, int thisItemCost, int thisItemIcon, int thisEquipslot, int thisAttackType, int thisWeaponDamage, int thisMaxHP, int thisTrade, int thisRent, int thisCraft, int thisLore, int thisLevelreq, int thisMaxStack, string thisItemName, string thisItemDesc, int thisDuration, int thisClassuse, int thisRaceuse, int thisProcanim, int Strength, int Stamina, int Agility, int Dexterity, int Wisdom, int Intelligence, int Charisma, int HpMax, int PowMax, int pot, int hot, int ac, int pr, int dr, int fr, int cr, int lr, int ar, int model, uint color)
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

        public void DumpItem(MemoryStream memStream)
        {
            //Start adding attributes to list for this item
            memStream.Write(Utility_Funcs.DoublePack(StackLeft));
            memStream.Write(Utility_Funcs.DoublePack(RemainingHP));
            memStream.Write(Utility_Funcs.DoublePack(Charges));
            memStream.Write(Utility_Funcs.DoublePack(EquipLocation));
            memStream.Write(Utility_Funcs.DoublePack(Location));
            memStream.Write(BitConverter.GetBytes(InventoryNumber));
            memStream.Write(Utility_Funcs.DoublePack(ItemID));
            memStream.Write(Utility_Funcs.DoublePack(ItemCost));
            memStream.Write(Utility_Funcs.DoublePack(Unk1));
            memStream.Write(Utility_Funcs.DoublePack(ItemIcon));
            memStream.Write(Utility_Funcs.DoublePack(Unk2));
            memStream.Write(Utility_Funcs.DoublePack(Equipslot));
            memStream.Write(Utility_Funcs.DoublePack(Unk3));
            memStream.Write(Utility_Funcs.DoublePack(Trade));
            memStream.Write(Utility_Funcs.DoublePack(Rent));
            memStream.Write(Utility_Funcs.DoublePack(Unk4));
            memStream.Write(Utility_Funcs.DoublePack(Attacktype));
            memStream.Write(Utility_Funcs.DoublePack(Weapondamage));
            memStream.Write(Utility_Funcs.DoublePack(Unk5));
            memStream.Write(Utility_Funcs.DoublePack(Levelreq));
            memStream.Write(Utility_Funcs.DoublePack(Maxstack));
            memStream.Write(Utility_Funcs.DoublePack(Maxhp));
            memStream.Write(Utility_Funcs.DoublePack(Duration));
            memStream.Write(Utility_Funcs.DoublePack(Classuse));
            memStream.Write(Utility_Funcs.DoublePack(Raceuse));
            memStream.Write(Utility_Funcs.DoublePack(Procanim));
            memStream.Write(Utility_Funcs.DoublePack(Lore));
            memStream.Write(Utility_Funcs.DoublePack(Unk6));
            memStream.Write(Utility_Funcs.DoublePack(Craft));
            memStream.Write(BitConverter.GetBytes(ItemName.Length));
            memStream.Write(Encoding.Unicode.GetBytes(ItemName));
            memStream.Write(BitConverter.GetBytes(ItemDesc.Length));
            memStream.Write(Encoding.Unicode.GetBytes(ItemDesc));
            PullStats(memStream);
        }

        private void PullStats(MemoryStream memStream)
        {

            //Gather stats if any exist
            //Increment counter if if statement true, then add identifier int for stat and then DoublePack and add stat value
            long position = memStream.Position;
            //Place 0 place holder here
            memStream.WriteByte(0);

            if (Str > 0)
            {
                Counter++;
                memStream.WriteByte(0);
                memStream.Write(Utility_Funcs.DoublePack(Str));
            }

            if (Sta > 0)
            {
                Counter++;
                memStream.WriteByte(2);
                memStream.Write(Utility_Funcs.DoublePack(Sta));
            }

            if (Agi > 0)
            {
                Counter++;
                memStream.WriteByte(4);
                memStream.Write(Utility_Funcs.DoublePack(Agi));
            }

            if (Dex > 0)
            {
                Counter++;
                memStream.WriteByte(6);
                memStream.Write(Utility_Funcs.DoublePack(Dex));
            }

            if (Wis > 0)
            {
                Counter++;
                memStream.WriteByte(8);
                memStream.Write(Utility_Funcs.DoublePack(Wis));
            }

            if (Int > 0)
            {
                Counter++;
                memStream.WriteByte(10);
                memStream.Write(Utility_Funcs.DoublePack(Int));
            }

            if (Cha > 0)
            {
                Counter++;
                memStream.WriteByte(12);
                memStream.Write(Utility_Funcs.DoublePack(Cha));
            }

            if (HPMax > 0)
            {
                Counter++;
                memStream.WriteByte(16);
                memStream.Write(Utility_Funcs.DoublePack(HPMax));
            }

            if (POWMax > 0)
            {
                Counter++;
                memStream.WriteByte(20);
                memStream.Write(Utility_Funcs.DoublePack(POWMax));
            }

            if (PoT > 0)
            {
                Counter++;
                memStream.WriteByte(24);
                memStream.Write(Utility_Funcs.DoublePack(PoT));
            }

            if (HoT > 0)
            {
                Counter++;
                memStream.WriteByte(26);
                memStream.Write(Utility_Funcs.DoublePack(HoT));
            }

            if (AC > 0)
            {
                Counter++;
                memStream.WriteByte(28);
                memStream.Write(Utility_Funcs.DoublePack(AC));
            }

            if (PR > 0)
            {
                Counter++;
                memStream.WriteByte(44);
                memStream.Write(Utility_Funcs.DoublePack(PR));
            }

            if (DR > 0)
            {
                Counter++;
                memStream.WriteByte(46);
                memStream.Write(Utility_Funcs.DoublePack(DR));
            }

            if (FR > 0)
            {
                Counter++;
                memStream.WriteByte(48);
                memStream.Write(Utility_Funcs.DoublePack(FR));
            }

            if (CR > 0)
            {
                Counter++;
                memStream.WriteByte(50);
                memStream.Write(Utility_Funcs.DoublePack(CR));
            }

            if (LR > 0)
            {
                Counter++;
                memStream.WriteByte(52);
                memStream.Write(Utility_Funcs.DoublePack(LR));
            }

            if (AR > 0)
            {
                Counter++;
                memStream.WriteByte(54);
                memStream.Write(Utility_Funcs.DoublePack(AR));
            }

            if (Counter == 0)
                return;
            long position2 = memStream.Position;
            memStream.Position = position;
            memStream.Write(Utility_Funcs.DoublePack(Counter));

            memStream.Position = position2;
        }
    }
}
