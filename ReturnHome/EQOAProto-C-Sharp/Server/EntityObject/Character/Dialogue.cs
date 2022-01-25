// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the MIT license.

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ReturnHome.Server.EntityObject.Player
{
    public class Dialogue
    {
        public string npcName;
        public uint counter = 0;
        public uint choice = 0;
        public string dialogue;
        public List<string> diagOptions;

        public Dialogue()
        {

        }

        public Dialogue(string npcName, uint counter, uint choice)
        {
            this.npcName = npcName;
            this.counter = counter;
            this.choice = choice;
        }

        public Dialogue convertLuaArray(Dialogue dialogue, string luaString)
        {
            dialogue.diagOptions.Add(luaString);
            return dialogue;
        }
    }
}
