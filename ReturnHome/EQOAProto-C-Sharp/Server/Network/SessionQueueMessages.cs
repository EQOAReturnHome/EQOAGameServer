using System;

using ReturnHome.Utilities;
using ReturnHome.Opcodes;
using ReturnHome.PacketProcessing;

namespace ReturnHome.Server.Network
{
    public static class SessionQueueMessages
    {
        ///Message processing for outbound section
        public static void PackMessage(Session session, ReadOnlyMemory<byte> ClientMessage, byte MessageOpcodeType)
        {
            //This is only needed so often
            int readBytes = 0;
            int offset = 0;

            Memory<byte> temp;
            ushort MessageSequence;
            //Check if message will span multiple packets
            if (ClientMessage.Length > 1024)
            {
                while ((ClientMessage.Length - readBytes) >= 1024)
                {
                    MessageHeaderReliableLong thisMessageHeader = new((ushort)((0xFF << 8) | MessageOpcodeTypes.MultiShortReliableMessage), 1024, session.rdpCommIn.connectionData.lastSentMessageSequence++);

                    temp = new byte[thisMessageHeader.Length + 6];
                    Span<byte> thisMessage = temp.Span;
                    thisMessage.Write(thisMessageHeader.getBytes(), ref offset);

                    //1018 + 6 = 1024
                    thisMessage.Write(ClientMessage[readBytes..(readBytes + 1024)], ref offset);
                    readBytes += 1024;
                    AddMessage(session, thisMessageHeader.Number, temp);
                    offset = 0;
                }

                //Slice remaining bytes left to put into a message which is < 1500
                ClientMessage = ClientMessage.Slice(readBytes, (ClientMessage.Length - readBytes));
            }
			
            ///Pack Message here into session.SessionMessages
            ///Check message length first
            if (ClientMessage.Length > 255)
            {
				//0xFC types (unreliable system messages)
				if (MessageOpcodeType == MessageOpcodeTypes.ShortUnreliableMessage)
				{
					MessageHeaderUnreliableLong thisMessageHeader = new((ushort)((0xFF << 8) | MessageOpcodeType), (ushort)ClientMessage.Length, 0);
                    temp = new byte[thisMessageHeader.Length + 4];
                    Span<byte> WholeClientMessage = temp.Span;
                    WholeClientMessage.Write(thisMessageHeader.getBytes(), ref offset);
                    offset -= 2;
                    WholeClientMessage.Write(ClientMessage, ref offset);
                    MessageSequence = thisMessageHeader.Number;
				}
				
				//Reliable system messages
				else
				{
					MessageHeaderReliableLong thisMessageHeader = new((ushort)((0xFF << 8) | MessageOpcodeType), (ushort)ClientMessage.Length, session.rdpCommIn.connectionData.lastSentMessageSequence++);
                    temp = new byte[thisMessageHeader.Length + 6];
                    Span<byte> WholeClientMessage = temp.Span;
                    WholeClientMessage.Write(thisMessageHeader.getBytes(), ref offset);
                    WholeClientMessage.Write(ClientMessage, ref offset);
                    MessageSequence = thisMessageHeader.Number;
                }
            }

            ///Message is < 255
            else
            {
				//0xFC types (unreliable system messages)
				if (MessageOpcodeType == MessageOpcodeTypes.ShortUnreliableMessage)
				{
					MessageHeaderUnreliableShort thisMessageHeader = new(MessageOpcodeType, (byte)ClientMessage.Length, 0);
                    temp = new byte[thisMessageHeader.Length + 2];
                    Span<byte> WholeClientMessage = temp.Span;
                    WholeClientMessage.Write(thisMessageHeader.getBytes(), ref offset);
                    offset -= 2;
                    WholeClientMessage.Write(ClientMessage, ref offset);
                    MessageSequence = thisMessageHeader.Number;
                }
				
				//Reliable system messages
				else
				{
					MessageHeaderReliableShort thisMessageHeader = new(MessageOpcodeType, (byte)ClientMessage.Length, session.rdpCommIn.connectionData.lastSentMessageSequence++);
                    temp = new byte[thisMessageHeader.Length + 4];
                    Span<byte> WholeClientMessage = temp.Span;
                    WholeClientMessage.Write(thisMessageHeader.getBytes(), ref offset);
                    WholeClientMessage.Write(ClientMessage, ref offset);
                    MessageSequence = thisMessageHeader.Number;
                }
            }
			
			AddMessage(session, MessageSequence, temp);
        }

        private static void AddMessage(Session session, ushort MessageSequence, ReadOnlyMemory<byte> MyMessage)
        {
            session.sessionQueue.Add(new MessageStruct(MessageSequence, MyMessage));
        }
    }
}
