using System.Collections.Generic;
using System.IO;
using ReturnHome.Server.Network;
using ReturnHome.Server.Opcodes.Messages.Server;
using ReturnHome.Utilities;

namespace ReturnHome.Server.EntityObject.Player
{
    public class Quest
    {
        private int questID;
        private int questNPCID;
        private int questIndex;
        private int questLogText;

        public Quest()
        {

        }

        public void DumpQuest(ref BufferWriter writer)
        {

        }

        public static void AddQuestLog(Session session, int questID)
        {
            /*if (session.MyCharacter.activeQuests.Count > 0)
            {
                questIndex = session.MyCharacter.activeQuests.Count;
                ServerAddQuestLog.AddQuestLog(session, questIndex, questText);
            }
            ServerAddQuestLog.AddQuestLog(session, questIndex, questText);*/

        }

        public static void DeleteQuest(Session session, byte questNumber)
        {
            /*for (int i = 0; i < session.MyCharacter.activeQuests.Count; i++)
            {
                ServerAddQuestLog.AddQuestLog(session, questIndex, questText);
            }*/
        }
    }
}
