using System;

namespace ReturnHome.Utilities
{
    public struct MessageStruct
    {
        public readonly ReadOnlyMemory<byte> Message;
        public readonly ushort Messagenumber;
        public long Time;

        public MessageStruct(ushort num, ReadOnlyMemory<byte> MyMessage)
        {
            Message = MyMessage;
            Messagenumber = num;
            Time = DateTimeOffset.Now.ToUnixTimeMilliseconds();
        }

        public MessageStruct(ReadOnlyMemory<byte> MyMessage)
        {
            Message = MyMessage;
            Messagenumber = 0;
            Time = 0;
        }

        public void updateTime() => Time = DateTimeOffset.Now.ToUnixTimeMilliseconds();
    }
}
