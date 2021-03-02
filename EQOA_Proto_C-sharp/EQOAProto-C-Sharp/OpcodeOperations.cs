using EQLogger;
using EQOAProto;
using Opcodes;
using RdpComm;
using SessManager;
using GameAccountActions;
using System;
using System.Collections.Generic;
using System.Text;
using Utility;
using EQOASQL;
using DNP3;
using Characters;
using Hotkeys;
using Quests;
using Items;
using WeaponHotbars;
using Auctions;
using Spells;
using Sessions;

namespace OpcodeOperations
{
    public class ProcessOpcode
    {
        public static readonly Dictionary<GameOpcode, Action<SessionManager, Session, ReadOnlyMemory<byte>>> OpcodeDictionary = new ()
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

        public ProcessOpcode(SessionManager sessionManager)//, SessionQueueMessages queueMessages)
        {
            _sessionManager = sessionManager;
            //_queueMessages = queueMessages;
        }

        public void ProcessOpcodes(Session MySession, ushort MessageTypeOpcode, ReadOnlyMemory<byte> ClientPacket, ref int offset)
        {
            ///Expected message length
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

            //Logger.Info($"Message Length: {MessageLength}; OpcodeType: {MessageTypeOpcode.ToString("X")}; Message Number: {MessageNumber.ToString("X")}; Opcode: {Opcode.ToString("X")}.");
            MySession.RdpReport = true;
            OpcodeDictionary[(GameOpcode)Opcode].Invoke(_sessionManager, MySession, ClientPacket.Slice(offset, MessageLength - 2));
            offset += MessageLength - 2;
        }


        public void ClientOpcodeUnknown(SessionManager sessionManager, Session MySession, GameOpcode opcode)
        {
            string theMessage = $"Unknown Opcode: {opcode.ToString("X")}";
            MySession.messageCreator.MessageWriter(BitConverter.GetBytes((ushort)GameOpcode.ClientMessage));
            MySession.messageCreator.MessageWriter(BitConverter.GetBytes(theMessage.Length));
            MySession.messageCreator.MessageWriter(Encoding.Unicode.GetBytes(theMessage));

            //Send Message
            _queueMessages.PackMessage(MySession, MessageOpcodeTypes.ShortReliableMessage);
        }

        public static void ProcessCharacterChanges(SessionManager sessionManager, Session MySession, ReadOnlyMemory<byte> ClientPacket)
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

        }

