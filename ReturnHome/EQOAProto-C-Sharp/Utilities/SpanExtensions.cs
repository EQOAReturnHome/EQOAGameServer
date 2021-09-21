// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the MIT license.

using System;
using System.Buffers.Binary;
using System.Runtime.InteropServices;
using System.Text;

namespace ReturnHome.Utilities
{
    static class SpanExtensions
    {
        ///<summary>
        ///Takes an offset and needed size, returning a ReadOnlySpan<byte> of said size from said offset
        ///</summary>
        public static ReadOnlySpan<byte> GetSpan(this ref ReadOnlySpan<byte> span, ref int offset, int size)
        {
            try
            {
                return span.Slice(offset, size);
            }

            finally
            {
                offset += size;
            }
        }

        ///<summary>
        ///Takes an offset and returns a byte from the span, incrementing offset by 1
        ///</summary>
        public static byte GetByte(this ref ReadOnlySpan<byte> span, ref int offset)
        {
            return span[offset++];
        }

        #region Read Little Endian
        ///<summary>
        ///Takes an offset and returns a Little Endian ushort from the span, incrementing offset by 2
        ///</summary>
        public static ushort GetLEUShort(this ref ReadOnlySpan<byte> span, ref int offset)
        {
            try
            {
                return BinaryPrimitives.ReadUInt16LittleEndian(span.Slice(offset, 2));
            }

            finally
            {
                offset += 2;
            }
        }

        ///<summary>
        ///Takes an offset and returns a Little Endian short from the span, incrementing offset by 2
        ///</summary>
        public static short GetLEShort(this ref ReadOnlySpan<byte> span, ref int offset)
        {
            try
            {
                return BinaryPrimitives.ReadInt16LittleEndian(span.Slice(offset, 2));
            }

            finally
            {
                offset += 2;
            }
        }

        ///<summary>
        ///Takes an offset and returns a Little Endian uint from the span, incrementing offset by 4
        ///</summary>
        public static uint GetLEUInt(this ref ReadOnlySpan<byte> span, ref int offset)
        {
            try
            {
                return BinaryPrimitives.ReadUInt32LittleEndian(span.Slice(offset, 4));
            }

            finally
            {
                offset += 4;
            }
        }

        ///<summary>
        ///Takes an offset and returns a Little Endian int from the span, incrementing offset by 4
        ///</summary>
        public static int GetLEInt(this ref ReadOnlySpan<byte> span, ref int offset)
        {
            try
            {
                return BinaryPrimitives.ReadInt32LittleEndian(span.Slice(offset, 4));
            }

            finally
            {
                offset += 4;
            }
        }


        ///<summary>
        ///Takes an offset and string from the span, incrementing offset by string length
        ///</summary>
        public static string GetString(this ref ReadOnlySpan<byte> span, ref int offset, int stringLength)
        {
            try
            {
                return Encoding.Default.GetString(span.Slice(offset, stringLength));
            }

            finally
            {
                offset += stringLength;
            }
        }
        #endregion

        #region Read Big Endian
        ///<summary>
        ///Takes an offset and returns a Little Endian ushort from the span, incrementing offset by 2
        ///</summary>
        public static ushort GetBEUShort(this ref ReadOnlySpan<byte> span, ref int offset)
        {
            try
            {
                return BinaryPrimitives.ReadUInt16BigEndian(span.Slice(offset, 2));
            }

            finally
            {
                offset += 2;
            }
        }

        ///<summary>
        ///Takes an offset and returns a Little Endian short from the span, incrementing offset by 2
        ///</summary>
        public static short GetBEShort(this ref ReadOnlySpan<byte> span, ref int offset)
        {
            try
            {
                return BinaryPrimitives.ReadInt16BigEndian(span.Slice(offset, 2));
            }

            finally
            {
                offset += 2;
            }
        }

        ///<summary>
        ///Takes an offset and returns a Little Endian uint from the span, incrementing offset by 4
        ///</summary>
        public static uint GetBEUInt(this ref ReadOnlySpan<byte> span, ref int offset)
        {
            try
            {
                return BinaryPrimitives.ReadUInt32BigEndian(span.Slice(offset, 4));
            }

            finally
            {
                offset += 4;
            }
        }

