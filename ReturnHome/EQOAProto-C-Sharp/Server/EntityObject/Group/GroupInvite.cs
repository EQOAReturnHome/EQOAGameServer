namespace ReturnHome.Server.EntityObject.Group
{
    public struct GroupInvite
    {
        public Entity GroupLeader;
        public Entity Invitee;

        public GroupInvite(Entity primary, Entity secondary)
        {
            GroupLeader = primary;
            Invitee = secondary;
        }
    }
}
