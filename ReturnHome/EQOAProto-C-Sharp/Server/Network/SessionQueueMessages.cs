using System;

using ReturnHome.Utilities;
using ReturnHome.Opcodes;

namespace ReturnHome.Server.Network
{
    public static class SessionQueueMessages
    {
        ///Message processing for outbound section
        public static void PackMessage(Session session, Memory<byte> ClientMessage, byte MessageOpcodeType)
        {
            switch (MessageOpcodeType)
            {
                case 0xF9:
                case 0xFB:
                    CreateFBMessage(session, ClientMessage, MessageOpcodeType);
                    break;

                case 0xFC:
                    CreateFCMessage(session, ClientMessage, MessageOpcodeType);
                    break;

                default:
                    Logger.Info($"Creating message for channel: {MessageOpcodeType}");
                    CreateChannelMessage(session, ClientMessage, MessageOpcodeType);
                    break;
            }
        }

        private static void AddMessage(Session session, ushort MessageSequence, ReadOnlyMemory<byte> MyMessage)
        {
            session.sessionQueue.Add(new MessageStruct(MessageSequence, MyMessage));
        }

        //Create FB Types including creating FA types
        private static void CreateFBMessage(Session session, ReadOnlyMemory<byte> ClientMessage, byte MessageOpcodeType)
        {
            int readBytes = 0;
            int offset = 0;

            Memory<byte> temp;

            //Check if message will span multiple packets
            if (ClientMessage.Length > 1024)
            {
                while ((ClientMessage.Length - readBytes) >= 1024)
                {
                    temp = new Memory<byte>(new byte[1024 + 6]);
                    Span<byte> thisMessage = temp.Span;

                    thisMessage.Write((ushort)(0xFF << 8 | MessageOpcodeTypes.MultiShortReliableMessage), ref offset);
                    thisMessage.Write((ushort)1024, ref offset);
                    thisMessage.Write(session.rdpCommIn.connectionData.lastSentMessageSequence, ref offset);
                    thisMessage.Write(ClientMessage[readBytes..(readBytes + 1024)], ref offset);
                    readBytes += 1024;
                    AddMessage(session, session.rdpCommIn.connectionData.lastSentMessageSequence++, temp);
                    offset = 0;
                }

                //Slice remaining bytes left to put into a message which is < 1500
                ClientMessage = ClientMessage.Slice(readBytes, (ClientMessage.Length - readBytes));
            }

            temp = new Memory<byte>(new byte[(ClientMessage.Length > 255) ? 6 + ClientMessage.Length : 4 + ClientMessage.Length]);
            Span<byte> WholeClientMessage = temp.Span;

            if (ClientMessage.Length > 255)
            {
                WholeClientMessage.Write((ushort)(0xFF << 8 | MessageOpcodeType), ref offset);
                WholeClientMessage.Write((ushort)ClientMessage.Length, ref offset);
                WholeClientMessage.Write(session.rdpCommIn.connectionData.lastSentMessageSequence, ref offset);
                WholeClientMessage.Write(ClientMessage, ref offset);
            }

            else
            {
                WholeClientMessage.Write(MessageOpcodeType, ref offset);
                WholeClientMessage.Write((byte)ClientMessage.Length, ref offset);
                WholeClientMessage.Write(session.rdpCommIn.connectionData.lastSentMessageSequence, ref offset);
                WholeClientMessage.Write(ClientMessage, ref offset);
            }

            AddMessage(session, session.rdpCommIn.connectionData.lastSentMessageSequence++, temp);
        }

        //Create FC Types 
        private static void CreateFCMessage(Session session, ReadOnlyMemory<byte> ClientMessage, byte MessageOpcodeType)
        {
            int offset = 0;

            Memory<byte> temp = new Memory<byte>(new byte[(ClientMessage.Length > 255) ? 4 + ClientMessage.Length : 2 + ClientMessage.Length]);
            Span<byte> WholeClientMessage = temp.Span;

            if (ClientMessage.Length > 255)
            {
                WholeClientMessage.Write((ushort)(0xFF << 8 | MessageOpcodeType), ref offset);
                WholeClientMessage.Write((ushort)ClientMessage.Length, ref offset);
                WholeClientMessage.Write(ClientMessage, ref offset);
            }

            else
            {
                WholeClientMessage.Write(MessageOpcodeType, ref offset);
                WholeClientMessage.Write((byte)ClientMessage.Length, ref offset);
                WholeClientMessage.Write(ClientMessage, ref offset);
            }

            AddMessage(session, 0, temp);
        }

        //Create FC Types 
        private static void CreateChannelMessage(Session session, Memory<byte> ClientMessage, byte MessageOpcodeType)
        {
            int offset = 0;
            //Do some magic to find which counter to apply, eventually add group, and both stat and buff messages to this
            byte xorByte = (MessageOpcodeType >= 0x00 & MessageOpcodeType <= 0x17) ? session.rdpCommIn.connectionData.serverObjects.Span[MessageOpcodeType].baseMessageCounter == 0 ? (byte)0 : (byte)(session.rdpCommIn.connectionData.serverObjects.Span[MessageOpcodeType].messageCounter - session.rdpCommIn.connectionData.serverObjects.Span[MessageOpcodeType].baseMessageCounter) : (byte)0;

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
