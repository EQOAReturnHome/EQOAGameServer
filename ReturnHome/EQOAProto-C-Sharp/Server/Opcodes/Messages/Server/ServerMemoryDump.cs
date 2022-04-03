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
            Memory<byte> buffer;

            //Perform SQl stuff
            CharacterSQL charDump = new CharacterSQL();

            //Probably change this to only pass in character ServerID
            charDump.GetPlayerHotkeys(session);
            charDump.GetPlayerWeaponHotbar(session);
            charDump.GetPlayerSpells(session);

            using (MemoryStream memStream = new())
            {
                //Toss opcode in
                memStream.Write(BitConverter.GetBytes((ushort)GameOpcode.MemoryDump));

                session.MyCharacter.DumpCharacter(memStream);
                memStream.Write(Utility_Funcs.DoublePack(session.MyCharacter.MyHotkeys.Count));

                //cycle over all our hotkeys and append them
                foreach (Hotkey h in session.MyCharacter.MyHotkeys)
                {
                    h.PullHotkey(memStream);
                }

                //Unknown at this time 4 byte null
                memStream.Write(new byte[4]);

                //Unknown at this time 4 byte null
                memStream.Write(new byte[4]);

                //Quest Count
                memStream.Write(BitConverter.GetBytes(session.MyCharacter.MyQuests.Count));

                //Iterate over quest data and append (Should be 0 for now...)
                foreach (Quest q in session.MyCharacter.MyQuests)
                {
                    q.DumpQuest(memStream);
                }

                //Get Inventory Item count
                memStream.Write(Utility_Funcs.DoublePack(session.MyCharacter.Inventory.Count));
                memStream.Write(BitConverter.GetBytes(session.MyCharacter.Inventory.Count));

                foreach (KeyValuePair<byte, Item> entry in session.MyCharacter.Inventory.itemContainer)
                {
                    entry.Value.DumpItem(memStream);
                }

                //While we are here, lets "equip" our equipped gear
                session.MyCharacter.EquipGear(session.MyCharacter);

                foreach (WeaponHotbar wb in session.MyCharacter.WeaponHotbars)
                {
                    wb.DumpWeaponHotbar(memStream);
                }

                //Get Bank Item count
                memStream.Write(Utility_Funcs.DoublePack(session.MyCharacter.Bank.Count));
                memStream.Write(BitConverter.GetBytes(session.MyCharacter.Bank.Count));
                foreach (KeyValuePair<byte, Item> entry in session.MyCharacter.Bank.itemContainer)
                {
                    entry.Value.DumpItem(memStream);
                }

                // end of bank? or could be something else for memory dump
                memStream.WriteByte(0);

                //Buying auctions
                memStream.WriteByte((byte)session.MyCharacter.MyBuyingAuctions.Count);
                foreach (Auction ba in session.MyCharacter.MyBuyingAuctions)
                {
                    ba.DumpAuction(memStream);
                }

                //Selling auctions
                memStream.WriteByte((byte)session.MyCharacter.MySellingAuctions.Count);
                foreach (Auction sa in session.MyCharacter.MySellingAuctions)
                {
                    sa.DumpAuction(memStream);
                }

                //Spell count and Spells
                memStream.Write(Utility_Funcs.DoublePack(session.MyCharacter.MySpells.Count));
                foreach (Spell s in session.MyCharacter.MySpells)
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

            SessionQueueMessages.PackMessage(session, temp, MessageOpcodeTypes.ShortReliableMessage);

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
