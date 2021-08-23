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
			
            //Check if message will span multiple packets
            if (ClientMessage.Length > 1024)
            {
                while ((ClientMessage.Length - readBytes) >= 1024)
                {
                    MessageHeaderReliableLong thisMessageHeader = new((ushort)((0xFF << 8) | MessageOpcodeTypes.MultiShortReliableMessage), 1024, session.rdpCommIn.connectionData.lastSentMessageSequence++);

                    Memory<byte> thisMessage = new byte[thisMessageHeader.Length + 6];
                    thisMessage.Write(thisMessageHeader.getBytes(), ref readBytes);

                    //1018 + 6 = 1024
                    thisMessage.Write(ClientMessage[readBytes..(readBytes + 1018)], ref readBytes);
                    AddMessage(session, thisMessageHeader.Number, thisMessage);
                }

                //Slice remaining bytes left to put into a message which is < 1500
                ClientMessage = ClientMessage.Slice(readBytes, (ClientMessage.Length - readBytes));
            }

			Memory<byte> WholeClientMessage;
			ushort MessageSequence;
			
            ///Pack Message here into session.SessionMessages
            ///Check message length first
            if (ClientMessage.Length > 255)
            {
				//0xFC types (unreliable system messages)
				if (MessageOpcodeType == MessageOpcodeTypes.ShortUnreliableMessage)
				{
					MessageHeaderUnreliableLong thisMessageHeader = new((ushort)((0xFF << 8) | MessageOpcodeType), (ushort)ClientMessage.Length, 0);
					WholeClientMessage = new byte[thisMessageHeader.Length + 4];
                    WholeClientMessage.Write(thisMessageHeader.getBytes(), ref readBytes);
					WholeClientMessage.Write(WholeClientMessage[readBytes..(readBytes + WholeClientMessage.Length)], ref readBytes);
					MessageSequence = thisMessageHeader.Number;
				}
				
				//Reliable system messages
				else
				{
					MessageHeaderReliableLong thisMessageHeader = new((ushort)((0xFF << 8) | MessageOpcodeType), (ushort)ClientMessage.Length, session.rdpCommIn.connectionData.lastSentMessageSequence++);
                    WholeClientMessage = new byte[thisMessageHeader.Length + 6];
                    WholeClientMessage.Write(thisMessageHeader.getBytes(), ref readBytes);
                    WholeClientMessage.Write(WholeClientMessage[readBytes..(readBytes + WholeClientMessage.Length)], ref readBytes);
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
                    WholeClientMessage = new byte[thisMessageHeader.Length + 2];
                    WholeClientMessage.Write(thisMessageHeader.getBytes(), ref readBytes);
                    WholeClientMessage.Write(WholeClientMessage[readBytes..(readBytes + WholeClientMessage.Length)], ref readBytes);
                    MessageSequence = thisMessageHeader.Number;
                }
				
				//Reliable system messages
				else
				{
					MessageHeaderReliableShort thisMessageHeader = new(MessageOpcodeType, (byte)ClientMessage.Length, session.rdpCommIn.connectionData.lastSentMessageSequence++);
                    WholeClientMessage = new byte[thisMessageHeader.Length + 4];
                    WholeClientMessage.Write(thisMessageHeader.getBytes(), ref readBytes);
                    WholeClientMessage.Write(WholeClientMessage[readBytes..(readBytes + WholeClientMessage.Length)], ref readBytes);
                    MessageSequence = thisMessageHeader.Number;
                }
            }
			
			AddMessage(session, MessageSequence, WholeClientMessage);
        }

        private static void AddMessage(Session session, ushort MessageSequence, ReadOnlyMemory<byte> MyMessage)
        {
            session.sessionQueue.Add(new MessageStruct(MessageSequence, MyMessage));
        }
    }
}
