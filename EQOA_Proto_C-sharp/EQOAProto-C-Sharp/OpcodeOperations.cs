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
using Characters;
using System.Linq;

namespace OpcodeOperations
{
    class ProcessOpcode
    {
        public static void ProcessOpcodes(Session MySession, short MessageTypeOpcode, List<byte> myPacket)
        {
            ///Expected message length
            ushort MessageLength;
            ushort MessageNumber;
            ushort Opcode;

            ///Short message, 1 byte for message length
            if (MessageTypeOpcode == MessageOpcodeTypes.ShortReliableMessage)
            {
                MessageLength = (ushort)myPacket[0];
                ///Remove read byte
                myPacket.RemoveRange(0, 1);
            }

            ///Should be captured in RDPComm now?
            else if (MessageTypeOpcode == MessageOpcodeTypes.UnknownMessage)
            {
                MessageLength = (ushort)myPacket[0];
                ///Remove read byte
                myPacket.RemoveRange(0, 1);

                ///We don't want to process further if F9 received.
                return;
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


                Opcode = (ushort)(myPacket[3] << 8 | myPacket[2]);

                Logger.Info($"Message Length: {MessageLength}; OpcodeType: {MessageTypeOpcode.ToString("X")}; Message Number: {MessageNumber.ToString("X")}; Opcode: {Opcode.ToString("X")}.");

                ///Remove 4 read bytes (Opcode and Message #)
                myPacket.RemoveRange(0, 4);

                ///Pass remaining to opcode checker for more processing
                OpcodeChecker(MySession, Opcode, myPacket);
            }

            ///Not expected order?
            ///Expand on eventually
            else
            {
                return;
            }
        }

