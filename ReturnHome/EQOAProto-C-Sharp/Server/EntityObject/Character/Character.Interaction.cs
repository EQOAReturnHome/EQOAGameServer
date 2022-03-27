// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the MIT license.

using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ReturnHome.Opcodes;
using ReturnHome.Server.Network;
using ReturnHome.Utilities;

namespace ReturnHome.Server.EntityObject.Player
{
    public partial class Character
    {

        public void DestroyItem(byte itemToDestroy, int quantityToDestroy)
        {
            //If this returns true, use quantityToDestroy
            if(Inventory.UpdateQuantity(itemToDestroy, quantityToDestroy, out Item item))
            {
                if (item.StackLeft <= 0)
                {
                    if (!Inventory.RemoveItem(itemToDestroy, out Item item2, out byte clientIndex2))
                        return;
                }

                int offset = 0;
                Memory<byte> temp = new byte[4 + Utility_Funcs.DoubleVariableLengthIntegerLength(quantityToDestroy)];
                Span<byte> message = temp.Span;

                message.Write((ushort)GameOpcode.RemoveInvItem, ref offset);
                message[offset++] = item.ClientIndex;
                message[offset++] = 1;
                message.Write7BitDoubleEncodedInt(quantityToDestroy, ref offset);

                SessionQueueMessages.PackMessage(characterSession, temp, MessageOpcodeTypes.ShortReliableMessage);
            }
        }

        //Rearranges item inventory for player, move item 1 to slot of item2 and reorder
        public void ArrangeItem(byte itemSlot1, byte itemSlot2)
        {
            if (Inventory.ArrangeItems(itemSlot1, itemSlot2, out byte clientItem1, out byte clientItem2))
            {
                //set offset
                int offset = 0;

                Console.WriteLine(clientItem1);
                Console.WriteLine(clientItem2);

                //Define Memory span
                Memory<byte> temp = new byte[4];
                Span<byte> Message = temp.Span;

                //Write arrangeop code back to memory span
                Message.Write((ushort)GameOpcode.ArrangeItem, ref offset);

                //send slot swap back to player to confirm
                Message.Write(clientItem1, ref offset);
                Message.Write(clientItem2, ref offset);

                //Send arrange op code back to player
                SessionQueueMessages.PackMessage(characterSession, temp, MessageOpcodeTypes.ShortReliableMessage);
            }
        }

        public void AddItem(Item itemToBeAdded)
        {
            if (Inventory.AddItem(itemToBeAdded))
            {
                Console.WriteLine($"{itemToBeAdded.ItemName} added.");
            }
        }

        //Method for withdrawing and depositing bank tunar
        public void BankTunar(uint targetNPC, uint giveOrTake, int transferAmount)
        {
            int offset = 0;

            //deposit transaction
            if (giveOrTake == 0)
            {
                if (transferAmount > Inventory.Tunar)
                {
                    Logger.Err($"Player: {CharName} Account: {characterSession.AccountID} attempted to add {transferAmount} to bank when only {Inventory.Tunar} on hand");
                    return;
                }

                //Remove from Inventory
                Inventory.RemoveTunar(transferAmount);

                //Add to bank
                Bank.AddTunar(transferAmount);
            }

            //withdraw transaction
            else if (giveOrTake == 1)
            {
                if (transferAmount > Bank.Tunar)
                {
                    Logger.Err($"Player: {CharName} Account: {characterSession.AccountID} attempted to remove {transferAmount} from bank when only {Bank.Tunar}");
                    return;
                }

                //remove from bank
                Bank.RemoveTunar(transferAmount);

                //Add To inventory
                Inventory.AddTunar(transferAmount);
            }

            Memory<byte> playerTemp = new byte[2 + Utility_Funcs.DoubleVariableLengthIntegerLength(Inventory.Tunar)];
            Span<byte> messagePlayer = playerTemp.Span;
            
            Memory<byte> bankTemp = new byte[2 + Utility_Funcs.DoubleVariableLengthIntegerLength(Bank.Tunar)];
            Span<byte> messageBank = bankTemp.Span;
            //reset span offset and write player amount to Message
            offset = 0;
            messagePlayer.Write((ushort)GameOpcode.PlayerTunar, ref offset);
            messagePlayer.Write7BitDoubleEncodedInt(Inventory.Tunar, ref offset);
            //resete span offset and write bank amount to message
            offset = 0;
            messageBank.Write((ushort)GameOpcode.ConfirmBankTunar, ref offset);
            messageBank.Write7BitDoubleEncodedInt(Bank.Tunar, ref offset);
            //send both messages to client 
            SessionQueueMessages.PackMessage(characterSession, playerTemp, MessageOpcodeTypes.ShortReliableMessage);
            SessionQueueMessages.PackMessage(characterSession, bankTemp, MessageOpcodeTypes.ShortReliableMessage);
            return;
        }

