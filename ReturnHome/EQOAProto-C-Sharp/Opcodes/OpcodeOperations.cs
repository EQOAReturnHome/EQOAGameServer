using System;
using System.Collections.Generic;
using System.Text;
using System.Buffers.Binary;

using ReturnHome.Utilities;
using ReturnHome.Database.SQL;
using ReturnHome.AccountAction;
using ReturnHome.Server.Network;
using ReturnHome.Server.Entity.Actor;
using ReturnHome.Server.Network.Managers;
using System.IO;
using ReturnHome.Playercharacter.Actor;

namespace ReturnHome.Opcodes
{
    public static class ProcessOpcode
    {
        public static readonly Dictionary<GameOpcode, Action<Session, PacketMessage>> OpcodeDictionary = new()
        {
            { GameOpcode.DiscVersion, ProcessGameDisc },
            { GameOpcode.Authenticate, ProcessAuthenticate },
            { GameOpcode.Authenticate2, ProcessAuthenticate },
            { GameOpcode.SELECTED_CHAR, ProcessCharacterChanges },
            { GameOpcode.DelCharacter, ProcessDelChar },
            { GameOpcode.CreateCharacter, ProcessCreateChar },
            { GameOpcode.ClientSayChat, ProcessChat },
            { GameOpcode.RandomName, GenerateRandomName },
        };

        public static void ProcessOpcodes(Session MySession, PacketMessage message)
        {

            //Logger.Info($"Message Length: {ClientPacket.Length}; OpcodeType: {MessageTypeOpcode.ToString("X")}; Message Number: {MessageNumber.ToString("X")}; Opcode: {Opcode.ToString("X")}.");
            try
            {
                OpcodeDictionary[(GameOpcode)message.Header.Opcode].Invoke(MySession, message);
            }

            catch
            {
                ClientOpcodeUnknown(MySession, message.Header.Opcode);
            }
        }

        public static void GenerateRandomName(Session MySession, PacketMessage ClientPacket)
        {
            int offset = 0;

            //This could be useful later if real names are created per race/sex
            ///Get Race Byte
            byte Race = ClientPacket.Data.Span[0];

            ///Make sure Message number is expected, needs to be in order.
            byte sex = ClientPacket.Data.Span[1];


            string Name = RandomName.GenerateName();
            //Maybe a check here to verify name isn't taken in database before sending to client?

            Memory<byte> temp = new Memory<byte>(new byte[2 + 4 + (Name.Length * 2)]);
            Span<byte> Message = temp.Span;

            Message.Write((ushort) GameOpcode.RandomName, ref offset);
            Message.Write(Name.Length, ref offset);
            Message.Write(Encoding.Default.GetBytes(Name), ref offset);
            //Send Message
            SessionQueueMessages.PackMessage(MySession, temp, MessageOpcodeTypes.ShortReliableMessage);
        }

        public static void ProcessChat(Session MySession, PacketMessage ClientPacket)
        {
            int offset = 0;

            int messageLength = BinaryPrimitives.ReadInt32LittleEndian(ClientPacket.Data.Span[0..]);
            string message = Encoding.Unicode.GetString(ClientPacket.Data.Span[4..(4 + messageLength * 2)]);

            if (message == "!c")
            {
                MySession.coordToggle ^= true;
            }
            if (message == "!o")
            {
                MySession.unkOpcode ^= true;
                if (MySession.unkOpcode)
                {
                    message = "Unknown opcode display is now on.";
                }

                else
                {
                    message = "Unknown opcode display is now off.";
                }

                Memory<byte> temp = new Memory<byte>(new byte[2 + 4 + (message.Length * 2)]);
                Span<byte> Message = temp.Span;
                Message.Write((ushort)GameOpcode.ClientMessage, ref offset);
                Message.Write(message.Length, ref offset);
                Message.Write(Encoding.Unicode.GetBytes(message), ref offset);

                //Send Message
                SessionQueueMessages.PackMessage(MySession, temp, MessageOpcodeTypes.ShortReliableMessage);
            }

            if (message.Substring(0, 2) == "!s")
            {
                float speed;
                try
                {
                    speed = float.Parse(message.Substring(3, messageLength - 3));
                }

                catch
                {
                    message = "Not a valid value for speed";

                    Memory<byte> temp = new Memory<byte>(new byte[2 + 4 + (message.Length * 2)]);
                    Span<byte> Message = temp.Span;

                    Message.Write((ushort)GameOpcode.ClientMessage, ref offset);
                    Message.Write(message.Length, ref offset);
                    Message.Write(Encoding.Unicode.GetBytes(message), ref offset);

                    //Send Message
                    SessionQueueMessages.PackMessage(MySession, temp, MessageOpcodeTypes.ShortReliableMessage);
                    return;
                }

                ActorSpeed(MySession, speed);
            }
        }

