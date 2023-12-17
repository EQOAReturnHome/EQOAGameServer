﻿// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the MIT license.

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ReturnHome.Server.EntityObject.Effect
{
    public enum Effects : byte
    {
       MINOR_HEALING = 0,
       DISEASE_CLOUD = 1,
       MINOR_BLESSING = 2,
       HIDE = 3
    }
}