        public void TransferItem(byte giveOrTake, byte itemToTransfer, int qtyToTransfer)
        {
            Memory<byte> buffer;

            //Deposit Item
            if (giveOrTake == 0)
            {
                //Remove item from Inventory
                if (Inventory.RemoveItem(itemToTransfer, out Item item, out byte clientIndex))
                {
                    int offset = 0;
                    Memory<byte> temp = new byte[4 + Utility_Funcs.DoubleVariableLengthIntegerLength(item.StackLeft)];
                    Span<byte> message = temp.Span;

                    message.Write((ushort)GameOpcode.RemoveInvItem, ref offset);
                    message[offset++] = clientIndex;
                    message[offset++] = 1;
                    message.Write7BitDoubleEncodedInt(item.StackLeft, ref offset);

                    SessionQueueMessages.PackMessage(characterSession, temp, MessageOpcodeTypes.ShortReliableMessage);

                    //Deposit into bank
                    Bank.AddItem(item);

                    using (MemoryStream memStream = new())
                    {

                        memStream.Write(BitConverter.GetBytes((ushort)GameOpcode.AddBankItem));

                        item.DumpItem(memStream);
                        long pos = memStream.Position;
                        buffer = new Memory<byte>(memStream.GetBuffer(), 0, (int)pos);

                        SessionQueueMessages.PackMessage(characterSession, buffer, MessageOpcodeTypes.ShortReliableMessage);
                    }
                }
            }

            //Pull from bank
            else if (giveOrTake == 1)
            {
                //Remove item from bank
                if (Bank.RemoveItem(itemToTransfer, out Item item, out byte clientIndex))
                {
                    int offset = 0;
                    Memory<byte> temp = new byte[3 + Utility_Funcs.DoubleVariableLengthIntegerLength(item.StackLeft)];
                    Span<byte> message = temp.Span;

                    message.Write((ushort)GameOpcode.RemoveBankItem, ref offset);
                    message[offset++] = clientIndex;
                    message.Write7BitDoubleEncodedInt(item.StackLeft, ref offset);

                    SessionQueueMessages.PackMessage(characterSession, temp, MessageOpcodeTypes.ShortReliableMessage);

                    //Deposit into inventory
                    Inventory.AddItem(item);

                    using (MemoryStream memStream = new())
                    {

                        memStream.Write(BitConverter.GetBytes((ushort)GameOpcode.AddInvItem));

                        item.DumpItem(memStream);
                        long pos = memStream.Position;
                        buffer = new Memory<byte>(memStream.GetBuffer(), 0, (int)pos);

                        SessionQueueMessages.PackMessage(characterSession, buffer, MessageOpcodeTypes.ShortReliableMessage);
                    }
                }
            }
        }

        public void SellItem(byte itemSlot, int itemQty, uint targetNPC)
        {
            if(Inventory.Exists(itemSlot))
            {
                if(Inventory.UpdateQuantity(itemSlot, itemQty, out Item item))
                {
                    Inventory.AddTunar((int)(item.Maxhp == 0 ? item.ItemCost * itemQty : item.ItemCost * (item.RemainingHP / item.Maxhp) * itemQty));

                    //Adjust player tunar
                    int offset = 0;
                    Memory<byte> playerTemp = new byte[2 + Utility_Funcs.DoubleVariableLengthIntegerLength(Inventory.Tunar)];
                    Span<byte> messagePlayer = playerTemp.Span;

                    messagePlayer.Write((ushort)GameOpcode.PlayerTunar, ref offset);
                    messagePlayer.Write7BitDoubleEncodedInt(Inventory.Tunar, ref offset);

                    SessionQueueMessages.PackMessage(characterSession, playerTemp, MessageOpcodeTypes.ShortReliableMessage);

                    if (item.StackLeft <= 0)
                    {
                        if (!Inventory.RemoveItem(itemSlot, out Item _, out byte _))
                            return;
                    }

                    offset = 0;
                    Memory<byte> temp = new byte[4 + Utility_Funcs.DoubleVariableLengthIntegerLength(itemQty)];
                    Span<byte> message = temp.Span;

                    message.Write((ushort)GameOpcode.RemoveInvItem, ref offset);
                    message[offset++] = item.ClientIndex; ;
                    message[offset++] = 1;
                    message.Write7BitDoubleEncodedInt(itemQty, ref offset);

                    SessionQueueMessages.PackMessage(characterSession, temp, MessageOpcodeTypes.ShortReliableMessage);
                }
            }
        }
    }
}
