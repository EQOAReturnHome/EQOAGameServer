using System;
using System.IO;
using System.Text;
using ReturnHome.Server.EntityObject.Player;
using ReturnHome.Server.Managers;
using ReturnHome.Server.Opcodes.Messages.Server;
using ReturnHome.Utilities;

namespace ReturnHome.Server.EntityObject.Spells
{
    //
    public class Spell
    {
        public bool coolDownCompleted { get; private set; } = true;
        public bool spellCastingCompleted { get; private set; } = true;
        public long SpellLastUsed { get; private set; }
        public int SpellID { get; private set; }
        public byte AddedOrder { get; private set; }
        public int OnHotBar { get; private set; }
        public int WhereOnHotBar { get; private set; }
        public int Unk1 { get; private set; }
        public int ShowHide { get; private set; }
        public int AbilityLevel { get; private set; }
        public int Unk2 { get; private set; }
        public int Unk3 { get; private set; }
        public float SpellRange { get; private set; }
        public int CastTime { get; private set; }
        public int RequiredPower { get; private set; }
        public int IconColor { get; private set; }
        public int Icon { get; private set; }
        public int Scope { get; private set; }
        public int Recast { get; private set; }
        public int EqpRequirement { get; private set; }
        public string SpellName { get; private set; }
        public string SpellDesc { get; private set; }
        public long SpellEffect { get; private set; }

        public Spell()
        { }

        //Will instantiate a spell object
        //Alot of this probably should move to some kind of scripting? Alot of repetitive data here on a per object instance basis, that could be maximized by scripting
        //Only items really needed are (int thisSpellID, int thisAddedOrder, int thisOnHotBar, int thisWhereOnHotBar, int thisUnk1, int thisShowHide) rest of data could be acquired from scripting
        public Spell(int thisSpellID, byte thisAddedOrder, int thisOnHotBar, int thisWhereOnHotBar, int thisUnk1, int thisShowHide,
            int thisAbilityLevel, int thisUnk2, int thisUnk3, float thisRange, int thisCastTime, int thisPower, int thisIconColor, int thisIcon,
            int thisScope, int thisRecast, int thisEqpRequirement,string thisSpellName, string thisSpellDesc)
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
            RequiredPower = thisPower;
            IconColor = thisIconColor;
            Icon = thisIcon;
            Scope = thisScope;
            Recast = thisRecast;
            EqpRequirement = thisEqpRequirement;
            SpellName = thisSpellName;
            SpellDesc = thisSpellDesc;
            //SpellEffect = thisSpellEffect;

            Console.WriteLine(SpellName);
        }


        public void DumpSpell(ref BufferWriter writer)
        {
            //Start gathering the data
            writer.Write7BitEncodedInt64(SpellID);
            writer.Write7BitEncodedInt64(AddedOrder);
            writer.Write7BitEncodedInt64(OnHotBar);
            writer.Write7BitEncodedInt64(WhereOnHotBar);
            writer.Write7BitEncodedInt64(Unk1);
            writer.Write7BitEncodedInt64(ShowHide);
            writer.Write7BitEncodedInt64(AbilityLevel);
            writer.Write7BitEncodedInt64(Unk2);
            writer.Write7BitEncodedInt64(Unk3);

            writer.Write((Half)SpellRange);
            writer.Write7BitEncodedInt64(CastTime);
            writer.Write7BitEncodedInt64(RequiredPower);
            writer.Write7BitEncodedInt64(IconColor);
            writer.Write7BitEncodedInt64(Icon);
            writer.Write7BitEncodedInt64(Scope);
            writer.Write7BitEncodedInt64(Recast);
            writer.Write7BitEncodedInt64(EqpRequirement);
            writer.WriteString(Encoding.Unicode, SpellName);
            writer.WriteString(Encoding.Unicode, SpellDesc);
            writer.Write((byte)0);
        }

        public bool StartSpellCast(Entity e, int hotbarlocation)
        {
            Console.WriteLine($"Starting spell cast.");
            //If Target is within expected spell range and required power is available and Spell isn't on cooldown (Any other considerations?), start casting the spell
            //Should also use CurrentPower here, and return true to spell book so it can pass to cast and cool down list
            //Also need to consider sharing this to all nearby players so they see you casting... keep it simple for now
            if (e.IsWithinRange(SpellRange) && (e.CurrentPower >= RequiredPower) && coolDownCompleted)
            {
                //If entity is a player, make sure they see their own spell
                if (e.isPlayer)
                {
                    Console.WriteLine($"Casting spell");
                    SpellManager.GetSpell(((Character)e).characterSession,(uint)hotbarlocation, e.Target);

                    //TODO: May need to be different here?
                    ServerCastSpell.CastSpell(((Character)e).characterSession, SpellEffect, e.Target, CastTime);
                    ServerSpellCoolDown.SpellCoolDown(((Character)e).characterSession, AddedOrder, Recast);

                }
                //Keep this commented out for now or you run out of power
                //e.CurrentPower -= RequiredPower;
                SpellLastUsed = DateTimeOffset.UtcNow.ToUnixTimeMilliseconds();
                coolDownCompleted = false;
                spellCastingCompleted = false;

                //Need to eventually consider AOE spells also and effective targets, damage dispersement and calculations
                //May need to explore best way to disperse spell casting to nearby players here eventually, so npc and characters can disperse that message.
                return true;
            }

            return false;
        }

        public bool CoolDownCompleted()
        {
            coolDownCompleted = DateTimeOffset.UtcNow.ToUnixTimeMilliseconds() >= (SpellLastUsed + (Recast * 1000)) ? true : false;
            return coolDownCompleted;
        }

        //When cast is completed, should the damage happen a moment later, or should we do a best effort for same timing and risk damage popping up before cast completes?
        public bool CastCompleted()
        {
            spellCastingCompleted = DateTimeOffset.UtcNow.ToUnixTimeMilliseconds() >= (SpellLastUsed + (CastTime * 1000)) ? true : false;
            return spellCastingCompleted;
        }
    }
}
