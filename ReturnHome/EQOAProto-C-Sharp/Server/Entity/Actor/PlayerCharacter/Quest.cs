using System.Collections.Generic;

namespace ReturnHome.Playercharacter.Actor
{
    public class Quest
    {
        private string ourMessage = "";
        public Quest()
        {

        }

        public byte[] DumpQuest()
        {
            return default;
        }

        public int GetSize()
        {
            return 4 + ourMessage.Length * 2;
        }
    }
}