        public static void ClientOpcodeUnknown(Session MySession, ushort opcode)
        {
            if (MySession.unkOpcode)
            {
                int offset = 0;
                string theMessage = $"Unknown Opcode: {opcode.ToString("X")}";

                Memory<byte> temp = new Memory<byte>(new byte[2 + 4 + (theMessage.Length * 2)]);
                Span<byte> Message = temp.Span;

                Message.Write((ushort)GameOpcode.ClientMessage, ref offset);
                Message.Write(theMessage.Length, ref offset);
                Message.Write(Encoding.Unicode.GetBytes(theMessage), ref offset);

                //Send Message
                SessionQueueMessages.PackMessage(MySession, temp, MessageOpcodeTypes.ShortReliableMessage);
            }
        }

        public static void ProcessCharacterChanges(Session MySession, PacketMessage ClientPacket)
        {
            int offset = 0;
            ReadOnlySpan<byte> Message = ClientPacket.Data.Span;
            //Retrieve CharacterID from client
            int ServerID = Message.GetLEInt(ref offset);
            int FaceOption = Message.GetLEInt( ref offset);
            int HairStyle = Message.GetLEInt( ref offset);
            int HairLength = Message.GetLEInt( ref offset);
            int HairColor = Message.GetLEInt( ref offset);

            //Update these on our character

            //Got character, update changes
            //CharacterSQL thisChar = new();
            //thisChar.UpdateFeatures(MySession, ServerID, HairColor, HairLength, HairStyle, FaceOption);

            SessionManager.CreateMemoryDumpSession(MySession);
        }

        
        public static async void ProcessMemoryDump(Session MySession)
        {
            byte[] buffer;
            //Perform SQl stuff
            CharacterSQL charDump = new CharacterSQL();
            //Probably change this to only pass in character ServerID
            charDump.GetPlayerHotkeys(MySession);
            charDump.GetPlayerWeaponHotbar(MySession);
            charDump.GetPlayerSpells(MySession);

            using(MemoryStream memStream = new())
            {
              //Toss opcode in
              memStream.Write(BitConverter.GetBytes((ushort)GameOpcode.MemoryDump));

              memStream.Write(MySession.MyCharacter.DumpCharacter());
              memStream.Write(new Span<byte> ().Double7bitencodedint(MySession.MyCharacter.MyHotkeys.Count));

              //cycle over all our hotkeys and append them
              foreach(Hotkey h in MySession.MyCharacter.MyHotkeys)
              {
                  memStream.Write(h.PullHotkey());
              }

              //Unknown at this time 4 byte null
              memStream.Write(new byte[4]);

              //Unknown at this time 4 byte null
              memStream.Write(new byte[4]);

              //Quest Count
              memStream.Write(BitConverter.GetBytes(MySession.MyCharacter.MyQuests.Count));

              //Iterate over quest data and append (Should be 0 for now...)
              foreach (Quest q in MySession.MyCharacter.MyQuests)
              {
                  memStream.Write(q.DumpQuest());
              }

              //Get Inventory Item count
              memStream.Write(double7bitencodedint(MySession.MyCharacter.InventoryItems.Count * 2));
              memStream.Write(BitConverter.GetBytes(MySession.MyCharacter.InventoryItems.Count));

              foreach( Item i in MySession.MyCharacter.InventoryItems)
              {
                  memStream.Write(i.DumpItem());
              }

              foreach( WeaponHotbar wb in MySession.MyCharacter.WeaponHotbars)
              {
                  memStream.Write(wb.DumpWeaponHotbar());
              }

              //Get Bank Item count
              memStream.Write(new byte[] { (byte)(MySession.MyCharacter.BankItems.Count * 2) });
              memStream.Write(BitConverter.GetBytes(MySession.MyCharacter.BankItems.Count));
              foreach (Item bi in MySession.MyCharacter.BankItems)
              {
                  memStream.Write(bi.DumpItem());
              }

              // end of bank? or could be something else for memory dump
              memStream.Write(new byte[] { 0 });

              //Buying auctions
              memStream.WriteByte(MySession.MyCharacter.MyBuyingAuctions.Count);
              foreach (Auction ba in MySession.MyCharacter.MyBuyingAuctions)
              {
                  memStream.Write(ba.DumpAuction());
              }

              //Selling auctions
              memStream.WriteByte(MySession.MyCharacter.MySellingAuctions.Count);
              foreach (Auction sa in MySession.MyCharacter.MySellingAuctions)
              {
                  memStream.Write(sa.DumpAuction());
              }

              //Spell count and Spells
              memStream.Write(Utility_Funcs.Technique(MySession.MyCharacter.MySpells.Count));
              foreach (Spell s in MySession.MyCharacter.MySpells)
              {
                  memStream.Write(s.DumpSpell());
              }

              //Not entirely known what this is at this time
              //Related to stats and CM's possibly. Needs testing, just using data from a pcap of live.
              memStream.Write(new byte[] {                  0x55, 0x55, 0x0d, 0x41, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x01, 0x00, 0x00,
                                                            0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x01, 0x00, 0x00, 0x00, 0x01, 0x00,
                                                            0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x02, 0x00, 0x00, 0x00, 0x01,
                                                            0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x03, 0x00, 0x00, 0x00,
                                                            0x01, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x04, 0x00, 0x00,
                                                            0x00, 0x01, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x05, 0x00,
                                                            0x00, 0x00, 0x01, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00,
                                                            0x00, 0x00, 0x00, 0x00, 0xa0, 0x0f, 0xae, 0x98, 0x4c, 0x00, 0x55, 0x55, 0x0d, 0x41, 0xe6,
                                                            0x01, 0x96, 0x01, 0x78, 0x96, 0x01, 0x00, 0x00, 0x00, 0xde, 0x02, 0xde, 0x02, 0x00, 0xfa,
                                                            0x01, 0x00, 0x00, 0x00, 0xe8, 0x07, 0x00, 0x5a, 0x00, 0x00, 0x04, 0x00, 0x0c, 0x4f, 0x00,
                                                            0x00, 0x00, 0x00, 0x00, 0x00, 0xde, 0x02, 0xde, 0x02, 0x00, 0xfa, 0x01, 0x00, 0x00, 0x00});

              buffer = memStream.GetBuffer();
            }

            Memory<byte> memoryDump = buffer;
            
            int offset = 0;
            Memory<byte> temp = new byte[18];
            Span<byte> Message = temp.Span;

            //Get our timestamp opcode in queue
            Message.Write((ushort)GameOpcode.Time, ref offset);
            Message.Write(DNP3Creation.CreateDNP3TimeStamp(), ref offset);

            SessionQueueMessages.PackMessage(MySession, temp, MessageOpcodeTypes.ShortReliableMessage);

            SessionQueueMessages.PackMessage(MySession, memoryDump, MessageOpcodeTypes.ShortReliableMessage);
            
            //Add player to world player list queue
            //IgnoreList(queueMessages, MySession);
            ActorSpeed(MySession);
        }

