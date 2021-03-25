using System;
using System.Collections.Generic;
using System.Text;
using ReturnHome.PacketProcessing;
using ReturnHome.Utilities;
using ReturnHome.Actor;
using ReturnHome.SQL;
using ReturnHome.AccountAction;
using System.Threading.Tasks;

namespace ReturnHome.Opcodes
{
    public class ProcessOpcode
    {
        public static readonly Dictionary<GameOpcode, Action<SessionQueueMessages, SessionManager, Session, ReadOnlyMemory<byte>>> OpcodeDictionary = new ()
        {
            { GameOpcode.DiscVersion, ProcessGameDisc },
            { GameOpcode.Authenticate, ProcessAuthenticate },
            { GameOpcode.Authenticate2, ProcessAuthenticate },
            { GameOpcode.SELECTED_CHAR, ProcessCharacterChanges },
            { GameOpcode.DelCharacter, ProcessDelChar },
            { GameOpcode.CreateCharacter, ProcessCreateChar }
        };

        //Make new class for packing Messages into Session queue
        private readonly SessionQueueMessages _queueMessages;
        private readonly SessionManager _sessionManager;

        public ProcessOpcode(SessionQueueMessages queueMessages, SessionManager sessionManager)
        {
            _queueMessages = queueMessages;
            _sessionManager = sessionManager;
        }

        public void ProcessOpcodes(Session MySession, ushort MessageTypeOpcode, ReadOnlyMemory<byte> ClientPacket, ref int offset)
        {
            ushort MessageLength;
            ushort MessageNumber;
            ushort Opcode;

            MessageLength = BinaryPrimitiveWrapper.GetLEByte(ClientPacket, ref offset) == 0xFF ? BinaryPrimitiveWrapper.GetLEUShort(ClientPacket, ref offset) : ClientPacket.Span[offset - 1];

            ///Make sure Message number is expected, needs to be in order.
            MessageNumber = BinaryPrimitiveWrapper.GetLEUShort(ClientPacket, ref offset);

            //If client message order was not expected, increment offset and drop it
            if (!(MySession.clientMessageNumber + 1 == MessageNumber))
            {
                offset += (MessageLength - 2);
                return;
            }

            ///Increment for every message read, in order.
            MySession.IncrementClientMessageNumber();

            //grab our opcode
            Opcode = BinaryPrimitiveWrapper.GetLEUShort(ClientPacket, ref offset);

            Logger.Info($"Message Length: {MessageLength}; OpcodeType: {MessageTypeOpcode.ToString("X")}; Message Number: {MessageNumber.ToString("X")}; Opcode: {Opcode.ToString("X")}.");
            try
            {
                OpcodeDictionary[(GameOpcode)Opcode].Invoke(_queueMessages, _sessionManager, MySession, ClientPacket.Slice(offset, MessageLength - 2));
            }

            catch
            {
                ClientOpcodeUnknown(_queueMessages, _sessionManager, MySession, Opcode);
            }
            offset += MessageLength - 2;
        }


        public static void ClientOpcodeUnknown(SessionQueueMessages queueMessages, SessionManager sessionManager, Session MySession, ushort opcode)
        {
            string theMessage = $"Unknown Opcode: {opcode.ToString("X")}";
            queueMessages.messageCreator.MessageWriter(BitConverter.GetBytes((ushort)GameOpcode.ClientMessage));
            queueMessages.messageCreator.MessageWriter(BitConverter.GetBytes(theMessage.Length));
            queueMessages.messageCreator.MessageWriter(Encoding.Unicode.GetBytes(theMessage));

            //Send Message
            queueMessages.PackMessage(MySession, MessageOpcodeTypes.ShortReliableMessage);
        }

