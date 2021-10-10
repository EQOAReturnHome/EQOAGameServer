// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the MIT license.

using System;
using System.Linq;
using System.Collections.Generic;
using ReturnHome.Server.Entity.Actor;
using ReturnHome.Utilities;

namespace ReturnHome.Server.Network
{
    public class ServerObjectUpdate
    {
        //Holds receiving clients session info
        private Session _session;

        //Stores our base data to xor against
        public Memory<byte> baseXOR = new Memory<byte>(new byte[0xC9]);

        //This is when the client ack's a specific message, we then set this to that ack'd message # to help generate our xor
        public ushort baseMessageCounter = 0;

        //Simple message counter
        public ushort messageCounter = 1;
        //May not be needed
        public byte ObjectChannel;

        //Place to hold all of our XOR result's till client acks. We then xor that against the baseXOR to get new base object update, clear list once a message is ack'd by client
        private Dictionary<ushort, Memory<byte>> CurrentXORResults = new Dictionary<ushort, Memory<byte>>();

        private Memory<byte> temp = new Memory<byte>(new byte[0xC9]);

        //Holds character to share object updates for
        private Character _character;
        public string characterName;

        //should be needed for transferring continents, probably can get away on just doing character changes? Could also be an easy way to flip character in and out of a channel on the client
        private bool _resetChannel = false;

        public ServerObjectUpdate(Session session, byte objectChannel)
        {
            _session = session;
            ObjectChannel = objectChannel;
        }

        public void AddObject(Character character)
        {
            _character = character;
            characterName = _character.CharName;
        }

        public void GenerateUpdate()
        {
            if (_character == null)
                return;

            //See if character and current message has changed
            if (!CompareObjects(baseXOR, _character.characterUpdate))
            {
                CoordinateConversions.Xor_data(temp, _character.characterUpdate, baseXOR, 0xC9);
                CurrentXORResults.Add(messageCounter, temp);
                SessionQueueMessages.PackMessage(_session, temp, ObjectChannel);
            }
        }

        public void updateCharacter(Character character)
        {
            _character = character;
            _resetChannel = true;
        }

        public void UpdateBaseXor(ushort msgCounter)
        {
            //Get the message client ack'd
            CurrentXORResults.TryGetValue(msgCounter, out Memory<byte> tempMemory);

            //xor base against this message
            CoordinateConversions.Xor_data(baseXOR, tempMemory, 0xC9);

            //Clear Dictionary
            CurrentXORResults.Clear();

            //Ensure this is new base
            baseMessageCounter = msgCounter;
        }

        private static bool CompareObjects(Memory<byte> first, Memory<byte> second)
        {
            if (first.Length != second.Length)
                return false;

            Span<byte> firstTemp = first.Span;
            Span<byte> secondTemp = second.Span;

            for( int i = 0; i < first.Length; i++)
            {
                if (firstTemp[i] == secondTemp[i])
                    continue;
                else
                    return false;
            }
            return true;
        }
    }
}