        public static void ProcessDelChar(Session MySession, PacketMessage ClientPacket)
        {
            int offset = 0;
            CharacterSQL deletedCharacter = new CharacterSQL();
            ReadOnlySpan<byte> temp = ClientPacket.Data.Span;
            //Passes in packet with ServerID on it, will grab, transform and return ServerID while also removing packet bytes
            int clientServID = temp.Get7BitDoubleEncodedInt( ref offset);

            //Call SQL delete method to actually process the delete.
            deletedCharacter.DeleteCharacter(clientServID, MySession);

            //Close SQL connection
            deletedCharacter.CloseConnection();
        }

        //Method to create new character when new character opcode is received
        public static void ProcessCreateChar(Session MySession, PacketMessage ClientPacket)
        {
            int offset = 0;
            CharacterSQL createCharacter = new CharacterSQL();
            ReadOnlySpan<byte> temp = ClientPacket.Data.Span;

            //Get length of characters name expected in packet
            int nameLength = temp.GetLEInt(ref offset);

            //Get Character Name
            string CharName = temp.GetString(ref offset, nameLength);

            //Before processing a full character creation check if the characters name already exists in the DB.
            //Later this will need to include a character/world combination if additional servers are spun up.
            if (CharName == createCharacter.CheckName(CharName))
            {
                Memory<byte> temp2 = new byte[2];
                Span<byte> Message = temp2.Span;
                Message.Write((ushort)GameOpcode.NameTaken, ref offset);

                //Close SQL connection
                createCharacter.CloseConnection();

                //Log character name taken and send out RDP message to pop up that name is taken.
                //Console.WriteLine("Character Name Already Taken");                //Send Message
                SessionQueueMessages.PackMessage(MySession, temp2, MessageOpcodeTypes.ShortReliableMessage);

            }

            //If name not found continue to actually create character
            else
            {
                //Create NewCharacter object
                Character charCreation = new Character();

                charCreation.CharName = CharName;
                //Get starting level
                charCreation.Level = temp.Get7BitDoubleEncodedInt(ref offset);

                //Divide startLevel by 2 because client doubles it
                //Get single byte attributes
                charCreation.Race = temp.Get7BitDoubleEncodedInt(ref offset);
                charCreation.StartingClass = temp.Get7BitDoubleEncodedInt(ref offset);
                charCreation.Gender = temp.Get7BitDoubleEncodedInt(ref offset);
                charCreation.HairColor = temp.Get7BitDoubleEncodedInt(ref offset);
                charCreation.HairLength = temp.Get7BitDoubleEncodedInt(ref offset);
                charCreation.HairStyle = temp.Get7BitDoubleEncodedInt(ref offset);
                charCreation.FaceOption = temp.Get7BitDoubleEncodedInt(ref offset);
                charCreation.HumTypeNum = temp.Get7BitDoubleEncodedInt(ref offset);

                //Get player attributes from packet and remove bytes after reading into variable
                charCreation.AddStrength = temp.GetLEInt(ref offset);
                charCreation.AddStamina = temp.GetLEInt(ref offset);
                charCreation.AddAgility = temp.GetLEInt(ref offset);
                charCreation.AddDexterity = temp.GetLEInt(ref offset);
                charCreation.AddWisdom = temp.GetLEInt(ref offset);
                charCreation.AddIntelligence = temp.GetLEInt(ref offset);
                charCreation.AddCharisma = temp.GetLEInt(ref offset);

                //Call SQL method for character creation
                createCharacter.CreateCharacter(MySession, charCreation);

                //CLose SQL connection
                createCharacter.CloseConnection();
            }
        }

