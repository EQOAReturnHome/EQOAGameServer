using System;
using System.Collections.Generic;

namespace ReturnHome.Server.EntityObject.Items
{
    public sealed class EquippedGear
    {
        private Entity _entity;
        //Used to translate from Itemslot to EquipSlot
        private readonly static Dictionary<ItemSlot, EquipSlot> ItemSlotToEquipSlots = new Dictionary<ItemSlot, EquipSlot>()
        {
            { ItemSlot.NotEquipped, EquipSlot.NotEquipped },
            { ItemSlot.Hand, EquipSlot.Hand },
            { ItemSlot.Head, EquipSlot.Head },
            { ItemSlot.Robe, EquipSlot.Robe },
            { ItemSlot.Earring, EquipSlot.RightEar},
            { ItemSlot.Neck, EquipSlot.Neck },
            { ItemSlot.Chest, EquipSlot.Chest },
            { ItemSlot.Forearm, EquipSlot.RightBracelet},
            { ItemSlot.TwoForearm, EquipSlot.Bracers },
            { ItemSlot.Ring, EquipSlot.RightRing },
            { ItemSlot.Belt, EquipSlot.Belt },
            { ItemSlot.Leg, EquipSlot.Leg },
            { ItemSlot.Feet, EquipSlot.Feet },
            { ItemSlot.Primary, EquipSlot.Primary },
            { ItemSlot.Shield, EquipSlot.Shield },
            { ItemSlot.Secondary, EquipSlot.Secondary },
            { ItemSlot.TwoHand, EquipSlot.TwoHand },
            { ItemSlot.Bow, EquipSlot.Thrown },
            { ItemSlot.Thrown, EquipSlot.Thrown },
            { ItemSlot.Held, EquipSlot.Held },
            { ItemSlot.Fishing, EquipSlot.Primary }, //Is this technically correct?
            { ItemSlot.Bait, EquipSlot.Secondary }, //Is this technically correct?
        };

        private Dictionary<EquipSlot, Item> EquippedItems;

        public EquippedGear(Entity entity)
        {
            _entity = entity;
            EquippedItems = new();

            foreach (EquipSlot e in Enum.GetValues(typeof(EquipSlot)))
                EquippedItems.Add(e, null);
        }

        //Should we find the appropriate slot first, then work from there?
        public void Add(Item item)
        {
            //Find appropriate slot
            EquipSlot equipSlot = ItemSlotToEquipSlots[item.itemSlot];

            //Add To Dictionary/Equip item
            EquipItem(equipSlot, item);
        }

        private void AddStats(Item item)
        {
            try
            {
                foreach (KeyValuePair<StatModifiers, int> s in item.Stats)
                    _entity.CurrentStats.Add(s.Key, s.Value);
            }

            catch
            {
                Console.WriteLine($"Issue adding stats for {_entity.CharName}");
            }
        }



        private void RemoveStats(Item item)
        {
            try
            {
                foreach (KeyValuePair<StatModifiers, int> s in item.Stats)
                    _entity.CurrentStats.Remove(s.Key, s.Value);
            }

            catch
            {
                Console.WriteLine($"Issue removing stats for {_entity.CharName}");
            }
        }

