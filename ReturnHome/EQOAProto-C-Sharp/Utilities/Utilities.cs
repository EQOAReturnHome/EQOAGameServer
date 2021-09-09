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

        public static byte VariableLengthIntegerLength(int val)
        {
            uint value = (uint)val;

            if (value >= 0 && value <= 127)
                return 1;

            else if (value >= 128 && value <= 16383)
                return 2;

            else if (value >= 16384 && value <= 2097151)
                return 3;

            else if (value >= 2097152 && value <= 268435455)
                return 4;

            else if (value >= 268435456 && value <= 4294967295)
                return 5;

            else
                //Throw an error and return 0?
                return 0;
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

        static public byte[] Pack(uint value)
        {
            //Work some magic. Basic VLI
            List<byte> myList = new List<byte> { };

            int myVal = 0;
            int shift = 0;
            do
            {
                byte lower7bits = (byte)(value & 0x7f);
                value >>= 7;
                if (value > 0)
                {
                    myVal |= (lower7bits |= 128) << shift;
                    shift += 8;
                }
                else
                {
                    myVal |= lower7bits << shift;
                }
            } while (value > 0);

            myList.AddRange(BitConverter.GetBytes(myVal));
            int i = myList.Count - 1;
            int j = 0;
            while (myList[i] == 0) { j++; --i; }
            myList.RemoveRange(i + 1, j);
            return myList.ToArray();
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
