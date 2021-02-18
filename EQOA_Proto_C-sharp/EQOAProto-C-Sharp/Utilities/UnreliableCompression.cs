using System;
using System.Collections.Generic;
using System.Linq;

namespace RunningLengthCompression
{
    static class Compression
    {
        //For uncompress
        private static int readBytes = 0;
        private static int myBytes = 0;
        private static int padding = 0;
        private static int realBytes = 0;

        public static List<byte> CompressUnreliable(List<byte> MyUnreliable)
        {
            int length = MyUnreliable.Count() - 1;
            int thisReal = 0;
            int thisCompress = 0;
            List<byte> CompressedUnreliable = new List<byte>();

            //Cycle through every byte opf update message
            for (int i = 0; i <= length; i++)
            {
                //Check if null byte
                if (MyUnreliable[i] == 0)
                {
                    //Check if prior bytes were not null
                    if (thisReal > 0)
                    {
                        //Need to add compression bytes
                        //If either of these are true, need 2 byte compression
                        if (thisReal > 7 || thisCompress > 15)
                        {
                            CompressedUnreliable.Add((byte)(thisReal | 0x80));
                            CompressedUnreliable.Add((byte)thisCompress);
                            CompressedUnreliable.AddRange(MyUnreliable.GetRange(i - thisReal, thisReal));
                        }

                        //Single byte compression
                        else
                        {
                            CompressedUnreliable.Add((byte)((thisReal * 0x10) + thisCompress));
                            CompressedUnreliable.AddRange(MyUnreliable.GetRange((i - thisReal), thisReal));
                        }

                        //Reset counters
                        thisCompress = 0;
                        thisReal = 0;
                    }

                    thisCompress += 1;

                    //check if this is the last value
                    if (length == i)
                    {
                        //Compress remaining bytes and stuff....
                        //Need to add compression bytes
                        //If either of these are true, need 2 byte compression
                        if (thisReal > 7 || thisCompress > 15)
                        {
                            CompressedUnreliable.Add((byte)(thisReal | 0x80));
                            CompressedUnreliable.Add((byte)thisCompress);
                            CompressedUnreliable.AddRange(MyUnreliable.GetRange(i - thisReal + 1, thisReal));
                        }

                        //Single byte compression
                        else
                        {
                            CompressedUnreliable.Add((byte)((thisReal * 0x10) + thisCompress));
                            CompressedUnreliable.AddRange(MyUnreliable.GetRange((i - thisReal + 1), thisReal));
                        }
                    }
                }

                //Real byte
                else
                {
                    thisReal += 1;

                    //check if this is the last value
                    if (length == i)
                    {
                        //Compress remaining bytes and stuff....
                        //Need to add compression bytes
                        //If either of these are true, need 2 byte compression
                        if (thisReal > 7 || thisCompress > 15)
                        {
                            CompressedUnreliable.Add((byte)(thisReal | 0x80));
                            CompressedUnreliable.Add((byte)thisCompress);
                            CompressedUnreliable.AddRange(MyUnreliable.GetRange(i - thisReal + 1, thisReal));
                        }

                        //Single byte compression
                        else
                        {
                            CompressedUnreliable.Add((byte)((thisReal * 0x10) + thisCompress));
                            CompressedUnreliable.AddRange(MyUnreliable.GetRange((i - thisReal + 1), thisReal));
                        }
                    }
                }
            }
            return CompressedUnreliable;
        }

        //Pass in Unreliable message and expected unreliable length
        public static byte[] UncompressUnreliable(ReadOnlySpan<byte> arg, ref int offset, int length)
        {
            //Make sure these values are 0 before starting
            myBytes = 0;
            //Start with offset here
            readBytes = offset;
            padding = 0;
            realBytes = 0;

            byte[] myByteArray = new byte[length + 1];
            do
            {
                padding = 0;
                realBytes = 0;

                if (arg[readBytes] >= 128)
                {
                    ///2 bytes define compression
                    ///Grab real bytes, in this case x-128
                    realBytes = arg[readBytes] ^ 128;
                    ///padding is padding here
                    padding = arg[readBytes + 1];

                    for (int i = 0; i < padding; i++)
                    { myByteArray[i + myBytes] = 0; }

                    myBytes += padding;
                    readBytes += 2;

                    for (int i = 0; i < realBytes; i++)
                    { myByteArray[i + myBytes] = arg[readBytes + i]; }

                    readBytes += realBytes;
                    myBytes += realBytes;
                }

                else
                {
                    ///Single byte for compression
                    ///Realbytes can be found with / 16
                    ///Padding is found with remainder of x % 16
                    realBytes = arg[readBytes] / 16;
                    padding = arg[readBytes] % 16;

                    for (int i = 0; i < padding; i++)
                    { myByteArray[i + myBytes] = 0; }

                    myBytes += padding;
                    readBytes += 1;

                    for (int i = 0; i < realBytes; i++)
                    { myByteArray[i + myBytes] = arg[readBytes + i]; }

                    readBytes += realBytes;
                    myBytes += realBytes;
                }
            } while (myBytes < length);

            //Clear packet here... unreliable should be last message... reliably... :^)
            return myByteArray;
        }
    }
}
