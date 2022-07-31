namespace ReturnHome.Server.EntityObject.Items
{
    public static class ItemInteraction
    {
        //Needs work here for equipping/unequipping gear/using items and checks against it
        public static bool EquipItem(Entity e, Item i)
        {
            if (e == null)
                return false;
            return true;
        }
    }
}
