// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the MIT license.
using ReturnHome.Opcodes;
using ReturnHome.Utilities;
using System;
using System.Collections.Concurrent;
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
                foreach(var item in ResendMessageQueue)
                {
                    if ((DateTimeOffset.Now.ToUnixTimeMilliseconds() - item.Value.Time) > 2000)
                    {
                        _session.RdpMessage = true;
                        return true;
                    }
                }
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

        public bool GatherMessages(out ReadOnlyMemory<byte> message)
        {
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
			
            //process unreliable messages
            if (OutGoingUnreliableMessageQueue.TryPeek(out MessageStruct temp))
            {
                if ((_session.rdpCommOut.totalLength + temp.Message.Length) < _session.rdpCommOut.maxSize)
                {
                    if (OutGoingUnreliableMessageQueue.TryDequeue(out MessageStruct reliableMessage))
                    {
                        //Place message into outgoing message
                        message = reliableMessage.Message;

                        //continue processing
                        return true;
                    }
                }
            }

            //Process reliable messages
            if (OutGoingReliableMessageQueue.TryPeek(out MessageStruct temp2))
            {
                if ((_session.rdpCommOut.totalLength + temp2.Message.Length) < _session.rdpCommOut.maxSize)
                {
                    if (OutGoingReliableMessageQueue.TryDequeue(out MessageStruct reliableMessage))
                    {
                        //Place message into outgoing message
                        message = reliableMessage.Message;

                        //Reset Timestamp
                        reliableMessage.updateTime();

                        //Place into resend queue
                        ResendMessageQueue.TryAdd(reliableMessage.Messagenumber, reliableMessage);

                        //continue processing
                        return true;
                    }
                }
            }

            message = default;
			return false;
        }

        public void RemoveReliables(SessionConnectionData connectionData)
        {
            var removalList = ResendMessageQueue.Keys.Where(x => x <= connectionData.lastReceivedMessageSequence);

            foreach (var key in removalList)
            {
                ResendMessageQueue.TryRemove(key, out MessageStruct serverMessage);
                Logger.Info($"Removed message {key}");
            }
        }
    }
}
