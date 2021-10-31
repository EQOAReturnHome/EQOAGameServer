using System;
using ReturnHome.Server.EntityObject.Player;
using ReturnHome.Utilities;

namespace ReturnHome.Server.EntityObject
{
    public partial class Entity
    {
        public int Race;
        public int Class;
        public string HumType;

        public string CharName;

        public int HairColor;
        public int HairLength;
        public int HairStyle;
        public int FaceOption;

        public int ModelID;
        public uint ObjectID;
        public float ModelSize = 1.0f;

        //Should Armour type info be tracked here? Makes sense
        //default is always 0
        public byte Helm = 0;
        public byte Chest = 0;
        public byte Gloves = 0;
        public byte Bracer = 0;
        public byte Legs = 0;
        public byte Boots = 0;

        //default is 0xFFFFFFFF, means no robe
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

        //This provides us with the proper gear and gear type for visual display on character
        public void EquipGear()
        {
            ///Start processing MyItem
            foreach (Item MyItem in Inventory)
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
    }
}
