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

        public Memory<byte> message { get; private set; }
        public ushort Sequence { get; private set; }
        public int headerSize { get; private set; }
        public MessageType messageType { get; private set; }
        public uint Created { get; private set; }
        public uint Time { get; private set; }
        public GameOpcode Opcode { get; private set; }
        public ushort Size { get; private set; }

        /*
        public ushort Size
        {
            get => _size;
            private set
            {
                if (value > MaximumSize)
                    throw new ArgumentOutOfRangeException(nameof(value));

                _size = value;
            }
        }
        */

        public Span<byte> Span => message.Span;

        /*public Message(GameOpcode opcode, Memory<byte> Message)
        {
            Opcode = (ushort)opcode;
            size = (ushort)Message.Length;
            message = Message;
        }

        public Message(ushort seq, ReadOnlyMemory<byte> MyMessage)
        {
            message = MyMessage;
            size = (ushort)message.Length;
            sequence = seq;
            updateTime();
        }

        public Message(ReadOnlyMemory<byte> MyMessage)
        {
            message = MyMessage;
            size = (ushort)message.Length;
            sequence = 0;
            Time = 0;
        }*/

        /// <summary>
        /// Creates a Message Object that auto calculates headersize
        /// </summary>
        public Message(int size, MessageType type, GameOpcode opcode)
        {
            headerSize = CalculateSize(size, type);
            message = new byte[size + headerSize];
            Size = (ushort)message.Length;
            messageType = type;
            Opcode = opcode;
            updateTime();
        }

        public Message(Memory<byte> message2, ushort sequence2, MessageType type, GameOpcode opcode)
        {
            headerSize = CalculateSize(message2.Length, type);
            message = message2;
            Sequence = sequence2;
            messageType = type;
            Opcode = opcode;
            Size = (ushort)message.Length;
            updateTime();
        }

        public void AddSequence(ushort sequence) => Sequence = sequence;

        public void updateTime() => Time = (uint)DateTimeOffset.Now.ToUnixTimeSeconds();

        /*
        public static Message Create(GameOpcode opcode, ushort size)
        {
            Message result = _messagePool.Get();
            result.Opcode = (ushort)opcode;
            result.size = size;
            result.updateTime();
            result.Created = result.Time;
            result.Span.Fill<byte>(0x00);
            return result;
            
        }*/

        public static int CalculateSize(int size, MessageType messageType)
        {
            if (messageType == MessageType.ReliableMessage || messageType == MessageType.PingMessage)
                return size > 255 ? 6 : 4;
            return size > 255 ? 4 : 2;
        }
    }
}
