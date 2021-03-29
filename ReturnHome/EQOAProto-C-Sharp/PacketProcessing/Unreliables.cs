using System.Collections.Generic;
using System;
using ReturnHome.Utilities;
using ReturnHome.Opcodes;
using ReturnHome.Actor;

namespace ReturnHome.PacketProcessing
{
    public class ProcessUnreliable
    {
        private byte UnreliableLength = 0;
        private ushort XorMessage = 0;
        private byte XorByte = 0;
        private readonly Compression _compression;

        public ProcessUnreliable()
        {
            _compression = new();
        }

        public void ProcessUnreliables(Session Mysession, ushort MessageType, ReadOnlyMemory<byte> ClientPacket, ref int offset)
        {
            if (MessageType == UnreliableTypes.ClientActorUpdate)
            {
                ProcessUnreliableClientUpdate(Mysession, ClientPacket, ref offset);
            }
        }

        //Uncompress and process update
        private void ProcessUnreliableClientUpdate(Session MySession, ReadOnlyMemory<byte> ClientPacket, ref int offset)
        {
            //Get Unreliable length
            UnreliableLength = BinaryPrimitiveWrapper.GetLEByte(ClientPacket, ref offset);

            //Get Message # we are acknowledging
            XorMessage = BinaryPrimitiveWrapper.GetLEUShort(ClientPacket, ref offset);

            //Get xor byte /Technically not needed...? Tells us what message to xor with but that should be the last message we xor'd... right?
            XorByte = BinaryPrimitiveWrapper.GetLEByte(ClientPacket, ref offset);

            //This means that this is a deprecated packet that may of got lost on the way, base xor is behind current basexor
            //Let's not bother to even process it
            if (XorByte != 0)
            {
                //If this fails... try resending ack? make sure client got it
                if (MySession.Channel40Base.Messagenumber != (XorMessage - XorByte))
                {
                    return;
                }
            }

            //Uncompress the packet
            ReadOnlyMemory<byte> MyPacket = _compression.Run_length_decode(ClientPacket.Span, ref offset, UnreliableLength);
            int bytesRead = 0;
            //First 0x4029 from client
            if (XorByte == 0)
            {
                MySession.MyCharacter.World = MyPacket.Span[0];
                bytesRead += 1;

                //This should match what we have stored for the character? Let's verify this?
                MySession.MyCharacter.XCoord = ConvertXZ(Get3ByteInt(MyPacket.Slice(bytesRead, 3)));
                bytesRead += 3;

                MySession.MyCharacter.YCoord = ConvertY(Get3ByteInt(MyPacket.Slice(bytesRead, 3)));
                bytesRead += 3;

                MySession.MyCharacter.ZCoord = ConvertXZ(Get3ByteInt(MyPacket.Slice(bytesRead, 3)));
                bytesRead += 3;

                //Skip 12 bytes...
                bytesRead += 12;
                MySession.MyCharacter.Facing = ConvertFacing(MyPacket.Span[bytesRead]);
                bytesRead += 1;

                //Skip 12 bytes...
                bytesRead += 12;
                MySession.MyCharacter.Animation = (GetShort(MyPacket.Slice(bytesRead, 2)));
                bytesRead += 2;

                MySession.MyCharacter.Target = Get4ByteInt(MyPacket.Slice(bytesRead, 4));
                bytesRead += 4;

                //MySession.BundleTypeTransition = true;
                //Add xor base message to our list to track
                MySession.Channel40Base = new MessageStruct(XorMessage, MyPacket);

                //Should we generate a C9 here?
                MessageStruct UpdateMessage = new(_compression.CompressUnreliable(ObjectUpdate.GatherObjectUpdate(MySession.MyCharacter, MySession.SessionID), MySession));

                MySession.sessionQueue.Add(UpdateMessage);
            }
            /*
            //Following client updates MySession.Channel40Base.ThisMessage
            //Lots of xor'ing...
            */

            //Purely a stop gap to run around... eventually need to expand on this to update the character object
            MySession.Channel40Base = new MessageStruct(XorMessage, MyPacket);
            MySession.Channel40Ack = true;
        }

        private static float ConvertXZ(int Value)
        { return (32000.0f + 4000.0f) * Value / 0xffffff - 4000.0f; }

        private static float ConvertY(int Value)
        { return (1000.0f + 1000.0f) * Value / 0xffffff - 1000.0f; }

        private static float ConvertFacing(byte Value)
        { return ((sbyte)Value * 0.0245433693f); }

        private static int Get3ByteInt(ReadOnlyMemory<byte> MyInt)
        { return (MyInt.Span[0] << 16 | MyInt.Span[1] << 8 | MyInt.Span[2]);  }

        private static int Get3ByteIntXOR(byte byte1, byte byte2, byte byte3)
        { return (byte1 << 16 | byte2 << 8 | byte3); }

        private static int Get4ByteInt(ReadOnlyMemory<byte> MyInt)
        { return MyInt.Span[0] << 24 | MyInt.Span[1] << 16 | MyInt.Span[2] << 8 | MyInt.Span[3]; }

        private static int Get4ByteIntXOR(byte byte1, byte byte2, byte byte3, byte byte4)
        { return byte1 << 24 | byte2 << 16 | byte3 << 8 | byte4; }

        private static short GetShort(ReadOnlyMemory<byte> MyInt)
        { return (short)(MyInt.Span[1] << 8 | MyInt.Span[0]); }

        private static short GetShortXOR(byte byte1, byte byte2)
        { return (short)(byte1 << 8 | byte2); }

        private static List<byte> GetArrayXOR(List<byte> BaseMessage, List<byte> ClientMessage)
        {
            List<byte> NewList = new List<byte> { };
            for(int i = 0; i < 0x29; i++)
            { NewList.Add((byte)(BaseMessage[i] ^ ClientMessage[i])); }

            return NewList;
        }
    }
}
