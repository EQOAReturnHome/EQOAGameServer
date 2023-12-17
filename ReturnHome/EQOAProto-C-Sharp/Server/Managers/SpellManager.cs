using System.IO;
using System;
using NLua;
using ReturnHome.Server.Network;
using ReturnHome.Server.Opcodes.Messages.Server;
using ReturnHome.Server.EntityObject.Player;
using ReturnHome.Server.EntityObject.Spells;
using System.Collections.Concurrent;
using ReturnHome.Server.EntityObject.Effect;
using ReturnHome.Server.EntityObject;
using System.Linq.Expressions;

namespace ReturnHome.Server.Managers
{

    public static class SpellManager
    {

        static SpellManager()
        {
        }

        private static readonly ConcurrentDictionary<int, SpellPattern> SpellList = new();

        public static void AddSpell(SpellPattern Spell) => SpellList.TryAdd(Spell.SpellID, Spell);


        public static SpellPattern GetSpellPattern(int SpellID) => SpellList[SpellID];


        public static void LearnSpell(Session session, SpellPattern Spell)
        {
            Spell newSpell = CreateSpell(Spell);
            newSpell.AddedOrder = (byte)session.MyCharacter.MySpellBook.Count;
            Console.WriteLine($"{newSpell.SpellName} going in added order {newSpell.AddedOrder}");
            session.MyCharacter.MySpellBook.AddSpellToBook(newSpell);

            Spell checkSpell = session.MyCharacter.MySpellBook.GetSpellFromBook(newSpell.AddedOrder);
            Console.WriteLine($"Spell in spellbook slot {newSpell.AddedOrder} is {checkSpell.SpellName}");
            ServerLearnSpell.LearnSpell(session, GetSpellPattern(Spell.SpellID));

        }

        public static Spell CreateSpell(SpellPattern pattern)
        {
            Spell newSpell = new Spell();
            newSpell.SpellLastUsed = 0;
            newSpell.SpellID = pattern.SpellID;
            newSpell.Unk1 = 0;
            newSpell.ShowHide = 1;
            newSpell.AbilityLevel = pattern.SpellLevel;
            newSpell.Unk2 = 0;
            newSpell.Unk3 = 0;
            newSpell.SpellRange = pattern.Range;
            newSpell.CastTime = pattern.CastTime;
            newSpell.RequiredPower = pattern.Power;
            newSpell.IconColor = pattern.IconColor;
            newSpell.Icon = pattern.Icon;
            newSpell.Scope = pattern.Scope;
            newSpell.Recast = pattern.Recast;
            newSpell.EqpRequirement = pattern.EquipReq;
            newSpell.SpellName = pattern.SpellName;
            newSpell.SpellDesc = pattern.SpellDescription;
            return newSpell;
        }

        public static void GetSpell(Entity entity, uint whereOnBar, uint target)
        {
            Spell spell = (((Character)entity).MySpellBook.GetSpell(whereOnBar));

            Console.WriteLine($"Getting initial spell {spell.SpellName}");
            int addedOrder = spell.AddedOrder;
            string spellName = spell.SpellName.ToLower().Replace(" ", "_");
            int spellID = spell.SpellID;
            //Find Lua script recursively through scripts directory by class
            string[] file = Directory.GetFiles("../../../Scripts", spellName + ".lua", SearchOption.AllDirectories);


            //TODO: work around for a spell with no scripts etc? Investigate more eventually
            if (file.Length < 1 || file == null)
                return;

            //Create handles for the lua script to access some c# variables and methods
            LuaState.State["CastSpell"] = ServerCastSpell.CastSpell;
            LuaState.State["Damage"] = ServerChangeHealth.Damage;
            LuaState.State["Heal"] = ServerChangeHealth.Heal;
            LuaState.State["CoolDown"] = ServerSpellCoolDown.SpellCoolDown;
            LuaState.State["session"] = ((Character)entity).characterSession;
            LuaState.State["target"] = target;
            LuaState.State["spellBookLoc"] = whereOnBar;
            LuaState.State["spellID"] = spellID;
            LuaState.State["addedOrder"] = addedOrder;
            LuaState.State["TeleportPlayer"] = ServerTeleportPlayer.TeleportPlayer;
            LuaState.State["GrantItem"] = ItemManager.GrantItem;
            LuaState.State["AddStatusEffect"] = entity.AddStatusEffect;

            LuaState.State["entity"] = entity;



            LuaState.State["boundWorld"] = ((Character)entity).boundWorld;
            LuaState.State["boundX"] = ((Character)entity).boundX;
            LuaState.State["boundY"] = ((Character)entity).boundY;
            LuaState.State["boundZ"] = ((Character)entity).boundZ;
            LuaState.State["boundFacing"] = ((Character)entity).boundFacing;

            LuaState.State["playerWorld"] = ((Character)entity).World;
            LuaState.State["playerX"] = ((Character)entity).x;
            LuaState.State["playerY"] = ((Character)entity).y;
            LuaState.State["playerZ"] = ((Character)entity).z;
            LuaState.State["playerFacing"] = ((Character)entity).Facing;



            //Call the Lua script found by the Directory Find above
            LuaState.State.DoFile(file[0]);

            //Call Lua function for initial interaction
            LuaFunction callFunction = LuaState.State.GetFunction("startSpell");
            callFunction.Call();



        }

