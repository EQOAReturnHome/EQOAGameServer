using System;
using ReturnHome.Utilities;
using ReturnHome.Opcodes;

namespace ReturnHome.Server.Network
{
    public class SessionQueueMessages
    {
        ///Message processing for outbound section
        public void PackMessage(Session session, ReadOnlyMemory<byte> ClientMessage, byte MessageOpcodeType)
        {
            //This is only needed so often
            int readBytes = 0;
			
            //Check if message will span multiple packets
            if (ClientMessage.Length > 1024)
            {
                while ((ClientMessage.Length - readBytes) >= 1024)
                {
                    MessageHeaderReliableLong thisMessageHeader = new(MessageOpcodeTypes.MultiLongReliableMessage, 1024, session.rdpCommIn.connectionData.lastSentMessageSequence++);

                    ReadOnlyMemory<byte> WholeClientMessage = new byte[thisMessageHeader.Length + 6];
                    thisMessageHeader.getBytes().CopyTo(WholeClientMessage[0..6]);
                    ClientMessage[readBytes..(readBytes + 1024)].CopyTo(WholeClientMessage[6..WholeClientMessage.Length]);
                    readBytes += 1024;
                    AddMessage(session, thisMessageHeader.Number, WholeClientMessage);
                }

                //Slice remaining bytes left to put into a message which is < 1500
                ClientMessage = ClientMessage.Slice(readBytes, (ClientMessage.Length - readBytes));
            }

			ReadOnlyMemory<byte> WholeClientMessage;
			ushort MessageSequence;
			
            ///Pack Message here into session.SessionMessages
            ///Check message length first
            if (ClientMessage.Length > 255)
            {
				//0xFC types (unreliable system messages)
				if (MessageOpcodeType == MessageOpcodeTypes.ShortUnreliableMessage))
				{
					MessageHeaderUnreliableLong thisMessageHeader = new(((0xFF << 8) | MessageOpcodeType), (ushort)ClientMessage.Length, 0);
					WholeClientMessage = new byte[thisMessageHeader.Length + 4];
					thisMessageHeader.getBytes().CopyTo(WholeClientMessage[0..4]);
					ClientMessage.CopyTo(WholeClientMessage[4..WholeClientMessage.Length]);
					MessageSequence = thisMessageHeader.Number;
				}
				
				//Reliable system messages
				else
				{
					MessageHeaderReliableLong thisMessageHeader = new((0xFF << 8) | MessageOpcodeType), (ushort)ClientMessage.Length, session.rdpCommIn.connectionData.lastSentMessageSequence++);
					WholeClientMessage = new byte[thisMessageHeader.Length + 6];
					thisMessageHeader.getBytes().CopyTo(WholeClientMessage[0..6]);
					ClientMessage.CopyTo(WholeClientMessage[6..WholeClientMessage.Length]);
					MessageSequence = thisMessageHeader.Number;
				}
            }

            ///Message is < 255
            else
            {
				//0xFC types (unreliable system messages)
				if (MessageOpcodeType == MessageOpcodeTypes.ShortUnreliableMessage))
				{
					MessageHeaderUnreliableShort thisMessageHeader = new(MessageOpcodeType, (ushort)ClientMessage.Length, 0);
					WholeClientMessage = new byte[thisMessageHeader.Length + 2];
					thisMessageHeader.getBytes().CopyTo(WholeClientMessage[0..2]);
					ClientMessage.CopyTo(WholeClientMessage[2..WholeClientMessage.Length]);
					MessageSequence = thisMessageHeader.Number;
				}
				
				//Reliable system messages
				else
				{
					MessageHeaderReliableShort thisMessageHeader = new(MessageOpcodeType, (ushort)ClientMessage.Length, session.rdpCommIn.connectionData.lastSentMessageSequence++);
					WholeClientMessage = new byte[thisMessageHeader.Length + 4];
					thisMessageHeader.getBytes().CopyTo(WholeClientMessage[0..4]);
					ClientMessage.CopyTo(WholeClientMessage[4..WholeClientMessage.Length]);
					MessageSequence = thisMessageHeader.Number;
				}
            }
			
			AddMessage(session, MessageSequence, WholeClientMessage);
        }

        private void AddMessage(Session session, ushort MessageSequence, ReadOnlyMemory<byte> MyMessage)
        {
            session.sessionQueue.Add(new MessageStruct(MessageSequence, MyMessage));
        }
    }
}