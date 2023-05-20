using ReturnHome.Server.Managers;
using ReturnHome.Server.Network;
using ReturnHome.Utilities;

namespace ReturnHome.Server.Opcodes.Messages.Client
{
    public static class ClientLoadedWorld
    {
        public static void LoadedWorld(Session session, Message clientPacket)
        {
            PlayerManager.AddPlayer(session.MyCharacter);
            EntityManager.AddEntity(session.MyCharacter);
            MapManager.Add(session.MyCharacter);

            session.inGame = true;

            //This is just a shim for the player intro.
            if (session.MyCharacter.GetPlayerFlags(session, "NewPlayerIntro") == "0")
            {
                session.MyCharacter.MyDialogue.npcName = "NewPlayerIntro";
                EventManager.GetNPCDialogue(GameOpcode.DialogueBox, session);
            }

            //Put player into channel 0
            session.rdpCommIn.connectionData.serverObjects.Span[0].AddObject(session.MyCharacter);
        }
    }
}
