using Microsoft.VisualBasic.CompilerServices;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Utility
{
    public static class Utility_Funcs
    {

        public static ulong GetUnixEpoch(this DateTime dateTime)
        {
            var unixTime = dateTime.ToUniversalTime() - new DateTime(1970, 1, 1, 0, 0, 0, DateTimeKind.Utc);

            return (ulong)(unixTime.TotalSeconds);
        }

        ///Helper function encompassing the technique with overloads
        public static List<byte> Technique(byte val)
        {
            byte[] MyVal = BitConverter.GetBytes(RealTechnique((long)val));

            int lastIndex = Array.FindLastIndex(MyVal, b => b != 0);

            if (lastIndex == -1) { Array.Resize(ref MyVal, lastIndex + 2); }///Resizes byte array to real value 
            else { Array.Resize(ref MyVal, lastIndex + 1); } ///Resizes byte array to real value  
            return MyVal.ToList();
        }

        ///Helper function encompassing the technique with overloads
        public static List<byte> Technique(sbyte val)
        {
            byte[] MyVal = BitConverter.GetBytes(RealTechnique((long)val));

            int lastIndex = Array.FindLastIndex(MyVal, b => b != 0);

            if (lastIndex == -1) { Array.Resize(ref MyVal, lastIndex + 2); }///Resizes byte array to real value 
            else { Array.Resize(ref MyVal, lastIndex + 1); } ///Resizes byte array to real value  
            return MyVal.ToList();
        }

        ///Helper function encompassing the technique with overloads
        public static List<byte> Technique(short val)
        {
            byte[] MyVal = BitConverter.GetBytes(RealTechnique((long)val));

            int lastIndex = Array.FindLastIndex(MyVal, b => b != 0);

            if (lastIndex == -1) { Array.Resize(ref MyVal, lastIndex + 2); }///Resizes byte array to real value 
            else { Array.Resize(ref MyVal, lastIndex + 1); } ///Resizes byte array to real value  
            return MyVal.ToList();
        }

        ///Helper function encompassing the technique with overloads
        public static List<byte> Technique(ushort val)
        {
            byte[] MyVal = BitConverter.GetBytes(RealTechnique((long)val));

            int lastIndex = Array.FindLastIndex(MyVal, b => b != 0);

            if (lastIndex == -1) { Array.Resize(ref MyVal, lastIndex + 2); }///Resizes byte array to real value 
            else { Array.Resize(ref MyVal, lastIndex + 1); } ///Resizes byte array to real value  
            return MyVal.ToList();
        }

        ///Helper function encompassing the technique with overloads
        public static List<byte> Technique(int val)
        {
            byte[] MyVal = BitConverter.GetBytes(RealTechnique((long)val));

            int lastIndex = Array.FindLastIndex(MyVal, b => b != 0);

            if (lastIndex == -1) { Array.Resize(ref MyVal, lastIndex + 2); }///Resizes byte array to real value 
            else { Array.Resize(ref MyVal, lastIndex + 1); } ///Resizes byte array to real value  
            return MyVal.ToList();
        }

        ///Helper function encompassing the technique with overloads
        public static List<byte> Technique(uint val)
        {
            byte[] MyVal = BitConverter.GetBytes(RealTechnique((long)val));

            int lastIndex = Array.FindLastIndex(MyVal, b => b != 0);

            if (lastIndex == -1) { Array.Resize(ref MyVal, lastIndex + 2); }///Resizes byte array to real value 
            else { Array.Resize(ref MyVal, lastIndex + 1); } ///Resizes byte array to real value  
            return MyVal.ToList();
        }

        ///Performs the actual Technique for us
        private static long RealTechnique(long value)
        {
            //Bool to indicate if negative
            bool isNegative = false;

            //Check for being negative, if true set bool to true
            if (value < 0)
            {
                isNegative = true;
                value = Math.Abs(value);
            }

            //First value * 2
            value *= 2;

            //Create a list to store our bytes
            List<byte> TechniqueVal = new List<byte> { };

            //Loop over value and break it down
            while (value != 0)
            {
                if (value > 0x80)
                {
                    TechniqueVal.Add((byte)((value & 0x7F) | 0x80));
                    value >>= 7;
                }

                else
                {
                    TechniqueVal.Add((byte)value);
                    value >>= 7;
                }
            }

            //Get # of bytes in List
            int length = TechniqueVal.Count;

            //long to store new value
            long NewValue = 0;
            //Console.WriteLine(TechniqueVal[length - 1]);
            for (int i = 1; i < length + 1; i++)
            {
                NewValue = (NewValue << 8) | TechniqueVal[length - i];
                //Console.WriteLine(Convert.ToString(NewValue, 2));
            }

            //If original value was negative, add 1 to new value
            if (isNegative) { NewValue -= 1; }

            return NewValue;
        }

        public static int Untechnique(ReadOnlySpan<byte> MyPacket, ref int offset)
        {
            bool isNegative = false;

            //Check if value is negative, is so add 1 to first byte and check bool
            if ((MyPacket[offset] & 1) == 1) isNegative = true; 

            //Work some magic
            uint value = 0;
            uint val = 0;

            //Loop over our packet
            for (int i = 0; i < MyPacket.Length; i++)
            {
                Console.WriteLine(i);
                //Grab the current byte and shift i * 7
                val |= (uint)((MyPacket[offset] & 0x7f) << (i * 7));

                //Keep processing if true
                if (!((MyPacket[offset] & 0x80) == 0))
                {
                    offset += 1;
                    continue;
                }

                offset += 1;
                break;
            }

            //If value is supposed to be negative, add 1
            if (isNegative) val += 1;

            //Divide the unsigned value by 2 and cast to an integer
            int newVal = (int)(val / 2);

            //If its supposed to be negative, make it so
            if (isNegative) { newVal *= -1; }

            return newVal;
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

        static public List<byte> Pack(uint value)
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
            return myList;
        }

        public static string GetMemoryString(ReadOnlySpan<byte> ClientPacket, ref int offset, int stringLength)
        {
            try
            { return Encoding.Default.GetString(ClientPacket.Slice(offset, stringLength)); }

            finally
            { offset += stringLength; }
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
