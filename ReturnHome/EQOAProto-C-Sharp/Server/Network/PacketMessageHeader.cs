using System;
using System.IO;
using ReturnHome.Utilities;

namespace ReturnHome.Server.Network
{
    public class PacketMessageHeader
    {
        public int HeaderSize { get; set; }

        public byte messageType { get; set; }
        public ushort MessageNumber { get; set; }
        public ushort Opcode { get; private set; }
        public ushort Size { get; set; }
		public int Count { get; set; }
        public int Index { get; set; } // If client sends 0xFA types, may be needed to combine message splits
        public bool Split { get; set; } // If client sends 0xFA types, may be needed to combine message splits

        public void Unpack(ReadOnlyMemory<byte> temp, ref int offset)
        {
            ReadOnlySpan<byte> buffer = temp.Span;
            //Read first byte, if it is FF, read an additional byte (Indicates >255byte message
            messageType = buffer.GetByte(ref offset);

            byte bigCheck = buffer.GetByte(ref offset);

            if (bigCheck == 0xFF)
                Size = buffer.GetLEUShort(ref offset);

            else
                Size = bigCheck;

            if((byte)MessageType.UnreliableMessage != messageType)
                MessageNumber = buffer.GetLEUShort(ref offset);

            if ((byte)MessageType.PingMessage == messageType)
                return;

            Opcode = buffer.GetLEUShort(ref offset);

            //Subtract 2 from size after reading opcode
            Size -= 2;
        }
    }
}
