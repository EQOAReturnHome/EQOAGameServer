using System;
using System.Collections.Generic;
using System.Runtime.InteropServices;
using System.Text;

namespace ReturnHome.Utilities
{
    public static class Utility_Funcs
    {

        public static ulong GetUnixEpoch(this DateTime dateTime)
        {
            var unixTime = dateTime.ToUniversalTime() - new DateTime(1970, 1, 1, 0, 0, 0, DateTimeKind.Utc);

            return (ulong)(unixTime.TotalSeconds);
        }

        //TODO: Include into BufferWriter
        public static byte DoubleVariableLengthIntegerLength(int val)
        {
            uint value = (uint)val;
            value *= 2;

            return VariableLengthIntegerLength(value);
        }

        public static byte VariableLengthIntegerLength(uint param_2)
        {
            byte result = 0;

            do
            {
                param_2 = param_2 >> 7;
                ++result;
            } while (param_2 != 0);

            return result;
        }

        //This method unpacks VLI data
        static public uint Unpack(ReadOnlySpan<byte> ClientPacket, ref int offset)
        {
            uint val = 0;

            //Loop over our packet
            for (int i = 0; i < ClientPacket.Length; i++)
            {
                //Grab the current byte and shift i * 7
                val |= (uint)((ClientPacket[offset] & 0x7f) << (i * 7));

                //Keep processing if true
                if (!((ClientPacket[offset] & 0x80) == 0))
                {
                    offset += 1;
                    continue;
                }

                offset += 1;
                break;
            }

            return val;
        }

        //Packing
        static public byte[] Pack(uint value)
        {
            byte[] temp = new byte[VariableLengthIntegerLength(value)];
            byte b;
            byte counter = 0;

            b = (byte)(value & 0xFF);
            do
            {
                value = value >> 7;
                b = (byte)(b & 0x7f);
                if (value != 0)
                    b = (byte)(b | 0x80);

                temp[counter++] = (byte)b;
                b = (byte)(value & 0xff);
            } while (value != 0);
            return temp;
        }

        static public byte[] DoublePack(int val)
        {
            uint uVar1;
            uVar1 = (uint)val;
            uVar1 = val < 0 ? ((~uVar1) << 1) + 1 : uVar1 <<= 1;

            return Pack(uVar1);
        }

        //Unpacking
        static public int DoubleUnpack(byte[] buffer)
        {
            uint val = Unpack(buffer);
            return (int)((val & 1) != 0 ? ~((val - 1) >> 1) : val >> 1);
        }

        static public uint Unpack(byte[] buffer)
        {
            byte local_60;
            uint uVar2 = 0;
            byte uVar1 = 0;
            byte counter = 0;

            while (uVar1 < 0x40)
            {
                local_60 = buffer[counter++];
                uVar2 = (uint)(uVar2 | (local_60 & 0x7f) << uVar1);
                if ((local_60 & 0x80) == 0)
                    break;
                uVar1 += 7;
            }

            return uVar2;
        }

        public static string GetMemoryString(ReadOnlySpan<byte> ClientPacket, ref int offset, int stringLength)
        {
            try
            { return Encoding.Default.GetString(ClientPacket.Slice(offset, stringLength)); }

            finally
            { offset += stringLength; }
        }

        public static void WriteToBuffer<T>(this Memory<byte> buffer, T value, ref int offset) where T : unmanaged
        {
            Span<T> span = MemoryMarshal.CreateSpan(ref value, 1);
            Span<byte> data = MemoryMarshal.AsBytes(span);
            int size = data.Length;
            data.CopyTo(buffer[offset..(offset + size)].Span);
            for (int i = 0; i < size; i++)
            {
                if (data[i] == 0)
                {
                    size = i;
                    break;
                }
            }
            offset += size;
        }
    }

    public class ByteSwaps
    {
        public static ushort SwapBytes(ushort x)
        {
            return (ushort)((ushort)((x & 0xff) << 8) | ((x >> 8) & 0xff));
        }

        public static uint SwapBytes(uint x)
        {
            return (x & 0x000000FFU) << 24 | 
                   (x & 0x0000FF00U) << 8 |
                   (x & 0x00FF0000U) >> 8 | 
                   (x & 0xFF000000U) >> 24;
        }


        public static ulong SwapBytes(ulong value)
        {
            ulong uvalue = value;
            ulong swapped =
                 ((0x00000000000000FF) & (uvalue >> 56)
                 | (0x000000000000FF00) & (uvalue >> 40)
                 | (0x0000000000FF0000) & (uvalue >> 24)
                 | (0x00000000FF000000) & (uvalue >> 8)
                 | (0x000000FF00000000) & (uvalue << 8)
                 | (0x0000FF0000000000) & (uvalue << 24)
                 | (0x00FF000000000000) & (uvalue << 40)
                 | (0xFF00000000000000) & (uvalue << 56));
            return swapped;
        }
    }
}
