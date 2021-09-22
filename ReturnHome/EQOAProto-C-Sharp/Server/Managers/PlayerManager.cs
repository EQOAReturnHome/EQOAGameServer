using System;
using System.Collections.Generic;
using ReturnHome.Server.Entity.Actor;

namespace ReturnHome.Server.Managers
{
    public static class PlayerManager
    {
        private static List<Character> playerList = new();

        public static bool AddPlayer(Character character)
        {
            //Add character to our tracking List
            if (playerList.Contains(character))
                //Return false here? Boot in world character and load new one?
                return false;

            playerList.Add(character);
            return true;
        }

        public static bool RemovePlayer(Character character)
        {
            if (!playerList.Contains(character))
                return false;
            playerList.Remove(character);
            return true;   
        }
    }
}
