
namespace ReturnHome.Server.Network
{
    public enum MessageType : byte
    {
        ReliableMessage   = 0xFB,
        UnreliableMessage = 0xFC,
        PingMessage       = 0xF9,
        ObjectUpdate      = 0xC9,
        GroupUpdate       = 0x40, //?
        BuffUpdate        = 0x42, //?
        StatUpdate        = 0x43 //?
    }
}
