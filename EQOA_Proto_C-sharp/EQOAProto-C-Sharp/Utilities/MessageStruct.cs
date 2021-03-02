using System;

namespace Packet
{
    public readonly struct MessageStruct
    {
        public readonly Memory<byte> Message;
        public readonly ushort Messagenumber;
        public readonly long Time;

        public MessageStruct(ushort num, Memory<byte> MyMessage)
        {
            Message = MyMessage;
            Messagenumber = num;
            Time = DateTimeOffset.Now.ToUnixTimeMilliseconds();
        }

        public MessageStruct(Memory<byte> MyMessage)
        {
            Message = MyMessage;
            Messagenumber = 0;
            Time = 0;
        }
    }
}
