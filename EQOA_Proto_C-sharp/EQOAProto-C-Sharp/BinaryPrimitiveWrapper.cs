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
        public static int GetBEInt(ReadOnlySpan<byte> span, ref int offset)
        {
            offset += 4;
            return BinaryPrimitives.ReadInt32BigEndian(span.Slice(offset - 4));
        }

        public static uint GetBEUInt(ReadOnlySpan<byte> span, ref int offset)
        {
            offset += 4;
            return BinaryPrimitives.ReadUInt32BigEndian(span.Slice(offset - 4));
        }
        public static short GetBEShort(ReadOnlySpan<byte> span, ref int offset)
        {
            offset += 2;
            return BinaryPrimitives.ReadInt16BigEndian(span.Slice(offset - 2));
        }

        public static ushort GetBEUShort(ReadOnlySpan<byte> span, ref int offset)
        {
            offset += 2;
            return BinaryPrimitives.ReadUInt16BigEndian(span.Slice(offset - 2));
        }
    }
}
