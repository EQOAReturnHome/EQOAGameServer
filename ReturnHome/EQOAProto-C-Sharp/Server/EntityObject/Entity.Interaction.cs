
using System;
using System.Collections.Generic;
using System.Numerics;
using System.Text;
using System.Linq;
using ReturnHome.Opcodes;
using ReturnHome.Server.EntityObject.Player;
using ReturnHome.Server.Managers;
using ReturnHome.Server.Network;
using ReturnHome.Utilities;
using NLua;
using ReturnHome.Opcodes.Chat;
using System.IO;

namespace ReturnHome.Server.EntityObject
{
    public partial class Entity
    {
        public ItemContainer Inventory;
        private uint _target;
        private uint _targetCounter = 1;
        private Entity _ourTarget;

        public uint Target
        {
            get { return _target; }
            set
            {
                if (true)
                {
                    _target = value;
                    ObjectUpdateTarget();

                    //Keep a reference to our current target on hand
                    EntityManager.QueryForEntity(_target, out _ourTarget);

                    if (isPlayer && ObjectID != 0)
                    {
                        //Get target information about the object
                        TargetInformation(_target);
                    }
                }
            }
        }

        public void TargetInformation(uint Target)
        {
            Entity ent;
            Memory<byte> message;
            //If false, ignore? Might need some kind of escalation, why would we target something not known about?
            //If true, prep message
            if (EntityManager.QueryForEntity(_target, out ent))
            {
                //This shouldn't happen, but to be safe? Eventually could be an expired object that was originally target?
                if (ent == null)
                    return;

                int offset = 0;

                //This message type seems to always be this length from live packet captures?
                message = new byte[0x0109];

                Span<byte> temp = message.Span;
                temp.Write((ushort)GameOpcode.TargetInformation, ref offset);
                temp.Write((byte)3, ref offset); // 0/1 = red face 2/3 = neutral face 4/5 = blue face //Perform Calculations to check for 
                temp.Write(GenerateConColor(), ref offset); // 0 = red con 1 = yellow con 2 = white con 3 = Dark Blue con 4 = Light Blue Con 5 = Green con 6 = Yellowish/white con? 7 = no con at all? But can still target? 14 = faded yellow con? 15 = faded orange con? 60 = yellowish/green con?

                offset = 124;
                temp.Write(_target, ref offset);

                offset = 261;
                temp.Write(_targetCounter++, ref offset);
                SessionQueueMessages.PackMessage(((Character)this).characterSession, message, MessageOpcodeTypes.ShortReliableMessage);

                message = new byte[2];
                offset = 0;
                temp = message.Span;
                temp.Write((ushort)0x63, ref offset);
                SessionQueueMessages.PackMessage(((Character)this).characterSession, message, MessageOpcodeTypes.ShortReliableMessage);
            }
        }

        public bool IsWithinRange()
        {
            //What should be the distance check against interacting with NPC's?
            //Should this distance check be farther or shorter for attacking in combat, too? Such as auto attack
            if (Vector3.Distance(Position, _ourTarget.Position) <= 10.0f)
                return true;
            return false;

        }

        //Method use to determine Entities target con color. Specifically for Characters
        //but can be altered some to introduce if an Actor should attack a player based on level
        private byte GenerateConColor()
        {

            sbyte difference = (sbyte)(Level - _ourTarget.Level);

            switch (difference)
            {
                //Red con target
                case <= -3:
                    return 0;

                //Yellow con'ing target
                case -2:
                case -1:
                    return 1;

                //White con target
                case 0:
                    return 2;

                //Dark Blue con target
                case 1:
                case 2:
                    return 3;

                //Decide Light blue and green con targets here
                default:

                    //return (byte)(Level <= 15 ? (Level - _ourTarget.Level) > 3 ? 5 : 4 : (Level - _ourTarget.Level) > (Level / 4) ? 5 : 4);
                    if (Level <= 15)
                    {
                        //Green con target
                        if ((Level - _ourTarget.Level) > 3)
                            return 5;

                        //Light blue con target
                        else
                            return 4;
                    }

                    else
                    {
                        //Green con target
                        if ((Level - _ourTarget.Level) > (Level / 4))
                            return 5;

                        //Light blue con target
                        else
                            return 4;
                    }
            }
        }

        public void BankItem(uint targetNPC, byte giveOrTake, uint itemToTransfer)
        {
            //Deposit Item
            if (giveOrTake == 0)
            {

            }

            else if (giveOrTake == 1)
            {

            }
        }

