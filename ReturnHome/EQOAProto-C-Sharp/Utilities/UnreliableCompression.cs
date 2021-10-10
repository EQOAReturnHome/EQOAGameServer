using System;
using System.Runtime.InteropServices;

namespace ReturnHome.Utilities
{
    
    class Compression
    {
        public static Memory<byte> CompressUnreliable(ReadOnlyMemory<byte> temp)
        {
            ReadOnlySpan<byte> MyUnreliable = temp.Span;
            int length = MyUnreliable.Length;
            int offset = 0;
            int thisReal = 0;
            int thisCompress = 0;

            //Start with a Memory/Span of size 0xc9, will need to resize after compression
            Memory<byte> tempMem = new Memory<byte>(new byte[0xC9]);
            Span<byte> tempSpan = tempMem.Span;

            //Cycle through every byte of update message
            //skip first 5 bytes
            for (int i = 0; i < length; i++)
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
                            tempSpan[offset++] = (byte)(thisReal | 0x80);
                            tempSpan[offset++] = (byte)thisCompress;
                            tempSpan.Write(MyUnreliable[(i - thisReal)..i], ref offset);
                        }

                        //Single byte compression
                        else
                        {
                            tempSpan[offset++] = (byte)((thisReal * 0x10) + thisCompress);
                            tempSpan.Write(MyUnreliable[(i - thisReal)..i], ref offset);
                        }

                        //Reset counters
                        thisCompress = 0;
                        thisReal = 0;
                    }

                    thisCompress += 1;

                    //check if this is the last value
                    if (length - 1 == i)
                    {
                        //Compress remaining bytes and stuff....
                        //Need to add compression bytes
                        //If either of these are true, need 2 byte compression
                        if (thisReal > 7 || thisCompress > 15)
                        {
                            tempSpan[offset++] = (byte)(thisReal | 0x80);
                            tempSpan[offset++] = (byte)thisCompress;
                            tempSpan.Write(MyUnreliable[(i - thisReal + 1)..i], ref offset);
                        }

                        //Single byte compression
                        else
                        {
                            tempSpan[offset++] = (byte)((thisReal * 0x10) + thisCompress);
                            tempSpan.Write(MyUnreliable[(i - thisReal + 1)..i], ref offset);
                        }
                    }
                }

                //Real byte
                else
                {
                    thisReal += 1;

                    //check if this is the last value
                    if (length - 1 == i)
                    {
                        //Compress remaining bytes and stuff....
                        //Need to add compression bytes
                        //If either of these are true, need 2 byte compression
                        if (thisReal > 7 || thisCompress > 15)
                        {
                            tempSpan[offset++] = (byte)(thisReal | 0x80);
                            tempSpan[offset++] = (byte)thisCompress;
                            tempSpan.Write(MyUnreliable[(i - thisReal + 1)..(i+1)], ref offset);
                        }

                        //Single byte compression
                        else
                        {
                            tempSpan[offset++] = (byte)((thisReal * 0x10) + thisCompress);
                            tempSpan.Write(MyUnreliable[(i - thisReal + 1)..(i+1)], ref offset);
                        }
                    }
                }
            }

            //Return the memory that was written too
            return tempMem.Slice(0, offset);
        }

        //Pass in Unreliable message and expected unreliable length
        public static unsafe Memory<byte> Run_length_decode(ReadOnlySpan<byte> arg1, ref int offset, int length)
        {

            byte[] messageBuf = new byte[length];

            fixed (byte* tempBuf = &MemoryMarshal.GetReference(arg1))
            {
                byte* buf = tempBuf;
                byte local_70;
                byte counter = 0;

                //real bytes
                uint len_00;

                //null byte count
                uint uVar2;
                int uVar1;

                while (true)
                {
                    local_70 = buf[offset++];
                    if (local_70 == 0) break;
                    len_00 = (uint)local_70 & 0x7f;
                    if ((local_70 & 0x80) == 0)
                    {
                        len_00 = (uint)(local_70 >> 4);
                        uVar2 = (uint)local_70 & 0xf;
                    }

                    else
                    {
                        local_70 = buf[offset++];
                        uVar2 = (uint)local_70;
                    }

                    uVar1 = 0;
                    if (uVar2 != 0)
                    {
                        do
                        {
                            messageBuf[counter] = 0;
                            counter += 1;
                            uVar1 += 1;
                        } while (uVar1 < uVar2);
                    }

                    if (len_00 != 0)
                    {
                        for (int i = 0; i < len_00; i++)
                        {
                            messageBuf[counter] = buf[offset++];
                            counter += 1;
                        }
                    }
                }
            }
            return messageBuf;
        }
    }
}
