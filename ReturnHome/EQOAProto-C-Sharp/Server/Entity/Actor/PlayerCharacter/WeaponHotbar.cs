using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using ReturnHome.Utilities;

namespace ReturnHome.Playercharacter.Actor
{
    //Memory dump always expects 4 weapon hotbars, even if it's just "null"
    public class WeaponHotbar
    {
        public string HotbarName { get; private set; }
        public int PrimaryHandID { get; private set; }
        public int SecondaryHandID { get; private set; }

        //Default constructor
        //Even if not hotbar data, these must be -1 (Techniqued is 1)
        public WeaponHotbar()
        {
            HotbarName = "";
            PrimaryHandID = -1;
            SecondaryHandID = -1;
        }

        public WeaponHotbar(string thisHotBarName, int thisPrimaryHandID, int thisSecondaryHandID)
        {
            HotbarName = thisHotBarName;
            PrimaryHandID = thisPrimaryHandID;
            SecondaryHandID = thisSecondaryHandID;
        }
        
        public void DumpWeaponHotbar(MemoryStream memStream)
        {
            memStream.Write(Utility_Funcs.DoublePack(PrimaryHandID));
            memStream.Write(Utility_Funcs.DoublePack(SecondaryHandID));
            memStream.Write(BitConverter.GetBytes(HotbarName.Length));
            memStream.Write(Encoding.Unicode.GetBytes(HotbarName));
        }
    }
}