        private void EquipItem(EquipSlot equipSlot,Item item)
        {
            switch (equipSlot)
            {
                case EquipSlot.Robe:
                    if(EquippedItems[equipSlot] != null)
                        RemoveStats(EquippedItems[equipSlot]);
                    EquippedItems[equipSlot] = item;
                    _entity.Robe = item.Model;
                    _entity.RobeColor = item.Color;
                    break;

                case EquipSlot.Head:
                    if (EquippedItems[equipSlot] != null)
                        RemoveStats(EquippedItems[equipSlot]);
                    EquippedItems[equipSlot] = item;
                    _entity.Helm = (byte)item.Model;
                    _entity.HelmColor = item.Color;
                    break;

                case EquipSlot.Chest:
                    if (EquippedItems[equipSlot] != null)
                        RemoveStats(EquippedItems[equipSlot]);
                    EquippedItems[equipSlot] = item;
                    _entity.ChestColor = item.Color;
                    _entity.Chest = (byte)item.Model;
                    break;

                case EquipSlot.Bracers:
                    //If any bracelet's are equipped, unequip them and remove stats
                    if (EquippedItems[EquipSlot.LeftBracelet] != null)
                    {
                        RemoveStats(EquippedItems[EquipSlot.LeftBracelet]);
                        EquippedItems[EquipSlot.LeftBracelet] = null;
                    }

                    else if (EquippedItems[EquipSlot.RightBracelet] != null)
                    {
                        RemoveStats(EquippedItems[EquipSlot.RightBracelet]);
                        EquippedItems[EquipSlot.RightBracelet] = null;
                    }

                    EquippedItems[equipSlot] = item;
                    _entity.Bracer = (byte)item.Model;
                    _entity.BracerColor = item.Color;
                    break;

                //If bracers are equipped, remove them and stats, first bracelet is always right, left always changes.
                case EquipSlot.RightBracelet:
                case EquipSlot.LeftBracelet:
                    if (EquippedItems[EquipSlot.Bracers] != null)
                    {
                        RemoveStats(EquippedItems[EquipSlot.Bracers]);
                        EquippedItems[EquipSlot.Bracers] = null;
                    }

                    if (EquippedItems[EquipSlot.RightBracelet] == null)
                        EquippedItems[EquipSlot.RightBracelet] = item;

                    else
                    {
                        if(EquippedItems[EquipSlot.LeftBracelet] != null)
                            RemoveStats(EquippedItems[EquipSlot.LeftBracelet]);

                        EquippedItems[EquipSlot.LeftBracelet] = item;
                    }

                    _entity.Bracer = 0;
                    _entity.BracerColor = 0;
                    break;

                case EquipSlot.Hand:
                    if (EquippedItems[equipSlot] != null)
                        RemoveStats(EquippedItems[equipSlot]);
                    EquippedItems[equipSlot] = item;
                    _entity.Gloves = (byte)item.Model;
                    _entity.GloveColor = item.Color;
                    break;

                case EquipSlot.Leg:
                    if (EquippedItems[equipSlot] != null)
                        RemoveStats(EquippedItems[equipSlot]);
                    EquippedItems[equipSlot] = item;
                    _entity.LegColor = item.Color;
                    _entity.Legs = (byte)item.Model;
                    break;

                case EquipSlot.Feet:
                    if (EquippedItems[equipSlot] != null)
                        RemoveStats(EquippedItems[equipSlot]);
                    EquippedItems[equipSlot] = item;
                    _entity.Boots = (byte)item.Model;
                    _entity.BootsColor = item.Color;
                    break;

                case EquipSlot.RightRing:
                case EquipSlot.LeftRing:
                    if (EquippedItems[EquipSlot.RightRing] == null)
                        EquippedItems[EquipSlot.RightRing] = item;
                    else
                    {
                        if (EquippedItems[EquipSlot.LeftRing] != null)
                            RemoveStats(EquippedItems[EquipSlot.LeftRing]);

                        EquippedItems[EquipSlot.LeftRing] = item;
                    }
                    break;

                case EquipSlot.RightEar:
                case EquipSlot.LeftEar:
                    if (EquippedItems[EquipSlot.RightEar] == null)
                        EquippedItems[EquipSlot.RightEar] = item;
                    else
                    {
                        if (EquippedItems[EquipSlot.LeftEar] != null)
                            RemoveStats(EquippedItems[EquipSlot.LeftEar]);

                        EquippedItems[EquipSlot.LeftEar] = item;
                    }
                    break;

                case EquipSlot.Neck:
                case EquipSlot.Belt:
                    if (EquippedItems[equipSlot] == null)
                        RemoveStats(EquippedItems[equipSlot]);

                    EquippedItems[equipSlot] = item;
                    break;

                case EquipSlot.Primary:
                case EquipSlot.Secondary:
                case EquipSlot.Shield:
                case EquipSlot.Held:
                case EquipSlot.Held2:
                case EquipSlot.TwoHand:
                case EquipSlot.Thrown:
                    EquipWeapons(equipSlot, item);
                    break;
            }

            AddStats(item);
        }

