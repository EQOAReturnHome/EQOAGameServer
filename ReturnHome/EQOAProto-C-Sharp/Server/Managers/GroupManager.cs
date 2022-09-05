using System.Collections.Concurrent;
using System.Collections.Generic;
using ReturnHome.Server.EntityObject;
using ReturnHome.Server.EntityObject.Group;
using ReturnHome.Server.EntityObject.Player;
using ReturnHome.Server.Network;
using ReturnHome.Server.Opcodes.Messages.Server;

namespace ReturnHome.Server.Managers
{
    public static class GroupManager
    {
        private static ConcurrentDictionary<uint, Group> _groupDict = new();
        //Probably not needed but... was an idea to track invites
        private static List<GroupInvite> GroupInviteList = new();
        public static void InviteCharacterToGroup(Session s, uint id, string name)
        {
            //Grab the invited character
            if (PlayerManager.QueryForPlayer(name, out Character c))
            {
                //is invited character in a group?
                if (c.GroupID != 0)
                {
                    //Send message to inviter stating player is in a group?
                    return;
                }

                //GroupInviteList.Add(new GroupInvite(s.MyCharacter, c));
                //Send invite to said character
                ServerCharacterGroupInvite.GroupInvite(s, c);
            }

            else
            {
                //send some kind of message to inviting player? Character may of went offline?
            }
        }

        public static void AcceptInviteToGroup(Session session, uint InviterID)
        {
            if(EntityManager.QueryForEntity(InviterID, out Entity temp))
            {
                Character GroupOwner = (Character)temp;

                //if inviting player doesn't own a group, create it
                if(GroupOwner.GroupID == 0)
                {
                    //Create the group
                    _groupDict.TryAdd(GroupOwner.ObjectID, new Group(GroupOwner.ObjectID, GroupOwner, session.MyCharacter));
                    ServerGroup.PlayerAcceptedInvite(GroupOwner.characterSession, session.MyCharacter);
                }

                //Add the member to the group
                else
                {
                    _groupDict[GroupOwner.ObjectID].AddMember(session.MyCharacter);
                    ServerGroup.PlayerAcceptedInvite(GroupOwner.characterSession, session.MyCharacter);
                }
            }
        }

        public static void DisbandGroup(Session session)
        {
            //If this works, then requesting player is leader, and able to disband
            if (_groupDict.TryRemove(session.MyCharacter.ObjectID, out Group group))
            {
                group.Disband();
            }

            //Character isn't the leader? => Send an error message? Client natively *should* catch this
        }

        public static void DenyInviteToGroup()
        {

        }

        public static void DistributeGroupUpdates()
        {
            foreach(KeyValuePair<uint, Group> pair in _groupDict)
            {
                pair.Value.DistributeUpdates();
            }
        }
    }
}
