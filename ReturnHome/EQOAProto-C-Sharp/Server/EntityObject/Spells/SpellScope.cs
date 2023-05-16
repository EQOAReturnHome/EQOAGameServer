// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the MIT license.


namespace ReturnHome.Server.EntityObject.Spells
{
    public enum SpellScope : byte
    {
        Self = 0,
        Target = 1,
        Group = 2,
        Pet = 3,
        Corpse = 4,
    }
}