        /* Skipping this for now
        public static void ProcessMemoryDump(Session MySession)
        {
            //Get our timestamp opcode in queue
            MySession.messageCreator.MessageWriter(BitConverter.GetBytes((ushort)GameOpcode.Time));
            MySession.messageCreator.MessageWriter(DNP3Creation.CreateDNP3TimeStamp());
            _queueMessages.PackMessage(MySession, MessageOpcodeTypes.ShortReliableMessage);

            //Toss opcode in
            MySession.messageCreator.MessageWriter(BitConverter.GetBytes((ushort)GameOpcode.MemoryDump));
            //Let's get remaining character data before preparing it for transport
            //Hotkeys
            SQLOperations.GetPlayerHotkeys(MySession);

            //Quests, skip for now
            //Weaponhotbars
            SQLOperations.GetPlayerWeaponHotbar(MySession);

            //Auctions go here, skip for now
            //Spells
            SQLOperations.GetPlayerSpells(MySession);

            MySession.messageCreator.MessageWriter(MySession.MyCharacter.PullCharacter());
            MySession.messageCreator.MessageWriter(BitConverter.GetBytes((byte)(MySession.MyCharacter.MyHotkeys.Count * 2)));
               
            //cycle over all our hotkeys and append them
            foreach(Hotkey MyHotkey in MySession.MyCharacter.MyHotkeys)
            {
                MySession.messageCreator.MessageWriter(MyHotkey.PullHotkey());
            }
            //Unknown at this time 4 byte null
            MySession.messageCreator.MessageWriter(BitConverter.GetBytes(0));

            //Unknown at this time 4 byte null
            MySession.messageCreator.MessageWriter(BitConverter.GetBytes(0));

            //Quest Count
            MySession.messageCreator.MessageWriter(BitConverter.GetBytes(MySession.MyCharacter.MyQuests.Count));
                
            //Iterate over quest data and append (Should be 0 for now...)
            foreach(Quest MyQuest in MySession.MyCharacter.MyQuests)
            {
                MySession.messageCreator.MessageWriter(MyQuest.PullQuest());
            }

            //Get Inventory Item count
            MySession.messageCreator.MessageWriter(BitConverter.GetBytes((byte)(MySession.MyCharacter.InventoryItems.Count * 2)));
            MySession.messageCreator.MessageWriter(BitConverter.GetBytes(MySession.MyCharacter.InventoryItems.Count));
            foreach( Item MyItem in MySession.MyCharacter.InventoryItems)
            {
                MySession.messageCreator.MessageWriter(MyItem.PullItem());
            }

            foreach(WeaponHotbar MyWeaponHotbar in MySession.MyCharacter.WeaponHotbars)
            {
                MySession.messageCreator.MessageWriter(MyWeaponHotbar.PullWeaponHotbar());
            }

            //Get Bank Item count
            MySession.messageCreator.MessageWriter(BitConverter.GetBytes((byte)(MySession.MyCharacter.BankItems.Count * 2)));
            MySession.messageCreator.MessageWriter(BitConverter.GetBytes(MySession.MyCharacter.BankItems.Count));
            foreach (Item MyItem in MySession.MyCharacter.BankItems)
            {
                MySession.messageCreator.MessageWriter(MyItem.PullItem());
            }

            // end of bank? or could be something else for memory dump
            MySession.messageCreator.MessageWriter(BitConverter.GetBytes((byte)0));

            //Buying auctions
            MySession.messageCreator.MessageWriter(BitConverter.GetBytes((byte)(MySession.MyCharacter.MyBuyingAuctions.Count)));
            foreach (Auction MyAuction in MySession.MyCharacter.MyBuyingAuctions)
            {
                MySession.messageCreator.MessageWriter(MyAuction.PullAuction());
            }

            //Selling auctions
            MySession.messageCreator.MessageWriter(BitConverter.GetBytes((byte)(MySession.MyCharacter.MySellingAuctions.Count)));
            foreach (Auction MyAuction in MySession.MyCharacter.MySellingAuctions)
            {
                MySession.messageCreator.MessageWriter(MyAuction.PullAuction());
            }

            //Spell count and Spells
            MySession.messageCreator.MessageWriter(Utility_Funcs.Technique(MySession.MyCharacter.MySpells.Count));
            foreach(Spell MySpell in MySession.MyCharacter.MySpells)
            {
                MySession.messageCreator.MessageWriter(MySpell.PullSpell());
            }

            MySession.messageCreator.MessageWriter(new byte[] {0x55, 0x55, 0x0d, 0x41, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x01, 0x00, 0x00,
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

            MySession.Dumpstarted = true;

            //Set this to true to send packet to client
            MySession.ClientFirstConnect = true;

            ///Handles packing message into outgoing packet
            _queueMessages.PackMessage(MySession, MessageOpcodeTypes.MultiShortReliableMessage);
        }*/

        public static void ProcessDelChar(SessionManager sessionManager, Session MySession, ReadOnlyMemory<byte> ClientPacket)
        {
            int offset = 0;

            //Passes in packet with ServerID on it, will grab, transform and return ServerID while also removing packet bytes
            int clientServID = Utility_Funcs.Untechnique(ClientPacket.Span, ref offset);
            //Call SQL delete method to actually process the delete.
            SQLOperations.DeleteCharacter(sessionManager, clientServID, MySession);
        }