        private void EquipWeapons(EquipSlot equipSlot, Item item)
        {
            switch(equipSlot)
            {
                case EquipSlot.Primary:
                    //If any of the first 2 if's are equipped, unequip them and remove stats
                    if (EquippedItems[EquipSlot.TwoHand] != null)
                    {
                        RemoveStats(EquippedItems[EquipSlot.TwoHand]);
                        EquippedItems[EquipSlot.TwoHand] = null;
                        _entity.Primary = 0;
                    }

                    if (EquippedItems[EquipSlot.Thrown] != null)
                    {
                        RemoveStats(EquippedItems[EquipSlot.Thrown]);
                        EquippedItems[EquipSlot.Thrown] = null;
                        _entity.Primary = 0;
                    }

                    if (EquippedItems[EquipSlot.Held] != null)
                    {
                        RemoveStats(EquippedItems[EquipSlot.Held]);
                        EquippedItems[EquipSlot.Held] = null;
                        _entity.Primary = 0;
                    }

                    if (EquippedItems[equipSlot] != null)
                        RemoveStats(EquippedItems[equipSlot]);
                    _entity.Primary = item.Model;
                    break;

                //If a primary is equipped, secondary is always in secondary
                case EquipSlot.Secondary:
                    //If any of the first 2 if's are equipped, unequip them and remove stats
                    if (EquippedItems[EquipSlot.TwoHand] != null)
                    {
                        RemoveStats(EquippedItems[EquipSlot.TwoHand]);
                        EquippedItems[EquipSlot.TwoHand] = null;
                        _entity.Primary = 0;
                    }

                    if(EquippedItems[EquipSlot.Thrown] != null)
                    {
                        RemoveStats(EquippedItems[EquipSlot.Thrown]);
                        EquippedItems[EquipSlot.Thrown] = null;
                        _entity.Primary = 0;
                    }

                    if (EquippedItems[EquipSlot.Primary] == null)
                    {
                        EquippedItems[EquipSlot.Primary] = item;
                        _entity.Primary = item.Model;
                    }

                    else
                    {
                        if(EquippedItems[EquipSlot.Shield] != null)
                        {
                            RemoveStats(EquippedItems[EquipSlot.Shield]);
                            EquippedItems[EquipSlot.Shield] = null;
                            _entity.Shield = 0;
                        }

                        if (EquippedItems[EquipSlot.Held2] != null)
                        {
                            RemoveStats(EquippedItems[EquipSlot.Held2]);
                            EquippedItems[EquipSlot.Held2] = null;
                            _entity.Secondary = 0;
                        }

                        if (EquippedItems[EquipSlot.Secondary] != null)
                        {
                            RemoveStats(EquippedItems[EquipSlot.Secondary]);
                            EquippedItems[EquipSlot.Secondary] = null;
                            _entity.Secondary = 0;
                        }

                        EquippedItems[equipSlot] = item;
                        _entity.Secondary = item.Model;
                    }
                    break;

                case EquipSlot.Shield:
                    if (EquippedItems[EquipSlot.TwoHand] != null)
                    {
                        RemoveStats(EquippedItems[EquipSlot.TwoHand]);
                        EquippedItems[EquipSlot.TwoHand] = null;
                        _entity.Primary = 0;
                    }

                    if (EquippedItems[EquipSlot.Thrown] != null)
                    {
                        RemoveStats(EquippedItems[EquipSlot.Thrown]);
                        EquippedItems[EquipSlot.Thrown] = null;
                        _entity.Primary = 0;
                    }

                    if (EquippedItems[EquipSlot.Shield] != null)
                    {
                        RemoveStats(EquippedItems[EquipSlot.Shield]);
                        EquippedItems[EquipSlot.Shield] = null;
                        _entity.Shield = 0;
                    }

                    if (EquippedItems[EquipSlot.Held2] != null)
                    {
                        RemoveStats(EquippedItems[EquipSlot.Held2]);
                        EquippedItems[EquipSlot.Held2] = null;
                        _entity.Secondary = 0;
                    }

                    if (EquippedItems[EquipSlot.Secondary] != null)
                    {
                        RemoveStats(EquippedItems[EquipSlot.Secondary]);
                        EquippedItems[EquipSlot.Secondary] = null;
                        _entity.Secondary = 0;
                    }

                    EquippedItems[equipSlot] = item;
                    _entity.Shield = item.Model;
                    break;

                case EquipSlot.Held:
                case EquipSlot.Held2:
                    if (EquippedItems[EquipSlot.TwoHand] != null)
                    {
                        RemoveStats(EquippedItems[EquipSlot.TwoHand]);
                        EquippedItems[EquipSlot.TwoHand] = null;
                        _entity.Primary = 0;
                    }

                    if (EquippedItems[EquipSlot.Thrown] != null)
                    {
                        RemoveStats(EquippedItems[EquipSlot.Thrown]);
                        EquippedItems[EquipSlot.Thrown] = null;
                        _entity.Primary = 0;
                    }

                    if (EquippedItems[EquipSlot.Primary] == null)
                    {
                        EquippedItems[EquipSlot.Held] = item;
                        _entity.Primary = item.Model;
                    }

                    else
                    {
                        if (EquippedItems[EquipSlot.Shield] != null)
                        {
                            RemoveStats(EquippedItems[EquipSlot.Shield]);
                            EquippedItems[EquipSlot.Shield] = null;
                            _entity.Shield = 0;
                        }

                        if (EquippedItems[EquipSlot.Held2] != null)
                        {
                            RemoveStats(EquippedItems[EquipSlot.Held2]);
                            EquippedItems[EquipSlot.Held2] = null;
                            _entity.Secondary = 0;
                        }

                        if (EquippedItems[EquipSlot.Secondary] != null)
                        {
                            RemoveStats(EquippedItems[EquipSlot.Secondary]);
                            EquippedItems[EquipSlot.Secondary] = null;
                            _entity.Secondary = 0;
                        }

                        EquippedItems[EquipSlot.Held2] = item;
                        _entity.Secondary = item.Model;
                    }
                    break;

                case EquipSlot.TwoHand:
                case EquipSlot.Thrown:
                    if (EquippedItems[EquipSlot.TwoHand] != null)
                    {
                        RemoveStats(EquippedItems[EquipSlot.TwoHand]);
                        EquippedItems[EquipSlot.TwoHand] = null;
                        _entity.Primary = 0;
                    }

                    if (EquippedItems[EquipSlot.Thrown] != null)
                    {
                        RemoveStats(EquippedItems[EquipSlot.Thrown]);
                        EquippedItems[EquipSlot.Thrown] = null;
                        _entity.Primary = 0;
                    }

                    if (EquippedItems[EquipSlot.Primary] != null)
                    {
                        RemoveStats(EquippedItems[EquipSlot.Primary]);
                        EquippedItems[EquipSlot.Primary] = null;
                        _entity.Primary = 0;
                    }

                    if (EquippedItems[EquipSlot.Held] != null)
                    {
                        RemoveStats(EquippedItems[EquipSlot.Held]);
                        EquippedItems[EquipSlot.Held] = null;
                        _entity.Primary = 0;
                    }

                    if (EquippedItems[EquipSlot.Shield] != null)
                    {
                        RemoveStats(EquippedItems[EquipSlot.Shield]);
                        EquippedItems[EquipSlot.Shield] = null;
                        _entity.Shield = 0;
                    }

                    if (EquippedItems[EquipSlot.Held2] != null)
                    {
                        RemoveStats(EquippedItems[EquipSlot.Held2]);
                        EquippedItems[EquipSlot.Held2] = null;
                        _entity.Secondary = 0;
                    }

                    if (EquippedItems[EquipSlot.Secondary] != null)
                    {
                        RemoveStats(EquippedItems[EquipSlot.Secondary]);
                        EquippedItems[EquipSlot.Secondary] = null;
                        _entity.Secondary = 0;
                    }

                    EquippedItems[equipSlot] = item;
                    _entity.Primary = item.Model;
                    break;
            }
        }
    }
}
