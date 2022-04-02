using System;
using System.Collections.Generic;
using System.Text;
using ReturnHome.Server.EntityObject.Player;
using ReturnHome.Server.Network;
using ReturnHome.Utilities;

namespace ReturnHome.Server.Opcodes.Messages.Server
{
    class ServerCreateCharacterList
    {
        public static void CreateCharacterList(List<Character> MyCharacterList, Session session)
        {
            //gather expected buffer size... start with 3, opcode and character count should always be 3
            int bufferSize = 3;
            for (int i = 0; i < MyCharacterList.Count; i++)
            {
                //Everycharacter has this as a "standard" amount of bytes
                bufferSize += 82;
                bufferSize += MyCharacterList[i].CharName.Length;
                bufferSize += Utility_Funcs.DoubleVariableLengthIntegerLength(MyCharacterList[i].ServerID);
                bufferSize += Utility_Funcs.DoubleVariableLengthIntegerLength(MyCharacterList[i].ModelID);
            }

            int offset = 0;
            Memory<byte> temp = new byte[bufferSize];
            Span<byte> Message = temp.Span;

            //Holds list of characters pulled from the DB for the AccountID
            Message.Write(BitConverter.GetBytes((ushort)GameOpcode.CharacterSelect), ref offset);

            ///Gets our character count and uses technique to double it
            Message.Write7BitDoubleEncodedInt(MyCharacterList.Count, ref offset);

            //Iterates through each charcter in the list and converts attribute values to packet values
            foreach (Character character in MyCharacterList)
            {
                ///Add the Character name length
                Message.Write(BitConverter.GetBytes((uint)character.CharName.Length), ref offset);

                ///Add character name
                Message.Write(Encoding.ASCII.GetBytes(character.CharName), ref offset);

                ///Add Server ID
                Message.Write7BitDoubleEncodedInt(character.ServerID, ref offset);

                ///Add Model
                Message.Write7BitDoubleEncodedInt(character.ModelID, ref offset);

                ///Add Class
                Message.Write7BitDoubleEncodedInt(character.Class, ref offset);

                ///Add Race
                Message.Write7BitDoubleEncodedInt(character.Race, ref offset);

                ///Add Level
                Message.Write7BitDoubleEncodedInt(character.Level, ref offset);

                ///Add Hair color
                Message.Write7BitDoubleEncodedInt(character.HairColor, ref offset);

                ///Add Hair Length
                Message.Write7BitDoubleEncodedInt(character.HairLength, ref offset);

                ///Add Hair Style
                Message.Write7BitDoubleEncodedInt(character.HairStyle, ref offset);

                ///Add Face option
                Message.Write7BitDoubleEncodedInt(character.FaceOption, ref offset);

                //Equip Gear
                character.EquipGear(character);

                ///Add Robe
                Message.Write(BitConverter.GetBytes(character.Robe), ref offset);

                ///Add Primary
                Message.Write(BitConverter.GetBytes(character.Primary), ref offset);

                ///Add Secondary
                Message.Write(BitConverter.GetBytes(character.Secondary), ref offset);

                ///Add Shield
                Message.Write(BitConverter.GetBytes(character.Shield), ref offset);

                ///Add Character animation here, dumby for now
                Message.Write(BitConverter.GetBytes((ushort)0x0004), ref offset);

                ///unknown value?
                Message.Write(new byte[] { 0x00 }, ref offset);

                ///Chest Model
                Message.Write(new byte[] { character.Chest }, ref offset);

                ///uBracer Model
                Message.Write(new byte[] { character.Bracer }, ref offset);

                ///Glove Model
                Message.Write(new byte[] { character.Gloves }, ref offset);

                ///Leg Model
                Message.Write(new byte[] { character.Legs }, ref offset);

                ///Boot Model
                Message.Write(new byte[] { character.Boots }, ref offset);

                ///Helm Model
                Message.Write(new byte[] { character.Helm }, ref offset);

                ///unknown value?
                Message.Write(BitConverter.GetBytes((uint)0), ref offset);

                ///unknown value?
                Message.Write(BitConverter.GetBytes((ushort)0), ref offset);

                ///unknown value?
                Message.Write(BitConverter.GetBytes(0xFFFFFFFF), ref offset);

                ///unknown value?
                Message.Write(BitConverter.GetBytes(0xFFFFFFFF), ref offset);

                ///unknown value?
                Message.Write(BitConverter.GetBytes(0xFFFFFFFF), ref offset);

                ///Chest color
                Message.Write(BitConverter.GetBytes(ByteSwaps.SwapBytes(character.ChestColor)), ref offset);

                ///Bracer color
                Message.Write(BitConverter.GetBytes(ByteSwaps.SwapBytes(character.BracerColor)), ref offset);

                ///Glove color
                Message.Write(BitConverter.GetBytes(ByteSwaps.SwapBytes(character.GloveColor)), ref offset);

                ///Leg color
                Message.Write(BitConverter.GetBytes(ByteSwaps.SwapBytes(character.LegColor)), ref offset);

                ///Boot color
                Message.Write(BitConverter.GetBytes(ByteSwaps.SwapBytes(character.BootsColor)), ref offset);

                ///Helm color
                Message.Write(BitConverter.GetBytes(ByteSwaps.SwapBytes(character.HelmColor)), ref offset);

                ///Robe color
                Message.Write(BitConverter.GetBytes(ByteSwaps.SwapBytes(character.RobeColor)), ref offset);
            }

            ///Character list is complete
            ///Handles packing message into outgoing packet
            SessionQueueMessages.PackMessage(session, temp, MessageOpcodeTypes.ShortReliableMessage);
        }
    }
}
