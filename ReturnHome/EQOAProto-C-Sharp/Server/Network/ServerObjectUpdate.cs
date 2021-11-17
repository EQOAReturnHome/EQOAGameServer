// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the MIT license.

using System;
using System.Collections.Generic;
using ReturnHome.Utilities;
using ReturnHome.Server.EntityObject;

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

        //Holds character to share object updates for
        public Entity entity;
        public string entityName;

        //should be needed for transferring continents, probably can get away on just doing character changes? Could also be an easy way to flip character in and out of a channel on the client
        private bool _isActive = false;

        public ServerObjectUpdate(Session session, byte objectChannel)
        {
            _session = session;
            ObjectChannel = objectChannel;
        }

        public void AddObject(Entity entity1)
        {
            entity = entity1;
            entityName = entity.CharName;
            _isActive = true;
        }

        public void GenerateUpdate()
        {
            if (entity == null)
                return;

            //See if character and current message has changed
            if (!CompareObjects(baseXOR.Slice(1, 0xC8), entity.ObjectUpdate))
            {
                Memory<byte> temp = new Memory<byte>(new byte[0xC9]);
                temp.Span[0] = _isActive & (baseXOR.Span[0] == 0) ? (byte)1 : (byte)0;
                CoordinateConversions.Xor_data(temp.Slice(1, 0xC8), entity.ObjectUpdate, baseXOR.Slice(1, 0xC8), 0xC8);
                CurrentXORResults.Add(messageCounter, temp);
                SessionQueueMessages.PackMessage(_session, temp, ObjectChannel);
            }
        }

        public void updateCharacter(Entity entity1)
        {
            entity = entity1;
            _isActive = true;
        }

        public void DisableChannel()
        {
            entity = null;
            if (_isActive)
            {
                _isActive = false;
                Memory<byte> temp = new Memory<byte>(new byte[0xC9]);
                temp.Span[0] = 1;
                baseXOR.Slice(1, 0XC8).CopyTo(temp.Slice(1, 0xC8));
                CurrentXORResults.Add(messageCounter, temp);
                SessionQueueMessages.PackMessage(_session, temp, ObjectChannel);
            }
        }

        public void UpdateBaseXor(ushort msgCounter)
        {
            //Get the message client ack'd
            Memory<byte> tempMemory = CurrentXORResults.GetValueOrDefault(msgCounter);

            if (tempMemory.Length == 0)
                return;

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
