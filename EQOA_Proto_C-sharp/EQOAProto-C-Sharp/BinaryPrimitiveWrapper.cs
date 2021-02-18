using System;
using System.Buffers.Binary;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EQOAProto
{
    class BinaryPrimitiveWrapper
    {
        public static int GetLEInt(ReadOnlySpan<byte> span, ref int offset)
        {
            offset += 4;
            return BinaryPrimitives.ReadInt32LittleEndian(span.Slice(offset - 4));
        }

        public static uint GetLEUint(ReadOnlySpan<byte> span, ref int offset)
        {
            offset += 4;
            return BinaryPrimitives.ReadUInt32LittleEndian(span.Slice(offset - 4));
        }
        public static short GetLEShort(ReadOnlySpan<byte> span, ref int offset)
        {
            offset += 2;
            return BinaryPrimitives.ReadInt16LittleEndian(span.Slice(offset - 2));
        }

        public static ushort GetLEUShort(ReadOnlySpan<byte> span, ref int offset)
        {
            offset += 2;
            return BinaryPrimitives.ReadUInt16LittleEndian(span.Slice(offset - 2));
        }

        public static byte GetLEByte(ReadOnlySpan<byte> span, ref int offset)
        {
            offset += 1;
            return span[offset - 1];
        }
    }
}
