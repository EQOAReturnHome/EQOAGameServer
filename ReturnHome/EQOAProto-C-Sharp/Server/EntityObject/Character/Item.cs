using System;
using System.IO;
using System.Text;

using ReturnHome.Utilities;

namespace ReturnHome.Server.EntityObject.Player
{
    public class Item
    {
        //Should these be public? How would we handle adding stats to character overall stats without 100 methods?
        public byte ClientIndex;
        public int StackLeft { get; set; }
        public int RemainingHP { get; set; }
        public int Charges { get; set; }
        public int EquipLocation { get; set; }
        public byte Location { get; set; } //inventory, bank auction etc
        public int ServerKey { get; set; } //Location in inventory, would location in List suffice for this?

        public int ItemID { get; private set; }
        public uint ItemCost { get; private set; }
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
        public Item(int thisStacksLeft, int thisCharges, byte thisLocation, byte thisInventoryNumber, int thisItemID, uint thisItemCost, int thisItemIcon, int thisTrade, int thisRent, int thisCraft, int thisLore, int thisLevelreq, int thisMaxStack, string thisItemName, string thisItemDesc)

        {
            StackLeft = thisStacksLeft;
            Charges = thisCharges;
            Location = thisLocation;
            ServerKey = thisInventoryNumber;
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
        public Item(int thisStacksLeft, int thisRemainingHP, int thisCharges, int thisEquipLocation, byte thisLocation, byte thisInventoryNumber, int thisItemID, uint thisItemCost, int thisItemIcon, int thisEquipslot, int thisAttackType, int thisWeaponDamage, int thisMaxHP, int thisTrade, int thisRent, int thisCraft, int thisLore, int thisLevelreq, int thisMaxStack, string thisItemName, string thisItemDesc, int thisDuration, int thisClassuse, int thisRaceuse, int thisProcanim, int Strength, int Stamina, int Agility, int Dexterity, int Wisdom, int Intelligence, int Charisma, int HpMax, int PowMax, int pot, int hot, int ac, int pr, int dr, int fr, int cr, int lr, int ar, int model, uint color)

        {
            StackLeft = thisStacksLeft;
            Charges = thisCharges;
            Location = thisLocation;
            ServerKey = thisInventoryNumber;
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

        public Item AcquireItem(int qty)
        {
            Item item = (Item)MemberwiseClone();
            item.StackLeft = qty;
            return item;
        }

        public void DumpItem(ref BufferWriter writer)
        {
            //Start adding attributes to list for this item
            writer.Write7BitEncodedInt64(StackLeft);
            writer.Write7BitEncodedInt64(RemainingHP);
            writer.Write7BitEncodedInt64(Charges);
            writer.Write7BitEncodedInt64(EquipLocation);
            writer.Write(Location);
            writer.Write(ServerKey);
            writer.Write7BitEncodedInt64(ItemID);
            writer.Write7BitEncodedUInt64(ItemCost);
            writer.Write7BitEncodedInt64(Unk1);
            writer.Write7BitEncodedInt64(ItemIcon);
            writer.Write7BitEncodedInt64(Unk2);
            writer.Write7BitEncodedInt64(Equipslot);
            writer.Write7BitEncodedInt64(Unk3);
            writer.Write7BitEncodedInt64(Trade);
            writer.Write7BitEncodedInt64(Rent);
            writer.Write7BitEncodedInt64(Unk4);
            writer.Write7BitEncodedInt64(Attacktype);
            writer.Write7BitEncodedInt64(Weapondamage);
            writer.Write7BitEncodedInt64(Unk5);
            writer.Write7BitEncodedInt64(Levelreq);
            writer.Write7BitEncodedInt64(Maxstack);
            writer.Write7BitEncodedInt64(Maxhp);
            writer.Write7BitEncodedInt64(Duration);
            writer.Write7BitEncodedInt64(Classuse);
            writer.Write7BitEncodedInt64(Raceuse);
            writer.Write7BitEncodedInt64(Procanim);
            writer.Write7BitEncodedInt64(Lore);
            writer.Write7BitEncodedInt64(Unk6);
            writer.Write7BitEncodedInt64(Craft);
            writer.WriteString(Encoding.Unicode, ItemName);
            writer.WriteString(Encoding.Unicode, ItemDesc);
            PullStats(ref writer);
        }

        private void PullStats(ref BufferWriter writer)
        {

            //Gather stats if any exist
            //Increment counter if if statement true, then add identifier int for stat and then DoublePack and add stat value
            int position = writer.Position;
            //Place 0 place holder here
            writer.Write((byte)0);

            if (Str > 0)
            {
                Counter++;
                writer.Write((byte)0);
                writer.Write7BitEncodedInt64(Str);
            }

            if (Sta > 0)
            {
                Counter++;
                writer.Write((byte)2);
                writer.Write7BitEncodedInt64(Sta);
            }

            if (Agi > 0)
            {
                Counter++;
                writer.Write((byte)4);
                writer.Write7BitEncodedInt64(Agi);
            }

            if (Dex > 0)
            {
                Counter++;
                writer.Write((byte)6);
                writer.Write7BitEncodedInt64(Dex);
            }

            if (Wis > 0)
            {
                Counter++;
                writer.Write((byte)8);
                writer.Write7BitEncodedInt64(Wis);
            }

            if (Int > 0)
            {
                Counter++;
                writer.Write((byte)10);
                writer.Write7BitEncodedInt64(Int);
            }

            if (Cha > 0)
            {
                Counter++;
                writer.Write((byte)12);
                writer.Write7BitEncodedInt64(Cha);
            }

            if (HPMax > 0)
            {
                Counter++;
                writer.Write((byte)16);
                writer.Write7BitEncodedInt64(HPMax);
            }

            if (POWMax > 0)
            {
                Counter++;
                writer.Write((byte)20);
                writer.Write7BitEncodedInt64(POWMax);
            }

            if (PoT > 0)
            {
                Counter++;
                writer.Write((byte)24);
                writer.Write7BitEncodedInt64(PoT);
            }

            if (HoT > 0)
            {
                Counter++;
                writer.Write((byte)26);
                writer.Write7BitEncodedInt64(HoT);
            }

            if (AC > 0)
            {
                Counter++;
                writer.Write((byte)28);
                writer.Write7BitEncodedInt64(AC);
            }

            if (PR > 0)
            {
                Counter++;
                writer.Write((byte)44);
                writer.Write7BitEncodedInt64(PR);
            }

            if (DR > 0)
            {
                Counter++;
                writer.Write((byte)46);
                writer.Write7BitEncodedInt64(DR);
            }

            if (FR > 0)
            {
                Counter++;
                writer.Write((byte)48);
                writer.Write7BitEncodedInt64(FR);
            }

            if (CR > 0)
            {
                Counter++;
                writer.Write((byte)50);
                writer.Write7BitEncodedInt64(CR);
            }

            if (LR > 0)
            {
                Counter++;
                writer.Write((byte)52);
                writer.Write7BitEncodedInt64(LR);
            }

            if (AR > 0)
            {
                Counter++;
                writer.Write((byte)54);
                writer.Write7BitEncodedInt64(AR);
            }

            if (Counter == 0)
                return;

            int position2 = writer.Position;
            writer.Position = position;
            writer.Write7BitEncodedInt64(Counter);

            writer.Position = position2;
            Counter = 0;
        }
    }
}
