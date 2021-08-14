using System;
using System.IO;

namespace ReturnHome.Server.Network
{
    public class ClientPacketMessage : PacketMessage
    {
        public bool Unpack(ReadOnlyMemory<byte> buffer, ref int offset)
        {
            Header.Unpack(buffer, ref offset);
			
            Data = buffer.Slice(offset, Header.Size);
			offset += Header.Size;
            return true;
        }
    }
}
