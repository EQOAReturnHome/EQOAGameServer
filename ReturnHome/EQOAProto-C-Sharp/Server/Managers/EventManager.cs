// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the MIT license.

using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ReturnHome.Opcodes;
using ReturnHome.Server.EntityObject.Player;
using NLua;
using System.Collections;

namespace ReturnHome.Server.Managers
{
    //Can't really be a static class because then Lua can't access the methods from it.
    //If it's discovered it has to be a static class for functionality later on
    //We can go the route of using Lua's Register function but you can't pass
    //static classes to Lua because you can't instantiate them.
    public class EventManager
    {

        public Dialogue GetNPCDialogue(GameOpcode opcode, Character player)
        {
            string npcString;
            string npcStatement;
            Dialogue npcDialogue = new Dialogue();
            //Strip white spaces from NPC name and replace with Underscores
            player.MyDialogue.npcName = player.MyDialogue.npcName.Replace(" ", "_");

            //Find Lua script recursively through scripts directory by zone
            //May rewrite later if this proves slow. Probably needs exception catching in case it doesn't find it
            string[] file = Directory.GetFiles("../../../Scripts/", player.MyDialogue.npcName + ".lua", SearchOption.AllDirectories);

            //Instantiate EventManager class to pass to Lua scripts
            EventManager events = new EventManager();

            //Create new lua object
            Lua lua = new Lua();
            //load lua CLR library 
            lua.LoadCLRPackage();

            //pass lua a reference to the EventManager class which allows referencing methods and attributes of the class in Lua
            lua["events"] = events;
            lua["dialogue"] = player.MyDialogue.dialogue;
            //Call the Lua script found by the Dictionary Find above
            lua.DoFile(file[0]);

            //switch to find correct lua function based on op code from NPC Interact 0x04
            if (opcode == GameOpcode.DialogueBox || opcode == GameOpcode.OptionBox)
            {
                //Call Lua function for initial interaction
                LuaFunction callFunction = lua.GetFunction("event_say");
                callFunction.Call();
                //player.MyDialogue.dialogue = (string)lua["npcDialogue"];
                npcString = (string)lua["npcDialogue"];
                if (npcString.Contains(":::"))
                {
                    npcStatement = npcString.Split(":::")[0];
                    Console.WriteLine("In the event Manager");
                    if (npcString.Split(":::")[1] != null)
                    {

                        string npcOptionString = npcString.Split(":::")[1];
                        List<string> npcOptions = npcOptionString.Split("%").ToList();
                        npcDialogue.dialogue = npcStatement;
                        npcDialogue.diagOptions = npcOptions;
                    }
                }
                else
                {
                    npcDialogue.dialogue = npcString;
                    npcDialogue.diagOptions = null;
                }



            }
            return npcDialogue;
        }
    }




}