        //This is wrong... We are just referencing the npc item's state to a character here where the state will be shared to all users
        //Need to some how make a new copy of it, not sure the easiest way to do that
        public void MerchantBuy(byte itemSlot, int itemQty, uint targetNPC)
        {
            Memory<byte> buffer;

            if (EntityManager.QueryForEntity(targetNPC, out Entity npc))
            {
                npc.Inventory.RetrieveItem(itemSlot, out Item item);
                Inventory.RemoveTunar((int)(item.ItemCost * itemQty));

                //Adjust player tunar
                int offset = 0;
                Memory<byte> playerTemp = new byte[2 + Utility_Funcs.DoubleVariableLengthIntegerLength(Inventory.Tunar)];
                Span<byte> messagePlayer = playerTemp.Span;

                messagePlayer.Write((ushort)GameOpcode.PlayerTunar, ref offset);
                messagePlayer.Write7BitDoubleEncodedInt(Inventory.Tunar, ref offset);

                SessionQueueMessages.PackMessage(((Character)this).characterSession, playerTemp, MessageOpcodeTypes.ShortReliableMessage);


                Item newItem = item.AcquireItem(itemQty);

                Inventory.AddItem(newItem);

                using (MemoryStream memStream = new())
                {

                    memStream.Write(BitConverter.GetBytes((ushort)GameOpcode.AddInvItem));

                    newItem.DumpItem(memStream);
                    long pos = memStream.Position;
                    buffer = new Memory<byte>(memStream.GetBuffer(), 0, (int)pos);

                    SessionQueueMessages.PackMessage(((Character)this).characterSession, buffer, MessageOpcodeTypes.ShortReliableMessage);
                }
            }
        }

        public void TriggerMerchant(uint targetNPC)
        {
            int unknownInt = 200;

            Memory<byte> buffer;

            if (EntityManager.QueryForEntity(targetNPC, out Entity npc))
            {
                Entity merchantNPC = npc;

                using (MemoryStream memStream = new())
                {
                    memStream.Write(BitConverter.GetBytes((ushort)GameOpcode.MerchantBox));
                    memStream.Write(BitConverter.GetBytes(targetNPC));
                    memStream.Write(Utility_Funcs.DoublePack(unknownInt));
                    memStream.Write(Utility_Funcs.DoublePack(unknownInt));
                    memStream.Write(Utility_Funcs.DoublePack(merchantNPC.Inventory.Count));
                    memStream.Write(BitConverter.GetBytes(merchantNPC.Inventory.Count));
                    foreach (Item entry in merchantNPC.Inventory.itemContainer.Values)
                        entry.DumpItem(memStream);

                    long pos = memStream.Position;
                    buffer = new Memory<byte>(memStream.GetBuffer(), 0, (int)pos);

                    SessionQueueMessages.PackMessage(((Character)this).characterSession, buffer, MessageOpcodeTypes.ShortReliableMessage);
                    memStream.Flush();
                }
            }
        }

