using System;
using System.Collections.Generic;
using System.Runtime.InteropServices;
using ReturnHome.PacketProcessing;

namespace ReturnHome.Utilities
{
    class Compression
    {
        //For uncompress
        private int readBytes = 0;
        private int myBytes = 0;
        private int padding = 0;
        private int realBytes = 0;
        private MessageCreator _meassgeCreator;

        public Compression()
        {
            _meassgeCreator = new();
        }

        public ReadOnlyMemory<byte> CompressUnreliable(byte[] MyUnreliable, Session MySession)
        {
            int length = MyUnreliable.Length;

            int thisReal = 0;
            int thisCompress = 0;

            //Considerations... Channel, MessageLength, Message #, Xor Back #
            _meassgeCreator.MessageWriter(new byte[] { 0x00, 0xC9 });
            _meassgeCreator.MessageWriter(BitConverter.GetBytes(MySession.Channel40Message));
            _meassgeCreator.MessageWriter(new byte[] { 0x00 });

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
                            _meassgeCreator.MessageWriter(new byte[] { (byte)(thisReal | 0x80), (byte)thisCompress });
                            _meassgeCreator.MessageWriter(MyUnreliable[(i - thisReal)..i]);
                        }

                        //Single byte compression
                        else
                        {
                            _meassgeCreator.MessageWriter(new byte[] { (byte)((thisReal * 0x10) + thisCompress) });
                            _meassgeCreator.MessageWriter(MyUnreliable[(i - thisReal)..i]);
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
                            _meassgeCreator.MessageWriter(new byte[] { (byte)(thisReal | 0x80), (byte)thisCompress });
                            _meassgeCreator.MessageWriter(MyUnreliable[(i - thisReal + 1)..i]);
                        }

                        //Single byte compression
                        else
                        {
                            _meassgeCreator.MessageWriter(new byte[] { (byte)((thisReal * 0x10) + thisCompress) });
                            _meassgeCreator.MessageWriter(MyUnreliable[(i - thisReal + 1)..i]);
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
                            _meassgeCreator.MessageWriter(new byte[] { (byte)(thisReal | 0x80), (byte)thisCompress });
                            _meassgeCreator.MessageWriter(MyUnreliable[(i - thisReal + 1)..(i+1)]);
                        }

                        //Single byte compression
                        else
                        {
                            _meassgeCreator.MessageWriter(new byte[] { (byte)((thisReal * 0x10) + thisCompress) });
                            _meassgeCreator.MessageWriter(MyUnreliable[(i - thisReal + 1)..(i+1)]);
                        }
                    }
                }
            }

            //Add 0x00 to end of message
            _meassgeCreator.MessageWriter(new byte[] { 0x00 });

            //Return ReadOnlyMemory Message
            return _meassgeCreator.MessageReader();
        }

        //Pass in Unreliable message and expected unreliable length
        public unsafe ReadOnlyMemory<byte> Run_length_decode(ReadOnlySpan<byte> arg1, ref int offset, int length)
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
                    local_70 = buf[offset];
                    offset += 1;
                    if (local_70 == 0) break;
                    len_00 = (uint)local_70 & 0x7f;
                    if ((local_70 & 0x80) == 0)
                    {
                        len_00 = (uint)(local_70 >> 4);
                        uVar2 = (uint)local_70 & 0xf;
                    }

                    else
                    {
                        local_70 = buf[offset];
                        offset += 1;
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
                            messageBuf[counter] = buf[offset];
                            counter += 1;
                            offset += 1;
                        }
                    }
                }
            }

            return messageBuf;
        }
    }
}
