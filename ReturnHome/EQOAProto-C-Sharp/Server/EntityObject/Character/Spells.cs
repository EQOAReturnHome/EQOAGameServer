using System;
using System.IO;
using System.Text;
using ReturnHome.Utilities;

namespace ReturnHome.Server.EntityObject.Player
{
  //
    public class Spell
    {
        public int SpellID { get; private set; }
        public int AddedOrder { get; private set; }
        public int OnHotBar { get; private set; }
        public int WhereOnHotBar { get; private set; }
        public int Unk1 { get; private set; }
        public int ShowHide { get; private set; }
        public int AbilityLevel { get; private set; }
        public int Unk2 { get; private set; }
        public int Unk3 { get; private set; }
        public float SpellRange { get; private set; }
        public int CastTime { get; private set; }
        public int Power { get; private set; }
        public int IconColor { get; private set; }
        public int Icon { get; private set; }
        public int Scope { get; private set; }
        public int Recast { get; private set; }
        public int EqpRequirement { get; private set; }
        public string SpellName { get; private set; }
        public string SpellDesc { get; private set; }

        public Spell()
        { }

        //Will instantiate a spell object
        //Alot of this probably should move to some kind of scripting? Alot of repetitive data here on a per object instance basis, that could be maximized by scripting
        //Only items really needed are (int thisSpellID, int thisAddedOrder, int thisOnHotBar, int thisWhereOnHotBar, int thisUnk1, int thisShowHide) rest of data could be acquired from scripting
        public Spell(int thisSpellID, int thisAddedOrder, int thisOnHotBar, int thisWhereOnHotBar, int thisUnk1, int thisShowHide, int thisAbilityLevel, int thisUnk2, int thisUnk3, float thisRange, int thisCastTime, int thisPower, int thisIconColor, int thisIcon, int thisScope, int thisRecast, int thisEqpRequirement, string thisSpellName, string thisSpellDesc)
        {
            SpellID = thisSpellID;
            AddedOrder = thisAddedOrder;
            OnHotBar = thisOnHotBar;
            WhereOnHotBar = thisWhereOnHotBar;
            Unk1 = thisUnk1;
            ShowHide = thisShowHide;
            AbilityLevel = thisAbilityLevel;
            Unk2 = thisUnk2;
            Unk3 = thisUnk3;
            SpellRange = thisRange;
            CastTime = thisCastTime;
            Power = thisPower;
            IconColor = thisIconColor;
            Icon = thisIcon;
            Scope = thisScope;
            Recast = thisRecast;
            EqpRequirement = thisEqpRequirement;
            SpellName = thisSpellName;
            SpellDesc = thisSpellDesc;
        }

        
        public void DumpSpell(MemoryStream memStream)
        {
            //Start gathering the data
            memStream.Write(Utility_Funcs.DoublePack(SpellID));
            memStream.Write(Utility_Funcs.DoublePack(AddedOrder));
            memStream.Write(Utility_Funcs.DoublePack(OnHotBar));
            memStream.Write(Utility_Funcs.DoublePack(WhereOnHotBar));
            memStream.Write(Utility_Funcs.DoublePack(Unk1));
            memStream.Write(Utility_Funcs.DoublePack(ShowHide));
            memStream.Write(Utility_Funcs.DoublePack(AbilityLevel));
            memStream.Write(Utility_Funcs.DoublePack(Unk2));
            memStream.Write(Utility_Funcs.DoublePack(Unk3));

            //Only take the last 2 bytes, technically a half float but not much support yet in c# that I have seen
            memStream.Write(BitConverter.GetBytes(SpellRange)[2..4]);
            memStream.Write(Utility_Funcs.DoublePack(CastTime));
            memStream.Write(Utility_Funcs.DoublePack(Power));
            memStream.Write(Utility_Funcs.DoublePack(IconColor));
            memStream.Write(Utility_Funcs.DoublePack(Icon));
            memStream.Write(Utility_Funcs.DoublePack(Scope));
            memStream.Write(Utility_Funcs.DoublePack(Recast));
            memStream.Write(Utility_Funcs.DoublePack(EqpRequirement));
            memStream.Write(BitConverter.GetBytes(SpellName.Length));
            memStream.Write(Encoding.Unicode.GetBytes(SpellName));
            memStream.Write(BitConverter.GetBytes(SpellDesc.Length));
            memStream.Write(Encoding.Unicode.GetBytes(SpellDesc));
            memStream.WriteByte(0);
        }
    }
}
