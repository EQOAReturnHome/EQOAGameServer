// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the MIT license.

using System;
using System.Buffers.Binary;
using System.Runtime.InteropServices;
using System.Text;

namespace ReturnHome.Utilities
{
    static class MemoryExtensions
    {
        ///<summary>
        ///Takes an offset and needed size, returning a ReadOnlySpan<byte> of said size from said offset
        ///</summary>
        public static ReadOnlyMemory<byte> GetSpan(this ref ReadOnlyMemory<byte> memory, ref int offset, int size)
        {
            try
            {
                return memory.Slice(offset, size);
            }

            finally
            {
                offset += size;
            }
        }

        ///<summary>
        ///Takes an offset and returns a byte from the span, incrementing offset by 1
        ///</summary>
        public static byte GetByte(this ref ReadOnlyMemory<byte> memory, ref int offset)
        {
            return memory.Span[offset++];
        }

        #region Read Little Endian
        ///<summary>
        ///Takes an offset and returns a Little Endian ushort from the span, incrementing offset by 2
        ///</summary>
        public static ushort GetLEUShort(this ref ReadOnlyMemory<byte> memory, ref int offset)
        {
            try
            {
                return BinaryPrimitives.ReadUInt16LittleEndian(memory.Span.Slice(offset, 2));
            }

            finally
            {
                offset += 2;
            }
        }

        ///<summary>
        ///Takes an offset and returns a Little Endian short from the span, incrementing offset by 2
        ///</summary>
        public static short GetLEShort(this ref ReadOnlyMemory<byte> memory, ref int offset)
        {
            try
            {
                return BinaryPrimitives.ReadInt16LittleEndian(memory.Span.Slice(offset, 2));
            }

            finally
            {
                offset += 2;
            }
        }

        ///<summary>
        ///Takes an offset and returns a Little Endian uint from the span, incrementing offset by 4
        ///</summary>
        public static uint GetLEUInt(this ref ReadOnlyMemory<byte> memory, ref int offset)
        {
            try
            {
                return BinaryPrimitives.ReadUInt32LittleEndian(memory.Span.Slice(offset, 4));
            }

            finally
            {
                offset += 4;
            }
        }

        ///<summary>
        ///Takes an offset and returns a Little Endian int from the span, incrementing offset by 4
        ///</summary>
        public static int GetLEInt(this ref ReadOnlyMemory<byte> memory, ref int offset)
        {
            try
            {
                return BinaryPrimitives.ReadInt32LittleEndian(memory.Span.Slice(offset, 4));
            }

            finally
            {
                offset += 4;
            }
        }