        ///Game Disc Version
        public static void ProcessGameDisc(Session MySession, PacketMessage ClientPacket)
        {
            int offset = 0;
            ReadOnlySpan<byte> temp = ClientPacket.Data.Span;

            ///Gets Gameversion sent by client
            int GameVersion = temp.GetLEInt(ref offset);

            switch (GameVersion)
            {
                ///Game Disc Version
                case GameVersions.EQOA_FRONTIERS:
                    Logger.Info("EQOA Frontiers Selected.");
                    break;

                case GameVersions.EQOA_VANILLA:
                    Logger.Info("EQOA Vanilla Disc, no support");
                    break;

                case GameVersions.UNKNOWN:
                    Logger.Err("UNKNOWN Game Disc");
                    break;

                default:
                    Logger.Err("Unable to identify Game Disc");
                    break;
            }
            offset = 0;
            Memory<byte> temp2 = new byte[6];
            Span<byte> Message = temp2.Span;
            ///Need to send this back to client
            Message.Write(BitConverter.GetBytes((ushort)GameOpcode.DiscVersion), ref offset);
            Message.Write(BitConverter.GetBytes((uint)GameVersion), ref offset);

            ///Handles packing message into outgoing packet
            SessionQueueMessages.PackMessage(MySession, temp2, MessageOpcodeTypes.ShortReliableMessage);
        }

