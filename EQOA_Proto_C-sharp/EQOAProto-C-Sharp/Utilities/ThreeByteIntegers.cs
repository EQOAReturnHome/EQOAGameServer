using System.Runtime.InteropServices;

namespace ThreeByteInts
{
    [StructLayout(LayoutKind.Explicit)]
    public struct ThreeByteInt
    {
        [FieldOffset(0)]
        public byte byte0;

        [FieldOffset(1)]
        public byte byte1;
        
        [FieldOffset(2)]
        public byte byte2;
        
        [FieldOffset(3)]
        public byte byte3;

        [FieldOffset(0)]
        public int integer;

        public static implicit operator ThreeByteInt(int value)
        {
            return new ThreeByteInt() { integer = value };
        }
    }

   
}
