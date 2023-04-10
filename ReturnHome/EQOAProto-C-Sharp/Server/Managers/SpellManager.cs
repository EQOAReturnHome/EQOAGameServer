using System.IO;
using System;
using NLua;
using ReturnHome.Server.Network;
using System.Diagnostics;
using ReturnHome.Server.Opcodes.Messages.Server;
using ReturnHome.Server.EntityObject.Player;
using System.ServiceModel.Channels;
using ReturnHome.Server.EntityObject.Spells;
using ReturnHome.Server.EntityObject.Items;
using System.Collections.Concurrent;

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
            Console.Write("Trying to learn spell");
            Spell newSpell = CreateSpell(Spell);
            newSpell.AddedOrder = (byte)session.MyCharacter.MySpellBook.Count;
            session.MyCharacter.MySpellBook.AddSpellToBook(newSpell);
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

        public static void GetSpell(Session session, uint whereOnBar, uint target)
        {
            Console.WriteLine($"Activating spell on bar slot: {whereOnBar} - GetSpell");
            Spell spell = session.MyCharacter.MySpellBook.GetSpell(whereOnBar, session);

            int addedOrder = spell.AddedOrder;
            string spellName = spell.SpellName.Replace(" ", "_");
            int spellID = spell.SpellID;
            Console.WriteLine(spellName);
            //Find Lua script recursively through scripts directory by class
            string[] file = Directory.GetFiles("../../../Scripts", spellName + ".lua", SearchOption.AllDirectories);


            //TODO: work around for a spell with no scripts etc? Investigate more eventually
            if (file.Length < 1 || file == null)
                return;

            //Create handles for the lua script to access some c# variables and methods
            LuaState.State["CastSpell"] = ServerCastSpell.CastSpell;
            LuaState.State["Damage"] = ServerDamage.Damage;
            LuaState.State["CoolDown"] = ServerSpellCoolDown.SpellCoolDown;
            LuaState.State["session"] = session;
            LuaState.State["target"] = target;
            LuaState.State["spellBookLoc"] = whereOnBar;
            LuaState.State["spellID"] = spellID;
            LuaState.State["addedOrder"] = addedOrder;
            LuaState.State["TeleportPlayer"] = ServerTeleportPlayer.TeleportPlayer;

            LuaState.State["boundWorld"] = session.MyCharacter.boundWorld;
            LuaState.State["boundX"] = session.MyCharacter.boundX;
            LuaState.State["boundY"] = session.MyCharacter.boundY;
            LuaState.State["boundZ"] = session.MyCharacter.boundZ;
            LuaState.State["boundFacing"] = session.MyCharacter.boundFacing;

            LuaState.State["playerWorld"] = session.MyCharacter.World;
            LuaState.State["playerX"] = session.MyCharacter.x;
            LuaState.State["playerY"] = session.MyCharacter.y;
            LuaState.State["playerZ"] = session.MyCharacter.z;
            LuaState.State["playerFacing"] = session.MyCharacter.Facing;


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

        public static void CastSpell(Session session, uint whereOnBar, uint target)
        {
            Spell spell = session.MyCharacter.MySpellBook.GetSpell(whereOnBar, session);

            int addedOrder = spell.AddedOrder;
            string spellName = spell.SpellName.Replace(" ", "_");
            int spellID = spell.SpellID;
            Console.WriteLine(spellName);
            //Find Lua script recursively through scripts directory by class
            string[] file = Directory.GetFiles("../../../Scripts", spellName + ".lua", SearchOption.AllDirectories);


            //TODO: work around for a spell with no scripts etc? Investigate more eventually
            if (file.Length < 1 || file == null)
                return;

            //Create handles for the lua script to access some c# variables and methods
            LuaState.State["CastSpell"] = ServerCastSpell.CastSpell;
            LuaState.State["Damage"] = ServerDamage.Damage;
            LuaState.State["CoolDown"] = ServerSpellCoolDown.SpellCoolDown;
            LuaState.State["session"] = session;
            LuaState.State["target"] = target;
            LuaState.State["spellBookLoc"] = whereOnBar;
            LuaState.State["spellID"] = spellID;
            LuaState.State["addedOrder"] = addedOrder;
            LuaState.State["TeleportPlayer"] = ServerTeleportPlayer.TeleportPlayer;

            LuaState.State["boundWorld"] = session.MyCharacter.boundWorld;
            LuaState.State["boundX"] = session.MyCharacter.boundX;
            LuaState.State["boundY"] = session.MyCharacter.boundY;
            LuaState.State["boundZ"] = session.MyCharacter.boundZ;
            LuaState.State["boundFacing"] = session.MyCharacter.boundFacing;

            LuaState.State["playerWorld"] = session.MyCharacter.World;
            LuaState.State["playerX"] = session.MyCharacter.x;
            LuaState.State["playerY"] = session.MyCharacter.y;
            LuaState.State["playerZ"] = session.MyCharacter.z;
            LuaState.State["playerFacing"] = session.MyCharacter.Facing;


            //Call the Lua script found by the Directory Find above
            LuaState.State.DoFile(file[0]);

            //Call Lua function for initial interaction
            LuaFunction callFunction = LuaState.State.GetFunction("completeSpell");
            callFunction.Call();


        }
    }
}
