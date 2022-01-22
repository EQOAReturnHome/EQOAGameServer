// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the MIT license.

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NLua;

namespace ReturnHome.Server.Managers
{
    class EventManager
    {
        public string npcDialogue;
        public string GetNPCDialogue(string npcName)
        {

            EventManager events = new EventManager();
            npcName = npcName.Replace(" ", "_");
            Lua lua = new Lua();
            lua.LoadCLRPackage();
            lua["events"] = events;

            lua.DoFile("../../../Scripts/Freeport/" + npcName + ".lua");
            LuaFunction callFunction = lua.GetFunction("event_say");
            callFunction.Call();
            string returnNPC = lua["npcDialogue"] as string;
            return returnNPC;




        }
    }
}
