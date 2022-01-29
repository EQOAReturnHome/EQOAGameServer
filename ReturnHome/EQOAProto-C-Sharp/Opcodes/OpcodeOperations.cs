using System;
using System.Collections.Generic;
using System.Text;
using System.IO;
using System.Configuration;

using ReturnHome.Utilities;
using ReturnHome.Database.SQL;
using ReturnHome.Server.Network;
using ReturnHome.Server.EntityObject.Player;
using ReturnHome.Server.Network.Managers;
using ReturnHome.Opcodes.Chat;
using ReturnHome.Server.Managers;
using ReturnHome.Server.EntityObject;

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
            { GameOpcode.ClientSayChat, ChatMessage.ProcessClientChat },
            { GameOpcode.RandomName, GenerateRandomName },
            { GameOpcode.ClientShout, ShoutChat.ProcessShout },
            { GameOpcode.ChangeChatMode, ChangeChatMode },
            { GameOpcode.DisconnectClient, DisconnectClient },
            { GameOpcode.Target, PlayerTarget },
            { GameOpcode.Interact, InteractActor },
            { GameOpcode.DialogueBoxOption, InteractActor },
            { GameOpcode.BankUI, InteractActor },
            { GameOpcode.MerchantDiag, InteractActor },
            { GameOpcode.DepositBankTunar, InteractActor },
            { GameOpcode.PlayerTunar, InteractActor },
            { GameOpcode.ConfirmBankTunar, InteractActor },
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

            Message.Write((ushort)GameOpcode.RandomName, ref offset);
            Message.Write(Name.Length, ref offset);
            Message.Write(Encoding.Default.GetBytes(Name), ref offset);
            //Send Message
            SessionQueueMessages.PackMessage(MySession, temp, MessageOpcodeTypes.ShortReliableMessage);
        }

        public static void ClientOpcodeUnknown(Session MySession, ushort opcode)
        {
            if (MySession.unkOpcode)
            {
                int offset = 0;
                string message = $"Unknown Opcode: {opcode.ToString("X")}";

                ChatMessage.GenerateClientSpecificChat(MySession, message);
            }
        }

        public static void ChangeChatMode(Session MySession, PacketMessage ClientPacket)
        {
            //Just accept and change chat mode
            MySession.MyCharacter.chatMode = ClientPacket.Data.Span[0];
        }

        public static void AddInvItem(Session MySession, Item item)
        {
        }

        public static void PlayerTarget(Session mySession, PacketMessage clientPacket)
        {
            //Offset is 4 because first 4 bytes is targeting counter
            int offset = 4;
            ReadOnlySpan<byte> Message = clientPacket.Data.Span;

            uint targetID = Message.GetLEUInt(ref offset);

            mySession.MyCharacter.Target = targetID;
            mySession.TargetUpdate();
        }

        /*Some thoughts
         * In Dustin's Database under NPCType, certain npc's have a specific value
        If it has 0x80+, it is/should be unattackable
        Bankers are 0x02. So most likely 0x82 for unattackable and banker
        Coachmen are 0x0100, so 0x0180 for coachmen and unattackable
        */

        /*LOOK HERE Use multiple 0x46 to send multiple dialogue boxes without response*/
        public static void InteractActor(Session MySession, PacketMessage ClientPacket)
        {
            int offset = 0;
            Console.WriteLine(ClientPacket.Header.Opcode);

            ReadOnlySpan<byte> IncMessage = ClientPacket.Data.Span;
            //Bank popup window
            if (ClientPacket.Header.Opcode == 4685)
            {
                //reset offset
                offset = 0;
                //Define Memory span
                Memory<byte> temp = new byte[2];
                Span<byte> Message = temp.Span;
                //Write bank op code back to memory span
                Message.Write((ushort)GameOpcode.BankUI, ref offset);
                //Send bank op code back to player
                SessionQueueMessages.PackMessage(MySession, temp, MessageOpcodeTypes.ShortReliableMessage);
                //Exit method early without processing any other conditions
                return;
            }

            //Deposit bank tunar
            if (ClientPacket.Header.Opcode == 4693)
            {
                Console.WriteLine("4693");
                offset = 0;
                uint targetNPC = IncMessage.GetLEUInt(ref offset);
                uint giveOrTake = IncMessage.Get7BitEncodedInt(ref offset);
                int transferAmount = IncMessage.Get7BitDoubleEncodedInt(ref offset);

                int newPlayerAmt = 0;
                int newBankAmt = 0;

                Console.WriteLine(targetNPC);
                Console.WriteLine(giveOrTake);
                Console.WriteLine(transferAmount);

                //deposit
                if (giveOrTake == 0)
                {
                    Console.WriteLine("Deposit");
                    newPlayerAmt = MySession.MyCharacter.Tunar - transferAmount;
                    MySession.MyCharacter.Tunar = newPlayerAmt;
                    newBankAmt = MySession.MyCharacter.BankTunar + transferAmount;
                    MySession.MyCharacter.BankTunar = newBankAmt;
                }
                //withdraw
                else if (giveOrTake == 1)
                {
                    Console.WriteLine("Withdraw");
                    newPlayerAmt = MySession.MyCharacter.Tunar + transferAmount;
                    MySession.MyCharacter.Tunar = newPlayerAmt;
                    newBankAmt = MySession.MyCharacter.BankTunar - transferAmount;
                    MySession.MyCharacter.BankTunar = newBankAmt;
                }

                //Define memory span
                Memory<byte> playerTemp = new byte[2 + Utility_Funcs.DoubleVariableLengthIntegerLength(newPlayerAmt)];
                Span<byte> messagePlayer = playerTemp.Span;

                Memory<byte> bankTemp = new byte[2 + Utility_Funcs.DoubleVariableLengthIntegerLength(newBankAmt)];
                Span<byte> messageBank = bankTemp.Span;

                offset = 0;
                messagePlayer.Write((ushort)GameOpcode.PlayerTunar, ref offset);
                messagePlayer.Write7BitDoubleEncodedInt(newPlayerAmt, ref offset);

                offset = 0;
                messageBank.Write((ushort)GameOpcode.ConfirmBankTunar, ref offset);
                messageBank.Write7BitDoubleEncodedInt(newBankAmt, ref offset);

                SessionQueueMessages.PackMessage(MySession, playerTemp, MessageOpcodeTypes.ShortReliableMessage);
                SessionQueueMessages.PackMessage(MySession, bankTemp, MessageOpcodeTypes.ShortReliableMessage);
                return;
            }



            //Read the incoming message and get the objectID that was interacted with
            IncMessage = ClientPacket.Data.Span;
            uint interactTarget = IncMessage.GetLEUInt(ref offset);
            if (ClientPacket.Header.Opcode == 4)
            {

                //Create new instance of the event manager
                EventManager eManager = new EventManager();
                Entity npcEntity = new Entity(false);
                Dialogue dialogue = MySession.MyCharacter.MyDialogue;

                //Gets NPC name from ObjectID
                if (EntityManager.QueryForEntity(interactTarget, out Entity npc))
                {
                    npcEntity.CharName = npc.CharName;
                    dialogue.npcName = npc.CharName;
                }




                //Reset offset for outgoing message
                offset = 0;
                //Length of choices
                uint choicesLength = 0;
                //Dialogue at top of box
                string TextboxMessage;
                //The number of text choices that exist
                //uint textChoicesNum = 0;
                //List of dialogue options
                List<string> textChoices = new List<String>();
                //Counter to keep track of how many 
                uint choiceCounter = 0;
                byte textOptions = 1;




                MySession.MyCharacter = eManager.GetNPCDialogue(GameOpcode.DialogueBox, MySession.MyCharacter);
                if (dialogue.diagOptions != null)
                {

                    textChoices = dialogue.diagOptions;
                    choiceCounter = (uint)textChoices.Count;

                    //Length of choices
                    foreach (string choice in textChoices)
                    {
                        choicesLength += (uint)choice.Length;

                    }
                    textOptions = (byte)textChoices.Count;
                }

                TextboxMessage = dialogue.dialogue;




                Memory<byte> temp = new Memory<byte>(new byte[11 + (TextboxMessage.Length * 2) + (choiceCounter * 4) + 1 + (choicesLength * 2)]);
                Span<byte> Message = temp.Span;

                Message.Write((ushort)GameOpcode.DialogueBox, ref offset);
                Message.Write(choiceCounter, ref offset);
                Message.Write(TextboxMessage.Length, ref offset);
                Message.Write(Encoding.Unicode.GetBytes(TextboxMessage), ref offset);
                Message.Write(textOptions, ref offset);

                if (dialogue.diagOptions != null)
                {
                    for (int i = 0; i < textChoices.Count; i++)
                    {
                        Message.Write(textChoices[i].Length, ref offset);
                        Message.Write(Encoding.Unicode.GetBytes(textChoices[i]), ref offset);
                    }
                    textChoices.Clear();
                    dialogue.diagOptions = null;
                }



                //Send Message
                SessionQueueMessages.PackMessage(MySession, temp, MessageOpcodeTypes.ShortReliableMessage);
            }

        }

        public static void DisconnectClient(Session MySession, PacketMessage ClientPacket)
        {
            MySession.PendingTermination = true;
            //Create new handle for mysql connection
            CharacterSQL savePlayerData = new();
            //Call the mysql update query to save player data
            savePlayerData.SavePlayerData(MySession.MyCharacter);
            //Actually drop the player's session
            MySession.DropSession();
        }

        public static void ProcessCharacterChanges(Session MySession, PacketMessage ClientPacket)
        {
            int offset = 0;
            ReadOnlySpan<byte> Message = ClientPacket.Data.Span;
            //Retrieve CharacterID from client
            int ServerID = Message.GetLEInt(ref offset);
            int FaceOption = Message.GetLEInt(ref offset);
            int HairStyle = Message.GetLEInt(ref offset);
            int HairLength = Message.GetLEInt(ref offset);
            int HairColor = Message.GetLEInt(ref offset);

            CharacterSQL cSQL = new();
            //Query Character
            Character MyCharacter = cSQL.AcquireCharacter(MySession, ServerID);
            cSQL.CloseConnection();
            try
            {
                SessionManager.CreateMemoryDumpSession(MySession, MyCharacter);
            }
            catch
            {
                Logger.Err($"Unable to create Memory Dump Session for {MySession.SessionID} : {MyCharacter.CharName}");
            }
        }

        public static void ProcessMemoryDump(Session MySession)
        {
            Memory<byte> buffer;

            //Perform SQl stuff
            CharacterSQL charDump = new CharacterSQL();

            //Probably change this to only pass in character ServerID
            charDump.GetPlayerHotkeys(MySession);
            charDump.GetPlayerWeaponHotbar(MySession);
            charDump.GetPlayerSpells(MySession);

            using (MemoryStream memStream = new())
            {
                //Toss opcode in
                memStream.Write(BitConverter.GetBytes((ushort)GameOpcode.MemoryDump));

                MySession.MyCharacter.DumpCharacter(memStream);
                memStream.Write(Utility_Funcs.DoublePack(MySession.MyCharacter.MyHotkeys.Count));

                //cycle over all our hotkeys and append them
                foreach (Hotkey h in MySession.MyCharacter.MyHotkeys)
                {
                    h.PullHotkey(memStream);
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
                    q.DumpQuest(memStream);
                }

                //Get Inventory Item count
                memStream.Write(Utility_Funcs.DoublePack(MySession.MyCharacter.Inventory.Count));
                memStream.Write(BitConverter.GetBytes(MySession.MyCharacter.Inventory.Count));

                foreach (Item i in MySession.MyCharacter.Inventory)
                {
                    i.DumpItem(memStream);
                }

                //While we are here, lets "equip" our equipped gear
                MySession.MyCharacter.EquipGear();

                foreach (WeaponHotbar wb in MySession.MyCharacter.WeaponHotbars)
                {
                    wb.DumpWeaponHotbar(memStream);
                }

                //Get Bank Item count
                memStream.Write(Utility_Funcs.DoublePack(MySession.MyCharacter.BankItems.Count));
                memStream.Write(BitConverter.GetBytes(MySession.MyCharacter.BankItems.Count));
                foreach (Item bi in MySession.MyCharacter.BankItems)
                {
                    bi.DumpItem(memStream);
                }

                // end of bank? or could be something else for memory dump
                memStream.WriteByte(0);

                //Buying auctions
                memStream.WriteByte((byte)MySession.MyCharacter.MyBuyingAuctions.Count);
                foreach (Auction ba in MySession.MyCharacter.MyBuyingAuctions)
                {
                    ba.DumpAuction(memStream);
                }

                //Selling auctions
                memStream.WriteByte((byte)MySession.MyCharacter.MySellingAuctions.Count);
                foreach (Auction sa in MySession.MyCharacter.MySellingAuctions)
                {
                    sa.DumpAuction(memStream);
                }

                //Spell count and Spells
                memStream.Write(Utility_Funcs.DoublePack(MySession.MyCharacter.MySpells.Count));
                foreach (Spell s in MySession.MyCharacter.MySpells)
                {
                    s.DumpSpell(memStream);
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
                long pos = memStream.Position;
                buffer = new Memory<byte>(memStream.GetBuffer(), 0, (int)pos);
            }

            int offset = 0;
            Memory<byte> temp = new byte[18];
            Span<byte> Message = temp.Span;

            //Get our timestamp opcode in queue
            Message.Write((ushort)GameOpcode.Time, ref offset);
            Message.Write(DNP3Creation.CreateDNP3TimeStamp(), ref offset);

            SessionQueueMessages.PackMessage(MySession, temp, MessageOpcodeTypes.ShortReliableMessage);

            SessionQueueMessages.PackMessage(MySession, buffer, MessageOpcodeTypes.ShortReliableMessage);

            //At this point, character should be loading in game, so we would want to get them added to the Player List and receiving any updates
            //MySession.inGame = true;

            //Put player into channel 0?
            MySession.rdpCommIn.connectionData.serverObjects.Span[0].AddObject(MySession.MyCharacter);

            //Add player to world player list queue
            IgnoreList(MySession);
            ActorSpeed(MySession);
        }

        public static void ProcessDelChar(Session MySession, PacketMessage ClientPacket)
        {
            int offset = 0;
            CharacterSQL deletedCharacter = new CharacterSQL();
            ReadOnlySpan<byte> temp = ClientPacket.Data.Span;
            //Passes in packet with ServerID on it, will grab, transform and return ServerID while also removing packet bytes
            int clientServID = temp.Get7BitDoubleEncodedInt(ref offset);

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
                offset = 0;
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

                ReadOnlyMemory<byte> Password = ClientPacket.Data.Slice(offset, 16);
                offset += 32;

                ///Uncomment once ready
                //MySession.AccountID = 3;
                MySession.AccountID = Convert.ToInt32(ConfigurationManager.AppSettings["StaticAccount"]);


                ///Theoretically we want to verify account # is not 0 here, if it is, drop it.
                if (MySession.AccountID == -1)
                {
                    //Verifications failed, drop session?
                    MySession.DropSession();
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
                character.EquipGear();

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
            Memory<byte> temp = new byte[6];
            Span<byte> Message = temp.Span;

            Message.Write(BitConverter.GetBytes((ushort)GameOpcode.ActorSpeed), ref offset);
            Message.Write(BitConverter.GetBytes(speed), ref offset);
            //For now send a standard speed
            SessionQueueMessages.PackMessage(MySession, temp, MessageOpcodeTypes.ShortReliableMessage);
        }
    }
}
