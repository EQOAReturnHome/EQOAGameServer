using System;

using ReturnHome.Utilities;
using ReturnHome.Server.Opcodes;

namespace ReturnHome.Server.Network
{
    //TODO: Consider moving message Packing to RdpCommOut.PrepPackets, incorporate Message Class to pass through
    public static class SessionQueueMessages
    {
        ///Message processing for outbound section
        public static void PackMessage(Session session, Message message, BufferWriter writer)
        {
            switch (message.messageType)
            {
                case MessageType.PingMessage:
                case MessageType.ReliableMessage:
                    CreateFBMessage(session, message, writer);
                    break;

                case MessageType.UnreliableMessage:
                    CreateFCMessage(session, message, writer);
                    break;

                default:
                    CreateChannelMessage(session, message, writer);
                    break;
            }
        }

        private static void AddMessage(Session session, Message message)
        {
            session.sessionQueue.Add(message);
        }

        //Create FB Types including creating FA types
        private static void CreateFBMessage(Session session, Message message, BufferWriter writer)
        {
            int readBytes = 0;
            //Check if message will span multiple packets
            if (message.Size > 0x514)
            {
                //Skip first 6 bytes
                while ((message.Size - readBytes) >= 0x514)
                {
                    Message message2 = new(new Memory<byte>(new byte[0x51A]), session.rdpCommIn.connectionData.lastSentMessageSequence++, message.messageType, message.Opcode);
                    BufferWriter writer2 = new(message2.Span);

                    writer2.Write((byte)0xFF);
                    writer2.Write(MessageType.SegmentReliableMessage);
                    writer2.Write((ushort)0x514);
                    writer2.Write(message2.Sequence);
                    writer2.Write(writer.Span[readBytes..(readBytes + 0x514)]);
                    readBytes += 0x514;
                    AddMessage(session, message2);
                }

                Message message3 = new(new Memory<byte>(new byte[(message.Size - readBytes) > 255 ? (message.Size - readBytes) + 6 : (message.Size - readBytes) + 4]), session.rdpCommIn.connectionData.lastSentMessageSequence++, message.messageType, message.Opcode);
                BufferWriter writer3 = new(message3.Span);
                if(message3.Size > 255)
                    writer3.Write((byte)0xFF);

                writer3.Write(MessageType.ReliableMessage);
                ushort temp2 = (ushort)(message3.Size - message3.headerSize);
                writer3.Write(temp2 > 255 ? temp2 : (byte)temp2);
                writer3.Write(message3.Sequence);
                writer3.Write(writer.Span[readBytes..writer.Position]);

                AddMessage(session, message3);
                return;

            }

            //Add Sequence and set Position of writer to 0
            message.AddSequence(session.rdpCommIn.connectionData.lastSentMessageSequence++);
            writer.Position = 0;
            ushort size = (ushort)(message.Size - message.headerSize);
            if (size > 255)
                writer.Write((byte)0xFF);
            writer.Write(message.messageType);
            writer.Write(size > 255 ? size : (byte)size);
            writer.Write(message.Sequence);

            AddMessage(session, message);
        }

        //Create FC Types 
        private static void CreateFCMessage(Session session, Message message, BufferWriter writer)
        {
            writer.Position = 0;
            ushort size = (ushort)(message.Size - message.headerSize);
            if (size > 255)
                writer.Write((byte)0xFF);
            writer.Write(message.messageType);
            writer.Write(size > 255 ? size : (byte)size);
            writer.Write(message.Sequence);

            AddMessage(session, message);
        }

        //Create channel Type
        private static void CreateChannelMessage(Session session, Message message, BufferWriter writer)
        {
            int offset = 0;
            //Do some magic to find which counter to apply, eventually add group, and both stat and buff messages to this
            byte xorByte = (MessageType >= 0x00 && MessageType <= 0x17) ? session.rdpCommIn.connectionData.serverObjects.Span[MessageOpcodeType].baseMessageCounter == 0 ? (byte)0 : (byte)(session.rdpCommIn.connectionData.serverObjects.Span[MessageOpcodeType].messageCounter - session.rdpCommIn.connectionData.serverObjects.Span[MessageOpcodeType].baseMessageCounter) : (byte)0;

            Memory<byte> temp = new Memory<byte>(new byte[(ClientMessage.Length > 255) ? 7 + ClientMessage.Length : 5 + ClientMessage.Length]);
            Span<byte> WholeClientMessage = temp.Span;

            if (ClientMessage.Length > 255)
            {
                WholeClientMessage.Write((ushort)(0xFF << 8 | MessageOpcodeType), ref offset);
                WholeClientMessage.Write((ushort)ClientMessage.Length, ref offset);

                //Need some way to distinguish which channel message coutner to use here?
                WholeClientMessage.Write(session.rdpCommIn.connectionData.serverObjects.Span[MessageOpcodeType].messageCounter++, ref offset);
                WholeClientMessage.Write(xorByte, ref offset);
                WholeClientMessage.Write(Compression.runLengthEncode(ClientMessage), ref offset);

            }

            else
            {
                WholeClientMessage.Write(MessageOpcodeType, ref offset);
                WholeClientMessage.Write((byte)ClientMessage.Length, ref offset);

                //Need some way to distinguish which channel message coutner to use here?
                WholeClientMessage.Write(session.rdpCommIn.connectionData.serverObjects.Span[MessageOpcodeType].messageCounter++, ref offset);
                WholeClientMessage.Write(xorByte, ref offset);
                WholeClientMessage.Write(Compression.runLengthEncode(ClientMessage), ref offset);
                WholeClientMessage.Write((byte)0, ref offset);

            }

            //Slice out the actual message and send it off, can probably be reworked later to compress it comwhere else but still reliably read message size?
            AddMessage(session, 0, temp.Slice(0, offset));
        }
    }
}
