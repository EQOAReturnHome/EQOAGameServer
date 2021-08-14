using System.Collections.Generic;
using System.IO;

namespace ReturnHome.Server.Network
{
    public abstract class Packet
    {
        public PacketHeader Header { get; } = new PacketHeader();
        public Dictionary<ushort, PacketMessage> Messages { get; } = new Dictionary<ushort, PacketMessage>();
    }
}