        public static void ProcessCharacterChanges(SessionQueueMessages queueMessages, SessionManager sessionManager, Session MySession, ReadOnlyMemory<byte> ClientPacket)
        {
            int offset = 0;

            //Retrieve CharacterID from client
            int ServerID = BinaryPrimitiveWrapper.GetLEInt(ClientPacket, ref offset);
            int FaceOption = BinaryPrimitiveWrapper.GetLEInt(ClientPacket, ref offset);
            int HairStyle = BinaryPrimitiveWrapper.GetLEInt(ClientPacket, ref offset);
            int HairLength = BinaryPrimitiveWrapper.GetLEInt(ClientPacket, ref offset);
            int HairColor = BinaryPrimitiveWrapper.GetLEInt(ClientPacket, ref offset);

            //Update these on our character
            Character thisChar = MySession.CharacterData.Find(i => Equals(i.ServerID, ServerID));

            //Got character, update changes
            thisChar.UpdateFeatures(MySession, HairColor, HairLength, HairStyle, FaceOption);

            sessionManager.CreateMemoryDumpSession(MySession);
        }

        public static async void ProcessMemoryDump(SessionQueueMessages queueMessages, Session MySession)
        {
            MySession.StartPipe(20000);

            //Toss opcode in
            MySession.WriteMessage(BitConverter.GetBytes((ushort)GameOpcode.MemoryDump));
            //Let's get remaining character data before preparing it for transport
            //Hotkeys
            SQLOperations.GetPlayerHotkeys(MySession);

            //Quests, skip for now
            //Weaponhotbars
            SQLOperations.GetPlayerWeaponHotbar(MySession);

            //Auctions go here, skip for now
            //Spells
            SQLOperations.GetPlayerSpells(MySession);

            MySession.WriteMessage(MySession.MyCharacter.PullCharacter());
            MySession.WriteMessage(new byte[] { (byte)(MySession.MyCharacter.MyHotkeys.Count * 2) });

            //cycle over all our hotkeys and append them
            for (int i = 0; i < MySession.MyCharacter.MyHotkeys.Count; i++)
            {
                MySession.WriteMessage(MySession.MyCharacter.MyHotkeys[i].PullHotkey());
            }
            //Unknown at this time 4 byte null
            MySession.WriteMessage(BitConverter.GetBytes(0));

            //Unknown at this time 4 byte null
            MySession.WriteMessage(BitConverter.GetBytes(0));

            //Quest Count
            MySession.WriteMessage(BitConverter.GetBytes(MySession.MyCharacter.MyQuests.Count));

            //Iterate over quest data and append (Should be 0 for now...)
            for (int i = 0; i < MySession.MyCharacter.MyQuests.Count; i++)
            {
                MySession.WriteMessage(MySession.MyCharacter.MyQuests[i].PullQuest());
            }

            //Get Inventory Item count
            MySession.WriteMessage(new byte[] { (byte)(MySession.MyCharacter.InventoryItems.Count * 2) });
            MySession.WriteMessage(BitConverter.GetBytes(MySession.MyCharacter.InventoryItems.Count));
            for (int i = 0; i < MySession.MyCharacter.InventoryItems.Count; i++)
            {
                MySession.WriteMessage(MySession.MyCharacter.InventoryItems[i].PullItem());
            }

            for (int i = 0; i < MySession.MyCharacter.WeaponHotbars.Count; i++)
            {
                MySession.WriteMessage(MySession.MyCharacter.WeaponHotbars[i].PullWeaponHotbar());
            }

            //Get Bank Item count
            MySession.WriteMessage(new byte[] { (byte)(MySession.MyCharacter.BankItems.Count * 2)});
            MySession.WriteMessage(BitConverter.GetBytes(MySession.MyCharacter.BankItems.Count));
            for (int i = 0; i < MySession.MyCharacter.BankItems.Count; i++)
            {
                MySession.WriteMessage(MySession.MyCharacter.BankItems[i].PullItem());
            }

            // end of bank? or could be something else for memory dump
            MySession.WriteMessage(new byte[] { 0x00 });

            //Buying auctions
            MySession.WriteMessage(new byte[] { (byte)(MySession.MyCharacter.MyBuyingAuctions.Count) });
            for (int i = 0; i < MySession.MyCharacter.MyBuyingAuctions.Count; i++)
            {
                MySession.WriteMessage(MySession.MyCharacter.MyBuyingAuctions[i].PullAuction());
            }

            //Selling auctions
            MySession.WriteMessage(new byte[] { (byte)(MySession.MyCharacter.MySellingAuctions.Count) });
            for (int i = 0; i < MySession.MyCharacter.MySellingAuctions.Count; i++)
            {
                MySession.WriteMessage(MySession.MyCharacter.MySellingAuctions[i].PullAuction());
            }

            //Spell count and Spells
            MySession.WriteMessage(Utility_Funcs.Technique(MySession.MyCharacter.MySpells.Count));
            for (int i = 0; i < MySession.MyCharacter.MySpells.Count; i++)
            {
                MySession.WriteMessage(MySession.MyCharacter.MySpells[i].PullSpell());
            }

            MySession.WriteMessage(new byte[] {0x55, 0x55, 0x0d, 0x41, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x01, 0x00, 0x00,
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

            //Get our timestamp opcode in queue
            queueMessages.messageCreator.MessageWriter(BitConverter.GetBytes((ushort)GameOpcode.Time));
            queueMessages.messageCreator.MessageWriter(DNP3Creation.CreateDNP3TimeStamp());
            queueMessages.PackMessage(MySession, MessageOpcodeTypes.ShortReliableMessage);

            //Get our Memory allocAtions
            ReadOnlyMemory<byte> enterGame = MySession.ReadMessage();
            int bytesRead = 0;
            while ((bytesRead + 4096) < enterGame.Length)
            {
                queueMessages.messageCreator.MessageWriter(enterGame[bytesRead..(bytesRead + 4096)]);
                ///Handles packing message into outgoing packet
                queueMessages.PackMessage(MySession, MessageOpcodeTypes.MultiShortReliableMessage);
                bytesRead += 4096;
            }

            queueMessages.messageCreator.MessageWriter(enterGame[bytesRead..enterGame.Length]);

            await Task.Delay(1000);
            ///Handles packing message into outgoing packet
            queueMessages.PackMessage(MySession, MessageOpcodeTypes.ShortReliableMessage);

            IgnoreList(queueMessages, MySession);
            ActorSpeed(queueMessages, MySession);
        }

        public static void ProcessDelChar(SessionQueueMessages queueMessages, SessionManager sessionManager, Session MySession, ReadOnlyMemory<byte> ClientPacket)
        {
            int offset = 0;

            //Passes in packet with ServerID on it, will grab, transform and return ServerID while also removing packet bytes
            int clientServID = Utility_Funcs.Untechnique(ClientPacket.Span, ref offset);

            //Call SQL delete method to actually process the delete.
            SQLOperations.DeleteCharacter(queueMessages, sessionManager, clientServID, MySession);
        }

        //Method to create new character when new character opcode is received
        public static void ProcessCreateChar(SessionQueueMessages queueMessages, SessionManager sessionManager, Session MySession, ReadOnlyMemory<byte> ClientPacket)
        {
            int offset = 0;

            //Get length of characters name expected in packet
            int nameLength = BinaryPrimitiveWrapper.GetLEInt(ClientPacket, ref offset);

            //Get Character Name
            string CharName = Utility_Funcs.GetMemoryString(ClientPacket.Span, ref offset, nameLength);

            //Before processing a full character creation check if the characters name already exists in the DB.
            //Later this will need to include a character/world combination if additional servers are spun up.
            if (CharName == SQLOperations.CheckName(CharName))
            {
                queueMessages.messageCreator.MessageWriter(BitConverter.GetBytes((ushort)GameOpcode.NameTaken));
                //Log character name taken and send out RDP message to pop up that name is taken.
                //Console.WriteLine("Character Name Already Taken");
                queueMessages.PackMessage(MySession, MessageOpcodeTypes.ShortReliableMessage);
            }

            //If name not found continue to actually create character
            else
            {
                //Create NewCharacter object
                Character charCreation = new Character();

                charCreation.CharName = CharName;
                //Get starting level
                charCreation.Level = Utility_Funcs.Untechnique(ClientPacket.Span, ref offset);

                //Divide startLevel by 2 because client doubles it
                //Get single byte attributes
                charCreation.Race = Utility_Funcs.Untechnique(ClientPacket.Span, ref offset);
                charCreation.StartingClass = Utility_Funcs.Untechnique(ClientPacket.Span, ref offset);
                charCreation.Gender = Utility_Funcs.Untechnique(ClientPacket.Span, ref offset);
                charCreation.HairColor = Utility_Funcs.Untechnique(ClientPacket.Span, ref offset);
                charCreation.HairLength = Utility_Funcs.Untechnique(ClientPacket.Span, ref offset);
                charCreation.HairStyle = Utility_Funcs.Untechnique(ClientPacket.Span, ref offset);
                charCreation.FaceOption = Utility_Funcs.Untechnique(ClientPacket.Span, ref offset);
                charCreation.HumTypeNum = Utility_Funcs.Untechnique(ClientPacket.Span, ref offset);

                //Get player attributes from packet and remove bytes after reading into variable
                charCreation.AddStrength = BinaryPrimitiveWrapper.GetLEInt(ClientPacket, ref offset);
                charCreation.AddStamina = BinaryPrimitiveWrapper.GetLEInt(ClientPacket, ref offset);
                charCreation.AddAgility = BinaryPrimitiveWrapper.GetLEInt(ClientPacket, ref offset);
                charCreation.AddDexterity = BinaryPrimitiveWrapper.GetLEInt(ClientPacket, ref offset);
                charCreation.AddWisdom = BinaryPrimitiveWrapper.GetLEInt(ClientPacket, ref offset);
                charCreation.AddIntelligence = BinaryPrimitiveWrapper.GetLEInt(ClientPacket, ref offset);
                charCreation.AddCharisma = BinaryPrimitiveWrapper.GetLEInt(ClientPacket, ref offset);

                //Call SQL method for character creation
                SQLOperations.CreateCharacter(queueMessages, sessionManager, MySession, charCreation);
            }
        }

        ///Game Disc Version
        public static void ProcessGameDisc(SessionQueueMessages queueMessages, SessionManager sessionManager, Session MySession, ReadOnlyMemory<byte> ClientPacket)
        {
            queueMessages.messageCreator.MessageWriter(BitConverter.GetBytes((ushort)GameOpcode.DiscVersion));

            int offset = 0;
            ///Gets Gameversion sent by client
            int GameVersion = BinaryPrimitiveWrapper.GetLEInt(ClientPacket, ref offset);

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
            ///Need to send this back to client
            queueMessages.messageCreator.MessageWriter(BitConverter.GetBytes((uint)GameVersion));

            ///Handles packing message into outgoing packet
            queueMessages.PackMessage(MySession, MessageOpcodeTypes.ShortReliableMessage);
        }

        ///Authentication check
        public static void ProcessAuthenticate(SessionQueueMessages queueMessages, SessionManager sessionManager, Session MySession, ReadOnlyMemory<byte> ClientPacket)
        {
            int offset = 0;
            Logger.Info("Processing Authentication");
            ///Opcode option? just skip for now
            offset += 1;

            ///Unknown also, supposedly can be 03 00 00 00 or  01 00 00 00
            offset += 4;

            ///Game Code Length
            int GameCodeLength = BinaryPrimitiveWrapper.GetLEInt(ClientPacket, ref offset);

            ///the actual gamecode
            string GameCode = Utility_Funcs.GetMemoryString(ClientPacket.Span, ref offset, GameCodeLength);

            if (GameCode == "EQOA")
            {
                ///Authenticate
                Logger.Info("Received EQOA Game Code, continuing...");

                ///Account name Length
                int AccountNameLength = BinaryPrimitiveWrapper.GetLEInt(ClientPacket, ref offset);

                ///the actual gamecode
                string AccountName = Utility_Funcs.GetMemoryString(ClientPacket.Span, ref offset, AccountNameLength);

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

        public void ProcessPingRequest(SessionQueueMessages queueMessages, Session MySession, ReadOnlyMemory<byte> ClientPacket, ref int offset)
        {
            //Verify the ping only has 1 byte of data
            if (BinaryPrimitiveWrapper.GetLEByte(ClientPacket, ref offset) == 1)
            {
                //Verify the message is in order
                if (BinaryPrimitiveWrapper.GetLEUShort(ClientPacket, ref offset) == MySession.clientMessageNumber + 1)
                {
                    ///Increment for every message read, in order.
                    MySession.IncrementClientMessageNumber();

                    if (ClientPacket.Span[offset] == 0x14)
                    {
                        queueMessages.messageCreator.MessageWriter(new byte[] { 0x14 });
                        ///Do stuff here?
                        ///Handles packing message into outgoing packet
                        _queueMessages.PackMessage(MySession, MessageOpcodeTypes.UnknownMessage);
                    }
                }
            }

            offset += 1;
        }

        public static void CreateCharacterList(SessionQueueMessages queueMessages, SessionManager sessionManager, List<Character> MyCharacterList, Session MySession)
        {
            //Holds list of characters pulled from the DB for the AccountID
            queueMessages.messageCreator.MessageWriter(BitConverter.GetBytes((ushort)GameOpcode.CharacterSelect));

            ///Gets our character count and uses technique to double it
            queueMessages.messageCreator.MessageWriter(Utility_Funcs.Technique((byte)MyCharacterList.Count));

            //Iterates through each charcter in the list and converts attribute values to packet values
            foreach (Character character in MyCharacterList)
            {
                ///Add the Character name length
                queueMessages.messageCreator.MessageWriter(BitConverter.GetBytes((uint)character.CharName.Length));

                ///Add character name
                queueMessages.messageCreator.MessageWriter(Encoding.ASCII.GetBytes(character.CharName));

                ///Add Server ID
                queueMessages.messageCreator.MessageWriter(Utility_Funcs.Technique(character.ServerID));

                ///Add Model
                queueMessages.messageCreator.MessageWriter(Utility_Funcs.Technique(character.ModelID));

                ///Add Class
                queueMessages.messageCreator.MessageWriter(Utility_Funcs.Technique(character.TClass));

                ///Add Race
                queueMessages.messageCreator.MessageWriter(Utility_Funcs.Technique(character.Race));

                ///Add Level
                queueMessages.messageCreator.MessageWriter(Utility_Funcs.Technique(character.Level));

                ///Add Hair color
                queueMessages.messageCreator.MessageWriter(Utility_Funcs.Technique(character.HairColor));

                ///Add Hair Length
                queueMessages.messageCreator.MessageWriter(Utility_Funcs.Technique(character.HairLength));

                ///Add Hair Style
                queueMessages.messageCreator.MessageWriter(Utility_Funcs.Technique(character.HairStyle));

                ///Add Face option
                queueMessages.messageCreator.MessageWriter(Utility_Funcs.Technique(character.FaceOption));

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
                queueMessages.messageCreator.MessageWriter(BitConverter.GetBytes(character.Robe));

                ///Add Primary
                queueMessages.messageCreator.MessageWriter(BitConverter.GetBytes(character.Primary));

                ///Add Secondary
                queueMessages.messageCreator.MessageWriter(BitConverter.GetBytes(character.Secondary));

                ///Add Shield
                queueMessages.messageCreator.MessageWriter(BitConverter.GetBytes(character.Shield));

                ///Add Character animation here, dumby for now
                queueMessages.messageCreator.MessageWriter(BitConverter.GetBytes((ushort)0x0004));

                ///unknown value?
                queueMessages.messageCreator.MessageWriter(new byte[] { 0x00 });

                ///Chest Model
                queueMessages.messageCreator.MessageWriter(new byte[] { character.Chest });

                ///uBracer Model
                queueMessages.messageCreator.MessageWriter(new byte[] { character.Bracer });

                ///Glove Model
                queueMessages.messageCreator.MessageWriter(new byte[] { character.Gloves });

                ///Leg Model
                queueMessages.messageCreator.MessageWriter(new byte[] { character.Legs });

                ///Boot Model
                queueMessages.messageCreator.MessageWriter(new byte[] { character.Boots });

                ///Helm Model
                queueMessages.messageCreator.MessageWriter(new byte[] { character.Helm });

                ///unknown value?
                queueMessages.messageCreator.MessageWriter(BitConverter.GetBytes((uint)0));

                ///unknown value?
                queueMessages.messageCreator.MessageWriter(BitConverter.GetBytes((ushort)0));

                ///unknown value?
                queueMessages.messageCreator.MessageWriter(BitConverter.GetBytes(0xFFFFFFFF));

                ///unknown value?
                queueMessages.messageCreator.MessageWriter(BitConverter.GetBytes(0xFFFFFFFF));

                ///unknown value?
                queueMessages.messageCreator.MessageWriter(BitConverter.GetBytes(0xFFFFFFFF));

                ///Chest color
                queueMessages.messageCreator.MessageWriter(BitConverter.GetBytes(ByteSwaps.SwapBytes(character.ChestColor)));

                ///Bracer color
                queueMessages.messageCreator.MessageWriter(BitConverter.GetBytes(ByteSwaps.SwapBytes(character.BracerColor)));

                ///Glove color
                queueMessages.messageCreator.MessageWriter(BitConverter.GetBytes(ByteSwaps.SwapBytes(character.GlovesColor)));

                ///Leg color
                queueMessages.messageCreator.MessageWriter(BitConverter.GetBytes(ByteSwaps.SwapBytes(character.LegsColor)));

                ///Boot color
                queueMessages.messageCreator.MessageWriter(BitConverter.GetBytes(ByteSwaps.SwapBytes(character.BootsColor)));

                ///Helm color
                queueMessages.messageCreator.MessageWriter(BitConverter.GetBytes(ByteSwaps.SwapBytes(character.HelmColor)));

                ///Robe color
                queueMessages.messageCreator.MessageWriter(BitConverter.GetBytes(ByteSwaps.SwapBytes(character.RobeColor)));

                Logger.Info($"Processed {character.CharName}");
            }

            ///Character list is complete
            ///Handles packing message into outgoing packet
            queueMessages.PackMessage(MySession, MessageOpcodeTypes.ShortReliableMessage);
        }

        public static void IgnoreList(SessionQueueMessages queueMessages, Session MySession)
        {
            queueMessages.messageCreator.MessageWriter(BitConverter.GetBytes((ushort)GameOpcode.IgnoreList));
            queueMessages.messageCreator.MessageWriter(new byte[] { 0 });
            //For now send no ignored people
            queueMessages.PackMessage(MySession, MessageOpcodeTypes.ShortReliableMessage);
        }

        public static void ActorSpeed(SessionQueueMessages queueMessages, Session MySession)
        {
            queueMessages.messageCreator.MessageWriter(BitConverter.GetBytes((ushort)GameOpcode.ActorSpeed));
            queueMessages.messageCreator.MessageWriter(BitConverter.GetBytes(25.0f));
            //For now send a standard speed
            queueMessages.PackMessage(MySession, MessageOpcodeTypes.ShortReliableMessage);
        }
    }
}

