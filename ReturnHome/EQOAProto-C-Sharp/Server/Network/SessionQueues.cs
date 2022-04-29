using ReturnHome.Utilities;
using System;
using System.Collections.Concurrent;

namespace ReturnHome.Server.Network
{
    public class SessionQueue
    {
        private Message _segmentMessage = null;
        private ConcurrentQueue<Message> _outGoingReliableMessageQueue = new();
        private ConcurrentQueue<Message> _outGoingUnreliableMessageQueue = new();
        private ConcurrentDictionary<ushort, Message> _resendMessageQueue = new();
		
		private Session _session;

		public SessionQueue(Session session)
		{
			_session = session;
		}
		
        public void Add(Message message)
        {
            //If it is a reliable message type
            if (message.Messagetype == MessageType.ReliableMessage || message.Messagetype == MessageType.PingMessage)
                _outGoingReliableMessageQueue.Enqueue(message);

            //Unreliable message types
            else
                _outGoingUnreliableMessageQueue.Enqueue(message);
        }

        public bool CheckQueue()
        {
            //If no message exist, just return false
            if (_outGoingReliableMessageQueue.IsEmpty && _outGoingUnreliableMessageQueue.IsEmpty && _resendMessageQueue.IsEmpty && _segmentMessage == null)
                return false;

            //If either of these have items in the queue, return true
            if (!_outGoingReliableMessageQueue.IsEmpty || !_outGoingUnreliableMessageQueue.IsEmpty || _segmentMessage != null)
                return true;

            //If resend queue is not empty, check resend timer.
            if (!_resendMessageQueue.IsEmpty)
            {
                //Should we track how many times a message doesn't end up getting ack'd? Eventually disconnect client?
                foreach (var item in _resendMessageQueue)
                {
                    long stuff = DateTimeOffset.Now.ToUnixTimeMilliseconds() - item.Value.Time;
                    //If message needs to be resent, just add it back to the reliable queue
                    if (stuff > 2000)
                        return true;
                }
            }

            return false;
        }

