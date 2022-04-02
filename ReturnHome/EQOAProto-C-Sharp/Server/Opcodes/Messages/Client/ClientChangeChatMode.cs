using ReturnHome.Server.Network;

namespace ReturnHome.Server.Opcodes.Messages.Client
{
    class ClientChangeChatMode
    {
        public static void ChangeChatMode(Session MySession, PacketMessage ClientPacket)
        {
            //Just accept and change chat mode
            MySession.MyCharacter.chatMode = ClientPacket.Data.Span[0];
        }
    }
}
