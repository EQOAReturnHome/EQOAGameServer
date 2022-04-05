using System;
using System.Collections.Generic;
using System.IO;
using ReturnHome.Database.SQL;
using ReturnHome.Server.EntityObject.Player;
using ReturnHome.Server.Network;
using ReturnHome.Utilities;

namespace ReturnHome.Server.Opcodes.Messages.Server
{
    class ServerMemoryDump
    {
        public static void MemoryDump(Session session)
        {
            Memory<byte> temp = new byte[40000];
            Span<byte> Message = temp.Span;

            BufferWriter writer = new BufferWriter(Message);

            //Perform SQl stuff
            CharacterSQL charDump = new CharacterSQL();

            //Probably change this to only pass in character ServerID
            charDump.GetPlayerHotkeys(session);
            charDump.GetPlayerWeaponHotbar(session);
            charDump.GetPlayerSpells(session);

            //Toss opcode in
            writer.Write((ushort)GameOpcode.MemoryDump);

            session.MyCharacter.DumpCharacter(ref writer);
            writer.Write7BitEncodedInt64(session.MyCharacter.MyHotkeys.Count);

            //cycle over all our hotkeys and append them
            foreach (Hotkey h in session.MyCharacter.MyHotkeys)
                h.PullHotkey(ref writer);

            //Unknown at this time 4 byte null
            writer.Write(0);

            //Unknown at this time 4 byte null
            writer.Write(0);

            //Quest Count
            writer.Write(session.MyCharacter.MyQuests.Count);

            //Iterate over quest data and append (Should be 0 for now...)
            foreach (Quest q in session.MyCharacter.MyQuests)
                q.DumpQuest(ref writer);

            //Get Inventory Item count
            writer.Write7BitEncodedInt64(session.MyCharacter.Inventory.Count);
            writer.Write(session.MyCharacter.Inventory.Count);

            foreach (KeyValuePair<byte, Item> entry in session.MyCharacter.Inventory.itemContainer)
                entry.Value.DumpItem(ref writer);

            //While we are here, lets "equip" our equipped gear
            session.MyCharacter.EquipGear(session.MyCharacter);

            foreach (WeaponHotbar wb in session.MyCharacter.WeaponHotbars)
                wb.DumpWeaponHotbar(ref writer);

            //Get Bank Item count
            writer.Write7BitEncodedInt64(session.MyCharacter.Bank.Count);
            writer.Write(session.MyCharacter.Bank.Count);
            foreach (KeyValuePair<byte, Item> entry in session.MyCharacter.Bank.itemContainer)
                entry.Value.DumpItem(ref writer);

            // end of bank? or could be something else for memory dump
            writer.Write((byte)0);

            //Buying auctions
            writer.Write((byte)session.MyCharacter.MyBuyingAuctions.Count);
            foreach (Auction ba in session.MyCharacter.MyBuyingAuctions)
                ba.DumpAuction(ref writer);

            //Selling auctions
            writer.Write((byte)session.MyCharacter.MySellingAuctions.Count);
            foreach (Auction sa in session.MyCharacter.MySellingAuctions)
                sa.DumpAuction(ref writer);

            //Spell count and Spells
            writer.Write7BitEncodedInt64(session.MyCharacter.MySpells.Count);
            foreach (Spell s in session.MyCharacter.MySpells)
                s.DumpSpell(ref writer);

            //Not entirely known what this is at this time
            //Related to stats and CM's possibly. Needs testing, just using data from a pcap of live.
            writer.Write(new byte[] {                  0x55, 0x55, 0x0d, 0x41, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x01, 0x00, 0x00,
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


            ServerTime.Time(session);

            Memory<byte> buffer = temp.Slice(0, writer.Position);

            SessionQueueMessages.PackMessage(session, buffer, MessageOpcodeTypes.ShortReliableMessage);

            //At this point, character should be loading in game, so we would want to get them added to the Player List and receiving any updates
            //session.inGame = true;

            //Put player into channel 0?
            session.rdpCommIn.connectionData.serverObjects.Span[0].AddObject(session.MyCharacter);

            //Add player to world player list queue
            ServerPlayerIgnoreList.PlayerIgnoreList(session);
            ServerPlayerSpeed.PlayerSpeed(session);
        }
    }
}
