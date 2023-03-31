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

            //Create new lua object
            //Lua lua = new Lua();

            //load lua CLR library

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

        public static void CastSpell(Session session, uint whereOnBar, uint target)
        {
            Console.WriteLine($"Activating spell on bar slot: {whereOnBar}");
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

            //Create new lua object
            //Lua lua = new Lua();

            //load lua CLR library

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
