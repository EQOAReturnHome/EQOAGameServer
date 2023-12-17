namespace ReturnHome.Server.EntityObject.Items
{
    public class ItemPattern
    {
        public int ItemID { get;  set; }
        public uint ItemCost { get;  set; }
        public int Unk1 { get;  set; }
        public int ItemIcon { get;  set; }
        public int Unk2 { get;  set; }
        public ItemSlot itemSlot { get;  set; }
        public int Unk3 { get;  set; }
        public ItemFlags Flags { get;  set; }
        public int Unk4 { get;  set; }
        public int Attacktype { get;  set; }
        public int Weapondamage { get;  set; }
        public int ItemRange { get;  set; }
        public int Levelreq { get;  set; }
        public int Maxstack { get;  set; }
        public int Maxhp { get;  set; }
        public int Duration { get;  set; }
        public int Classuse { get;  set; }
        public int Raceuse { get;  set; }
        public int Procanim { get;  set; }
        public int Unk6 { get;  set; }
        public string ItemName { get;  set; }
        public string ItemDesc { get;  set; }
        public int Model { get;  set; }
        public uint Color { get;  set; }
        public int[] Stats;
        public int StatSize { get;  set; }

        public bool IsLore => (Flags & ItemFlags.Lore) == ItemFlags.Lore;
        public bool IsNoRent => (Flags & ItemFlags.NoRent) == ItemFlags.NoRent;
        public bool IsCraft => (Flags & ItemFlags.Craft) == ItemFlags.Craft;
        public bool IsNoTrade => (Flags & ItemFlags.NoTrade) == ItemFlags.NoTrade;

        public ItemPattern(int thisItemID, uint thisItemCost, int thisItemIcon, int thisItemSlot, int thisAttackType,
                    int thisWeaponDamage, int thisMaxHP, int thisLevelreq, int thisMaxStack, string thisItemName, string thisItemDesc, int thisDuration, int thisClassuse,
                    int thisRaceuse, int thisProcanim, int[] stats, int model, uint color, ItemFlags flags, int unk1, int unk2, int unk3, int unk4, int item_range, int unk6)
        {
            ItemID = thisItemID;
            ItemCost = thisItemCost;
            ItemIcon = thisItemIcon;
            itemSlot = (ItemSlot)thisItemSlot;
            Attacktype = thisAttackType;
            Weapondamage = thisWeaponDamage;
            Maxhp = thisMaxHP;
            Levelreq = thisLevelreq;
            Maxstack = thisMaxStack;
            ItemName = thisItemName;
            ItemDesc = thisItemDesc;
            Duration = thisDuration;
            Classuse = thisClassuse;
            Raceuse = thisRaceuse;
            Procanim = thisProcanim;
            Model = model;
            Color = color;
            Flags = flags;
            Unk1 = unk1;
            Unk2 = unk2;
            Unk3 = unk3;
            Unk4 = unk4;
            ItemRange = item_range;
            Unk6 = unk6;

            Stats = stats;

            for (int i = 0; i < Stats.Length; ++i)
                if (Stats[i] != 0)
                    StatSize += 1;
        }
    }
}