        //Method used to send any in game dialogue to player. Works for option box or regular dialogue box
        public void SendDialogue(Session mySession, string dialogue, LuaTable diagOptions)
        {
            //Clear player's previous dialogue options before adding new ones.
            mySession.MyCharacter.MyDialogue.diagOptions.Clear();
            //loop over luatable, assigning every value to an element of the players diagOptions list
            //lua table always returns a dict type object of <object,object>
            foreach (KeyValuePair<object, object> k in diagOptions)
            {
                mySession.MyCharacter.MyDialogue.diagOptions.Add(k.Value.ToString());
            }
            //If it's not a yes/no choice then sort alphabetically.
            //This forces it to return choices the same every time.
            if (!mySession.MyCharacter.MyDialogue.diagOptions.Contains("Yes"))
            {
                mySession.MyCharacter.MyDialogue.diagOptions.Sort();
            }
            int offset = 0;
            //Length of choices
            uint choicesLength = 0;
            //set choice counter for player
            uint choiceCounter = mySession.MyCharacter.MyDialogue.counter;
            byte textOptions = 0;
            GameOpcode dialogueType = GameOpcode.DialogueBoxOption;
            //if dialogue options exist set choice counter update
            if (mySession.MyCharacter.MyDialogue.diagOptions != null)
            {
                choiceCounter = (uint)mySession.MyCharacter.MyDialogue.diagOptions.Count;
                //Length of choices
                foreach (string choice in mySession.MyCharacter.MyDialogue.diagOptions)
                {
                    choicesLength += (uint)choice.Length;
                }
                //count the number of textOptions
                textOptions = (byte)mySession.MyCharacter.MyDialogue.diagOptions.Count;
                //set dialogue type to option
                dialogueType = GameOpcode.OptionBox;
            }
            //if there are no diagOptions send onlly diag box.
            if (mySession.MyCharacter.MyDialogue.diagOptions.Count <= 0)
            {
                dialogueType = GameOpcode.DialogueBox;
            }
            //set player dialogue to the incoming dialogue
            mySession.MyCharacter.MyDialogue.dialogue = dialogue;
            //create variable memory span for sending out dialogue
            Memory<byte> temp = new Memory<byte>(new byte[11 + (dialogue.Length * 2) + (choiceCounter * 4) + 1 + (choicesLength * 2)]);
            Span<byte> Message = temp.Span;
            //Write diag type, counter, dialogue length, and actual text to memssage
            Message.Write((ushort)dialogueType, ref offset);
            Message.Write(choiceCounter, ref offset);
            Message.Write(dialogue.Length, ref offset);
            Message.Write(Encoding.Unicode.GetBytes(dialogue), ref offset);
            //if it's an option box iterate through options and write options to message
            if (dialogueType == GameOpcode.OptionBox)
            {
                Message.Write(textOptions, ref offset);

                if (mySession.MyCharacter.MyDialogue.diagOptions != null)
                {
                    for (int i = 0; i < mySession.MyCharacter.MyDialogue.diagOptions.Count; i++)
                    {
                        Message.Write(mySession.MyCharacter.MyDialogue.diagOptions[i].Length, ref offset);
                        Message.Write(Encoding.Unicode.GetBytes(mySession.MyCharacter.MyDialogue.diagOptions[i]), ref offset);
                    }
                }
            }
            //Send Message
            SessionQueueMessages.PackMessage(mySession, temp, MessageOpcodeTypes.ShortReliableMessage);
            mySession.MyCharacter.MyDialogue.choice = 1000;
        }

        public void ProcessDialogue(Session mySession, BufferReader reader, PacketMessage ClientPacket)
        {
            //set offset
            int offset = 0;
            uint interactTarget = 0;
            //Read the incoming message and get the objectID that was interacted with

            if (ClientPacket.Header.Opcode == (ushort)GameOpcode.Interact)
            {
                interactTarget = reader.Read<uint>();
            }
            //if option choice incoming
            if (ClientPacket.Header.Opcode == (ushort)GameOpcode.DialogueBoxOption)
            {
                //try to pull the option counter and players choice out of message
                try
                {
                    offset = 0;
                    uint optionCounter = reader.Read<uint>();
                    mySession.MyCharacter.MyDialogue.choice = reader.Read<byte>();
                    //if diag message is 255(exit dialogue in client) return immediately without new message
                    //and set choice to incredibly high number to make sure it doesn't retrigger any specific dialogue.
                    if (mySession.MyCharacter.MyDialogue.choice == 255)
                    {
                        mySession.MyCharacter.MyDialogue.choice = 1000;
                        return;
                    }
                }
                catch (Exception)
                {
                    throw;
                }
            }
            //Create new instance of the event manager
            //EventManager eManager = new EventManager();
            Entity npcEntity = new Entity(false);
            Dialogue dialogue = mySession.MyCharacter.MyDialogue;
            GameOpcode dialogueType = GameOpcode.DialogueBoxOption;
            //if a diag option choice incoming set outgoing to diag box option
            if (ClientPacket.Header.Opcode == (ushort)GameOpcode.DialogueBoxOption)
            {
                dialogueType = GameOpcode.DialogueBoxOption;
            }
            //else this is just a regular interaction with only dialogue
            else if (ClientPacket.Header.Opcode == (ushort)GameOpcode.Interact)
            {
                dialogueType = GameOpcode.DialogueBox;
            }
            //Reset offset for outgoing message
            offset = 0;
            //Gets NPC name from ObjectID
            if (ClientPacket.Header.Opcode == (ushort)GameOpcode.Interact)
            {
                if (EntityManager.QueryForEntity(interactTarget, out Entity npc))
                {
                    dialogue.npcName = npc.CharName;
                }
            }
            //call the event manager to process dialogue from here. Passes to Lua scripts.
            EventManager.GetNPCDialogue(dialogueType, mySession);
        }

