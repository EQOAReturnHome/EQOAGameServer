using Microsoft.VisualBasic.CompilerServices;
using System;
using System.Collections.Generic;
using System.Linq;

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

        public static int Untechnique(long val)
        {
            return (int)RealUntechnique((long)val);
        }

        public static int Untechnique(int val)
        {
            return (int)RealUntechnique((long)val);
        }

        public static int Untechnique(short val)
        {
            return (int)RealUntechnique((long)val);
        }

        public static int Untechnique(byte val)
        {
            return (int)RealUntechnique((long)val);
        }

        public static int RealUntechnique(long val)
        {
            //Cast to long
            long value = (long)val;

            //Create a list to store our bytes
            List<byte> TechniqueVal = new List<byte> { };
            bool isNegative = false;

            //First check to see if value is negative
            if ((value & 1) == 1)
            {
                isNegative = true;
                //Remove bit making it negative
                value += 1;
            }

            //Loop over techniqued value to break it down
            while (value != 0)
            {
                if (value > 0x80)
                {
                    TechniqueVal.Add((byte)(value & 0x7F));
                    value >>= 8;
                }

                else
                {
                    TechniqueVal.Add((byte)value);
                    value >>= 8;
                }
            }

            //Get # of bytes in List
            int length = TechniqueVal.Count;

            //temp variable to store value, as it may be bigger then an int untill final steps
            long temp = 0;

            //Console.WriteLine(TechniqueVal[length - 1]);
            for (int i = 1; i < length + 1; i++)
            {
                temp = (temp << 7) | TechniqueVal[length - i];
                //Console.WriteLine(Convert.ToString(NewValue, 2));
            }

            //Divide the result by 2 to have the original value
            int OldValue =(int)(temp / 2);

            //Check if value was negative
            if (isNegative) { OldValue *= -1; }

            return OldValue;
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
            return ((x & 0x000000ff) << 24) +
                   ((x & 0x0000ff00) << 8) +
                   ((x & 0x00ff0000) >> 8) +
                   ((x & 0xff000000) >> 24);
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
