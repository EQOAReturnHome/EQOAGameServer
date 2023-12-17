using System.IO;
using ReturnHome.Server.Opcodes;
using ReturnHome.Server.EntityObject.Player;
using NLua;
using ReturnHome.Server.Network;
using ReturnHome.Server.EntityObject;
using ReturnHome.Database.SQL;
using System.Data;

namespace ReturnHome.Server.Managers
{
    public static class EventManager
    {

        static EventManager()
        {
        }

        public static void CreatePlayerDefaults(int serverID, Character charCreation)
        {

            CharacterSQL charDefaults = new CharacterSQL();
            
            string[] file = Directory.GetFiles("../../../Scripts", "character_defaults.lua", SearchOption.AllDirectories);

            
            LuaState.State["race"] = Entity.GetRace(charCreation.EntityRace);
            LuaState.State["class"] = Entity.GetClass(charCreation.EntityClass);
            LuaState.State["humanType"] = Entity.GetHumanType(charCreation.EntityHumanType);
            LuaState.State["AddDefaultSpell"] = charDefaults.CreateDefaultSpell;
            LuaState.State["AddDefaultGear"] = charDefaults.CreateDefaultGear;
            LuaState.State["serverID"] = serverID;

            LuaState.State.DoFile(file[0]);

            LuaFunction callFunction = LuaState.State.GetFunction("CharacterDefaults");
            callFunction.Call();


        }
        public static void GetNPCDialogue(GameOpcode opcode, Session mySession)
        {
            //placeholder for choice
            string choiceOption = "abjkej";
            
            LuaState.State["race"] = Entity.GetRace(mySession.MyCharacter.EntityRace);
            LuaState.State["class"] = Entity.GetClass(mySession.MyCharacter.EntityClass);
            LuaState.State["humanType"] = Entity.GetHumanType(mySession.MyCharacter.EntityHumanType);
            LuaState.State["level"] = Entity.GetLevel(mySession);

            LuaState.State["SendDialogue"] = mySession.MyCharacter.SendDialogue;
            LuaState.State["SendMultiDialogue"] = mySession.MyCharacter.SendMultiDialogue;
            LuaState.State["mySession"] = mySession;
            LuaState.State["thisNPC"] = mySession.MyCharacter.Target;
            LuaState.State["BindPlayer"] = mySession.MyCharacter.SetPlayerBinding;
            LuaState.State["GetPlayerFlags"] = mySession.MyCharacter.GetPlayerFlags;
            LuaState.State["SetPlayerFlags"] = mySession.MyCharacter.SetPlayerFlag;

            
            string[] file;
            EntityManager.QueryForEntity(mySession.MyCharacter.Target, out Entity targetNPC);
            LuaState.State["thisEntity"] = targetNPC;

            if (mySession.MyCharacter.MyDialogue.npcName == null)
                return;
            //Strip white spaces from NPC name and replace with Underscores
            mySession.MyCharacter.MyDialogue.npcName = mySession.MyCharacter.MyDialogue.npcName.Replace(" ", "_");
            //Find Lua script recursively through scripts directory by zone
            //May rewrite later if this proves slow. Probably needs exception catching in case it doesn't find it
            if (targetNPC != null && FileExistsRecursive("../../../Scripts", mySession.MyCharacter.MyDialogue.npcName + "_" + targetNPC.ServerID + ".lua"))
            {
                file = Directory.GetFiles("../../../Scripts", mySession.MyCharacter.MyDialogue.npcName + "_" + targetNPC.ServerID + ".lua", SearchOption.AllDirectories);

            }
            else
            {
                file = Directory.GetFiles("../../../Scripts", mySession.MyCharacter.MyDialogue.npcName + ".lua", SearchOption.AllDirectories);
                if (file.Length < 1 || file == null)
                    return;
            }


            //TODO: work around for a npc with no scripts etc? Investigate more eventually
            //If the op code to be sent is a dialogue box make sure we capture dialogue choices 
            if (opcode == GameOpcode.DialogueBoxOption)
            {
                //send the dialogue choice to the lua script based on the chosen option index
                choiceOption = mySession.MyCharacter.MyDialogue.diagOptions[(int)mySession.MyCharacter.MyDialogue.choice];

            }

            //Call the Lua script found by the Dictionary Find above
            LuaState.State.DoFile(file[0]);

            //switch to find correct lua function based on op code from NPC Interact 0x04
            if (opcode == GameOpcode.DialogueBox || opcode == GameOpcode.DialogueBoxOption)
            {
                //Call Lua function for initial interaction
                LuaFunction callFunction = LuaState.State.GetFunction("event_say");
                 mySession.MyCharacter.TurnToPlayer((int)mySession.MyCharacter.Target);
                callFunction.Call(choiceOption);
            }
        }

        public static string GetMerchantDialogue(Session session, Entity npc)
        {
            string myString = " ";
            //Strip white spaces from NPC name and replace with Underscores
            string npcNameSearch = npc.CharName.Replace(" ", "_");
            //Find Lua script recursively through scripts directory by zone
            //May rewrite later if this proves slow. Probably needs exception catching in case it doesn't find it
            string[] file = Directory.GetFiles("../../../Scripts", npcNameSearch + ".lua", SearchOption.AllDirectories);
            //TODO: work around for a npc with no scripts etc? Investigate more eventually
            if (file.Length < 1 || file == null)
            {
                return myString;
            }
            LuaState.State.DoFile(file[0]);

            myString = (string)LuaState.State["merchantDialogue"];

            if (string.IsNullOrEmpty(myString))
            {
                myString = " ";
            }

            if (myString != null)
            {
                myString = myString.Replace("playerName", session.MyCharacter.CharName);
            }

            return myString;
        }

        public static bool FileExistsRecursive(string rootPath, string filename)
        {
            if (File.Exists(Path.Combine(rootPath, filename)))
                return true;

            foreach (string subDir in Directory.GetDirectories(rootPath))
            {
                if (FileExistsRecursive(subDir, filename))
                    return true;
            }

            return false;
        }

        public static void printDataTable(DataTable tbl)
        {
            string line = "";
            foreach (DataColumn item in tbl.Columns)
            {
                line += item.ColumnName + "   ";
            }
            line += "\n";
            foreach (DataRow row in tbl.Rows)
            {
                for (int i = 0; i < tbl.Columns.Count; i++)
                {
                    line += row[i].ToString() + "   ";
                }
                line += "\n";
            }
        }
    }
}