        ///Authentication check
        public static void ProcessAuthenticate(Session MySession, PacketMessage ClientPacket)
        {
            int offset = 0;
            ReadOnlySpan<byte> temp = ClientPacket.Data.Span;

            Logger.Info("Processing Authentication");
            ///Opcode option? just skip for now
            offset += 1;

            ///Unknown also, supposedly can be 03 00 00 00 or  01 00 00 00
            offset += 4;

            ///Game Code Length
            int GameCodeLength = temp.GetLEInt(ref offset);

            ///the actual gamecode
            string GameCode = temp.GetString(ref offset, GameCodeLength);

            if (GameCode == "EQOA")
            {
                ///Authenticate
                Logger.Info("Received EQOA Game Code, continuing...");

                ///Account name Length
                int AccountNameLength = temp.GetLEInt(ref offset);

                ///the actual gamecode
                string AccountName = temp.GetString(ref offset, AccountNameLength);

                Logger.Info($"Received Account Name: {AccountName}");

                ///Username ends with 01, no known use, skip for now
                offset += 1;

                //Decrypting password information goes here?

                string Password = "password";

                ///skip encrypted password for now
                offset += 32;

                ///Uncomment once ready
                MySession.AccountID = AccountActions.VerifyPassword(AccountName, Password);

                ///Theoretically we want to verify account # is not 0 here, if it is, drop it.
                if (MySession.AccountID == 0)
                {
                    ///This work?
                    ///Just ignore the packet and let client resend. 
                    ///Something noteable went wrong here most likely
                    return;
                }
            }

            else
            {
                ///If not EQOA.... drop?
                Logger.Err("Did not receive EQOA Game Code, not continuing...");
                ///Should we attempt to disconnect the session here?
            }
        }

        public static void ProcessPingRequest(Session MySession, PacketMessage message)
        {
            if (message.Data.Span[0] == 0x12)
            {
                Logger.Info("Processed Ping Request");
                //int offset1 = 0;
                //Memory<byte> Message = new byte[1];

                //Message.Write(new byte[] { 0x14 }, ref offset1);
                ///Do stuff here?
                ///Handles packing message into outgoing packet
                //SessionQueueMessages.PackMessage(MySession, Message, MessageOpcodeTypes.ShortReliableMessage);
            }
        }

