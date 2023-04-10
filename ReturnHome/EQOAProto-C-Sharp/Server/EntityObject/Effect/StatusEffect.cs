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
        public uint icon;
        public uint tick;
        public uint duration;
        public uint tier;

        public StatusEffect(Effects id, uint icon, uint tick, uint duration, uint tier)
        {

        }


    }
}
