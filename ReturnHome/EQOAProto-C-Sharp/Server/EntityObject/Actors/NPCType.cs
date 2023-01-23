using System;

namespace ReturnHome.Server.EntityObject.Actors
{
    [Flags]
    public enum NPCType : ushort
    {
        None = 0,
        Merchant = 1,
        Banker = 2,
        Blacksmith = 8,
        NonAttackable = 0x80,
        Coachman = 0x100,
    }
}
