using System;

using ReturnHome.Utilities;

namespace ReturnHome.Server.Network
{
    public class ClientMessage
    {
        public ReadOnlyMemory<byte> Payload;

        public ushort Opcode { get; }
		
		public int offset { get; set; }

        public ClientMessage(ReadOnlyMemory<byte> payload)
        {
            Payload = payload;
			offset = 0;
            (Opcode, offset) = BinaryPrimitiveWrapper.GetLEUShort(Payload, offset);
        }
    }
}
