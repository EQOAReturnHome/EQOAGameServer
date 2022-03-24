﻿using System;
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
using System.Text.Json;

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
            { GameOpcode.BankItem, InteractActor },
            { GameOpcode.DeleteQuest, DeleteQuest },
            { GameOpcode.MerchantBuy, InteractActor },
            { GameOpcode.MerchantSell, InteractActor },
            { GameOpcode.ArrangeItem, InteractActor },
            { GameOpcode.RemoveInvItem, DeleteItem },
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

            BufferReader reader = new(ClientPacket.Data.Span);

            //This could be useful later if real names are created per race/sex
            ///Get Race Byte
            byte Race = reader.Read<byte>();

            ///Make sure Message number is expected, needs to be in order.
            byte sex = reader.Read<byte>();


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
                string message = $"Unknown Opcode: {opcode.ToString("X")}";

                ChatMessage.GenerateClientSpecificChat(MySession, message);
            }
        }

        public static void ChangeChatMode(Session MySession, PacketMessage ClientPacket)
        {
            //Just accept and change chat mode
            MySession.MyCharacter.chatMode = ClientPacket.Data.Span[0];
        }

        public static void DeleteQuest(Session mySession, PacketMessage clientPacket)
        {
            BufferReader reader = new(clientPacket.Data.Span);

            byte unknownSection = reader.Read<byte>();
            byte questNumber = reader.Read<byte>();

            Character.DeleteQuest(mySession, questNumber);
        }

        public static void AddInvItem(Session MySession, Item item)
        {
        }

        public static void PlayerTarget(Session mySession, PacketMessage clientPacket)
        {
            BufferReader reader = new(clientPacket.Data.Span);

            //First 4 bytes is targeting counter, just discarding for now
            _ = reader.Read<uint>();
            uint targetID = reader.Read<uint>();

            mySession.MyCharacter.Target = targetID;
            mySession.TargetUpdate();
        }

        /*Some thoughts
         * In Dustin's Database under NPCType, certain npc's have a specific value
        If it has 0x80+, it is/should be unattackable
        Bankers are 0x02. So most likely 0x82 for unattackable and banker
        Coachmen are 0x0100, so 0x0180 for coachmen and unattackable
        */

        public static void InteractActor(Session MySession, PacketMessage clientPacket)
        {
            BufferReader reader = new(clientPacket.Data.Span);

            if (clientPacket.Header.Opcode == (ushort)GameOpcode.ArrangeItem)
            {
                byte itemSlot1 = (byte)reader.Read<uint>();
                byte itemSlot2 = (byte)reader.Read<uint>();
                MySession.MyCharacter.ArrangeItem(itemSlot1, itemSlot2);
            }

            //Merchant Buy
            if (clientPacket.Header.Opcode == (ushort)GameOpcode.MerchantBuy)
            {
                byte itemSlot = (byte)reader.Read7BitEncodedInt64();
                int itemQty = (int)reader.Read7BitEncodedInt64();
                uint targetNPC = reader.Read<uint>();
                MySession.MyCharacter.MerchantBuy(itemSlot, itemQty, targetNPC);
            }

            //Merchant Sell
            if (clientPacket.Header.Opcode == (ushort)GameOpcode.MerchantSell)
            {
                byte itemSlot = (byte)reader.Read<int>();
                int itemQty = (int)reader.Read7BitEncodedInt64();
                uint targetNPC = reader.Read<uint>();
                //We just need to verify the player is talking to a merchant and within range here, just let it work for now
                MySession.MyCharacter.SellItem(itemSlot, itemQty, targetNPC);

            }

            //Merchant popup window
            if (clientPacket.Header.Opcode == (ushort)GameOpcode.MerchantDiag)
            {
                uint targetNPC = reader.Read<uint>();
                MySession.MyCharacter.TriggerMerchant(targetNPC);
            }


            //Bank popup window
            if (clientPacket.Header.Opcode == (ushort)GameOpcode.BankUI)
            {
                int offset = 0;
                Memory<byte> temp = new byte[2];
                Span<byte> Message = temp.Span;
                Message.Write((ushort)GameOpcode.BankUI, ref offset);
                SessionQueueMessages.PackMessage(MySession, temp, MessageOpcodeTypes.ShortReliableMessage);
            }

            //Deposit and Withdraw Bank Tunar
            if (clientPacket.Header.Opcode == (ushort)GameOpcode.DepositBankTunar)
            {

                uint targetNPC = reader.Read<uint>();
                uint giveOrTake = (uint)reader.Read7BitEncodedUInt64();
                int transferAmount = (int)reader.Read7BitEncodedInt64();
                Console.WriteLine("In the bank tunar");
                MySession.MyCharacter.BankTunar(targetNPC, giveOrTake, transferAmount);

            }

            //Deposit and Withdraw Bank item
            if (clientPacket.Header.Opcode == (ushort)GameOpcode.BankItem)
            {

                uint targetNPC = reader.Read<uint>();
                byte giveOrTake = reader.Read<byte>();
                byte itemToTransfer = (byte)reader.Read<uint>();
                int qtyToTransfer = (int)reader.Read7BitEncodedInt64();
                Console.WriteLine("In the Item op code");
                MySession.MyCharacter.TransferItem(giveOrTake, itemToTransfer, qtyToTransfer);

            }

            //Dialogue and Quest Interaction
            if (clientPacket.Header.Opcode == (ushort)GameOpcode.Interact || clientPacket.Header.Opcode == (ushort)GameOpcode.DialogueBoxOption)
            {
                MySession.MyCharacter.ProcessDialogue(MySession, reader, clientPacket);
            }
        }

        public static void DeleteItem(Session MySession, PacketMessage ClientPacket)
        {
            BufferReader reader = new(ClientPacket.Data.Span);

            byte itemToDestroy = (byte)reader.Read<uint>();
            int quantityToDestroy = (int)reader.Read<uint>();

            MySession.MyCharacter.DestroyItem(itemToDestroy, quantityToDestroy);
        }

        public static void DisconnectClient(Session MySession, PacketMessage ClientPacket)
        {
            MySession.PendingTermination = true;
            //Create new handle for mysql connection
            CharacterSQL savePlayerData = new();

            //Call the mysql update query to save player data
            savePlayerData.SavePlayerData(MySession.MyCharacter, (string)Newtonsoft.Json.JsonConvert.SerializeObject(MySession.MyCharacter.playerFlags));
            //Actually drop the player's session
            MySession.DropSession();
        }

        public static void ProcessCharacterChanges(Session MySession, PacketMessage ClientPacket)
        {
            BufferReader reader = new(ClientPacket.Data.Span);

            //Retrieve CharacterID from client
            int ServerID = reader.Read<int>();
            int FaceOption = reader.Read<int>();
            int HairStyle = reader.Read<int>();
            int HairLength = reader.Read<int>();
            int HairColor = reader.Read<int>();

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

                foreach (KeyValuePair<byte, Item> entry in MySession.MyCharacter.Inventory.itemContainer)
                {
                    entry.Value.DumpItem(memStream);
                }

                //While we are here, lets "equip" our equipped gear
                MySession.MyCharacter.EquipGear(MySession.MyCharacter);

                foreach (WeaponHotbar wb in MySession.MyCharacter.WeaponHotbars)
                {
                    wb.DumpWeaponHotbar(memStream);
                }

                //Get Bank Item count
                memStream.Write(Utility_Funcs.DoublePack(MySession.MyCharacter.Bank.Count));
                memStream.Write(BitConverter.GetBytes(MySession.MyCharacter.Bank.Count));
                foreach (KeyValuePair<byte, Item> entry in MySession.MyCharacter.Bank.itemContainer)
                {
                    entry.Value.DumpItem(memStream);
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
            CharacterSQL deletedCharacter = new CharacterSQL();

            BufferReader reader = new(ClientPacket.Data.Span);
            //Passes in packet with ServerID on it, will grab, transform and return ServerID while also removing packet bytes
            int clientServID = reader.Read<int>();

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
            BufferReader reader = new(ClientPacket.Data.Span);

            //Get length of characters name expected in packet
            int nameLength = reader.Read<int>();

            //Get Character Name
            string CharName = reader.ReadString(Encoding.UTF8, nameLength);

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
                charCreation.Level = (int)reader.Read7BitEncodedInt64();

                //Divide startLevel by 2 because client doubles it
                //Get single byte attributes
                charCreation.Race = (int)reader.Read7BitEncodedInt64();
                charCreation.StartingClass = (int)reader.Read7BitEncodedInt64();
                charCreation.Gender = (int)reader.Read7BitEncodedInt64();
                charCreation.HairColor = (int)reader.Read7BitEncodedInt64();
                charCreation.HairLength = (int)reader.Read7BitEncodedInt64();
                charCreation.HairStyle = (int)reader.Read7BitEncodedInt64();
                charCreation.FaceOption = (int)reader.Read7BitEncodedInt64();
                charCreation.HumTypeNum = (int)reader.Read7BitEncodedInt64();

                //Get player attributes from packet and remove bytes after reading into variable
                charCreation.AddStrength = reader.Read<int>();
                charCreation.AddStamina = reader.Read<int>();
                charCreation.AddAgility = reader.Read<int>();
                charCreation.AddDexterity = reader.Read<int>();
                charCreation.AddWisdom = reader.Read<int>();
                charCreation.AddIntelligence = reader.Read<int>();
                charCreation.AddCharisma = reader.Read<int>();

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
            BufferReader reader = new(ClientPacket.Data.Span);

            ///Gets Gameversion sent by client
            int GameVersion = reader.Read<int>();

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
            BufferReader reader = new(ClientPacket.Data.Span);

            Logger.Info("Processing Authentication");
            ///Opcode option? just skip for now
            reader.Position = 5;

            ///Game Code Length
            int GameCodeLength = reader.Read<int>();

            ///the actual gamecode
            string GameCode = reader.ReadString(Encoding.UTF8, GameCodeLength);

            if (GameCode == "EQOA")
            {
                ///Authenticate
                Logger.Info("Received EQOA Game Code, continuing...");

                ///Account name Length
                int AccountNameLength = reader.Read<int>();

                ///the actual gamecode
                string AccountName = reader.ReadString(Encoding.UTF8, AccountNameLength);

                Logger.Info($"Received Account Name: {AccountName}");

                ///Username ends with 01, no known use, skip for now
                reader.Position += 1;

                ReadOnlyMemory<byte> Password = reader.ReadArray<byte>(16);
                reader.Position += 16;

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

        public static void ActorSpeed(Session MySession, PacketMessage Message)
        {

        }
    }
}
