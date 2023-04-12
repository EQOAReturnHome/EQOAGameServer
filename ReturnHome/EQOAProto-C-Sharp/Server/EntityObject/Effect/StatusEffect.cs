// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the MIT license.

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ReturnHome.Server.EntityObject.Effect
{
    public class StatusEffect
    {

        public Effects id;
        public string name;
        public uint icon;
        public uint tick;
        public uint duration;
        public uint tier;

        //default constructor
        public StatusEffect()
        {

        }


        //Status effect contructor for players(need name and icon for 43 unreliable message)
        public StatusEffect(Effects id, string name, uint icon, uint tick, uint duration, uint tier)
        {
            this.id = id;
            this.name = name;
            this.icon = icon;
            this.tick = tick;
            this.duration = duration;
            this.tier = tier;
        }

        //Status effect constructor for non-players
        public StatusEffect(Effects id, uint tick, uint duration, uint tier)
        {
            this.id = id;
            this.tick = tick;
            this.duration = duration;
            this.tier = tier;
        }

        public StatusEffect(string name, uint icon)
        {
            this.name = name;
            this.icon = icon;
        }


    }
}
