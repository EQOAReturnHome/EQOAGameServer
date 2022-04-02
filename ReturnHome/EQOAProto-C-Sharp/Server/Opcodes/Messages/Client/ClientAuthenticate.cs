using System;
using System.Configuration;
using System.Text;
using ReturnHome.Server.Network;
using ReturnHome.Utilities;

namespace ReturnHome.Server.Opcodes.Messages.Client
{
    class ClientAuthenticate
    {
        ///Authentication check
        public static void Authenticate(Session MySession, PacketMessage ClientPacket)
        {
            BufferReader reader = new(ClientPacket.Data.Span);

            Logger.Info("Processing Authentication");
            ///Opcode option? just skip for now
            reader.Position = 5;

            ///Game Code Length
            int GameCodeLength = reader.Read<int>();

            ///the actual gamecode
            string GameCode = reader.ReadString(Encoding.UTF8, GameCodeLength);

            if (GameCode == "EQOA")
            {
                ///Authenticate
                Logger.Info("Received EQOA Game Code, continuing...");

                ///Account name Length
                int AccountNameLength = reader.Read<int>();

                ///the actual gamecode
                string AccountName = reader.ReadString(Encoding.UTF8, AccountNameLength);

                Logger.Info($"Received Account Name: {AccountName}");

                ///Username ends with 01, no known use, skip for now
                reader.Position += 1;

                ReadOnlyMemory<byte> Password = reader.ReadArray<byte>(16);
                reader.Position += 16;

                ///Uncomment once ready
                //MySession.AccountID = 3;
                MySession.AccountID = Convert.ToInt32(ConfigurationManager.AppSettings["StaticAccount"]);


                ///Theoretically we want to verify account # is not 0 here, if it is, drop it.
                if (MySession.AccountID == -1)
                {
                    //Verifications failed, drop session?
                    MySession.DropSession();
                    return;
                }
            }

            else
            {
                ///If not EQOA.... drop?
                Logger.Err("Did not receive EQOA Game Code, not continuing...");
                ///Should we attempt to disconnect the session here?
            }
        }
    }
}