        public static void CreateCharacterList(List<Character> MyCharacterList, Session MySession)
        {
            //gather expected buffer size... start with 3, opcode and character count should always be 3
            int bufferSize = 3;
            for( int i = 0; i < MyCharacterList.Count; i++)
            {
                //Everycharacter has this as a "standard" amount of bytes
                bufferSize += 82;
                bufferSize += MyCharacterList[i].CharName.Length;
                bufferSize += Utility_Funcs.VariableLengthIntegerLength(MyCharacterList[i].ServerID * 2);
                bufferSize += Utility_Funcs.VariableLengthIntegerLength(MyCharacterList[i].ModelID * 2);
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
                Message.Write7BitDoubleEncodedInt(character.TClass, ref offset);

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

                ///Start processing MyItem
                foreach (Item MyItem in character.InventoryItems)
                {
                    ///Use a switch to sift through MyItem and add them properly
                    switch (MyItem.EquipLocation)
                    {
                        ///Helm
                        case 1:
                            character.Helm = (byte)MyItem.Model;
                            character.HelmColor = MyItem.Color;
                            break;

                        ///Robe
                        case 2:
                            character.Robe = (byte)MyItem.Model;
                            character.RobeColor = MyItem.Color;
                            break;

                        ///Gloves
                        case 19:
                            character.Gloves = (byte)MyItem.Model;
                            character.GlovesColor = MyItem.Color;
                            break;

                        ///Chest
                        case 5:
                            character.Chest = (byte)MyItem.Model;
                            character.ChestColor = MyItem.Color;
                            break;

                        ///Bracers
                        case 8:
                            character.Bracer = (byte)MyItem.Model;
                            character.BracerColor = MyItem.Color;
                            break;

                        ///Legs
                        case 10:
                            character.Legs = (byte)MyItem.Model;
                            character.LegsColor = MyItem.Color;
                            break;

                        ///Feet
                        case 11:
                            character.Boots = (byte)MyItem.Model;
                            character.BootsColor = MyItem.Color;
                            break;

                        ///Primary
                        case 12:
                            character.Primary = MyItem.Model;
                            break;

                        ///Secondary
                        case 14:

                            ///If we have a secondary equipped already, puts next secondary into primary slot
                            if (character.Secondary > 0)
                            {
                                character.Primary = MyItem.Model;
                            }

                            ///If no secondary, add to secondary slot
                            else
                            {
                                character.Secondary = MyItem.Model;
                            }
                            break;

                        ///2 Hand
                        case 15:
                            character.Primary = MyItem.Model;
                            break;

                        ///Shield
                        case 13:
                            character.Shield = MyItem.Model;
                            break;

                        ///Bow
                        case 16:
                            character.Primary = MyItem.Model;
                            break;

                        ///Thrown
                        case 17:
                            character.Primary = MyItem.Model;
                            break;

                        ///Held
                        case 18:
                            ///If we have a secondary equipped already, puts next secondary into primary slot
                            if (character.Secondary > 0)
                            {
                                character.Primary = MyItem.Model;
                            }

                            ///If no secondary, add to secondary slot
                            else
                            {
                                character.Secondary = MyItem.Model;
                            }
                            break;

                        default:
                            Logger.Err("Equipment not in list, this may need to be changed");
                            break;
                    }
                }

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
                Message.Write(BitConverter.GetBytes(ByteSwaps.SwapBytes(character.GlovesColor)), ref offset);

                ///Leg color
                Message.Write(BitConverter.GetBytes(ByteSwaps.SwapBytes(character.LegsColor)), ref offset);

                ///Boot color
                Message.Write(BitConverter.GetBytes(ByteSwaps.SwapBytes(character.BootsColor)), ref offset);

                ///Helm color
                Message.Write(BitConverter.GetBytes(ByteSwaps.SwapBytes(character.HelmColor)), ref offset);

                ///Robe color
                Message.Write(BitConverter.GetBytes(ByteSwaps.SwapBytes(character.RobeColor)), ref offset);
            }

            ///Character list is complete
            ///Handles packing message into outgoing packet
            SessionQueueMessages.PackMessage(MySession, temp, MessageOpcodeTypes.ShortReliableMessage);
        }

        public static void IgnoreList(Session MySession)
        {
            int offset = 0;
            Memory<byte> temp = new byte[3];
            Span<byte> Message = temp.Span;
            Message.Write(BitConverter.GetBytes((ushort)GameOpcode.IgnoreList), ref offset);
            Message.Write(new byte[] { 0 }, ref offset);
            //For now send no ignored people
            SessionQueueMessages.PackMessage(MySession, temp, MessageOpcodeTypes.ShortReliableMessage);
        }

        public static void ActorSpeed(Session MySession)
        {
            int offset = 0;
            Memory<byte> temp = new byte[6];
            Span<byte> Message = temp.Span;

            Message.Write(BitConverter.GetBytes((ushort)GameOpcode.ActorSpeed), ref offset);
            Message.Write(BitConverter.GetBytes(25.0f), ref offset);
            //For now send a standard speed
            SessionQueueMessages.PackMessage(MySession, temp, MessageOpcodeTypes.ShortReliableMessage);
        }

        public static void ActorSpeed(Session MySession, float speed)
        {
            int offset = 0;
            Memory<byte> temp = new byte[3];
            Span<byte> Message = temp.Span;

            Message.Write(BitConverter.GetBytes((ushort)GameOpcode.ActorSpeed), ref offset);
            Message.Write(BitConverter.GetBytes(speed), ref offset);
            //For now send a standard speed
            SessionQueueMessages.PackMessage(MySession, temp, MessageOpcodeTypes.ShortReliableMessage);
        }
    }
}