        ///<summary>
        ///Takes an offset and string from the span, incrementing offset by string length
        ///</summary>
        public static string GetString(this ref ReadOnlyMemory<byte> memory, ref int offset, int stringLength)
        {
            try
            {
                return Encoding.Default.GetString(memory.Span.Slice(offset, stringLength));
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
        public static ushort GetBEUShort(this ref ReadOnlyMemory<byte> memory, ref int offset)
        {
            try
            {
                return BinaryPrimitives.ReadUInt16BigEndian(memory.Span.Slice(offset, 2));
            }

            finally
            {
                offset += 2;
            }
        }

        ///<summary>
        ///Takes an offset and returns a Little Endian short from the span, incrementing offset by 2
        ///</summary>
        public static short GetBEShort(this ref ReadOnlyMemory<byte> memory, ref int offset)
        {
            try
            {
                return BinaryPrimitives.ReadInt16BigEndian(memory.Span.Slice(offset, 2));
            }

            finally
            {
                offset += 2;
            }
        }

        ///<summary>
        ///Takes an offset and returns a Little Endian uint from the span, incrementing offset by 4
        ///</summary>
        public static uint GetBEUInt(this ref ReadOnlyMemory<byte> memory, ref int offset)
        {
            try
            {
                return BinaryPrimitives.ReadUInt32BigEndian(memory.Span.Slice(offset, 4));
            }

            finally
            {
                offset += 4;
            }
        }

        ///<summary>
        ///Takes an offset and returns a Little Endian int from the span, incrementing offset by 4
        ///</summary>
        public static int GetBEInt(this ref ReadOnlyMemory<byte> memory, ref int offset)
        {
            try
            {
                return BinaryPrimitives.ReadInt32BigEndian(memory.Span.Slice(offset, 4));
            }

            finally
            {
                offset += 4;
            }
        }
        #endregion

        #region Write Little Endian
		
		public static void Write(this ref Memory<byte> memory, byte data, ref int offset)
		{
			memory.Span[offset++] = data;
		}
		
		public static void Write(this ref Memory<byte> memory, short data, ref int offset)
        {
            MemoryMarshal.Write(memory.Span[offset..], ref data);
            offset += 2;
		}
		
		public static void Write(this ref Memory<byte> memory, ushort data, ref int offset)
        {
            MemoryMarshal.Write(memory.Span[offset..], ref data);
            offset += 2;
        }
		
		public static void Write(this ref Memory<byte> memory, int data, ref int offset)
        {
            MemoryMarshal.Write(memory.Span[offset..], ref data);
            offset += 4;
        }
		
		public static void Write(this ref Memory<byte> memory, uint data, ref int offset)
        {
            MemoryMarshal.Write(memory.Span[offset..], ref data);
            offset += 4;
        }
		
		public static void Write(this ref Memory<byte> memory, long data, ref int offset)
        {
            MemoryMarshal.Write(memory.Span[offset..], ref data);
            offset += 8;
        }
		
		public static void Write(this ref Memory<byte> memory, ulong data, ref int offset)
		{
            MemoryMarshal.Write(memory.Span[offset..], ref data);
            offset += 8;
        }

        public static void Write(this ref Memory<byte> memory, ReadOnlyMemory<byte> memory2, ref int offset)
        {
            memory2.CopyTo(memory[offset..(offset + memory2.Length)]);
            offset += memory2.Length;
        }
		
        #endregion

        #region Write Big Endian

        #endregion

		#region Read 7bit
		
        ///<summary>
        ///Takes an offset reads bytes till < 0x80. This allows values of unknown length to be read. Should not be bigger then an int
        ///See: "Variable Length Integers", Most significant Bit is an indicator if more bits to read, shifting remaining bits over
        ///</summary>
        public static uint Get7BitEncodedInt(this ref ReadOnlyMemory<byte> memory, ref int offset)
        {
            int r = -7, v = 0;
            do
            {
                int that = (memory.Span[offset] & 0x7F) << (r += 7);
                v |= that;
            }
            while (memory.Span[offset++] > 0x80);
            return (uint)v;
        }

        ///<summary>
        ///Takes an offset reads bytes till < 0x80. This allows values of unknown length to be read. Should not be bigger then an int
        ///See: "Variable Length Integers", Most significant Bit is an indicator if more bits to read, shifting remaining bits over
        ///This method is double the value then encoded. If value is negative, requires special care
        ///</summary>
        public static int Get7BitDoubleEncodedInt(this ref ReadOnlyMemory<byte> memory, ref int offset)
        {
            int r = -7, v = 0;
            do
            {
                int that = (memory.Span[offset] & 0x7F) << (r += 7);
                v |= that;
            }
            while (memory.Span[offset++] > 0x80);
            return 0 == v % 2 ? v / 2 : ((v - 1) / 2) * -1;
        }
		
		#endregion
		
		#region Write7bit
		
		public static void Write7BitEncodedInt(this ref Memory<byte> memory, uint value, ref int offset)
		{
			do
            {
                byte lower7bits = (byte)(value & 0x7f);
                value >>= 7;
                if (value > 0)
                {
                    memory.Span[offset++] = (byte)(lower7bits |= 128);
                }
                else
                {
                    memory.Span[offset++] = lower7bits;
                }
            } while (value > 0);
		}
		
		public static void Write7BitDoubleEncodedInt(this ref Memory<byte> memory, int value, ref int offset)
		{
			bool isNegative = false;
			
			if (value < 0)
			{
				isNegative = true;
				value = Math.Abs(value);
			}
			value *= 2;
			
			do
            {
                byte lower7bits = (byte)(value & 0x7f);
                value >>= 7;
                if (value > 0)
                {
                    memory.Span[offset++] = (byte)(lower7bits |= 128);
                }
                else
                {
					if(isNegative)
						lower7bits -= 1;
					
                    memory.Span[offset++] = lower7bits;
                }
            } while (value > 0);
		}
		#endregion
    
		#region Write block
		
		///<summary>
        ///Takes an offset and needed size, returning a ReadOnlySpan<byte> of said size from said offset
        ///</summary>
        public static void Write(this ref Memory<byte> memory, byte[] data, ref int offset)
        {
			data.CopyTo(memory[offset..(offset+data.Length)]);
			offset += data.Length;
        }
		
		#endregion
	}
}