        ///Big switch statement to process Opcodes.
        private static void OpcodeChecker(Session MySession, ushort Opcode, List<byte> myPacket)
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
                default:
                    Logger.Err("Unable to identify Bundle Type");
                    break;
            }
        }

        private static void ProcessDelChar(Session MySession, List<byte> myPacket)
        {

            //Gets serverID of character to be deleted sent by client
            int clientServID = myPacket[3] << 24 | myPacket[2] << 16 | myPacket[1] << 8 | myPacket[0];
            //Removes 4 read bytes
            myPacket.RemoveRange(0, 4);
            //Convert serverID fromm client to untechniqued value.
            clientServID = (int)Utility_Funcs.Untechnique(clientServID);
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
                //New character list to hold character objects

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
                charCreation.Level = Utility_Funcs.Untechnique(myPacket[0]);

                //Divide startLevel by 2 because client doubles it
                //Get single byte attributes
                charCreation.Race = Utility_Funcs.Untechnique(myPacket[1]);
                charCreation.StartingClass = Utility_Funcs.Untechnique(myPacket[2]);
                charCreation.Gender = Utility_Funcs.Untechnique(myPacket[3]);
                charCreation.HairColor = Utility_Funcs.Untechnique(myPacket[4]);
                charCreation.HairLength = Utility_Funcs.Untechnique(myPacket[5]);
                charCreation.HairStyle = Utility_Funcs.Untechnique(myPacket[6]);
                charCreation.FaceOption = Utility_Funcs.Untechnique(myPacket[7]);
                charCreation.HumTypeNum = Utility_Funcs.Untechnique(myPacket[8]);

                //Remove bytes for single byte attributes
                myPacket.RemoveRange(0, 9);

                //Get player attributes from packet and remove bytes after reading into variable
                charCreation.AddStrength = myPacket[3] << 24 | myPacket[2] << 16 | myPacket[1] << 8 | myPacket[0];
                myPacket.RemoveRange(0, 4);
                charCreation.AddStamina = myPacket[3] << 24 | myPacket[2] << 16 | myPacket[1] << 8 | myPacket[0];
                myPacket.RemoveRange(0, 4);
                charCreation.AddAgility = myPacket[3] << 24 | myPacket[2] << 16 | myPacket[1] << 8 | myPacket[0];
                myPacket.RemoveRange(0, 4);
                charCreation.AddDexterity = myPacket[3] << 24 | myPacket[2] << 16 | myPacket[1] << 8 | myPacket[0];
                myPacket.RemoveRange(0, 4);
                charCreation.AddWisdom = myPacket[3] << 24 | myPacket[2] << 16 | myPacket[1] << 8 | myPacket[0];
                myPacket.RemoveRange(0, 4);
                charCreation.AddIntelligence = myPacket[3] << 24 | myPacket[2] << 16 | myPacket[1] << 8 | myPacket[0];
                myPacket.RemoveRange(0, 4);
                charCreation.AddCharisma = myPacket[3] << 24 | myPacket[2] << 16 | myPacket[1] << 8 | myPacket[0];
                myPacket.RemoveRange(0, 4);

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
                CharacterList.AddRange(Utility_Funcs.Technique((int)character.ModelID));

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

                ///Start processing gear
                foreach (var Gear in character.GearList)
                {
                    ///Use a switch to sift through gear and add them properly
                    switch (Gear.Item3)
                    {
                        ///Helm
                        case 1:
                            character.Helm = (byte)Gear.Item1;
                            character.HelmColor = Gear.Item2;
                            break;

                        ///Robe
                        case 2:
                            character.Robe = (byte)Gear.Item1;
                            character.RobeColor = Gear.Item2;
                            break;

                        ///Gloves
                        case 19:
                            character.Gloves = (byte)Gear.Item1;
                            character.GlovesColor = Gear.Item2;
                            break;

                        ///Chest
                        case 5:
                            character.Chest = (byte)Gear.Item1;
                            character.ChestColor = Gear.Item2;
                            break;

                        ///Bracers
                        case 8:
                            character.Bracer = (byte)Gear.Item1;
                            character.BracerColor = Gear.Item2;
                            break;

                        ///Legs
                        case 10:
                            character.Legs = (byte)Gear.Item1;
                            character.LegsColor = Gear.Item2;
                            break;

                        ///Feet
                        case 11:
                            character.Boots = (byte)Gear.Item1;
                            character.BootsColor = Gear.Item2;
                            break;

                        ///Primary
                        case 12:
                            character.Primary = Gear.Item1;
                            break;

                        ///Secondary
                        case 14:

                            ///If we have a secondary equipped already, puts next secondary into primary slot
                            if (character.Secondary > 0)
                            {
                                character.Primary = Gear.Item1;
                            }

                            ///If no secondary, add to secondary slot
                            else
                            {
                                character.Secondary = Gear.Item1;
                            }
                            break;

                        ///2 Hand
                        case 15:
                            character.Primary = Gear.Item1;
                            break;

                        ///Shield
                        case 13:
                            character.Shield = Gear.Item1;
                            break;

                        ///Bow
                        case 16:
                            character.Primary = Gear.Item1;
                            break;

                        ///Thrown
                        case 17:
                            character.Primary = Gear.Item1;
                            break;

                        ///Held
                        case 18:
                            ///If we have a secondary equipped already, puts next secondary into primary slot
                            if (character.Secondary > 0)
                            {
                                character.Primary = Gear.Item1;
                            }

                            ///If no secondary, add to secondary slot
                            else
                            {
                                character.Secondary = Gear.Item1;
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
                CharacterList.AddRange(BitConverter.GetBytes((ushort)0x0003));

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
                CharacterList.AddRange(BitConverter.GetBytes(ByteSwaps.SwapBytes(character.ChestColor)));

                ///Bracer color
                CharacterList.AddRange(BitConverter.GetBytes(ByteSwaps.SwapBytes(character.BracerColor)));

                ///Glove color
                CharacterList.AddRange(BitConverter.GetBytes(ByteSwaps.SwapBytes(character.GlovesColor)));

                ///Leg color
                CharacterList.AddRange(BitConverter.GetBytes(ByteSwaps.SwapBytes(character.LegsColor)));

                ///Boot color
                CharacterList.AddRange(BitConverter.GetBytes(ByteSwaps.SwapBytes(character.BootsColor)));

                ///Helm color
                CharacterList.AddRange(BitConverter.GetBytes(ByteSwaps.SwapBytes(character.HelmColor)));

                ///Robe color
                CharacterList.AddRange(BitConverter.GetBytes(ByteSwaps.SwapBytes(character.RobeColor)));

                Logger.Info($"Processed {character.CharName}");
            }

            ///Character list is complete
            ///Handles packing message into outgoing packet
            RdpCommOut.PackMessage(MySession, CharacterList, MessageOpcodeTypes.ShortReliableMessage, GameOpcode.CharacterSelect);

        }
    }
}

