// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the MIT license.
using ReturnHome.Opcodes;
using ReturnHome.Utilities;
using System;
using System.Collections.Concurrent;

namespace ReturnHome.PacketProcessing
{
    public class EQOASessionQueue
    {
        private ConcurrentQueue<MessageStruct> OutGoingReliableMessageQueue = new();
        private ConcurrentQueue<MessageStruct> OutGoingUnreliableMessageQueue = new();
        private ConcurrentQueue<MessageStruct> ResendMessageQueue = new();
        private Session _session;

        public EQOASessionQueue(Session MySession)
        {
            _session = MySession;
        }

        public void Add(MessageStruct thisMessage)
        {
            //If it is a reliable message type
            if(thisMessage.Message.Span[0] == MessageOpcodeTypes.ShortReliableMessage || thisMessage.Message.Span[0] == MessageOpcodeTypes.PingMessage || thisMessage.Message.Span[0] == MessageOpcodeTypes.MultiShortReliableMessage)
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
                if (ResendMessageQueue.TryPeek(out MessageStruct resendMessage))
                {
                    if ((DateTimeOffset.Now.ToUnixTimeMilliseconds() - resendMessage.Time) > 2000)
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

        public void GatherMessages(PacketCreator packetCreator)
        {

            //process unreliable messages
            while (OutGoingUnreliableMessageQueue.TryPeek(out MessageStruct temp))
            {
                if ((packetCreator.ReadBytes + temp.Message.Length) < 1500)
                {
                    if (OutGoingUnreliableMessageQueue.TryDequeue(out MessageStruct reliableMessage))
                    {
                        //Place message into outgoing message
                        packetCreator.PacketWriter(reliableMessage.Message);

                        //continue processing
                        continue;
                    }
                }
                break;
            }

            //Check Resend queue first... this could be much better
            if (ResendMessageQueue.TryDequeue(out MessageStruct resendMessage))
            {
                //If no ack in 2 seconds resend
                if ((DateTimeOffset.Now.ToUnixTimeMilliseconds() - resendMessage.Time) > 2000)
                {
                    //Resend messages
                    packetCreator.PacketWriter(resendMessage.Message);
                    resendMessage.updateTime();
                }
            }

            //Process reliable messages
            while (OutGoingReliableMessageQueue.TryPeek(out MessageStruct temp))
            {
                if ((packetCreator.ReadBytes + temp.Message.Length) < 1500)
                {
                    if (OutGoingReliableMessageQueue.TryDequeue(out MessageStruct reliableMessage))
                    {
                        //Place message into outgoing message
                        packetCreator.PacketWriter(reliableMessage.Message);

                        //Reset Timestamp
                        reliableMessage.updateTime();

                        //Place into resend queue
                        ResendMessageQueue.Enqueue(reliableMessage);

                        //continue processing
                        continue;
                    }
                }
                break;
            }
        }

        public void RemoveReliables(int LastAckMessageNumber)
        {
            while (ResendMessageQueue.TryPeek(out MessageStruct temp))
            {
                if (temp.Messagenumber <= LastAckMessageNumber)
                {
                    // If first message to be resent is less then last ack'd #, drop it
                    if(ResendMessageQueue.TryDequeue(out MessageStruct resendMessage))
                    {
                        continue;
                    }
                }
                //Means that the last ack message is less then our resend queue message
                break;
            }
        }
    }
}
