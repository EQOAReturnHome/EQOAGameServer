using EQLogger;
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
        public static void ProcessOpcodes(Session MySession, ushort MessageTypeOpcode, List<byte> myPacket)
        {
            ///Expected message length
            ushort MessageLength;
            ushort MessageNumber;
            ushort Opcode;

            ///Short message, 1 byte for message length
            if ((MessageTypeOpcode == MessageOpcodeTypes.ShortReliableMessage) || (MessageTypeOpcode == MessageOpcodeTypes.UnknownMessage))
            {
                MessageLength = (ushort)myPacket[0];
                ///Remove read byte
                myPacket.RemoveRange(0, 1);
            }

            ///Long message, 2 bytes for message length
            else
            {
                MessageLength = (ushort)(myPacket[1] << 8 | myPacket[0]);
                ///Remove 2 read bytes
                myPacket.RemoveRange(0, 2);
            }

            ///Make sure Message number is expected, needs to be in order.
            MessageNumber = (ushort)(myPacket[1] << 8 | myPacket[0]);


            if (MySession.clientMessageNumber + 1 == MessageNumber)
            {
                ///Increment for every message read, in order.
                MySession.IncrementClientMessageNumber();

                //If F9 type, no opcode process it seperately.
                if (MessageTypeOpcode == MessageOpcodeTypes.UnknownMessage)
                {
                    myPacket.RemoveRange(0, 2);
                    ProcessPingRequest(MySession, myPacket);
                    return;
                }

                Opcode = (ushort)(myPacket[3] << 8 | myPacket[2]);

                Logger.Info($"Message Length: {MessageLength}; OpcodeType: {MessageTypeOpcode.ToString("X")}; Message Number: {MessageNumber.ToString("X")}; Opcode: {Opcode.ToString("X")}.");

                ///Remove 4 read bytes (Opcode and Message #)
                myPacket.RemoveRange(0, 4);

                //Remove the opcode bytes we read
                MessageLength -= 2;

                ///Pass remaining to opcode checker for more processing
                OpcodeChecker(MySession, Opcode, myPacket, MessageLength);
            }

            ///Not expected order?
            ///Expand on eventually
            else
            {
                //Remove packet# and message bytes
                myPacket.RemoveRange(0, MessageLength + 2);
                return;
            }
        }

        ///Big switch statement to process Opcodes.
        private static void OpcodeChecker(Session MySession, ushort Opcode, List<byte> myPacket, int MessageLength)
        {
            switch (Opcode)
            {
                ///Game Disc Version
                case GameOpcode.DiscVersion:
                    Logger.Info("Processing Game Disc Version");
                    ProcessGameDisc(MySession, myPacket);
                    break;

                case GameOpcode.Authenticate:
                    Logger.Info("Server Select Authentication");
                    ProcessAuthenticate(MySession, myPacket, false);
                    break;

                case GameOpcode.Authenticate2:
                    Logger.Info("Character Select Authentication");
                    ProcessAuthenticate(MySession, myPacket, true);
                    break;

                case GameOpcode.DelCharacter:
                    Logger.Info("Character Deletion Request");
                    ProcessDelChar(MySession, myPacket);
                    break;

                case GameOpcode.CreateCharacter:
                    Logger.Info("Create Character Request");
                    ProcessCreateChar(MySession, myPacket);
                    break;

                case GameOpcode.SELECTED_CHAR:
                    //Checking sent data to verify no changes to character
                    ProcessCharacterChanges(MySession, myPacket);
                    Logger.Info("Character Selected, starting memory dump");

                    //Start new session 
                    Session thisSession = new Session(MySession.clientEndpoint, MySession.MyIPEndPoint, MySession.remoteMaster, MySession.AccountID, MySession.sessionIDUp, MySession.MyCharacter);
                    SessionManager.AddMasterSession(thisSession);
                    ProcessMemoryDump(thisSession);
                    break;

                default:
                    Logger.Err($"Unable to identify Opcode: {Opcode}");
                    //Remove unknwon opcodes here
                    //Eventually add logic to send unknwon opcodes to client via chat
                    byte[] MyOpcodeMessage = new byte[MessageLength + 1];
                    myPacket.CopyTo(0, MyOpcodeMessage, 0, MessageLength);

                    //Log opcode message and contents
                    Logger.Info(MyOpcodeMessage);

                    ClientOpcodeUnknown(MySession, Opcode);

                    if (MessageLength > 0)
                    {
                        myPacket.RemoveRange(0, MessageLength);
                    }
                    break;
            }
        }

        private static void ClientOpcodeUnknown(Session MySession, ushort opcode)
        {
            string theMessage = $"Unknown Opcode: {opcode.ToString("X")}";
            List<byte> MyMessage = new List<byte> { };
            MyMessage.AddRange(BitConverter.GetBytes(theMessage.Length));
            MyMessage.AddRange(Encoding.Unicode.GetBytes(theMessage));

            //Send Message
            RdpCommOut.PackMessage(MySession, MyMessage, MessageOpcodeTypes.ShortReliableMessage, GameOpcode.ClientMessage);
        }

        private static void ProcessCharacterChanges(Session MySession, List<byte> myPacket)
        {
            //Retrieve CharacterID from client
            int ServerID = myPacket[3] << 24 | myPacket[2] << 16 | myPacket[1] << 8 | myPacket[0];
            myPacket.RemoveRange(0, 4);
            int HairColor = myPacket[3] << 24 | myPacket[2] << 16 | myPacket[1] << 8 | myPacket[0];
            myPacket.RemoveRange(0, 4);
            int HairLength = myPacket[3] << 24 | myPacket[2] << 16 | myPacket[1] << 8 | myPacket[0];
            myPacket.RemoveRange(0, 4);
            int HairStyle = myPacket[3] << 24 | myPacket[2] << 16 | myPacket[1] << 8 | myPacket[0];
            myPacket.RemoveRange(0, 4);
            int FaceOption = myPacket[3] << 24 | myPacket[2] << 16 | myPacket[1] << 8 | myPacket[0];
            myPacket.RemoveRange(0, 4);

            //Update these on our character
            Character thisChar = MySession.CharacterData.Find(i => Equals(i.ServerID, ServerID));

            //Add Selected character to Session
            MySession.MyCharacter = thisChar;

            //Got character, update changes
            thisChar.UpdateFeatures(HairColor, HairLength, HairStyle, FaceOption);
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
            RdpCommOut.PackMessage(MySession, DNP3Creation.CreateDNP3TimeStamp(), MessageOpcodeTypes.ShortReliableMessage, GameOpcode.Time);

            List<byte> ThisChunk;

            //Gather our dump data
            if (MySession.MyDumpData.Count() > 500)
            {
                ThisChunk = MySession.MyDumpData.GetRange(0, 500);
                MySession.MyDumpData.RemoveRange(0, 500);

                //Set this to true to send packet to client
                MySession.ClientFirstConnect = true;

                ///Handles packing message into outgoing packet
                RdpCommOut.PackMessage(MySession, ThisChunk, MessageOpcodeTypes.MultiShortReliableMessage, GameOpcode.MemoryDump);
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
                RdpCommOut.PackMessage(MySession, ThisChunk, MessageOpcodeTypes.ShortReliableMessage, GameOpcode.MemoryDump);
            }
        }

        private static void ProcessDelChar(Session MySession, List<byte> myPacket)
        {
            //Passes in packet with ServerID on it, will grab, transform and return ServerID while also removing packet bytes
            int clientServID = (int)Utility_Funcs.Untechnique(myPacket);
            //Call SQL delete method to actually process the delete.
            SQLOperations.DeleteCharacter(clientServID, MySession);

        }

        //Method to create new character when new character opcode is received
        private static void ProcessCreateChar(Session MySession, List<byte> myPacket)
        {
            //Create NewCharacter object
            Character charCreation = new Character();

            //Log that a new character creation packet was received
            Logger.Info("Received Character Creation Packet");

            //Get length of characters name expected in packet
            int nameLength = myPacket[3] << 24 | myPacket[2] << 16 | myPacket[1] << 8 | myPacket[0];

            //Remove nameLength from packet
            myPacket.RemoveRange(0, 4);

            //var for actual character name
            byte[] characterNameArray = new byte[nameLength];

            //Copy the actual character name to above variable
            myPacket.CopyTo(0, characterNameArray, 0, nameLength);

            //Remove charactername from packet
            myPacket.RemoveRange(0, nameLength);

            //Make charactername readable
            charCreation.CharName = Encoding.Default.GetString(characterNameArray);

            //Before processing a full character creation check if the characters name already exists in the DB.
            //Later this will need to include a character/world combination if additional servers are spun up.
            if (charCreation.CharName == SQLOperations.CheckName(charCreation.CharName))
            {
                myPacket.Clear();
                //List and assignment to hold game op code in bytes to send out
                List<byte> NameTaken = new List<byte>();
                NameTaken.AddRange(BitConverter.GetBytes(GameOpcode.NameTaken));

                //Log character name taken and send out RDP message to pop up that name is taken.
                Console.WriteLine("Character Name Already Taken");
                RdpCommOut.PackMessage(MySession, MessageOpcodeTypes.ShortReliableMessage, GameOpcode.NameTaken);
            }
            //If name not found continue to actually create character
            else
            {

                //Get starting level
                charCreation.Level = Utility_Funcs.Untechnique(myPacket);

                //Divide startLevel by 2 because client doubles it
                //Get single byte attributes
                charCreation.Race = Utility_Funcs.Untechnique(myPacket);
                charCreation.StartingClass = Utility_Funcs.Untechnique(myPacket);
                charCreation.Gender = Utility_Funcs.Untechnique(myPacket);
                charCreation.HairColor = Utility_Funcs.Untechnique(myPacket);
                charCreation.HairLength = Utility_Funcs.Untechnique(myPacket);
                charCreation.HairStyle = Utility_Funcs.Untechnique(myPacket);
                charCreation.FaceOption = Utility_Funcs.Untechnique(myPacket);
                charCreation.HumTypeNum = Utility_Funcs.Untechnique(myPacket);

                //Get player attributes from packet and remove bytes after reading into variable
                charCreation.AddStrength = myPacket[3] << 24 | myPacket[2] << 16 | myPacket[1] << 8 | myPacket[0];
                charCreation.AddStamina = myPacket[3] << 24 | myPacket[2] << 16 | myPacket[1] << 8 | myPacket[0];
                charCreation.AddAgility = myPacket[3] << 24 | myPacket[2] << 16 | myPacket[1] << 8 | myPacket[0];
                charCreation.AddDexterity = myPacket[3] << 24 | myPacket[2] << 16 | myPacket[1] << 8 | myPacket[0];
                charCreation.AddWisdom = myPacket[3] << 24 | myPacket[2] << 16 | myPacket[1] << 8 | myPacket[0];
                charCreation.AddIntelligence = myPacket[3] << 24 | myPacket[2] << 16 | myPacket[1] << 8 | myPacket[0];
                charCreation.AddCharisma = myPacket[3] << 24 | myPacket[2] << 16 | myPacket[1] << 8 | myPacket[0];

                //Call SQL method for character creation
                SQLOperations.CreateCharacter(MySession, charCreation);
            }
        }

        ///Game Disc Version
        private static void ProcessGameDisc(Session MySession, List<byte> myPacket)
        {

            ///Gets Gameversion sent by client
            int GameVersion = myPacket[3] << 24 | myPacket[2] << 16 | myPacket[1] << 8 | myPacket[0];

            ///Remove 4 read bytes (Game Version)
            myPacket.RemoveRange(0, 4);

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
            RdpCommOut.PackMessage(MySession, GameVersionResponse, MessageOpcodeTypes.ShortReliableMessage, GameOpcode.DiscVersion);

        }

        ///Authentication check
        private static void ProcessAuthenticate(Session MySession, List<byte> myPacket, bool CreateMasterSession)
        {
            if (!CreateMasterSession) { Logger.Info("Processing Authentication (Server Select)"); }
            else { Logger.Info("Processing Authentication (Character Select)"); }
            ///Opcode option? Just remove for now
            myPacket.RemoveRange(0, 1);

            ///Unknown also, supposedly can be 03 00 00 00 or  01 00 00 00
            myPacket.RemoveRange(0, 4);

            ///Game Code Length
            int GameCodeLength = myPacket[3] << 24 | myPacket[2] << 16 | myPacket[1] << 8 | myPacket[0];
            myPacket.RemoveRange(0, 4);

            ///Our Game Code String
            byte[] GameCodeArray = new byte[GameCodeLength];

            ///Copy The GameCode into other variable
            myPacket.CopyTo(0, GameCodeArray, 0, GameCodeLength);

            ///Remove GameCode from packet
            myPacket.RemoveRange(0, GameCodeLength);

            ///the actual gamecode
            string GameCode = Encoding.Default.GetString(GameCodeArray);

            if (GameCode == "EQOA")
            {
                ///Authenticate
                Logger.Info("Received EQOA Game Code, continuing...");

                ///Grab Character name
                ///Game Code Length
                int AccountNameLength = myPacket[3] << 24 | myPacket[2] << 16 | myPacket[1] << 8 | myPacket[0];
                myPacket.RemoveRange(0, 4);

                ///Our CharacterName Array
                byte[] AccountNameArray = new byte[AccountNameLength];

                ///Copy The characterName into other variable
                myPacket.CopyTo(0, AccountNameArray, 0, AccountNameLength);

                ///Remove characterName from packet
                myPacket.RemoveRange(0, AccountNameLength);

                ///the actual gamecode
                string AccountName = Encoding.Default.GetString(AccountNameArray);

                Logger.Info($"Received Account Name: {AccountName}");

                ///Username ends with 01, no known use, remove for now
                myPacket.RemoveRange(0, 1);

                //Decrypting password information goes here?

                string Password = "password";

                ///Remove encrypted password off packet
                myPacket.RemoveRange(0, 32);

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
            RdpCommOut.PackMessage(MySession, FirstMessage, MessageOpcodeTypes.ShortReliableMessage, GameOpcode.Camera1);

            List<byte> SecondMessage = new List<byte>() { 0x1B, 0x00, 0x00, 0x00 };
            ///Handles packing message into outgoing packet
            RdpCommOut.PackMessage(MySession, SecondMessage, MessageOpcodeTypes.ShortReliableMessage, GameOpcode.Camera2);
            MySession.ClientFirstConnect = true;
        }

        private static void ProcessPingRequest(Session MySession, List<byte> MyPacket)
        {
            if (MySession.InGame == false && (MyPacket[0] == 0x12))
            {
                //Nothing needed here I suppose?
            }

            else if (MySession.InGame == true && (MyPacket[0] == 0x14))
            {
                List<byte> MyMessage = new List<byte>() { 0x14 };
                ///Do stuff here?
                ///Handles packing message into outgoing packet
                RdpCommOut.PackMessage(MySession, MyMessage, MessageOpcodeTypes.UnknownMessage);
            }

            else
            {
                Logger.Err($"Received an F9 with unknown value {MyPacket[0]}");
            }

            ///Remove single byte
            MyPacket.RemoveRange(0, 1);
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
            RdpCommOut.PackMessage(MySession, CharacterList, MessageOpcodeTypes.ShortReliableMessage, GameOpcode.CharacterSelect);
        }

        public static void IgnoreList(Session MySession)
        {
            //For now send no ignored people
            RdpCommOut.PackMessage(MySession, new List<byte>{0}, MessageOpcodeTypes.ShortReliableMessage, GameOpcode.IgnoreList);
        }

        public static void ActorSpeed(Session MySession)
        {
            List<byte> CharacterSpeed = new List<byte> { };
            CharacterSpeed.AddRange(BitConverter.GetBytes(25.0f));
            //For now send a standard speed
            RdpCommOut.PackMessage(MySession, CharacterSpeed, MessageOpcodeTypes.ShortReliableMessage, GameOpcode.ActorSpeed);
        }
    }
}

