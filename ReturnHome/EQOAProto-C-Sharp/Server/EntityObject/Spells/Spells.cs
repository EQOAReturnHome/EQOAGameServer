using System;
using System.IO;
using System.Text;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using ReturnHome.Server.EntityObject.Player;
using ReturnHome.Server.Managers;
using ReturnHome.Server.Opcodes.Messages.Server;
using ReturnHome.Utilities;

namespace ReturnHome.Server.EntityObject.Spells
{
    //
    public class Spell
    {
        public bool coolDownCompleted { get; set; } = true;
        public bool spellCastingCompleted { get; set; } = true;
        public long SpellLastUsed { get; set; }
        public int SpellID { get; set; }
        public byte AddedOrder { get; set; }
        public int OnHotBar { get; set; }
        public int WhereOnHotBar { get; set; }
        public int Unk1 { get; set; }
        public int ShowHide { get; set; }
        public int AbilityLevel { get; set; }
        public int Unk2 { get; set; }
        public int Unk3 { get; set; }
        public float SpellRange { get; set; }
        public int CastTime { get; set; }
        public int RequiredPower { get; set; }
        public int IconColor { get; set; }
        public int Icon { get; set; }
        public int Scope { get; set; }
        public int Recast { get; set; }
        public int EqpRequirement { get; set; }
        public string SpellName { get; set; }
        public string SpellDesc { get; set; }
        public long SpellEffect { get; set; }
        public SpellType SType { get; set; }

        public Spell()
        { }

        //Will instantiate a spell object
        //Alot of this probably should move to some kind of scripting? Alot of repetitive data here on a per object instance basis, that could be maximized by scripting
        //Only items really needed are (int thisSpellID, int thisAddedOrder, int thisOnHotBar, int thisWhereOnHotBar, int thisUnk1, int thisShowHide) rest of data could be acquired from scripting
        public Spell(int thisSpellID, byte thisAddedOrder, int thisOnHotBar, int thisWhereOnHotBar, int thisUnk1, int thisShowHide,
            int thisAbilityLevel, int thisUnk2, int thisUnk3, float thisRange, int thisCastTime, int thisPower, int thisIconColor, int thisIcon,
            int thisScope, int thisRecast, int thisEqpRequirement, string thisSpellName, string thisSpellDesc, int spellType)
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
            SType = (SpellType)spellType;
            //SpellEffect = thisSpellEffect;

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
            //If Target is within expected spell range and required power is available and Spell isn't on cooldown (Any other considerations?), start casting the spell
            //Should also use CurrentPower here, and return true to spell book so it can pass to cast and cool down list
            //Also need to consider sharing this to all nearby players so they see you casting... keep it simple for now
            if (e.IsWithinRange(SpellRange) && (e.CurrentPower >= RequiredPower) && coolDownCompleted && e.isPlayer && IsValidTarget(e))
            {
                //TODO: Expand on this later  to include more functionality.
                //Is there a way to switch and re-assign target based on spell scope without fully writing all the method calls?
                switch (Scope)
                {
                    case (byte)SpellScope.Self:
                        if (e.isPlayer)
                        {
                            SpellManager.GetSpell(((Character)e), (uint)hotbarlocation, e.ObjectID);

                            //TODO: May need to be different here?
                            ServerCastSpell.CastSpell(((Character)e).characterSession, SpellEffect, e.ObjectID, CastTime);
                            ServerSpellCoolDown.SpellCoolDown(((Character)e).characterSession, AddedOrder, Recast);

                        }
                        break;
                    case (byte)SpellScope.Pet:
                        if (e.isPlayer)
                        {
                            SpellManager.GetSpell(((Character)e), (uint)hotbarlocation, e.Target);

                            //TODO: May need to be different here?
                            ServerCastSpell.CastSpell(((Character)e).characterSession, SpellEffect, e.Target, CastTime);
                            ServerSpellCoolDown.SpellCoolDown(((Character)e).characterSession, AddedOrder, Recast);

                        }
                        break;
                    case (byte)SpellScope.Group:
                        if (e.isPlayer)
                        {
                            SpellManager.GetSpell(((Character)e), (uint)hotbarlocation, e.Target);

                            //TODO: May need to be different here?
                            ServerCastSpell.CastSpell(((Character)e).characterSession, SpellEffect, e.Target, CastTime);
                            ServerSpellCoolDown.SpellCoolDown(((Character)e).characterSession, AddedOrder, Recast);

                        }
                        break;
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
            else
            {
                SpellManager.FizzleSpell(((Character)e).characterSession);
                return false;

            }

        }

        //TODO: Flesh out for enemies as well as players? This may not be quite right
        //verify whether the spells target is valid for the caster type
        public bool IsValidTarget(Entity e)
        {
            EntityManager.QueryForEntity(e.Target, out Entity ent);

            if (Scope == (byte)SpellScope.Self)
            {
                return true;
            }

            //if the player is targeting another player allow defenensive spells -- i.e heals, buffs, etc
            //but not offensive spells. Dueling later will need another consideration.
            if (((Character)e).isPlayer && ent.isPlayer)
            {
                switch (SType)
                {
                    case SpellType.Friendly: return true;
                    case SpellType.Offensive: return true;
                    case SpellType.Defensive: return true;
                }
            }
            //If the player is not targeting a player allow offensive spells only
            else if (((Character)e).isPlayer && !ent.isPlayer)
            {
                switch (SType)
                {
                    case SpellType.Friendly: return true;
                    case SpellType.Defensive: return false;
                    case SpellType.Offensive: return true;
                }
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
