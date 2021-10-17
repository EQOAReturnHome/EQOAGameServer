using System.Collections.Generic;
using System;
using System.Runtime.InteropServices;

using ReturnHome.Utilities;
using ReturnHome.Opcodes;
using ReturnHome.Server.Entity.Actor;
using System.Runtime.CompilerServices;
using ReturnHome.Server.Managers;

namespace ReturnHome.Server.Network
{
    public static class ProcessUnreliable
    {
        public static void ProcessUnreliables(Session Mysession, PacketMessage message)
        {
            if (message.Header.messageType == UnreliableTypes.ClientActorUpdate)
            {
                ProcessUnreliableClientUpdate(Mysession, message);
            }
        }

        //Uncompress and process update
        private static void ProcessUnreliableClientUpdate(Session Mysession, PacketMessage message)
        {
            Mysession.characterInWorld = true;
            int offset = 0;
            ReadOnlySpan<byte> ClientPacket = message.Data.Span;

            //If xorByte > 0, make sure it isn't an outdated update.
            //If message# - xorbyte < last ack'd, drop it
            if (message.Header.XorByte != 0)
            {
                //If this fails... try resending ack? make sure client got it
                if (Mysession.rdpCommIn.connectionData.client.BaseXorMessage != (message.Header.MessageNumber - message.Header.XorByte))
                {
                    //Doesn't really need any logging or anything, can just ignore it. so drop it...
                    return;
                }
            }

            ReadOnlyMemory<byte> thisTemp = message.Data;

            //Convert this ReadOnlyMemory to Memory
            Memory<byte> MyPacket = Unsafe.As<ReadOnlyMemory<byte>, Memory<byte>>(ref thisTemp);

            //First 0x4029 from client
            if (message.Header.XorByte == 0)
            {

                //Set new base message#, this will allow us to weed out messages trying to xor old base
                Mysession.rdpCommIn.connectionData.client.BaseXorMessage = message.Header.MessageNumber;

                //Copy base message
                Mysession.rdpCommIn.connectionData.client.UpdateBaseClientArray(MyPacket);

                Mysession.MyCharacter.World = MyPacket.Span[offset++];

                //This should match what we have stored for the character? Let's verify this?
                Mysession.MyCharacter.XCoord = CoordinateConversions.ConvertXZToFloat(ClientPacket.GetLEUInt24(ref offset));

                Mysession.MyCharacter.YCoord = CoordinateConversions.ConvertYToFloat(ClientPacket.GetLEUInt24(ref offset));

                Mysession.MyCharacter.ZCoord = CoordinateConversions.ConvertXZToFloat(ClientPacket.GetLEUInt24(ref offset));

                //Skip 12 bytes...
                offset += 12;

                Mysession.MyCharacter.Facing = CoordinateConversions.ConvertFacing(ClientPacket[offset++]);

                //Skip 12 bytes...
                offset += 12;

                Mysession.MyCharacter.Animation = ClientPacket.GetLEShort(ref offset);

                Mysession.MyCharacter.Target = ClientPacket.GetLEInt(ref offset);
                Mysession.objectUpdate = true;

                //Would likely need some checks here eventually? Shouldn't blindly trust client
                //First 4029 means we are ingame
                if (!Mysession.inGame)
                {
                    PlayerManager.AddPlayer(Mysession.MyCharacter);
                    MapManager.AddPlayerToTree(Mysession.MyCharacter);
                    Mysession.inGame = true;
                }

                else
                {
                    Mysession.MyCharacter.UpdatePosition();
                    MapManager.UpdatePosition(Mysession.MyCharacter);
                }
            }

            else
            {
                Memory<byte> temp = Mysession.rdpCommIn.connectionData.client.GetBaseClientArray();
                //Xor update message against base
                CoordinateConversions.Xor_data(temp, MyPacket, message.Header.Size);

                //Set new base message#, this will allow us to weed out messages trying to xor old base
                Mysession.rdpCommIn.connectionData.client.BaseXorMessage = message.Header.MessageNumber;

                ReadOnlySpan<byte> temp2 = temp.Span;

                Mysession.MyCharacter.World = temp2[offset++];

                //This should match what we have stored for the character? Let's verify this?
                Mysession.MyCharacter.XCoord = CoordinateConversions.ConvertXZToFloat(temp2.GetLEUInt24(ref offset));

                Mysession.MyCharacter.YCoord = CoordinateConversions.ConvertYToFloat(temp2.GetLEUInt24(ref offset));

                Mysession.MyCharacter.ZCoord = CoordinateConversions.ConvertXZToFloat(temp2.GetLEUInt24(ref offset));

                //Skip 12 bytes...
                offset += 12;
                Mysession.MyCharacter.Facing = CoordinateConversions.ConvertFacing(temp2[offset++]);

                //Skip 12 bytes...
                offset += 12;
                Mysession.MyCharacter.Animation = temp2.GetLEShort(ref offset);

                Mysession.MyCharacter.Target = temp2.GetLEInt(ref offset);
                Mysession.objectUpdate = true;

                Mysession.MyCharacter.UpdatePosition();
                MapManager.UpdatePosition(Mysession.MyCharacter);
            }

            Mysession.clientUpdateAck = true;
        }
    }
}
