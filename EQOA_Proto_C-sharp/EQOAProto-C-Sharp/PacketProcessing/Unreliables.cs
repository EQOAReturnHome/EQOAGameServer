using Sessions;
using System.Collections.Generic;
using RunningLengthCompression;
using Opcodes;
using System;
using RdpComm;
using ObjectUpdates;
using Characters;
using System.Linq;
using MessageStruct;
using EQOAProto;

namespace Unreliable
{
    public static class ProcessUnreliable
    {
        private static byte UnreliableLength = 0;
        private static ushort XorMessage = 0;
        private static byte XorByte = 0;

        public static void ProcessUnreliables(Session Mysession, ushort MessageType, ReadOnlyMemory<byte> ClientPacket, ref int offset)
        {
            if (MessageType == UnreliableTypes.ClientActorUpdate)
            {
                ProcessUnreliableClientUpdate(Mysession, ClientPacket, ref offset);
            }
        }

        //Uncompress and process update
        private static void ProcessUnreliableClientUpdate(Session MySession, ReadOnlyMemory<byte> ClientPacket, ref int offset)
        {
            //Get Unreliable length
            UnreliableLength = BinaryPrimitiveWrapper.GetLEByte(ClientPacket, ref offset);

            //Get Message # we are acknowledging
            XorMessage = BinaryPrimitiveWrapper.GetLEUShort(ClientPacket, ref offset);

            //Get xor byte /Technically not needed...? Tells us what message to xor with but that should be the last message we xor'd... right?
            XorByte = BinaryPrimitiveWrapper.GetLEByte(ClientPacket, ref offset);

            //This means that this is a deprecated packet that may of got lost on the way, base xor is behind current basexor
            //Let's not bother to even process it
            if (MySession.Channel40Base.ThisMessagenumber > (XorMessage - XorByte)) return;

            //Uncompress the packet
            List<byte> MyPacket = new List<byte> (Compression.UncompressUnreliable(ClientPacket.Span, ref offset, UnreliableLength));

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

                //Indicates character is appearing on screen/in world
                MySession.InGame = true;

                //Add xor base message to our list to track
                MySession.Channel40Base = new Message(XorMessage, MyPacket);

                //Should we generate a C9 here?
                List<byte> MyObject = Compression.CompressUnreliable(new List<byte>(ObjectUpdate.GatherObjectUpdate(MySession.MyCharacter, MySession.SessionID)));
                MyObject.Insert(0, 0);
                MyObject.InsertRange(0, BitConverter.GetBytes(MySession.Channel0Message));
                MyObject.Insert(0, 0xC9);
                MyObject.Insert(0, 0);
                //Add 0 on to end to denote end of this channel
                MyObject.Add(0);
                lock (MySession.SessionMessages)
                {
                    MySession.SessionMessages.AddRange(MyObject);
                }
                MyObject.Clear();

            }

            //Following client updates MySession.Channel40Base.ThisMessage
            //Lots of xor'ing...
            else
            {
                MySession.MyCharacter.World = (byte)(MySession.Channel40Base.ThisMessage[0] ^ MyPacket[0]);

                //This should match what we have stored for the character? Let's verify this?
                MySession.MyCharacter.XCoord = ConvertXZ(Get3ByteIntXOR((byte)(MySession.Channel40Base.ThisMessage[1] ^ MyPacket[1]), (byte)(MySession.Channel40Base.ThisMessage[2] ^ MyPacket[2]), (byte)(MySession.Channel40Base.ThisMessage[3] ^ MyPacket[3])));
                MySession.MyCharacter.YCoord = ConvertY(Get3ByteIntXOR((byte)(MySession.Channel40Base.ThisMessage[4] ^ MyPacket[4]), (byte)(MySession.Channel40Base.ThisMessage[5] ^ MyPacket[5]), (byte)(MySession.Channel40Base.ThisMessage[6] ^ MyPacket[6])));
                MySession.MyCharacter.ZCoord = ConvertXZ(Get3ByteIntXOR((byte)(MySession.Channel40Base.ThisMessage[7] ^ MyPacket[7]), (byte)(MySession.Channel40Base.ThisMessage[8] ^ MyPacket[8]), (byte)(MySession.Channel40Base.ThisMessage[9] ^ MyPacket[9])));

                //Skip 12 bytes...
                MySession.MyCharacter.Facing = ConvertFacing((byte)(MySession.Channel40Base.ThisMessage[22] ^ MyPacket[22]));

                //Skip 12 bytes...
                MySession.MyCharacter.Animation = (short)(GetShortXOR((byte)(MySession.Channel40Base.ThisMessage[35] ^ MyPacket[35]), (byte)(MySession.Channel40Base.ThisMessage[36] ^ MyPacket[36])));
                MySession.MyCharacter.Target = Get4ByteIntXOR((byte)(MySession.Channel40Base.ThisMessage[37] ^ MyPacket[37]), (byte)(MySession.Channel40Base.ThisMessage[38] ^ MyPacket[38]), (byte)(MySession.Channel40Base.ThisMessage[39] ^ MyPacket[39]), (byte)(MySession.Channel40Base.ThisMessage[40] ^ MyPacket[40]));
                
                //Means client has started a new basemessage, follow suit
                if (MySession.Channel40Base.ThisMessagenumber < (XorMessage - XorByte))
                {
                    //Grab new base message to xor against
                    Message NewBaseMessage = MySession.Channel40BaseList.Find(i => i.ThisMessagenumber == (XorMessage - XorByte));

                    //Add our new xor'd base message and start over
                    MySession.Channel40Base = new Message(XorMessage, GetArrayXOR(NewBaseMessage.ThisMessage, MyPacket));

                    //Remove all other possible base messages
                    MySession.Channel40BaseList.Clear();
                }

                //Means update is based off same xor base
                else if (MySession.Channel40Base.ThisMessagenumber == (XorMessage - XorByte))
                {
                    MySession.Channel40BaseList.Add(new Message(XorMessage, GetArrayXOR(MySession.Channel40Base.ThisMessage, MyPacket)));
                }
            }

            //Let outbound rdpreport know to include this to outbound ack's
            //This is purely as inbetween for message ack's
            MySession.Channel40Message = XorMessage;
            MySession.Channel40Ack = true;
            MyPacket.Clear();
        }

        private static float ConvertXZ(int Value)
        { return (32000.0f + 4000.0f) * Value / 0xffffff - 4000.0f; }

        private static float ConvertY(int Value)
        { return (1000.0f + 1000.0f) * Value / 0xffffff - 1000.0f; }

        private static float ConvertFacing(byte Value)
        { return ((sbyte)Value * 0.0245433693f); }

        private static int Get3ByteInt(List<byte> MyInt)
        { return (MyInt[0] << 16 | MyInt[1] << 8 | MyInt[2]);  }

        private static int Get3ByteIntXOR(byte byte1, byte byte2, byte byte3)
        { return (byte1 << 16 | byte2 << 8 | byte3); }

        private static int Get4ByteInt(List<byte> MyInt)
        { return MyInt[0] << 24 | MyInt[1] << 16 | MyInt[2] << 8 | MyInt[3]; }

        private static int Get4ByteIntXOR(byte byte1, byte byte2, byte byte3, byte byte4)
        { return byte1 << 24 | byte2 << 16 | byte3 << 8 | byte4; }

        private static short GetShort(List<byte> MyInt)
        { return (short)(MyInt[1] << 8 | MyInt[0]); }

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