        ///<summary>
        ///Takes an offset and returns a Little Endian int from the span, incrementing offset by 4
        ///</summary>
        public static int GetBEInt(this ref ReadOnlySpan<byte> span, ref int offset)
        {
            try
            {
                return BinaryPrimitives.ReadInt32BigEndian(span.Slice(offset, 4));
            }

            finally
            {
                offset += 4;
            }
        }
        #endregion

        #region Write Little Endian
		
		public static void Write(this ref Span<byte> span, byte data, ref int offset)
		{
			span[offset++] = data;
		}
		
		public static void Write(this ref Span<byte> span, short data, ref int offset)
        {
            MemoryMarshal.Write(span[offset..], ref data);
            offset += 2;
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
		
		public static void Write(this ref Span<byte> span, long data, ref int offset)
        {
            MemoryMarshal.Write(span[offset..], ref data);
            offset += 8;
        }
		
		public static void Write(this ref Span<byte> span, ulong data, ref int offset)
		{
            MemoryMarshal.Write(span[offset..], ref data);
            offset += 8;
        }

        public static void Write(this ref Span<byte> span, ReadOnlyMemory<byte> span2, ref int offset)
        {
            span2.Span.CopyTo(span[offset..(offset + span2.Length)]);
            offset += span2.Length;
        }
		
        #endregion

        #region Write Big Endian

        #endregion

		#region Read 7bit
		
        ///<summary>
        ///Takes an offset reads bytes till < 0x80. This allows values of unknown length to be read. Should not be bigger then an int
        ///See: "Variable Length Integers", Most significant Bit is an indicator if more bits to read, shifting remaining bits over
        ///</summary>
        public static uint Get7BitEncodedInt(this ref ReadOnlySpan<byte> span, ref int offset)
        {
            int r = -7, v = 0;
            do
            {
                int that = (span[offset] & 0x7F) << (r += 7);
                v |= that;
            }
            while (span[offset++] >= 0x80);
            return (uint)v;
        }

        ///<summary>
        ///Takes an offset reads bytes till < 0x80. This allows values of unknown length to be read. Should not be bigger then an int
        ///See: "Variable Length Integers", Most significant Bit is an indicator if more bits to read, shifting remaining bits over
        ///This method is double the value then encoded. If value is negative, requires special care
        ///</summary>
        public static int Get7BitDoubleEncodedInt(this ref ReadOnlySpan<byte> span, ref int offset)
        {
            int r = -7, v = 0;
            do
            {
                int that = (span[offset] & 0x7F) << (r += 7);
                v |= that;
            }
            while (span[offset++] >= 0x80);
            return 0 == v % 2 ? v / 2 : ((v - 1) / 2) * -1;
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
                {
                    span[offset++] = (byte)(lower7bits |= 128);
                }
                else
                {
                    span[offset++] = lower7bits;
                }
            } while (value > 0);
		}

        public static void Write7BitDoubleEncodedInt(this ref Span<byte> span, int val, ref int offset)
        {
            bool isNegative = false;

            if (val < 0)
            {
                isNegative = true;
                val = Math.Abs(val);
            }

            uint value = (uint)val;

            value *= 2;

            do
            {
                byte lower7bits = (byte)(value & 0x7f);
                value >>= 7;
                if (value > 0)
                {
                    if (isNegative)
                    {
                        isNegative = false;
                        lower7bits -= 1;
                    }
                    span[offset++] = (byte)(lower7bits |= 128);

                }
                else
                    span[offset++] = lower7bits;

            } while (value > 0);
        }
        #endregion

        #region Write block

        ///<summary>
        ///Takes an offset and needed size, returning a ReadOnlySpan<byte> of said size from said offset
        ///</summary>
        public static void Write(this ref Span<byte> span, byte[] data, ref int offset)
        {
			data.CopyTo(span[offset..(offset+data.Length)]);
			offset += data.Length;
        }
		
		#endregion
	}
}
