using Sessions;
using System.Collections.Generic;
using RunningLengthCompression;
using Opcodes;

namespace Unreliable
{
    public static class ProcessUnreliable
    {
        private static byte UnreliableLength = 0;
        private static ushort UnreliableMessageCount = 0;
        private static byte XorByte = 0;
        private static byte world = 0;
        private static float x = 0.0f;
        private static float y = 0.0f;
        private static float z = 0.0f;
        private static float facing = 0.0f;

        public static void ProcessUnreliables(Session Mysession, ushort MessageType, List<byte> MyPacket)
        {
            if (MessageType == UnreliableTypes.ClientActorUpdate)
            {
                ProcessUnreliableClientUpdate(Mysession, MyPacket);
            }
        }

        //Uncompress and process update
        private static void ProcessUnreliableClientUpdate(Session MySession, List<byte> MyPacket)
        {
            //Get Unreliable length
            UnreliableLength = MyPacket[0];

            //Get Message # we are acknowledging
            MySession.Channel40Message = (ushort)(MyPacket[2] << 8 | MyPacket[1]);

            //Get xor byte /Technically not needed...? Tells us what message to xor with but that should be the last message we xor'd... right?
            XorByte = MyPacket[3];

            //Remove read bytes
            MyPacket.RemoveRange(0, 4);

            //Uncompress the packet
            Compression.UncompressUnreliable(MyPacket, UnreliableLength);

            //First 0x4029 from client
            if (XorByte == 0)
            {
                MySession.MyCharacter.World = MyPacket[0];
                //This should match what we have stored for the character? Let's verify this?
                MySession.MyCharacter.XCoord = ConvertXZ(Get3ByteInt(MyPacket.GetRange(1, 3)));
                MySession.MyCharacter.YCoord = ConvertY(Get3ByteInt(MyPacket.GetRange(4, 3)));
                MySession.MyCharacter.ZCoord = ConvertXZ(Get3ByteInt(MyPacket.GetRange(7, 3)));

                //Skip 12 bytes...
                MySession.MyCharacter.Facing = ConvertFacing(MyPacket[22]);

                //Skip 12 bytes...
                MySession.MyCharacter.Animation = (short)(GetShort(MyPacket.GetRange(35, 2)));
                MySession.MyCharacter.Target = Get4ByteInt(MyPacket.GetRange(37, 4));

                //Add this as our base object update
                MySession.MyCharacter.BaseUpdate = MyPacket.GetRange(0, 41);

                //Should we generate a C9 here?

            }

            //Following client updates MySession.MyCharacter.BaseUpdate
            //Lots of xor'ing...
            else
            {
                MySession.MyCharacter.World = (byte)(MySession.MyCharacter.BaseUpdate[0] ^ MyPacket[0]);

                //This should match what we have stored for the character? Let's verify this?
                MySession.MyCharacter.XCoord = ConvertXZ(Get3ByteInt(MySession.MyCharacter.BaseUpdate.GetRange(1, 3)) ^ Get3ByteInt(MyPacket.GetRange(1, 3)));
                MySession.MyCharacter.YCoord = ConvertY(Get3ByteInt(MySession.MyCharacter.BaseUpdate.GetRange(4, 3)) ^ Get3ByteInt(MyPacket.GetRange(4, 3)));
                MySession.MyCharacter.ZCoord = ConvertXZ(Get3ByteInt(MySession.MyCharacter.BaseUpdate.GetRange(7, 3)) ^ Get3ByteInt(MyPacket.GetRange(7, 3)));

                //Skip 12 bytes...
                MySession.MyCharacter.Facing = ConvertFacing((byte)(MySession.MyCharacter.BaseUpdate[22] ^ MyPacket[22]));

                //Skip 12 bytes...
                MySession.MyCharacter.Animation = (short)(GetShort(MySession.MyCharacter.BaseUpdate.GetRange(35, 2)) ^ GetShort(MyPacket.GetRange(35, 2)));
                MySession.MyCharacter.Target = Get4ByteInt(MySession.MyCharacter.BaseUpdate.GetRange(37, 4)) ^ Get4ByteInt(MyPacket.GetRange(37, 4));
            }

            //Let outbound rdpreport know to include this to outbound ack's
            MySession.Channel40Ack = true;
        }

        private static float ConvertXZ(int Value)
        { return (32000.0f + 4000.0f) * Value / 0xffffff - 4000.0f; }

        private static float ConvertY(int Value)
        { return (1000.0f + 1000.0f) * Value / 0xffffff - 1000.0f; }

        private static float ConvertFacing(byte Value)
        { return ((sbyte)Value * 0.0245433693f); }

        private static int Get3ByteInt(List<byte> MyInt)
        { return (MyInt[2] << 16 | MyInt[1] << 8 | MyInt[0]);  }

        private static int Get4ByteInt(List<byte> MyInt)
        { return (MyInt[3] << 24 | MyInt[2] << 16 | MyInt[1] << 8 | MyInt[0]); }

        private static short GetShort(List<byte> MyInt)
        { return (short)(MyInt[1] << 8 | MyInt[0]); }
    }
}
