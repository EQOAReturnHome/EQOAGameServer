using System;
using System.Collections.Generic;
using System.Text;

namespace Inventory
{
    class Inventory
    {
        //Should these be private? How would we handle adding stats to character overall stats without 100 methods?
        private byte StackLeft;
        private int RemainingHP;
        private byte Charges;
        private byte EquipLocation;
        private byte Location; //inventory, bank auction etc
        private int InventoryNumber; //Location in inventory, would location in List suffice for this?
        private int ItemID;
        private int ItemCost;
        private int Unk1;
        private int ItemIcon;
        private int Unk2;
        private byte Equipslot;
        private int Unk3;
        private byte Trade;
        private byte Rent;
        private int Unk4;
        private int Attacktype;
        private int Weapondamage;
        private int Unk5;
        private byte Levelreq;
        private byte Maxstack;
        private int Maxhp;
        private byte Duration;
        private short Classuse;
        private short Raceuse;
        private byte Procanim;
        private byte Lore;
        private int Unk6;
        private byte Craft;
        private string ItemName;
        private string ItemDesc;
        private int Model;
        private int Color;

        //Gear stats
        private short Str;
        private short Sta;
        private short Agi;
        private short Dex;
        private short Wis;
        private short Int;
        private short Cha;
        private int HPMax;
        private int POWMax;
        private short PoT;
        private short HoT;
        private int AC;
        private short PR;
        private short DR;
        private short FR;
        private short CR;
        private short LR;
        private short AR;

        public Inventory()
        { }

        //This will instantiate an inventory object
        //Should we be able to instantiate a normal item and gear seperately? Seems the better choice
        public Inventory(int stuff)
        { 

        }
    }
}