        public void WriteMessages(ref BufferWriter writer, SegmentBodyFlags segmentBodyFlags)
        {
            //Resend queue is not needed here since we loop it back into the reliable queue on the check
            while (writer.Position + 4 < RdpCommOut.maxSize)
            {

                //Check for Segment messages needing to be sent sequentially
                if(_segmentMessage != null)
                {
                    int position;
                    //Write next message as a segment
                    if(_segmentMessage.Size - (_segmentMessage.Index * 0x514) > 0x514)
                    {
                        //Make sure to increment Index here
                        Message tempMessage = new(_segmentMessage.Messagetype, _segmentMessage.message.Slice(_segmentMessage.Index++ * 0x514, 0x514), _session.rdpCommIn.connectionData.lastSentMessageSequence++);
                        position = writer.Position;

                        writer.Write((byte)tempMessage.Messagetype);
                        writer.WriteSize(tempMessage.Size);
                        writer.Write(tempMessage.Sequence);
                        writer.Write(tempMessage.Span);

                        //Reset Timestamp, this works well in our favour of reliables needing to be resent here
                        tempMessage.updateTime();

                        //Copy whole message to Message object to easily recycle later
                        writer.Span[position..writer.Position].CopyTo(tempMessage.Span);

                        //Place segment into resend queue
                        _resendMessageQueue.TryAdd(tempMessage.Sequence, tempMessage);
                        segmentBodyFlags.RdpMessage = true;
                    }

                    //Final message of segment, use FB type
                    else
                    {
                        //Make sure to increment Index here
                        Message tempMessage = new(MessageType.ReliableMessage, _segmentMessage.message.Slice(_segmentMessage.Index++ * 0x514, _segmentMessage.Size - (_segmentMessage.Index++ * 0x514)), _session.rdpCommIn.connectionData.lastSentMessageSequence++);
                        position = writer.Position;

                        writer.Write((byte)tempMessage.Messagetype);
                        writer.WriteSize(tempMessage.Size);
                        writer.Write(tempMessage.Sequence);
                        writer.Write(tempMessage.Span);

                        //Reset Timestamp, this works well in our favour of reliables needing to be resent here
                        tempMessage.updateTime();

                        //Copy whole message to Message object to easily recycle later
                        writer.Span[position..writer.Position].CopyTo(tempMessage.Span);

                        //Place segment into resend queue
                        _resendMessageQueue.TryAdd(tempMessage.Sequence, tempMessage);
                        segmentBodyFlags.RdpMessage = true;

                        //Finished segmenting original message, set this to null to move on
                        _segmentMessage = null;
                    }
                }

                //Check for Resend
                //Should we track how many times a message doesn't end up getting ack'd? Eventually disconnect client?
                long time = DateTimeOffset.Now.ToUnixTimeMilliseconds();
                foreach (var item in _resendMessageQueue)
                {
                    //If message needs to be resent, just add it back to the reliable queue
                    if ((time - item.Value.Time) > 2000)
                    {
                        if(writer.Position + item.Value.Size + 4 < RdpCommOut.maxSize)
                        {
                            writer.Write(item.Value.Span);
                            item.Value.updateTime();
                        }
                    }
                }

                //Process reliable messages
                if (_outGoingReliableMessageQueue.TryPeek(out Message temp2))
                {
                    if ((writer.Position + 4 + temp2.Size + temp2.HeaderSize()) < RdpCommOut.maxSize)
                    {
                        if (_outGoingReliableMessageQueue.TryDequeue(out Message reliableMessage))
                        {
                            int position;
                            if (reliableMessage.Messagetype == MessageType.ReliableMessage || reliableMessage.Messagetype == MessageType.PingMessage)
                            {
                                position = writer.Position;

                                reliableMessage.AddSequence(_session.rdpCommIn.connectionData.lastSentMessageSequence++);

                                writer.Write((byte)reliableMessage.Messagetype);
                                writer.WriteSize(reliableMessage.Size);
                                writer.Write(reliableMessage.Sequence);
                                writer.Write(reliableMessage.Span);

                                //Reset Timestamp, this works well in our favour of reliables needing to be resent here
                                reliableMessage.updateTime();

                                //Copy whole message to Message object to easily recycle later
                                writer.Span[position..writer.Position].CopyTo(reliableMessage.Span);

                                //Place into resend queue
                                _resendMessageQueue.TryAdd(reliableMessage.Sequence, reliableMessage);
                                segmentBodyFlags.RdpMessage = true;

                                //continue processing
                                continue;
                            }

                            //Should be a segment message, process a little differently
                            else
                            {
                                //Make sure to increment Index here
                                Message tempMessage = new(reliableMessage.Messagetype, reliableMessage.message.Slice(reliableMessage.Index++ * 0x514, 0x514), _session.rdpCommIn.connectionData.lastSentMessageSequence++);
                                position = writer.Position;

                                writer.Write((byte)tempMessage.Messagetype);
                                writer.WriteSize(tempMessage.Size);
                                writer.Write(tempMessage.Sequence);
                                writer.Write(tempMessage.Span);

                                //Reset Timestamp, this works well in our favour of reliables needing to be resent here
                                tempMessage.updateTime();

                                //Copy whole message to Message object to easily recycle later
                                writer.Span[position..writer.Position].CopyTo(tempMessage.Span);

                                //Place segment into resend queue
                                _resendMessageQueue.TryAdd(tempMessage.Sequence, tempMessage);
                                _segmentMessage = reliableMessage;
                                segmentBodyFlags.RdpMessage = true;
                            }
                        }
                    }
                }

                //process unreliable messages
                if (_outGoingUnreliableMessageQueue.TryPeek(out Message temp))
                {
                    if (temp.Messagetype == MessageType.UnreliableMessage)
                    {
                        if ((writer.Position + 4 + temp.Size + temp.HeaderSize()) < RdpCommOut.maxSize)
                        {
                            if (_outGoingUnreliableMessageQueue.TryDequeue(out Message message))
                            {
                                //Place message into outgoing message
                                writer.Write((byte)message.Messagetype);
                                writer.WriteSize(message.Size);
                                writer.Write(message.Span);
                                continue;
                            }
                        }
                    }

                    else
                    {
                        if ((writer.Position + 4 + temp.Size + temp.HeaderSize()) < RdpCommOut.maxSize)
                        {
                            if (_outGoingUnreliableMessageQueue.TryDequeue(out Message message))
                            {
                                byte messageType = (byte)message.Messagetype;
                                ushort sequence = messageType > 0 && messageType < 0x17 ? _session.rdpCommIn.connectionData.serverObjects.Span[messageType].messageCounter++ : messageType == 0x40 ? (ushort)0 : messageType == 0x42 ? (ushort)0 : messageType == 0x43 ? (ushort)0 : (ushort)0;
                                byte xorByte = (messageType >= 0x00 & messageType <= 0x17) ? _session.rdpCommIn.connectionData.serverObjects.Span[messageType].baseMessageCounter == 0 ? (byte)0 : (byte)(_session.rdpCommIn.connectionData.serverObjects.Span[messageType].messageCounter - _session.rdpCommIn.connectionData.serverObjects.Span[messageType].baseMessageCounter) : (byte)0;

                                //Place message into outgoing message
                                writer.Write(messageType);
                                writer.WriteSize(message.Size);
                                writer.Write(sequence);
                                writer.Write(xorByte);
                                Compression.runLengthEncode(ref writer, message.Span);
                                continue;
                            }
                        }

                    }
                }

                //Shouldn't hit this unless there is no messages at all
                break;
            }
        }

        //This method will check against 2 connection variables, if the hardack is less then the soft ack
        //It will cycle over the dictionary and try to remove the resend messages, and increment the counter of the hard ack in the process.
        public void RemoveReliables()
        {
            while(_session.rdpCommIn.connectionData.clientLastReceivedMessage > _session.rdpCommIn.connectionData.clientLastReceivedMessageFinal)
            {
                if(_session.sessionQueue._resendMessageQueue.TryRemove(++_session.rdpCommIn.connectionData.clientLastReceivedMessageFinal, out Message message))
                    //TODO: Successfully removed message, should these get placed into a backup dictionary?
                    //Not 100% on proof yet, but pretty sure client can backstep and request a message #
                    continue;

                else
                {
                    //Back step message ack
                    _session.rdpCommIn.connectionData.clientLastReceivedMessageFinal--;

                    //Log this, would mean the message wasn't in our resend queue
                    Logger.Err($"Message #{_session.rdpCommIn.connectionData.clientLastReceivedMessageFinal}");
                    break;
                    //Should session get dropped at this point? Something went wrong here.
                }
            }
        }
    }
}
