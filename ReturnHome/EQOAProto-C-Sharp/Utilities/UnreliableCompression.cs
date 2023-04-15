using System;
using System.Runtime.InteropServices;

namespace ReturnHome.Utilities
{
    
    class Compression
    {
        public static void runLengthEncode(ref BufferWriter writer, Span<byte> span)
        {
            int length = span.Length;
            int thisReal = 0;
            int thisCompress = 0;

            //Cycle through every byte of update message
            for (int i = 0; i < length; i++)
            {
                //Check if null byte
                if (span[i] == 0)
                {
                    //Check if prior bytes were not null
                    if (thisReal > 0)
                    {
                        writer.Write((byte)(thisReal | 0x80));
                        writer.Write((byte)thisCompress);
                        writer.Write(span.Slice((i - thisReal), thisReal));

                        //Reset counters
                        thisCompress = 0;
                        thisReal = 0;
                    }

                    if(thisCompress == 0xFF)
                    {
                        writer.Write((byte)(thisReal | 0x80));
                        writer.Write((byte)thisCompress);
                        writer.Write(span.Slice((i - thisReal), thisReal));

                        //Reset counters
                        thisCompress = 0;
                        thisReal = 0;
                    }

                    thisCompress += 1;
                    continue;
                }

                //Real byte
                thisReal += 1;
            }

            writer.Write((byte)(thisReal | 0x80));
            writer.Write((byte)thisCompress);
            if (thisReal != 0)
                writer.Write(span.Slice((length - thisReal), thisReal));

            //Ensure any compression is terminated by a 0x00
            writer.Write((byte)0);
        }

        //Pass in Unreliable message and expected unreliable length
        public static Memory<byte> Run_length_decode(ref BufferReader reader, int length)
        {
            Memory<byte> messageBuf = new Memory<byte>(new byte[length]);
            Span<byte> temp = messageBuf.Span;

            int local_70;
            int counter = 0;
            //real bytes
            int len_00;

            while (true)
            {
                //Console.WriteLine($"Counter: " + counter);
                local_70 = reader.Read<byte>();
                if (local_70 == 0) break;
                len_00 = local_70 & 0x7f;

                if ((local_70 & 0x80) == 0)
                {
                    len_00 = (local_70 >> 4);
                    local_70 &= 0xf;
                }

                else
                    local_70 = reader.Read<byte>();

                if (local_70 != 0)
                    counter += local_70;

                reader.Buffer[reader.Position..(reader.Position + len_00)].CopyTo(temp[counter..]);
                counter += len_00;
                reader.Advance(len_00);
            }
            return messageBuf;
        }
    }
}
