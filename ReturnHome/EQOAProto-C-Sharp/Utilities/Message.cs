using System;
using ReturnHome.Server.Network;
using ReturnHome.Server.Opcodes;

namespace ReturnHome.Utilities
{
    public class Message
    {
        private readonly int MaximumSize;

        public Message()
        {
            MaximumSize = 0x514;
            message = new byte[MaximumSize];
        }

        public Memory<byte> message { get; set; }
        public ushort Sequence { get; private set; }
        public int headerSize { get; private set; }
        public MessageType Messagetype { get; private set; }
        public uint Created { get; private set; }
        public uint Time { get; private set; }

        public int Index = 0;
        public GameOpcode Opcode { get; private set; }

        private int _size;

        public int HeaderSize() => Messagetype switch 
        {
            MessageType.ReliableMessage or MessageType.PingMessage => Size > 0xFF ? 6 : 4,
            MessageType.UnreliableMessage => Size > 255 ? 4 : 2,
            _ => Size > 255 ? 7 : 5
        };

        
        public int Size
        {
            get => _size;
            set
            {
                if (value > MaximumSize)
                    throw new ArgumentOutOfRangeException(nameof(value));

                _size = value;
            }
        }

        public Span<byte> Span => message.Span;

        public void AddSequence(ushort sequence) => Sequence = sequence;

        public void updateTime() => Time = (uint)DateTimeOffset.Now.ToUnixTimeSeconds();

        //When we are creating our Fragments, use this **Maybe we can use the pool?**
        public Message(MessageType messageType, Memory<byte> thisMessage, ushort sequence)
        {
            Messagetype = messageType;
            message = thisMessage;
            Sequence = sequence;
            MaximumSize = message.Length;
            Size = message.Length;
        }

        //For our unreliable channels **Maybe we can use the pool?**
        public Message(MessageType messageType, Memory<byte> thisMessage)
        {
            Messagetype = messageType;
            message = thisMessage;
            MaximumSize = message.Length;
            Size = message.Length;
        }

        //Used for messages that are, or could be, bigger then 0xFB type. needing segments
        public Message(MessageType messageType, GameOpcode opcode)
        {
            Opcode = opcode;
            Messagetype = messageType;
            if(messageType == MessageType.SegmentReliableMessage)
                MaximumSize = ushort.MaxValue;
            else
                MaximumSize = 0x514;
            message = new byte[MaximumSize];
        }

        public static Message Create(MessageType messageType, GameOpcode opcode)
        {
            Message result = _messagePool.Get();
            result.Opcode = opcode;
            result.Messagetype = messageType;
            result.updateTime();
            result.Created = result.Time;
            result.Span.Fill(0);
            return result;
        }
    }
}
