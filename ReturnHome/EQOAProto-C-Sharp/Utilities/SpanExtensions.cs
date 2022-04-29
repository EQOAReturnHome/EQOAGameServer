using System;

namespace ReturnHome.Utilities
{
    static class SpanExtensions
    {
        public static void Write3Bytes(this ref Span<byte> span, uint data, ref int offset)
        {
            span[offset++] = (byte)((data & 0xFF0000) >> 16);
            span[offset++] = (byte)((data & 0xFF00) >> 8);
            span[offset++] = (byte)((data & 0xFF));
        }
    }
}
