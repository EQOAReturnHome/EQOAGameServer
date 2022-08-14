using System.IO;
using System;
using NLua;
using ReturnHome.Server.Network;
using System.Diagnostics;
using ReturnHome.Server.Opcodes.Messages.Server;
using ReturnHome.Server.EntityObject.Player;

namespace ReturnHome.Server.Managers
{
    public static class SpellManager
    {
        public static void GetSpell(Session session, uint whereOnBar, uint target)
        {
            /*
            int spellID = session.MyCharacter.MySpellIDs[whereOnBar];
            int addedOrder = session.MyCharacter.MySpellBook[spellID];
            //Find Lua script recursively through scripts directory by class
            string[] file = Directory.GetFiles("Scripts\\", spellID.ToString() + ".lua", SearchOption.AllDirectories);

            //TODO: work around for a spell with no scripts etc? Investigate more eventually
            if (file.Length < 1 || file == null)
                return;

            //Create new lua object
            Lua lua = new Lua();

            //load lua CLR library
            lua.LoadCLRPackage();

            //Create handles for the lua script to access some c# variables and methods
            lua["CastSpell"] = ServerCastSpell.CastSpell;
            lua["Damage"] = ServerDamage.Damage;
            lua["CoolDown"] = ServerSpellCoolDown.SpellCoolDown;
            lua["session"] = session;
            lua["target"] = target;
            lua["spellBookLoc"] = whereOnBar;
            lua["spellID"] = spellID;
            lua["addedOrder"] = addedOrder;

            //Call the Lua script found by the Directory Find above
            lua.DoFile(file[0]);

            //Call Lua function for initial interaction
            LuaFunction callFunction = lua.GetFunction("spell");
            callFunction.Call();
            */
        }
    }
}
