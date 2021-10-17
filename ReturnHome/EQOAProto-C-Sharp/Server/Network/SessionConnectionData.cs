using System;
using System.Collections.Generic;
using ReturnHome.Server.Entity.Actor;

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

        public void AddChannelObjects(List<Character> charList)
        {
            //Check if list is greater then ~24 characters

            //If list is greater, remove farther characters

            //Cross reference list against current channels, ensauring they all match or that characters are swapped around in the channel's array
            Span<ServerObjectUpdate> temp = serverObjects.Span;
            foreach(Character chara in charList)
            {
                bool ChannelExists = false;
                for(int i = 0; i < temp.Length; i++)
                {
                    if (chara.CharName == temp[i].characterName)
                    {
                        ChannelExists ^= true;
                        break;
                    }
                }

                //If channel does not exist, find next available... or bump a character off. Just add to next available, for simplicity
                if (!ChannelExists)
                {
                    for (int i = 0; i < temp.Length; i++)
                    {
                        if (temp[i].characterName == null)
                        {
                            temp[i].updateCharacter(chara);
                        }
                    }
                }
            }
        }
    }
}