        //Method to create new character when new character opcode is received
        public static void ProcessCreateChar(SessionManager sessionManager, Session MySession, ReadOnlyMemory<byte> ClientPacket)
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
                MySession.messageCreator.MessageWriter(BitConverter.GetBytes((ushort)GameOpcode.NameTaken));
                //Log character name taken and send out RDP message to pop up that name is taken.
                //Console.WriteLine("Character Name Already Taken");
                sessionManager._queueMessages.PackMessage(MySession, MessageOpcodeTypes.ShortReliableMessage);
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
                SQLOperations.CreateCharacter(sessionManager, MySession, charCreation);
            }
        }

        ///Game Disc Version
        public static void ProcessGameDisc(SessionManager sessionManager, Session MySession, ReadOnlyMemory<byte> ClientPacket)
        {
            MySession.messageCreator.MessageWriter(BitConverter.GetBytes((ushort)GameOpcode.DiscVersion));

            int offset = 0;
            ///Gets Gameversion sent by client
            int GameVersion = BinaryPrimitiveWrapper.GetLEInt(ClientPacket, ref offset);

            switch (GameVersion)
            {
                ///Game Disc Version
                case GameVersions.EQOA_FRONTIERS:
                    //Logger.Info("EQOA Frontiers Selected.");
                    break;

                case GameVersions.EQOA_VANILLA:
                    //Logger.Info("EQOA Vanilla Disc, no support");
                    break;

                case GameVersions.UNKNOWN:
                    //Logger.Err("UNKNOWN Game Disc");
                    break;

                default:
                    //Logger.Err("Unable to identify Game Disc");
                    break;
            }
            ///Need to send this back to client
            MySession.messageCreator.MessageWriter(BitConverter.GetBytes((uint)GameVersion));

            ///Handles packing message into outgoing packet
            sessionManager._queueMessages.PackMessage(MySession, MessageOpcodeTypes.ShortReliableMessage);
        }

        ///Authentication check
        public static void ProcessAuthenticate(SessionManager sessionManager, Session MySession, ReadOnlyMemory<byte> ClientPacket)
        {
            int offset = 0;
            //Logger.Info("Processing Authentication");
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
                //Logger.Info("Received EQOA Game Code, continuing...");

                ///Account name Length
                int AccountNameLength = BinaryPrimitiveWrapper.GetLEInt(ClientPacket, ref offset);

                ///the actual gamecode
                string AccountName = Utility_Funcs.GetMemoryString(ClientPacket.Span, ref offset, AccountNameLength);

                //Logger.Info($"Received Account Name: {AccountName}");

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
                //Logger.Err("Did not receive EQOA Game Code, not continuing...");
                ///Should we attempt to disconnect the session here?
            }
        }

        public void ProcessPingRequest(Session MySession, ReadOnlyMemory<byte> ClientPacket, ref int offset)
        {
            //Verify the ping only has 1 byte of data
            if (BinaryPrimitiveWrapper.GetLEByte(ClientPacket, ref offset) == 1)
            {
                //Verify the message is in order
                if (BinaryPrimitiveWrapper.GetLEUShort(ClientPacket, ref offset) == MySession.clientMessageNumber + 1)
                {
                    ///Increment for every message read, in order.
                    MySession.IncrementClientMessageNumber();

                    if (MySession.InGame == false && (ClientPacket.Span[offset] == 0x12))
                    {
                        //Nothing needed here I suppose?
                    }

                    else if (MySession.InGame == true && (ClientPacket.Span[offset] == 0x14))
                    {
                        MySession.messageCreator.MessageWriter(new byte[] { 0x14 });
                        ///Do stuff here?
                        ///Handles packing message into outgoing packet
                        _queueMessages.PackMessage(MySession, MessageOpcodeTypes.UnknownMessage);
                    }

                    else
                    {
                        //Logger.Err($"Received an F9 with unknown value {ClientPacket.Span[offset]}");
                    }
                }
            }

            offset += 1;
        }

        public static void CreateCharacterList(SessionManager sessionManager, List<Character> MyCharacterList, Session MySession)
        {
            //Holds list of characters pulled from the DB for the AccountID
            MySession.messageCreator.MessageWriter(BitConverter.GetBytes((ushort)GameOpcode.CharacterSelect));

            ///Gets our character count and uses technique to double it
            MySession.messageCreator.MessageWriter(Utility_Funcs.Technique((byte)MyCharacterList.Count));

            //Iterates through each charcter in the list and converts attribute values to packet values
            foreach (Character character in MyCharacterList)
            {
                ///Add the Character name length
                MySession.messageCreator.MessageWriter(BitConverter.GetBytes((uint)character.CharName.Length));

                ///Add character name
                MySession.messageCreator.MessageWriter(Encoding.ASCII.GetBytes(character.CharName));

                ///Add Server ID
                MySession.messageCreator.MessageWriter(Utility_Funcs.Technique(character.ServerID));

                ///Add Model
                MySession.messageCreator.MessageWriter(Utility_Funcs.Technique(character.ModelID));

                ///Add Class
                MySession.messageCreator.MessageWriter(Utility_Funcs.Technique(character.TClass));

                ///Add Race
                MySession.messageCreator.MessageWriter(Utility_Funcs.Technique(character.Race));

                ///Add Level
                MySession.messageCreator.MessageWriter(Utility_Funcs.Technique(character.Level));

                ///Add Hair color
                MySession.messageCreator.MessageWriter(Utility_Funcs.Technique(character.HairColor));

                ///Add Hair Length
                MySession.messageCreator.MessageWriter(Utility_Funcs.Technique(character.HairLength));

                ///Add Hair Style
                MySession.messageCreator.MessageWriter(Utility_Funcs.Technique(character.HairStyle));

                ///Add Face option
                MySession.messageCreator.MessageWriter(Utility_Funcs.Technique(character.FaceOption));

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
                            //Logger.Err("Equipment not in list, this may need to be changed");
                            break;
                    }
                }

                ///Add Robe
                MySession.messageCreator.MessageWriter(BitConverter.GetBytes(character.Robe));

                ///Add Primary
                MySession.messageCreator.MessageWriter(BitConverter.GetBytes(character.Primary));

                ///Add Secondary
                MySession.messageCreator.MessageWriter(BitConverter.GetBytes(character.Secondary));

                ///Add Shield
                MySession.messageCreator.MessageWriter(BitConverter.GetBytes(character.Shield));

                ///Add Character animation here, dumby for now
                MySession.messageCreator.MessageWriter(BitConverter.GetBytes((ushort)0x0004));

                ///unknown value?
                MySession.messageCreator.MessageWriter(new byte[] { 0x00 });

                ///Chest Model
                MySession.messageCreator.MessageWriter(new byte[] { character.Chest });

                ///uBracer Model
                MySession.messageCreator.MessageWriter(new byte[] { character.Bracer });

                ///Glove Model
                MySession.messageCreator.MessageWriter(new byte[] { character.Gloves });

                ///Leg Model
                MySession.messageCreator.MessageWriter(new byte[] { character.Legs });

                ///Boot Model
                MySession.messageCreator.MessageWriter(new byte[] { character.Boots });

                ///Helm Model
                MySession.messageCreator.MessageWriter(new byte[] { character.Helm });

                ///unknown value?
                MySession.messageCreator.MessageWriter(BitConverter.GetBytes((uint)0));

                ///unknown value?
                MySession.messageCreator.MessageWriter(BitConverter.GetBytes((ushort)0));

                ///unknown value?
                MySession.messageCreator.MessageWriter(BitConverter.GetBytes(0xFFFFFFFF));

                ///unknown value?
                MySession.messageCreator.MessageWriter(BitConverter.GetBytes(0xFFFFFFFF));

                ///unknown value?
                MySession.messageCreator.MessageWriter(BitConverter.GetBytes(0xFFFFFFFF));

                ///Chest color
                MySession.messageCreator.MessageWriter(BitConverter.GetBytes(ByteSwaps.SwapBytes(character.ChestColor)));
                ///Bracer color
                MySession.messageCreator.MessageWriter(BitConverter.GetBytes(ByteSwaps.SwapBytes(character.BracerColor)));

                ///Glove color
                MySession.messageCreator.MessageWriter(BitConverter.GetBytes(ByteSwaps.SwapBytes(character.GlovesColor)));

                ///Leg color
                MySession.messageCreator.MessageWriter(BitConverter.GetBytes(ByteSwaps.SwapBytes(character.LegsColor)));

                ///Boot color
                MySession.messageCreator.MessageWriter(BitConverter.GetBytes(ByteSwaps.SwapBytes(character.BootsColor)));

                ///Helm color
                MySession.messageCreator.MessageWriter(BitConverter.GetBytes(ByteSwaps.SwapBytes(character.HelmColor)));

                ///Robe color
                MySession.messageCreator.MessageWriter(BitConverter.GetBytes(ByteSwaps.SwapBytes(character.RobeColor)));

                //Logger.Info($"Processed {character.CharName}");
            }

            ///Character list is complete
            ///Handles packing message into outgoing packet
            sessionManager._queueMessages.PackMessage(MySession, MessageOpcodeTypes.ShortReliableMessage);
        }

        public static void IgnoreList(SessionManager sessionManager, Session MySession)
        {
            MySession.messageCreator.MessageWriter(BitConverter.GetBytes((ushort)GameOpcode.IgnoreList));
            MySession.messageCreator.MessageWriter(new byte[] { 0 });
            //For now send no ignored people
            sessionManager._queueMessages.PackMessage(MySession, MessageOpcodeTypes.ShortReliableMessage);
        }

        public static void ActorSpeed(SessionManager sessionManager, Session MySession)
        {
            MySession.messageCreator.MessageWriter(BitConverter.GetBytes((ushort)GameOpcode.ActorSpeed));
            MySession.messageCreator.MessageWriter(BitConverter.GetBytes(25.0f));
            //For now send a standard speed
            sessionManager._queueMessages.PackMessage(MySession, MessageOpcodeTypes.ShortReliableMessage);
        }
    }
}

