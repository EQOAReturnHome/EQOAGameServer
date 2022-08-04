using System;

namespace ReturnHome.Server.EntityObject.Items
{
    public static class ItemInteraction
    {
        //Needs work here for equipping/unequipping gear/using items and checks against it
        public static bool EquipItem(Entity e, Item i)
        {
            if (e.isPlayer)
            {
                if ((((i.Classuse >> (byte)e.EntityClass) & 1) == 1) && (((i.Raceuse >> (byte)e.EntityRace) & 1) == 1) && e.Level >= i.Levelreq)
                {
                    e.equippedGear.Add(i);
                    return true;
                }
                return false;
            }

            return false;
        }
    }
}
