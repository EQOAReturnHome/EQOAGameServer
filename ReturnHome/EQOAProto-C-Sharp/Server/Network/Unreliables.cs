using System.Collections.Generic;
using System;
using System.Runtime.InteropServices;

using ReturnHome.Utilities;
using ReturnHome.Opcodes;
using ReturnHome.Server.Network;
using ReturnHome.Server.Entity.Actor;

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
        private void ProcessUnreliableClientUpdate(Session MySession, ReadOnlyMemory<byte> temp, ref int offset)
        {
            ReadOnlySpan<byte> ClientPacket = temp.Span;
            //Get Unreliable length
            UnreliableLength = ClientPacket.GetByte(ref offset);

            //Get Message # we are acknowledging
            XorMessage = ClientPacket.GetLEUShort(ref offset);

            //Get xor byte /Technically not needed...? Tells us what message to xor with but that should be the last message we xor'd... right?
            XorByte = ClientPacket.GetByte(ref offset);

            //This means that this is a deprecated packet that may of got lost on the way, base xor is behind current basexor
            //Let's not bother to even process it
            if (XorByte != 0)
            {
                //If this fails... try resending ack? make sure client got it
                if (MySession.Channel40MessageNumber != (XorMessage - XorByte))
                {
                    //0x29 is max 0x4029 size
                    for (int i = 0; i < 0x29; i++)
                    {
                        if (ClientPacket[i + offset] == 0)
                        {
                            offset += i;
                            break;
                        }
                    }

                    return;
                }
            }

            //Uncompress the packet
            ReadOnlyMemory<byte> MyPacket = _compression.Run_length_decode(ClientPacket, ref offset, UnreliableLength);
            int bytesRead = 0;
            //First 0x4029 from client
            if (XorByte == 0)
            {
                //Firsat 4029 means we are ingame
                MySession.inGame = true;

                //Copy base message to session base
                MyPacket.CopyTo(MySession.Channel40Base);

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

                MySession.Channel40MessageNumber = XorMessage;

                //Should we generate a C9 here?
                MessageStruct UpdateMessage = new(_compression.CompressUnreliable(ObjectUpdate.GatherObjectUpdate(MySession.MyCharacter, MySession.SessionID), MySession));

                MySession.sessionQueue.Add(UpdateMessage);
            }

            else
            {
                //Xor update message against base
                Xor_data(MySession.Channel40Base, MyPacket, 0x29);

                MySession.MyCharacter.World = MySession.Channel40Base.Span[0];
                bytesRead += 1;

                //This should match what we have stored for the character? Let's verify this?
                MySession.MyCharacter.XCoord = ConvertXZ(Get3ByteInt(MySession.Channel40Base.Slice(bytesRead, 3)));
                bytesRead += 3;

                MySession.MyCharacter.YCoord = ConvertY(Get3ByteInt(MySession.Channel40Base.Slice(bytesRead, 3)));
                bytesRead += 3;

                MySession.MyCharacter.ZCoord = ConvertXZ(Get3ByteInt(MySession.Channel40Base.Slice(bytesRead, 3)));
                bytesRead += 3;

                //Skip 12 bytes...
                bytesRead += 12;
                MySession.MyCharacter.Facing = ConvertFacing(MySession.Channel40Base.Span[bytesRead]);
                bytesRead += 1;

                //Skip 12 bytes...
                bytesRead += 12;
                MySession.MyCharacter.Animation = (GetShort(MySession.Channel40Base.Slice(bytesRead, 2)));
                bytesRead += 2;

                MySession.MyCharacter.Target = Get4ByteInt(MySession.Channel40Base.Slice(bytesRead, 4));

                MySession.Channel40MessageNumber = XorMessage;
            }

            MySession.Channel40Ack = true;
        }

        private static float ConvertXZ(int Value)
        { return (32000.0f + 4000.0f) * Value / 0xffffff - 4000.0f; }

        private static float ConvertY(int Value)
        { return (1000.0f + 1000.0f) * Value / 0xffffff - 1000.0f; }

        private static float ConvertFacing(byte Value)
        { return ((sbyte)Value * 0.0245433693f); }

        private static int Get3ByteInt(ReadOnlyMemory<byte> MyInt)
        { return (MyInt.Span[0] << 16 | MyInt.Span[1] << 8 | MyInt.Span[2]); }

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
            for (int i = 0; i < 0x29; i++)
            { NewList.Add((byte)(BaseMessage[i] ^ ClientMessage[i])); }

            return NewList;
        }

        public static unsafe void Xor_data(Memory<byte> dst, ReadOnlyMemory<byte> src, int len)
        {
            fixed (byte* srctempBuf = &MemoryMarshal.GetReference(src.Span))
            fixed (byte* dsttempBuf = &MemoryMarshal.GetReference(dst.Span))
            {
                byte* ptr;
                byte* srcBuf = srctempBuf;
                byte* dstBuf = dsttempBuf;
                int i;

                i = 0;
                if (0 < len)
                {
                    do
                    {
                        ptr = srcBuf + i;
                        i = i + 1;
                        *dstBuf = (byte)(*dstBuf ^ *ptr);
                        dstBuf = dstBuf + 1;
                    } while (i < len);
                }
                return;
            }
        }
    }
}
