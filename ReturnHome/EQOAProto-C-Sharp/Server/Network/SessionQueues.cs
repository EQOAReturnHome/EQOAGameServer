// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the MIT license.
using ReturnHome.Server.Opcodes;
using ReturnHome.Utilities;
using System;
using System.Collections.Concurrent;
using System.Collections.Generic;

namespace ReturnHome.Server.Network
{
    public class SessionQueue
    {
        private ConcurrentQueue<MessageStruct> OutGoingReliableMessageQueue = new();
        private ConcurrentQueue<MessageStruct> OutGoingUnreliableMessageQueue = new();
        private ConcurrentDictionary<ushort, MessageStruct> ResendMessageQueue = new();
		
		private Session _session;

		public SessionQueue(Session session)
		{
			_session = session;
		}
		
        public void Add(MessageStruct thisMessage)
        {
            byte messageType = thisMessage.Message.Span[0];

            //If it is a reliable message type
            if (messageType == MessageOpcodeTypes.ShortReliableMessage || messageType == MessageOpcodeTypes.PingMessage || messageType == MessageOpcodeTypes.MultiShortReliableMessage)
            {
                OutGoingReliableMessageQueue.Enqueue(thisMessage);
            }

            //Unreliable message types
            else
            {
                OutGoingUnreliableMessageQueue.Enqueue(thisMessage);
            }
        }

        public bool CheckQueue()
        {
            //If no message exist, just return false
            if (OutGoingReliableMessageQueue.IsEmpty && OutGoingUnreliableMessageQueue.IsEmpty && ResendMessageQueue.IsEmpty)
            {
                return false;
            }

            //If resend queue is not empty, check resend timer.
            if (!ResendMessageQueue.IsEmpty)
            {
                //Should we track how many times a message doesn't end up getting ack'd? Eventually disconnect client?
                foreach (var item in ResendMessageQueue)
                {
                    long stuff = DateTimeOffset.Now.ToUnixTimeMilliseconds() - item.Value.Time;
                    //If message needs to be resent, just add it back to the reliable queue
                    if (stuff > 2000)
                    {
                        OutGoingReliableMessageQueue.Enqueue(item.Value);
                    }
                }
            }

            //If either of these have items in the queue, return true
            if (!OutGoingReliableMessageQueue.IsEmpty || !OutGoingUnreliableMessageQueue.IsEmpty)
            {
                _session.PacketBodyFlags.RdpMessage = true;
                return true;
            }

            return false;
        }

        public int GatherMessages(SegmentBodyFlags PacketBodyFlags, List<ReadOnlyMemory<byte>> messageList)
        {
            int messageLength = 0;

            //Resend queue is not needed here since we loop it back into the reliable queue on the check
            while (messageLength < _session.rdpCommOut.maxSize)
            {
                //Process reliable messages
                if (OutGoingReliableMessageQueue.TryPeek(out MessageStruct temp2))
                {
                    if ((messageLength + temp2.Message.Length) < _session.rdpCommOut.maxSize)
                    {
                        if (OutGoingReliableMessageQueue.TryDequeue(out MessageStruct reliableMessage))
                        {
                            //Place message into outgoing message
                            messageList.Add(reliableMessage.Message);
                            messageLength += reliableMessage.Message.Length;

                            //Reset Timestamp, this works well in our favour of reliables needing to be resent here
                            reliableMessage.updateTime();

                            //Place into resend queue
                            if (ResendMessageQueue.TryAdd(reliableMessage.Messagenumber, reliableMessage))
                            {
                                PacketBodyFlags.RdpMessage = true;

                                //continue processing
                                continue;
                            }
                            else
                            {
                                //Should we track how many times a message doesn't end up getting ack'd? Eventually disconnect client?
                                if (ResendMessageQueue.TryUpdate(reliableMessage.Messagenumber, reliableMessage, reliableMessage))
                                {
                                    PacketBodyFlags.RdpMessage = true;
                                    continue;
                                }

                                else
                                    Logger.Err($"Error occured adding or updating Message #{reliableMessage.Messagenumber} for session {_session.rdpCommIn.clientID.ToString("X")}");
                                    //Log an error here?
                            }
                        }
                    }

                    else
                        return messageLength;
                }

                //process unreliable messages
                if (OutGoingUnreliableMessageQueue.TryPeek(out MessageStruct temp))
                {
                    if ((messageLength + temp.Message.Length) < _session.rdpCommOut.maxSize)
                    {
                        if (OutGoingUnreliableMessageQueue.TryDequeue(out MessageStruct reliableMessage))
                        {
                            //Place message into outgoing message
                            messageList.Add(reliableMessage.Message);
                            messageLength += reliableMessage.Message.Length;
                            continue;
                        }
                    }

                    else
                        return messageLength;
                }

                //Shouldn't hit this unless there is no messages at all
                break;
            }

			return messageLength;
        }

        public int CalculateMessageList(List<ReadOnlyMemory<byte>> messageList)
        {
            int length = 0;
            foreach (ReadOnlyMemory<byte> mes in messageList)
                length += mes.Length;
            return length;
        }

        //This method will check against 2 connection variables, if the hardack is less then the soft ack
        //It will cycle over the dictionary and try to remove the resend messages, and increment the counter of the hard ack in the process.
        public void RemoveReliables()
        {
            while(_session.rdpCommIn.connectionData.clientLastReceivedMessage > _session.rdpCommIn.connectionData.clientLastReceivedMessageFinal)
            {
                if(_session.sessionQueue.ResendMessageQueue.TryRemove(++_session.rdpCommIn.connectionData.clientLastReceivedMessageFinal, out MessageStruct message))
                {
                    //Successfully removed message, should these get placed into a backup dictionary?
                    //Not 100% on proof yet, but pretty sure client can backstep and request a message #
                    continue;
                }

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
