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
            //Check if list is greater then ~24 characters

            //If list is greater, remove farther characters

            //Cross reference list against current channels, ensauring they all match or that characters are swapped around in the channel's array
            Span<ServerObjectUpdate> temp = serverObjects.Span;
            List<Entity> removeList = new();

            //If this list is empty... means no one is nearby
            if (charList.Count <= 1)
            {
                //print single Character Out
                if (charList.Count == 1)
                    

                return;
            }

            charList = charList.GetRange(0, charList.Count);

            //Iterate over List from QuadTree against Channels, skip ourself
            for (int i = 0; i < charList.Count; i++)
            {
                for (int j = 0; j < 0x17; j++)
                {
                    if (temp[j].entity == null)
                        continue;

                    //If Character is already in array, move on to next character
                    if (temp[j].entity.ObjectID == charList[i].ObjectID)
                    {
                        removeList.Add(charList[i]);
                        break;
                    }
                }

                //Remove all objects from charList in removeList
                charList.RemoveAll(x => removeList.Any(y => x.ObjectID == y.ObjectID));

                if (charList.Count == 0)
                    return;

                //Iterate over channels one last time to place in new objects if any
                for (int j = 1; j < 0x17; j++)
                {
                    //If Character is already in array, move on to next character
                    if (removeList.Contains(temp[j].entity))
                    {
                        continue;
                    }

                    //Place new character into channel
                    temp[j].updateCharacter(charList[0]);

                    //Remove character at first index
                    charList.RemoveAt(0);

                    if (charList.Count == 0)
                        break;
                }

                if (charList.Count > 0)
                    Console.WriteLine("ERROR, EXTRA CHARACTERS LEFT OVER");
            }
        }
    }
}
