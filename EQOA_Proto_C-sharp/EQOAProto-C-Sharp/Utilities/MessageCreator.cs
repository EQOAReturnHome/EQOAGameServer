using System;
using System.Buffers;
using System.IO.Pipelines;


namespace EQOAProto
{
    public class MessageCreator
    {
        private Pipe pipe = new Pipe();

        public MessageCreator() { }

        public void MessageWriter(byte[] MyMessage)
        {
            pipe.Writer.Write(MyMessage);
        }

        public ReadOnlyMemory<byte> MessageReader()
        {
            pipe.Writer.Complete();
            if (pipe.Reader.TryRead(out ReadResult OurResult))
            {
                try
                {
                    return OurResult.Buffer.First;
                }

                finally
                {
                    pipe.Reader.Complete();
                    pipe.Reset();
                }
            }
            return default;
        }
    }

    public class PacketCreator
    {
        private Pipe pipe = new Pipe();
        public int ReadBytes = 0;

        public PacketCreator() { }

        public void PacketWriter(byte[] MyMessage)
        {
            ReadBytes += MyMessage.Length;
            pipe.Writer.Write(MyMessage);
        }

        public void PacketWriter(Memory<byte> MyMessage)
        {
            ReadBytes += MyMessage.Length;
            pipe.Writer.Write(MyMessage.Span);
        }

        public ReadOnlyMemory<byte> PacketReader()
        {
            pipe.Writer.Complete();
            if (pipe.Reader.TryRead(out ReadResult OurResult))
            {
                try
                {
                    //Reset readbytes to 0
                    ReadBytes = 0;
                    
                    return OurResult.Buffer.First;
                }

                finally
                {
                    pipe.Reader.Complete();
                    pipe.Reset();
                }
            }
            return default;
        }
    }
}