        public static void FizzleSpell(Session session)
        {

            string[] file = Directory.GetFiles("../../../Scripts", "fizzle.lua", SearchOption.AllDirectories);

            //Call the Lua script found by the Directory Find above
            LuaState.State.DoFile(file[0]);

            //Call Lua function for initial interaction
            LuaFunction callFunction = LuaState.State.GetFunction("startSpell");
            callFunction.Call();

        }

        public static void CastSpell(Entity entity, uint whereOnBar, uint target)
        {
            Spell spell = (((Character)entity).MySpellBook.GetSpell(whereOnBar));
            Console.WriteLine($"Trying to cast spell {spell.SpellName}");




            int addedOrder = spell.AddedOrder;
            string spellName = spell.SpellName.ToLower().Replace(" ", "_");
            int spellID = spell.SpellID;
            //Find Lua script recursively through scripts directory by class
            string[] file = Directory.GetFiles("../../../Scripts", spellName + ".lua", SearchOption.AllDirectories);


            //TODO: work around for a spell with no scripts etc? Investigate more eventually
            if (file.Length < 1 || file == null)
                return;

            //Create handles for the lua script to access some c# variables and methods
            LuaState.State["CastSpell"] = ServerCastSpell.CastSpell;
            LuaState.State["Damage"] = ServerChangeHealth.Damage;
            LuaState.State["Heal"] = ServerChangeHealth.Heal;
            LuaState.State["CoolDown"] = ServerSpellCoolDown.SpellCoolDown;
            LuaState.State["session"] = ((Character)entity).characterSession;
            LuaState.State["target"] = target;
            LuaState.State["spellBookLoc"] = whereOnBar;
            LuaState.State["spellID"] = spellID;
            LuaState.State["addedOrder"] = addedOrder;
            LuaState.State["TeleportPlayer"] = ServerTeleportPlayer.TeleportPlayer;
            LuaState.State["entity"] = entity;
            LuaState.State["UpdateInvis"] = Entity.UpdateInvis;
            LuaState.State["UpdateSpeed"] = Entity.UpdateSpeed;


            EntityManager.QueryForEntity(entity.Target, out Entity entTarget);
            LuaState.State["entityTarget"] = entTarget;
            LuaState.State["AddStatusEffect"] = entTarget.AddStatusEffect;






            //Call the Lua script found by the Directory Find above
            LuaState.State.DoFile(file[0]);


            try
            {
                //Call Lua function for initial interaction
                LuaFunction callFunction = LuaState.State.GetFunction("completeSpell");
                callFunction.Call();
            }
            catch
            {
                return;
            }

        }

        public static void OnEffectEnd(Entity entity)
        {
            if (entity is Character)
            {
                LuaState.State["session"] = ((Character)entity).characterSession;
            }

            //Call Lua function for initial interaction
            LuaFunction callFunction = LuaState.State.GetFunction("onEffectEnd");
            callFunction.Call(entity);
        }

        public static void TickSpell(Entity entity, StatusEffect effect)
        {

            string effectName = effect.name.ToLower().Replace(" ", "_");
            //Find Lua script recursively through scripts directory by class
            string[] file = Directory.GetFiles("../../../Scripts", effectName + ".lua", SearchOption.AllDirectories);


            //TODO: work around for a spell with no scripts etc? Investigate more eventually
            if (file.Length < 1 || file == null)
                return;

            //Create handles for the lua script to access some c# variables and methods
            LuaState.State["CastSpell"] = ServerCastSpell.CastSpell;
            LuaState.State["Damage"] = ServerChangeHealth.Damage;
            LuaState.State["Heal"] = ServerChangeHealth.Heal;
            LuaState.State["CoolDown"] = ServerSpellCoolDown.SpellCoolDown;
            LuaState.State["Effect"] = effect;


            //Call the Lua script found by the Directory Find above
            LuaState.State.DoFile(file[0]);

            //Call Lua function for initial interaction
            LuaFunction callFunction = LuaState.State.GetFunction("tickSpell");
            callFunction.Call(entity);

        }


    }
}
