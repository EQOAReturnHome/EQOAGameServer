// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the MIT license.

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ReturnHome.Server.Network;
using ReturnHome.Utilities;

namespace ReturnHome.Server.EntityObject.Spells
{
    public class SpellPattern
    {
        public int SpellID { get; set; }
        public int SpellLevel { get; set; }
        public int Unk2 { get; set; }
        public int Unk3 { get; set; }
        public int Range {  get; set; }
        public int CastTime { get; set; }
        public int Power { get; set; }
        public int IconColor { get; set; }
        public int Icon { get; set; }
        public int Scope { get; set; }
        public int Recast { get; set; }
        public int EquipReq { get; set; }
        public string SpellName { get; set; }
        public string SpellDescription { get; set; }

        public SpellPattern(int spellID, int spellLevel, int unk2, int unk3, int range, int castTime, int power, int iconColor, int icon, int scope, int recast, int equipReq, string spellName, string spellDescription)
        {
            this.SpellID = spellID;
            SpellLevel = spellLevel;
            Unk2 = unk2;
            Unk3 = unk3;
            Range = range;
            CastTime = castTime;
            Power = power;
            IconColor = iconColor;
            Icon = icon;
            Scope = scope;
            Recast = recast;
            EquipReq = equipReq;
            SpellName = spellName;
            SpellDescription = spellDescription;
        }

        public void DumpSpell(Session session, ref BufferWriter writer)
        {
            //Start gathering the data
            writer.Write7BitEncodedInt64(SpellID);
            writer.Write7BitEncodedInt64(session.MyCharacter.MySpellBook.Count);
            writer.Write7BitEncodedInt64(0);
            writer.Write7BitEncodedInt64(0);
            writer.Write7BitEncodedInt64(1);
            writer.Write7BitEncodedInt64(1);
            writer.Write7BitEncodedInt64(SpellLevel);
            writer.Write7BitEncodedInt64(Unk2);
            writer.Write7BitEncodedInt64(Unk3);

            writer.Write((Half)Range);
            writer.Write7BitEncodedInt64(CastTime);
            writer.Write7BitEncodedInt64(Power);
            writer.Write7BitEncodedInt64(IconColor);
            writer.Write7BitEncodedInt64(Icon);
            writer.Write7BitEncodedInt64(Scope);
            writer.Write7BitEncodedInt64(Recast);
            writer.Write7BitEncodedInt64(EquipReq);
            writer.WriteString(Encoding.Unicode, SpellName);
            writer.WriteString(Encoding.Unicode, SpellDescription);
            writer.Write((byte)0);
        }

    }

}
