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
using System.Linq;
using Hotkeys;
using Quests;
using Items;
using WeaponHotbars;
using Auctions;
using Spells;
using Sessions;

namespace OpcodeOperations
{
    class ProcessOpcode
    {
        public static void ProcessOpcodes(Session MySession, ushort MessageTypeOpcode, ReadOnlyMemory<byte> ClientPacket, ref int offset)
        {
            ///Expected message length
            ushort MessageLength;
            ushort MessageNumber;
            ushort Opcode;

            ///Short message, 1 byte for message length
            if ((MessageTypeOpcode == MessageOpcodeTypes.ShortReliableMessage) || (MessageTypeOpcode == MessageOpcodeTypes.UnknownMessage))
            {
                MessageLength = BinaryPrimitiveWrapper.GetLEByte(ClientPacket, ref offset);
            }

            ///Long message, 2 bytes for message length
            else
            {
                MessageLength = BinaryPrimitiveWrapper.GetLEUShort(ClientPacket, ref offset);
            }

            ///Make sure Message number is expected, needs to be in order.
            MessageNumber = BinaryPrimitiveWrapper.GetLEUShort(ClientPacket, ref offset);


            if (MySession.clientMessageNumber + 1 == MessageNumber)
            {
                ///Increment for every message read, in order.
                MySession.IncrementClientMessageNumber();

                //If F9 type, no opcode process it seperately.
                if (MessageTypeOpcode == MessageOpcodeTypes.UnknownMessage)
                {
                    ProcessPingRequest(MySession, ClientPacket, ref offset);
                    return;
                }

                Opcode = BinaryPrimitiveWrapper.GetLEUShort(ClientPacket, ref offset);

                Logger.Info($"Message Length: {MessageLength}; OpcodeType: {MessageTypeOpcode.ToString("X")}; Message Number: {MessageNumber.ToString("X")}; Opcode: {Opcode.ToString("X")}.");

                ///Pass remaining to opcode checker for more processing
                //OpcodeChecker(MySession, Opcode, ClientPacket, ref offset, MessageLength);
                OpcodeTypes.OpcodeDictionary[(GameOpcode)Opcode].Invoke(MySession, ClientPacket.Slice(offset, MessageLength - 2));
            }

            ///Not expected order?
            ///Expand on eventually
            else
            {
                //If out of order, add Message length -2(opcode we already read) to offset and move on
                offset += (MessageLength - 2);
                return;
            }
        }
        /*
        ///Big switch statement to process Opcodes.
        public static void OpcodeChecker(Session MySession, ushort Opcode, ReadOnlyMemory<byte> ClientPacket, ref int offset, int MessageLength)
        {
            switch ((Opcode)
            {
                ///Game Disc Version
                case GameOpcode.DiscVersion:
                    Logger.Info("Processing Game Disc Version");
                    ProcessGameDisc(MySession, ClientPacket, ref offset);
                    break;

                case GameOpcode.Authenticate:
                    Logger.Info("Server Select Authentication");
                    ProcessAuthenticate(MySession, ClientPacket, ref offset, false);
                    break;

                case GameOpcode.Authenticate2:
                    Logger.Info("Character Select Authentication");
                    ProcessAuthenticate(MySession, ClientPacket, ref offset, true);
                    break;

                case GameOpcode.DelCharacter:
                    Logger.Info("Character Deletion Request");
                    ProcessDelChar(MySession, ClientPacket, ref offset);
                    break;

                case GameOpcode.CreateCharacter:
                    Logger.Info("Create Character Request");
                    ProcessCreateChar(MySession, ClientPacket, ref offset);
                    break;

                case GameOpcode.SELECTED_CHAR:
                    //Checking sent data to verify no changes to character
                    ProcessCharacterChanges(MySession, ClientPacket, ref offset);
                    Logger.Info("Character Selected, starting memory dump");

                    break;

                case GameOpcode.DisconnectClient:
                    //Client is disconnecting from the game world
                    //Add logic here to save character data, position etc

                    //Drop the session
                    SessionManager.DropSession(MySession);
                    break;

                default:
                    Logger.Err($"Unable to identify Opcode: {Opcode}");
                    //Remove unknwon opcodes here
                    //Eventually add logic to send unknwon opcodes to client via chat
                    //Log opcode message and contents
                    Logger.Info(ClientPacket.Slice(offset, (MessageLength - 2)));

                    ClientOpcodeUnknown(MySession, Opcode);


                    //increment offset and jump past this message
                    offset += (MessageLength - 2);
                    break;
            }
        }*/

