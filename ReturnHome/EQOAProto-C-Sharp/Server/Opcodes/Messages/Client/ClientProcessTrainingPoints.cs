using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ReturnHome.Server.Network;
using ReturnHome.Server.Opcodes.Messages.Server;
using ReturnHome.Utilities;

namespace ReturnHome.Server.Opcodes.Messages.Client
{
    public class ClientProcessTrainingPoints
    {
        public static void ProcessTrainingPoints(Session session, PacketMessage ClientPacket)
        {
            BufferReader reader = new(ClientPacket.Data.Span);
            int StrAdd = (int)reader.Read7BitEncodedInt64();
            int StaAdd = (int)reader.Read7BitEncodedInt64();
            int AgiAdd = (int)reader.Read7BitEncodedInt64();
            int DexAdd = (int)reader.Read7BitEncodedInt64();
            int WisAdd = (int)reader.Read7BitEncodedInt64();
            int IntAdd = (int)reader.Read7BitEncodedInt64();
            int ChaAdd = (int)reader.Read7BitEncodedInt64();

            int Total = StrAdd + StaAdd + AgiAdd + DexAdd + WisAdd + IntAdd + ChaAdd;
            //Verify Total Client is requesting to spend passes server side
            if (session.MyCharacter.PlayerTrainingPoints.SpendTrainingPoints(Total))
            {
                //Check passed, add the requested stats to character
                session.MyCharacter.BaseStrength     += StrAdd;
                session.MyCharacter.BaseStamina      += StaAdd;
                session.MyCharacter.BaseAgility      += AgiAdd;
                session.MyCharacter.BaseDexterity    += DexAdd;
                session.MyCharacter.BaseWisdom       += WisAdd;
                session.MyCharacter.BaseIntelligence += IntAdd;
                session.MyCharacter.BaseCharisma     += ChaAdd;

                //Send total points as is, sending a positive number removes x amount on client
                ServerAdjustTrainingPoints.AdjustTrainingPoints(session, Total);
            }

            else
                Logger.Err($"Player: {session.MyCharacter.CharName} attempted to spend {Total} training points, only has {session.MyCharacter.PlayerTrainingPoints.RemainingTrainingPoints} points left");
        }
    }
}
