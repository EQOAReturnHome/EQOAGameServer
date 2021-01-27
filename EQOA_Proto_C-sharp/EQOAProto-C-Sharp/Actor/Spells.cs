using System;
using System.Collections.Generic;
using System.Text;
using Utility;

namespace Spells
{
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

        private List<byte> ourMessage = new List<byte> { };

        public Spell()
        { }

        //Will instantiate a spell object
        public Spell(int thisSpellID, int thisAddedOrder, int thisOnHotBar, int thisWhereOnHotBar, int thisUnk1, int thisShowHide, int thisAbilityLevel, int thisUnk2, int thisUnk3,
                     float thisRange, int thisCastTime, int thisPower, int thisIconColor, int thisIcon, int thisScope, int thisRecast, int thisEqpRequirement, string thisSpellName, string thisSpellDesc)
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

        public List<byte> PullSpell()
        {
            //Make sure this is empty before pulling new data
            ourMessage.Clear();

            //Start gathering the data
            ourMessage.AddRange(Utility_Funcs.Technique(SpellID));
            ourMessage.AddRange(Utility_Funcs.Technique(AddedOrder));
            ourMessage.AddRange(Utility_Funcs.Technique(OnHotBar));
            ourMessage.AddRange(Utility_Funcs.Technique(WhereOnHotBar));
            ourMessage.AddRange(Utility_Funcs.Technique(Unk1));
            ourMessage.AddRange(Utility_Funcs.Technique(ShowHide));
            ourMessage.AddRange(Utility_Funcs.Technique(AbilityLevel));
            ourMessage.AddRange(Utility_Funcs.Technique(Unk2));
            ourMessage.AddRange(Utility_Funcs.Technique(Unk3));
            byte[] MyRange = BitConverter.GetBytes(SpellRange);
            ourMessage.AddRange(MyRange[2..4]);
            ourMessage.AddRange(Utility_Funcs.Technique(CastTime));
            ourMessage.AddRange(Utility_Funcs.Technique(Power));
            ourMessage.AddRange(Utility_Funcs.Technique(IconColor));
            ourMessage.AddRange(Utility_Funcs.Technique(Icon));
            ourMessage.AddRange(Utility_Funcs.Technique(Scope));
            ourMessage.AddRange(Utility_Funcs.Technique(Recast));
            ourMessage.AddRange(Utility_Funcs.Technique(EqpRequirement));
            ourMessage.AddRange(BitConverter.GetBytes(SpellName.Length));
            ourMessage.AddRange(Encoding.Unicode.GetBytes(SpellName));
            ourMessage.AddRange(BitConverter.GetBytes(SpellDesc.Length));
            ourMessage.AddRange(Encoding.Unicode.GetBytes(SpellDesc));

            return ourMessage;
        }
    }
}
