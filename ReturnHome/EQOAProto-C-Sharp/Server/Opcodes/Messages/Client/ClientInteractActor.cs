using System;
using ReturnHome.Server.Network;
using ReturnHome.Utilities;

namespace ReturnHome.Server.Opcodes.Messages.Client
{
    class ClientInteractActor
    {
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
                MySession.MyCharacter.TriggerMerchantMenu(targetNPC);
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
    }
}
