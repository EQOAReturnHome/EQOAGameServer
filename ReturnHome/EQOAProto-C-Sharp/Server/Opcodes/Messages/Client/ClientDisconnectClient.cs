using ReturnHome.Database.SQL;
using ReturnHome.Server.Network;

namespace ReturnHome.Server.Opcodes.Messages.Client
{
    class ClientDisconnectClient
    {
        public static void DisconnectClient(Session MySession, PacketMessage ClientPacket)
        {
            MySession.PendingTermination = true;
            //Create new handle for mysql connection
            CharacterSQL savePlayerData = new();

            //Call the mysql update query to save player data
            savePlayerData.SavePlayerData(MySession.MyCharacter, (string)Newtonsoft.Json.JsonConvert.SerializeObject(MySession.MyCharacter.playerFlags));
            //Actually drop the player's session
            MySession.DropSession();
        }
    }
}
