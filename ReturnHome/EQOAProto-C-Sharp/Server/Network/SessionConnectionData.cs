using System;
using System.Collections.Generic;
using System.Linq;
using ReturnHome.Server.EntityObject;
using ReturnHome.Server.EntityObject.Player;

namespace ReturnHome.Server.Network
{
    public class SessionConnectionData
    {
        public ushort lastReceivedMessageSequence { get; set; }
        public ushort lastReceivedPacketSequence { get; set; }
        public ushort lastSentMessageSequence { get; set; }
        public ushort lastSentPacketSequence { get; set; }
        public ushort clientLastReceivedMessage { get; set; }
        public ushort clientLastReceivedMessageFinal { get; set; }
        public ClientObjectUpdate client {get; set;}

        //Create an array of all 24 object updates
        //Need a way to ensure no more then 24 objects can be allocated, array may be best?
        public Memory<ServerObjectUpdate> serverObjects;


        public SessionConnectionData(Session _session)
        {
            lastReceivedMessageSequence = 0;
            lastReceivedPacketSequence = 0;

            //Client last soft ack of message's received.
            clientLastReceivedMessage = 0;

            //Server hard ack of client messages received. Server will compare this against client,
            //and if client ack is higher, server will cycle through resend list and remove.
            clientLastReceivedMessageFinal = 0;

			lastSentMessageSequence = 1;
			lastSentPacketSequence = 1;

            //Create the object to track incoming client updates
            client = new();

            serverObjects = new Memory<ServerObjectUpdate>(new ServerObjectUpdate[0x17]);
            Span<ServerObjectUpdate> tempSpan = serverObjects.Span;

            for (int i = 0; i < tempSpan.Length; i++)
                tempSpan[i] = new(_session, (byte)i);
        }

        public void AddChannelObjects(List<Entity> charList)
        {
            if (charList.Count == 0)
                return;

            charList = charList.GetRange(0, charList.Count > 23 ? 23 : charList.Count);

            Span<ServerObjectUpdate> temp = serverObjects.Span;
            //Iterate over List from QuadTree against Channels, skip ourself
            for (int i = 0; i < serverObjects.Length; i++)
            {
                //Character is already in a channel
                if (charList.Contains(temp[i].entity))
                {
                    charList.Remove(temp[i].entity);
                    continue;
                }

                else
                    temp[i].DisableChannel();
            }

            if (charList.Count == 0)
                return;

            for(int i = 0; i < serverObjects.Length; i++)
            {
                if(temp[i].entity == null)
                {
                    temp[i].AddObject(charList[0]);
                    charList.RemoveAt(0);
                    if (charList.Count == 0)
                        return;
                }
                continue;
            }
        }
    }
}
