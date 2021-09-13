// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the MIT license.
using ReturnHome.Opcodes;
using ReturnHome.Utilities;
using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Linq;

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
            //If it is a reliable message type
            if (thisMessage.Message.Span[0] == MessageOpcodeTypes.ShortReliableMessage || thisMessage.Message.Span[0] == MessageOpcodeTypes.PingMessage || thisMessage.Message.Span[0] == MessageOpcodeTypes.MultiShortReliableMessage)
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
            else if (!ResendMessageQueue.IsEmpty)
            {
                /*
                foreach(var item in ResendMessageQueue)
                {
                    long stuff = DateTimeOffset.Now.ToUnixTimeMilliseconds() - item.Value.Time;
                    if (stuff > 2000)
                    {
                        _session.RdpMessage = true;
                        return true;
                    }
                }*/
                return false;
            }

            //If either of these have items in the queue, return true
            else if (!OutGoingReliableMessageQueue.IsEmpty || !OutGoingUnreliableMessageQueue.IsEmpty)
            {
                _session.RdpMessage = true;
                return true;
            }

            return false;
        }

        public (int, List<ReadOnlyMemory<byte>>) GatherMessages()
        {
            List<ReadOnlyMemory<byte>> messageList = new();
            int messageLength = 0;

            /*
			//Rework this
            //Check Resend queue first... this could be much better
            if (ResendMessageQueue.TryPeek(out MessageStruct resendMessage))
            {
                //If no ack in 2 seconds resend
                if ((DateTimeOffset.Now.ToUnixTimeMilliseconds() - resendMessage.Time) > 2000)
                {
                    //Loop over Queue
                    foreach (MessageStruct thisResend in ResendMessageQueue)
                    {
                        //Resend messages
                        packetCreator.PacketWriter(thisResend.Message);
                        thisResend.updateTime();
                    }
                }
            }*/

            while (messageLength < _session.rdpCommOut.maxSize)
            {
                //process unreliable messages
                if (OutGoingUnreliableMessageQueue.TryPeek(out MessageStruct temp))
                {
                    if ((_session.rdpCommOut.totalLength + temp.Message.Length) < _session.rdpCommOut.maxSize)
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
                        return (messageLength, messageList);
                }

                //Process reliable messages
                if (OutGoingReliableMessageQueue.TryPeek(out MessageStruct temp2))
                {
                    if ((_session.rdpCommOut.totalLength + temp2.Message.Length) < _session.rdpCommOut.maxSize)
                    {
                        if (OutGoingReliableMessageQueue.TryDequeue(out MessageStruct reliableMessage))
                        {
                            //Place message into outgoing message
                            messageList.Add(reliableMessage.Message);
                            messageLength += reliableMessage.Message.Length;
                            //Reset Timestamp
                            reliableMessage.updateTime();

                            //Place into resend queue
                            ResendMessageQueue.TryAdd(reliableMessage.Messagenumber, reliableMessage);

                            //continue processing
                            continue;
                        }
                    }

                    else
                        return (messageLength, messageList);
                }

                //Shouldn't hit this unless there is no messages at all
                break;
            }

			return (messageLength, messageList);
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
                if(_session.sessionQueue.ResendMessageQueue.TryRemove(_session.rdpCommIn.connectionData.clientLastReceivedMessageFinal++, out MessageStruct message))
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

                    //Should session get dropped at this point? Something went wrong here.
                }
            }
        }
    }
}
