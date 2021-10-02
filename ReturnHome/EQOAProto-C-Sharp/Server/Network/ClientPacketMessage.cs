using System;
using ReturnHome.Utilities;

namespace ReturnHome.Server.Network
{
    public class ClientPacketMessage : PacketMessage
    {
        public bool Unpack(ReadOnlyMemory<byte> buffer, ref int offset)
        {
            Header.Unpack(buffer, ref offset);

            //Check if message type is client update
            if (Header.messageType == (byte)MessageType.ClientUpdate)
            {
                Data = Compression.Run_length_decode(buffer.Span, ref offset, Header.Size);
                return true;
            }

            else
            {
                Data = buffer.Slice(offset, Header.Size);
                offset += Header.Size;
                return true;
            }
        }
    }
}
