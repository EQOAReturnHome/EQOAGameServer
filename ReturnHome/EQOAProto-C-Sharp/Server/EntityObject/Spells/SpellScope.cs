// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the MIT license.


namespace ReturnHome.Server.EntityObject.Spells
{
    public enum SpellScope : byte
    {
        Self = 0,
        Target = 2,
        Group = 4,
        Pet = 6,
        Corpse = 8,
    }
}
