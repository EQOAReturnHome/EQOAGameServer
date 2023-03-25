using System.IO;
using System;
using NLua;
using ReturnHome.Server.Network;
using System.Diagnostics;
using ReturnHome.Server.Opcodes.Messages.Server;
using ReturnHome.Server.EntityObject.Player;
using System.ServiceModel.Channels;
using ReturnHome.Server.EntityObject.Spells;

namespace ReturnHome.Server.Managers
{

    public static class SpellManager
    {
        public static Lua lua = new Lua();

        static SpellManager()
        {
            lua.LoadCLRPackage();
        }

        public static void GetSpell(Session session, uint whereOnBar, uint target)
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
            lua["CastSpell"] = ServerCastSpell.CastSpell;
            lua["Damage"] = ServerDamage.Damage;
            lua["CoolDown"] = ServerSpellCoolDown.SpellCoolDown;
            lua["session"] = session;
            lua["target"] = target;
            lua["spellBookLoc"] = whereOnBar;
            lua["spellID"] = spellID;
            lua["addedOrder"] = addedOrder;
            lua["TeleportPlayer"] = ServerTeleportPlayer.TeleportPlayer;

            lua["boundWorld"] = session.MyCharacter.boundWorld;
            lua["boundX"] = session.MyCharacter.boundX;
            lua["boundY"] = session.MyCharacter.boundY;
            lua["boundZ"] = session.MyCharacter.boundZ;
            lua["boundFacing"] = session.MyCharacter.boundFacing;

            lua["playerWorld"] = session.MyCharacter.World;
            lua["playerX"] = session.MyCharacter.x;
            lua["playerY"] = session.MyCharacter.y;
            lua["playerZ"] = session.MyCharacter.z;
            lua["playerFacing"] = session.MyCharacter.Facing;


            //Call the Lua script found by the Directory Find above
            lua.DoFile(file[0]);

                //Call Lua function for initial interaction
                LuaFunction callFunction = lua.GetFunction("startSpell");
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
            lua["CastSpell"] = ServerCastSpell.CastSpell;
            lua["Damage"] = ServerDamage.Damage;
            lua["CoolDown"] = ServerSpellCoolDown.SpellCoolDown;
            lua["session"] = session;
            lua["target"] = target;
            lua["spellBookLoc"] = whereOnBar;
            lua["spellID"] = spellID;
            lua["addedOrder"] = addedOrder;
            lua["TeleportPlayer"] = ServerTeleportPlayer.TeleportPlayer;

            lua["boundWorld"] = session.MyCharacter.boundWorld;
            lua["boundX"] = session.MyCharacter.boundX;
            lua["boundY"] = session.MyCharacter.boundY;
            lua["boundZ"] = session.MyCharacter.boundZ;
            lua["boundFacing"] = session.MyCharacter.boundFacing;

            lua["playerWorld"] = session.MyCharacter.World;
            lua["playerX"] = session.MyCharacter.x;
            lua["playerY"] = session.MyCharacter.y;
            lua["playerZ"] = session.MyCharacter.z;
            lua["playerFacing"] = session.MyCharacter.Facing;


            //Call the Lua script found by the Directory Find above
            lua.DoFile(file[0]);

            //Call Lua function for initial interaction
            LuaFunction callFunction = lua.GetFunction("completeSpell");
            callFunction.Call();


        }
    }
}