        public static void ClientOpcodeUnknown(Session MySession, GameOpcode opcode)
        {
            string theMessage = $"Unknown Opcode: {opcode.ToString("X")}";
            List<byte> MyMessage = new List<byte> { };
            MyMessage.AddRange(BitConverter.GetBytes(theMessage.Length));
            MyMessage.AddRange(Encoding.Unicode.GetBytes(theMessage));

            //Send Message
            RdpCommOut.PackMessage(MySession, MyMessage, MessageOpcodeTypes.ShortReliableMessage, (ushort)GameOpcode.ClientMessage);
        }

        public static void ProcessCharacterChanges(Session MySession, ReadOnlyMemory<byte> ClientPacket)
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

            //Add Selected character to Session
            MySession.MyCharacter = thisChar;

            //Got character, update changes
            thisChar.UpdateFeatures(HairColor, HairLength, HairStyle, FaceOption);

            //Start new session 
            Session thisSession = new Session(MySession.clientEndpoint, MySession.MyIPEndPoint, MySession.remoteMaster, MySession.AccountID, MySession.sessionIDUp, MySession.MyCharacter);
            SessionManager.AddMasterSession(thisSession);
            ProcessMemoryDump(thisSession);
        }

        public static void ProcessMemoryDump(Session MySession)
        {
            
            //Let's get remaining character data before preparing it for transport
            //Hotkeys
            SQLOperations.GetPlayerHotkeys(MySession);

            //Quests, skip for now
            //Weaponhotbars
            SQLOperations.GetPlayerWeaponHotbar(MySession);

            //Auctions go here, skip for now
            //Spells
            SQLOperations.GetPlayerSpells(MySession);

            MySession.MyDumpData.AddRange(MySession.MyCharacter.PullCharacter());
            MySession.MyDumpData.Add((byte)(MySession.MyCharacter.MyHotkeys.Count() * 2));
               
            //cycle over all our hotkeys and append them
            foreach(Hotkey MyHotkey in MySession.MyCharacter.MyHotkeys)
            {
                MySession.MyDumpData.AddRange(MyHotkey.PullHotkey());
            }
            //Unknown at this time 4 byte null
            MySession.MyDumpData.AddRange(BitConverter.GetBytes(0));
                
            //Unknown at this time 4 byte null
            MySession.MyDumpData.AddRange(BitConverter.GetBytes(0));
                
            //Quest Count
            MySession.MyDumpData.AddRange(BitConverter.GetBytes(MySession.MyCharacter.MyQuests.Count()));
                
            //Iterate over quest data and append (Should be 0 for now...)
            foreach(Quest MyQuest in MySession.MyCharacter.MyQuests)
            {
                MySession.MyDumpData.AddRange(MyQuest.PullQuest());
            }
                
            //Get Inventory Item count
            MySession.MyDumpData.Add((byte)(MySession.MyCharacter.InventoryItems.Count() * 2));
            MySession.MyDumpData.AddRange(BitConverter.GetBytes(MySession.MyCharacter.InventoryItems.Count()));
            foreach( Item MyItem in MySession.MyCharacter.InventoryItems)
            {
                MySession.MyDumpData.AddRange(MyItem.PullItem());
            }

            foreach(WeaponHotbar MyWeaponHotbar in MySession.MyCharacter.WeaponHotbars)
            {
                MySession.MyDumpData.AddRange(MyWeaponHotbar.PullWeaponHotbar());
            }
                
            //Get Bank Item count
            MySession.MyDumpData.Add((byte)(MySession.MyCharacter.BankItems.Count() * 2));
            MySession.MyDumpData.AddRange(BitConverter.GetBytes(MySession.MyCharacter.BankItems.Count()));
            foreach (Item MyItem in MySession.MyCharacter.BankItems)
            {
                MySession.MyDumpData.AddRange(MyItem.PullItem());
            }
                
            // end of bank? or could be something else for memory dump
            MySession.MyDumpData.Add((byte)0);
                
            //Buying auctions
            MySession.MyDumpData.Add((byte)(MySession.MyCharacter.MyBuyingAuctions.Count()));
            foreach (Auction MyAuction in MySession.MyCharacter.MyBuyingAuctions)
            {
                MySession.MyDumpData.AddRange(MyAuction.PullAuction());
            }
                
            //Selling auctions
            MySession.MyDumpData.Add((byte)(MySession.MyCharacter.MySellingAuctions.Count()));
            foreach (Auction MyAuction in MySession.MyCharacter.MySellingAuctions)
            {
                MySession.MyDumpData.AddRange(MyAuction.PullAuction());
            }
               
            //Spell count and Spells
            MySession.MyDumpData.AddRange(Utility_Funcs.Technique(MySession.MyCharacter.MySpells.Count()));
            foreach(Spell MySpell in MySession.MyCharacter.MySpells)
            {
                MySession.MyDumpData.AddRange(MySpell.PullSpell());
                //Indicates end of spell?
                MySession.MyDumpData.Add((byte)0);
            }
                
            MySession.MyDumpData.AddRange(new byte[] {0x55, 0x55, 0x0d, 0x41, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x01, 0x00, 0x00,
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

            //Get our timestamp opcode in queue
            RdpCommOut.PackMessage(MySession, DNP3Creation.CreateDNP3TimeStamp(), MessageOpcodeTypes.ShortReliableMessage, (ushort)GameOpcode.Time);

            List<byte> ThisChunk;

            //Gather our dump data
            if (MySession.MyDumpData.Count() > 500)
            {
                ThisChunk = MySession.MyDumpData.GetRange(0, 500);
                MySession.MyDumpData.RemoveRange(0, 500);

                //Set this to true to send packet to client
                MySession.ClientFirstConnect = true;

                ///Handles packing message into outgoing packet
                RdpCommOut.PackMessage(MySession, ThisChunk, MessageOpcodeTypes.MultiShortReliableMessage, (ushort)GameOpcode.MemoryDump);
            }

            //Dump data is smaller then 500 bytes
            else
            {
                ThisChunk = MySession.MyDumpData.GetRange(0, MySession.MyDumpData.Count());
                MySession.MyDumpData.Clear();

                //Set this to true to send packet to client
                MySession.ClientFirstConnect = true;

                //Appears dump is short, end it here
                MySession.Dumpstarted = false;

                ///Handles packing message into outgoing packet
                RdpCommOut.PackMessage(MySession, ThisChunk, MessageOpcodeTypes.ShortReliableMessage, (ushort)GameOpcode.MemoryDump);
            }
        }

        public static void ProcessDelChar(Session MySession, ReadOnlyMemory<byte> ClientPacket)
        {
            int offset = 0;

            //Passes in packet with ServerID on it, will grab, transform and return ServerID while also removing packet bytes
            int clientServID = Utility_Funcs.Untechnique(ClientPacket.Span, ref offset);
            //Call SQL delete method to actually process the delete.
            SQLOperations.DeleteCharacter(clientServID, MySession);
        }

        //Method to create new character when new character opcode is received
        public static void ProcessCreateChar(Session MySession, ReadOnlyMemory<byte> ClientPacket)
        {
            int offset = 0;

            //Create NewCharacter object
            Character charCreation = new Character();

            //Log that a new character creation packet was received
            Logger.Info("Received Character Creation Packet");

            //Get length of characters name expected in packet
            int nameLength = BinaryPrimitiveWrapper.GetLEInt(ClientPacket, ref offset);

            //Get Character Name
            charCreation.CharName = Utility_Funcs.GetMemoryString(ClientPacket.Span, ref offset, nameLength);

            //Before processing a full character creation check if the characters name already exists in the DB.
            //Later this will need to include a character/world combination if additional servers are spun up.
            if (charCreation.CharName == SQLOperations.CheckName(charCreation.CharName))
            {
                //List and assignment to hold game op code in bytes to send out
                List<byte> NameTaken = new List<byte>();
                NameTaken.AddRange(BitConverter.GetBytes((ushort)GameOpcode.NameTaken));

                //Log character name taken and send out RDP message to pop up that name is taken.
                Console.WriteLine("Character Name Already Taken");
                RdpCommOut.PackMessage(MySession, MessageOpcodeTypes.ShortReliableMessage, GameOpcode.NameTaken);
            }
            //If name not found continue to actually create character
            else
            {

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
                SQLOperations.CreateCharacter(MySession, charCreation);
            }
        }

        ///Game Disc Version
        public static void ProcessGameDisc(Session MySession, ReadOnlyMemory<byte> ClientPacket)
        {
            int offset = 0;
            ///Gets Gameversion sent by client
            int GameVersion = BinaryPrimitiveWrapper.GetLEInt(ClientPacket, ref offset);

            switch (GameVersion)
            {
                ///Game Disc Version
                case GameVersions.EQOA_FRONTIERS:
                    Logger.Info("EQOA Frontiers Selected.");
                    MySession.gameVersion = GameVersion;
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
            List<byte> GameVersionResponse = new List<byte>();

            ///Need to send this back to client
            byte[] GameVersionList = BitConverter.GetBytes((uint)GameVersion);
            GameVersionResponse.AddRange(GameVersionList);

            ///Handles packing message into outgoing packet
            RdpCommOut.PackMessage(MySession, GameVersionResponse, MessageOpcodeTypes.ShortReliableMessage, (ushort)GameOpcode.DiscVersion);

        }

        ///Authentication check
        public static void ProcessAuthenticate(Session MySession, ReadOnlyMemory<byte> ClientPacket)
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

        ///Used when starting a master session with client.
        public static void GenerateClientContact(Session MySession)
        {
            List<byte> FirstMessage = new List<byte>() { 0x03, 0x00, 0x00, 0x00 };
            ///Handles packing message into outgoing packet
            RdpCommOut.PackMessage(MySession, FirstMessage, MessageOpcodeTypes.ShortReliableMessage, (ushort)GameOpcode.Camera1);

            List<byte> SecondMessage = new List<byte>() { 0x1B, 0x00, 0x00, 0x00 };
            ///Handles packing message into outgoing packet
            RdpCommOut.PackMessage(MySession, SecondMessage, MessageOpcodeTypes.ShortReliableMessage, (ushort)GameOpcode.Camera2);
            MySession.ClientFirstConnect = true;
        }

        public static void ProcessPingRequest(Session MySession, ReadOnlyMemory<byte> ClientPacket, ref int offset)
        {
            if (MySession.InGame == false && (ClientPacket.Span[offset] == 0x12))
            {
                //Nothing needed here I suppose?
            }

            else if (MySession.InGame == true && (ClientPacket.Span[offset] == 0x14))
            {
                List<byte> MyMessage = new List<byte>() { 0x14 };
                ///Do stuff here?
                ///Handles packing message into outgoing packet
                RdpCommOut.PackMessage(MySession, MyMessage, MessageOpcodeTypes.UnknownMessage);
            }

            else
            {
                Logger.Err($"Received an F9 with unknown value {ClientPacket.Span[offset]}");
            }

            offset += 1;
        }

        public static void CreateCharacterList(List<Character> MyCharacterList, Session MySession)
        {
            //Holds list of characters pulled from the DB for the AccountID
            List<byte> CharacterList = new List<byte>();

            ///Gets our character count and uses technique to double it
            CharacterList.AddRange(Utility_Funcs.Technique((byte)MyCharacterList.Count()));

            //Iterates through each charcter in the list and converts attribute values to packet values
            foreach (Character character in MyCharacterList)
            {
                ///Add the Character name length
                CharacterList.AddRange(BitConverter.GetBytes((uint)character.CharName.Length));

                ///Add character name
                CharacterList.AddRange(Encoding.ASCII.GetBytes(character.CharName));

                ///Add Server ID
                CharacterList.AddRange(Utility_Funcs.Technique(character.ServerID));

                ///Add Model
                CharacterList.AddRange(Utility_Funcs.Technique(character.ModelID));

                ///Add Class
                CharacterList.AddRange(Utility_Funcs.Technique(character.TClass));

                ///Add Race
                CharacterList.AddRange(Utility_Funcs.Technique(character.Race));

                ///Add Level
                CharacterList.AddRange(Utility_Funcs.Technique(character.Level));

                ///Add Hair color
                CharacterList.AddRange(Utility_Funcs.Technique(character.HairColor));

                ///Add Hair Length
                CharacterList.AddRange(Utility_Funcs.Technique(character.HairLength));

                ///Add Hair Style
                CharacterList.AddRange(Utility_Funcs.Technique(character.HairStyle));

                ///Add Face option
                CharacterList.AddRange(Utility_Funcs.Technique(character.FaceOption));

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
                CharacterList.AddRange(BitConverter.GetBytes(character.Robe));

                ///Add Primary
                CharacterList.AddRange(BitConverter.GetBytes(character.Primary));

                ///Add Secondary
                CharacterList.AddRange(BitConverter.GetBytes(character.Secondary));

                ///Add Shield
                CharacterList.AddRange(BitConverter.GetBytes(character.Shield));

                ///Add Character animation here, dumby for now
                CharacterList.AddRange(BitConverter.GetBytes((ushort)0x0004));

                ///unknown value?
                CharacterList.Add((byte)0);

                ///Chest Model
                CharacterList.Add(character.Chest);

                ///uBracer Model
                CharacterList.Add(character.Bracer);

                ///Glove Model
                CharacterList.Add(character.Gloves);

                ///Leg Model
                CharacterList.Add(character.Legs);

                ///Boot Model
                CharacterList.Add(character.Boots);

                ///Helm Model
                CharacterList.Add(character.Helm);

                ///unknown value?
                CharacterList.AddRange(BitConverter.GetBytes((uint)0));

                ///unknown value?
                CharacterList.AddRange(BitConverter.GetBytes((ushort)0));

                ///unknown value?
                CharacterList.AddRange(BitConverter.GetBytes(0xFFFFFFFF));

                ///unknown value?
                CharacterList.AddRange(BitConverter.GetBytes(0xFFFFFFFF));

                ///unknown value?
                CharacterList.AddRange(BitConverter.GetBytes(0xFFFFFFFF));

                ///Chest color
                CharacterList.AddRange(BitConverter.GetBytes(character.ChestColor).Reverse());

                ///Bracer color
                CharacterList.AddRange(BitConverter.GetBytes(character.BracerColor).Reverse());

                ///Glove color
                CharacterList.AddRange(BitConverter.GetBytes(character.GlovesColor).Reverse());

                ///Leg color
                CharacterList.AddRange(BitConverter.GetBytes(character.LegsColor).Reverse());

                ///Boot color
                CharacterList.AddRange(BitConverter.GetBytes(character.BootsColor).Reverse());

                ///Helm color
                CharacterList.AddRange(BitConverter.GetBytes(character.HelmColor).Reverse());

                ///Robe color
                CharacterList.AddRange(BitConverter.GetBytes(character.RobeColor).Reverse());

                Logger.Info($"Processed {character.CharName}");
            }

            ///Character list is complete
            ///Handles packing message into outgoing packet
            RdpCommOut.PackMessage(MySession, CharacterList, MessageOpcodeTypes.ShortReliableMessage, (ushort)GameOpcode.CharacterSelect);
        }

        public static void IgnoreList(Session MySession)
        {
            //For now send no ignored people
            RdpCommOut.PackMessage(MySession, new List<byte>{0}, MessageOpcodeTypes.ShortReliableMessage, (ushort)GameOpcode.IgnoreList);
        }

        public static void ActorSpeed(Session MySession)
        {
            List<byte> CharacterSpeed = new List<byte> { };
            CharacterSpeed.AddRange(BitConverter.GetBytes(25.0f));
            //For now send a standard speed
            RdpCommOut.PackMessage(MySession, CharacterSpeed, MessageOpcodeTypes.ShortReliableMessage, (ushort)GameOpcode.ActorSpeed);
        }
    }
}

