
using System.Collections.Generic;

namespace Quests
{
    public class Quest
    {
        private List<byte> ourMessage = new List<byte> { };
        public Quest()
        {

        }

        public List<byte> PullQuest()
        {
            //Clear List
            ourMessage.Clear();

            return ourMessage;

        }
    }
}