        public static void AddQuestLog(Session mySession, uint questNumber, string questText)
        {
            int offset = 0;

            Memory<byte> temp = new Memory<byte>(new byte[11 + (questText.Length * 2)]);
            Span<byte> Message = temp.Span;

            Message.Write((ushort)GameOpcode.AddQuestLog, ref offset);
            Message.Write(questNumber, ref offset);
            Message.Write(questText.Length, ref offset);
            Message.Write(Encoding.Unicode.GetBytes(questText), ref offset);
            SessionQueueMessages.PackMessage(mySession, temp, MessageOpcodeTypes.ShortReliableMessage);
        }

        public static void DeleteQuest(Session mySession, byte questNumber)
        {
            int offset = 0;

            Memory<byte> temp = new Memory<byte>(new byte[8]);
            Span<byte> Message = temp.Span;

            Message.Write((ushort)GameOpcode.DeleteQuest, ref offset);
            Message.Write(new byte[4], ref offset);
            Message.Write(questNumber, ref offset);


            SessionQueueMessages.PackMessage(mySession, temp, MessageOpcodeTypes.ShortReliableMessage);
        }

        public static void GrantXP(Session mySession, int xpAmount)
        {
            int debt = 0;
            int offset = 0;
            int totXP = xpAmount;
            int paidDebt = 0;
            int cm = 0;
            int CMXP = 0;
            if (mySession.MyCharacter.totalDebt > 0)
            {
                debt = mySession.MyCharacter.totalDebt;

                //If true, will erase remaining debt
                if ((xpAmount / 2) >= mySession.MyCharacter.totalDebt)
                {
                    paidDebt = mySession.MyCharacter.totalDebt;
                    xpAmount -= mySession.MyCharacter.totalDebt;
                    mySession.MyCharacter.totalDebt = 0;
                }

                //Otherwise, chop xp amount in half to satisify some debt
                else
                {
                    paidDebt = (xpAmount / 2);
                    mySession.MyCharacter.totalDebt -= (xpAmount / 2);
                    xpAmount -= (xpAmount / 2);
                }
            }

            //check to see if player is having a percentage of XP going to CMs
            if (cm > 0)
            {
                CMXP = (int)(cm / 100f) * xpAmount;
                xpAmount -= CMXP;

                //Only XP based towards cm's should remove from our total earned xp
                totXP -= CMXP;
            }

            //Inform client of the gainz
            if (xpAmount > 0)
            {
                if (debt > 0)
                    ChatMessage.GenerateClientSpecificChat(mySession, $"You received {totXP} XP. {paidDebt} was paid towards your debt of {debt}.");

                else
                    ChatMessage.GenerateClientSpecificChat(mySession, $"You received {xpAmount} XP.");
            }

            //Need to send earned CM opcode with this if any
            if (CMXP > 0)
                ChatMessage.GenerateClientSpecificChat(mySession, $"You received {CMXP} Mastery XP.");

            mySession.MyCharacter.TotalXPEarned += xpAmount;

            while (mySession.MyCharacter.TotalXPEarned > CharacterUtilities.CharXPDict[mySession.MyCharacter.Level])
            {
                mySession.MyCharacter.Level++;
            }

            //Something similar as above for Training points

            Memory<byte> temp = new Memory<byte>(new byte[Utility_Funcs.DoubleVariableLengthIntegerLength(mySession.MyCharacter.Level) + Utility_Funcs.DoubleVariableLengthIntegerLength(mySession.MyCharacter.TotalXPEarned) + 2]);
            Span<byte> Message = temp.Span;

            Message.Write((ushort)GameOpcode.GrantXP, ref offset);
            Message.Write7BitDoubleEncodedInt(mySession.MyCharacter.Level, ref offset);
            Message.Write7BitDoubleEncodedInt(mySession.MyCharacter.TotalXPEarned, ref offset);
            SessionQueueMessages.PackMessage(mySession, temp, MessageOpcodeTypes.ShortReliableMessage);
        }

        public static bool CheckQuestItem(Session mySession, int itemID, int itemQty)
        {
            if (mySession.MyCharacter.Inventory.itemContainer.Any(p => p.Value.ItemID == itemID && p.Value.StackLeft >= itemQty))
            {
                return true;
            }
            else
            {
                return false;
            }
        }



    }
}
