using System;
using System.Collections.Generic;
using System.Linq;

namespace RunningLengthCompression
{
    static class Compression
    {
        //For uncompress
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
        public static void UncompressUnreliable(List<byte> arg, int length)
        {
            do
            {
                padding = 0;
                realBytes = 0;

                if (arg[myBytes] >= 128)
                {
                    ///2 bytes define compression
                    ///Grab real bytes, in this case x-128
                    realBytes = arg[myBytes] | 128;
                    ///padding is padding here
                    padding = arg[myBytes + 1];
                    ///Console.WriteLine("RealBytes {0:G}, Padding {1:N}", realBytes, padding);
                    ///Start index at 0 and remove 2 bytes, shaving off compression byte
                    arg.RemoveRange(myBytes, 2);
                    ///Iterate over total padding and add x padding bytes
                    for (int i = 0; i < padding; i++)
                    {
                        arg.Insert(myBytes, 0);
                    }
                    ///Add our padding and realbytes
                    myBytes += padding + realBytes;
                }

                else
                {
                    ///Single byte for compression
                    ///Realbytes can be found with / 16, or drop the first 4 bits (padding) by shifting right 4
                    ///Padding is found with remainder of x % 16
                    realBytes = arg[myBytes] >> 4;
                    padding = arg[myBytes] & 0x0F;
                    ///Console.WriteLine("RealBytes {0:G}, Padding {1:N}", realBytes, padding);
                    ///Start index at 0 and remove 2 bytes, shaving off compression byte
                    arg.RemoveAt(myBytes);
                    ///Iterate over total padding and add x padding bytes
                    for (int i = 0; i < padding; i++)
                    {
                        arg.Insert(myBytes, 0);
                    }
                    ///Add our padding and realbytes
                    myBytes += padding + realBytes;
                }
            } while (myBytes < length);

            //Reset these before next uncompress
            myBytes = 0;
            padding = 0;
            realBytes = 0;
        }
    }
}
