using System;
using System.Buffers.Binary;
using System.Runtime.InteropServices;
using System.Text;

namespace ReturnHome.Utilities
{
    static class SpanExtensions
    {
        #region Write Little Endian

        public static void Write(this ref Span<byte> span, byte data, ref int offset)
        {
            span[offset++] = data;
        }

        public static void Write(this ref Span<byte> span, ushort data, ref int offset)
        {
            MemoryMarshal.Write(span[offset..], ref data);
            offset += 2;
        }


        public static void Write(this ref Span<byte> span, int data, ref int offset)
        {
            MemoryMarshal.Write(span[offset..], ref data);
            offset += 4;
        }

        public static void Write(this ref Span<byte> span, uint data, ref int offset)
        {
            MemoryMarshal.Write(span[offset..], ref data);
            offset += 4;
        }

        public static void Write3Bytes(this ref Span<byte> span, uint data, ref int offset)
        {
            span[offset++] = (byte)((data & 0xFF0000) >> 16);
            span[offset++] = (byte)((data & 0xFF00) >> 8);
            span[offset++] = (byte)((data & 0xFF));
        }

        public static void Write(this ref Span<byte> span, ReadOnlyMemory<byte> span2, ref int offset)
        {
            span2.Span.CopyTo(span[offset..(offset + span2.Length)]);
            offset += span2.Length;
        }

        public static void Write(this ref Span<byte> span, Memory<byte> span2, ref int offset)
        {
            span2.Span.CopyTo(span[offset..(offset + span2.Length)]);
            offset += span2.Length;
        }

        public static void Write(this ref Span<byte> span, ReadOnlySpan<byte> span2, ref int offset)
        {
            span2.CopyTo(span[offset..(offset + span2.Length)]);
            offset += span2.Length;
        }

        #endregion

        #region Write7bit

        public static void Write7BitEncodedInt(this ref Span<byte> span, uint value, ref int offset)
        {
            do
            {
                byte lower7bits = (byte)(value & 0x7f);
                value >>= 7;
                if (value > 0)
                    span[offset++] = (byte)(lower7bits |= 128);

                else
                    span[offset++] = lower7bits;

            } while (value > 0);
        }

        #endregion
    }
}
