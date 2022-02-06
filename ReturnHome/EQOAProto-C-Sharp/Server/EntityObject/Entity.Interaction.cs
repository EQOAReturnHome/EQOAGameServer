
using System;
using System.Collections.Generic;
using System.Numerics;
using System.Text;
using ReturnHome.Opcodes;
using ReturnHome.Server.EntityObject.Player;
using ReturnHome.Server.Managers;
using ReturnHome.Server.Network;
using ReturnHome.Utilities;
using NLua;


namespace ReturnHome.Server.EntityObject
{
    public partial class Entity
    {

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

        //Message to trigger bank message in game
        public void TriggerBank(Session mySession, PacketMessage clientPacket)
        {
            //reset offset
            int offset = 0;
            //Define Memory span
            Memory<byte> temp = new byte[2];
            Span<byte> Message = temp.Span;
            //Write bank op code back to memory span
            Message.Write((ushort)GameOpcode.BankUI, ref offset);
            //Send bank op code back to player
            SessionQueueMessages.PackMessage(mySession, temp, MessageOpcodeTypes.ShortReliableMessage);
            //Exit method early without processing any other conditions
            return;
        }

        //Method for withdrawing and depositing bank tunar
        public void BankTunar(Session mySession, PacketMessage clientPacket)
        {
            //Create readonly span for the packet data
            ReadOnlySpan<byte> IncMessage = clientPacket.Data.Span;
            //set fresh offset
            int offset = 0;
            //pull relevant bank information out of packet
            uint targetNPC = IncMessage.GetLEUInt(ref offset);
            uint giveOrTake = IncMessage.Get7BitEncodedInt(ref offset);
            int transferAmount = IncMessage.Get7BitDoubleEncodedInt(ref offset);
            //Set int amounts of player tunar transfer
            int newPlayerAmt = 0;
            int newBankAmt = 0;

            //deposit transaction
            if (giveOrTake == 0)
            {
                //set the new player amount to the current player tunar minus transfer
                newPlayerAmt = mySession.MyCharacter.Tunar - transferAmount;
                //assign new value to player tunar
                mySession.MyCharacter.Tunar = newPlayerAmt;
                //Do the opposite of the above to transfer into bank
                newBankAmt = mySession.MyCharacter.BankTunar + transferAmount;
                //assign new value to players bank tunar
                mySession.MyCharacter.BankTunar = newBankAmt;
            }
            //withdraw transaction
            else if (giveOrTake == 1)
            {
                //set the new payer amount to the currently player tunar plus transfer
                newPlayerAmt = mySession.MyCharacter.Tunar + transferAmount;
                //assign new value to player tunar
                mySession.MyCharacter.Tunar = newPlayerAmt;
                //do the oppsoite of the above to transfer to player
                newBankAmt = mySession.MyCharacter.BankTunar - transferAmount;
                //assign new value to players bank tunar
                mySession.MyCharacter.BankTunar = newBankAmt;
            }

            //Define memory span for player
            Memory<byte> playerTemp = new byte[2 + Utility_Funcs.DoubleVariableLengthIntegerLength(newPlayerAmt)];
            Span<byte> messagePlayer = playerTemp.Span;
            //Define memory span for ban
            Memory<byte> bankTemp = new byte[2 + Utility_Funcs.DoubleVariableLengthIntegerLength(newBankAmt)];
            Span<byte> messageBank = bankTemp.Span;
            //reset span offset and write player amount to Message
            offset = 0;
            messagePlayer.Write((ushort)GameOpcode.PlayerTunar, ref offset);
            messagePlayer.Write7BitDoubleEncodedInt(newPlayerAmt, ref offset);
            //resete span offset and write bank amount to message
            offset = 0;
            messageBank.Write((ushort)GameOpcode.ConfirmBankTunar, ref offset);
            messageBank.Write7BitDoubleEncodedInt(newBankAmt, ref offset);
            //send both messages to client 
            SessionQueueMessages.PackMessage(mySession, playerTemp, MessageOpcodeTypes.ShortReliableMessage);
            SessionQueueMessages.PackMessage(mySession, bankTemp, MessageOpcodeTypes.ShortReliableMessage);
            return;
        }

        //Method used to send any in game dialogue to player. Works for option box or regular dialogue box
        public void SendDialogue(Session mySession, string dialogue, LuaTable diagOptions)
        {
            //Clear player's previous dialogue options
            mySession.MyCharacter.MyDialogue.diagOptions.Clear();
            //loop over luatable, assigning every value to an element of the players diagOptions list
            //LuaTables come back from lua scripts as effectively Dict<object,object>
            foreach (KeyValuePair<object, object> k in diagOptions)
            {
                mySession.MyCharacter.MyDialogue.diagOptions.Add(k.Value.ToString());
            }

            //If the dialogue isn't a Yes or no choice sort alphabetically
            //this maintains order with coaches across multiple dialogues
            if (!mySession.MyCharacter.MyDialogue.diagOptions.Contains("Yes"))
            {
                mySession.MyCharacter.MyDialogue.diagOptions.Sort();
            }

            //create offset for message
            int offset = 0;
            //Length of choices
            uint choicesLength = 0;
            //Keeps track of counter for player messages
            uint choiceCounter = mySession.MyCharacter.MyDialogue.counter;
            //create dialoguetype variable and set it default to option box type
            GameOpcode dialogueType = GameOpcode.DialogueBoxOption;

            //Check if player's diagOptions are null. If they aren't, count diagOptions
            if (mySession.MyCharacter.MyDialogue.diagOptions != null)
            {
                choiceCounter = (uint)mySession.MyCharacter.MyDialogue.diagOptions.Count;
                //Calculate the length of choicdes of dialogue options
                foreach (string choice in mySession.MyCharacter.MyDialogue.diagOptions)
                {
                    choicesLength += (uint)choice.Length;
                }
                //If the palyer's diag options are not null generate an option box instead of regular dialogue
                dialogueType = GameOpcode.OptionBox;
            }

            //IIf the player's dialogue options are less than or equal to 0 send regular dialogue box
            if (mySession.MyCharacter.MyDialogue.diagOptions.Count <= 0)
            {
                dialogueType = GameOpcode.DialogueBox;
            }

            //Create variable memory span based on dialogue length, choiceCounters, and the choice length.
            Memory<byte> temp = new Memory<byte>(new byte[11 + (dialogue.Length * 2) + (choiceCounter * 4) + 1 + (choicesLength * 2)]);
            Span<byte> Message = temp.Span;
            //Write dialogue type, chocie counter, diag length, and actual dialogue to message
            Message.Write((ushort)dialogueType, ref offset);
            Message.Write(choiceCounter, ref offset);
            Message.Write(dialogue.Length, ref offset);
            Message.Write(Encoding.Unicode.GetBytes(dialogue), ref offset);
            //If the message type is an option box write the dialogue options to the message as well.
            if (dialogueType == GameOpcode.OptionBox)
            {
                //Check to make sure somehow diagOptions wasn't null
                if (mySession.MyCharacter.MyDialogue.diagOptions != null)
                {
                    //Iterate over each diagOption writing its length and text to the Message.
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

        //Process dialogue called from the opcode ops to enter dialogue workflow.
        public void ProcessDialogue(Session mySession, PacketMessage clientPacket)
        {
            //Create offset for message
            int offset = 0;
            uint interactTarget = 0;
            //Read the incoming message and get the objectID that was interacted with
            ReadOnlySpan<byte> IncMessage = clientPacket.Data.Span;
            //If the op code is just an interact type pull the interact target off message
            if (clientPacket.Header.Opcode == (ushort)GameOpcode.Interact)
            {
                interactTarget = IncMessage.GetLEUInt(ref offset);
            }
            //If the op code is a dialogue choice go this route
            if (clientPacket.Header.Opcode == 53)
            {
                //reset offset for message type
                offset = 0;
                //get option counter to keep track
                uint optionCounter = IncMessage.GetLEUInt(ref offset);
                //Get the choice from the option message
                mySession.MyCharacter.MyDialogue.choice = IncMessage.GetByte(ref offset);
                //If the message is a 255(exit dialogue option)
                if (mySession.MyCharacter.MyDialogue.choice == 255)
                {
                    //Set the option to very high 1000 so it doesn't retrigger exit
                    mySession.MyCharacter.MyDialogue.choice = 1000;
                    //immediately end message generation, no return send
                    return;
                }
            }
            //create dialogueType and set base option as boxoption
            GameOpcode dialogueType = GameOpcode.DialogueBoxOption;
            //if the op code is a 35, set diagType to BoxOption
            if (clientPacket.Header.Opcode == 53)
            {
                dialogueType = GameOpcode.DialogueBoxOption;
            }
            //Otherwise if it's just a normal interaction set it to a regular dialogue
            else if (clientPacket.Header.Opcode == 4)
            {
                dialogueType = GameOpcode.DialogueBox;
            }
            //Reset offset for outgoing message
            offset = 0;
            //Gets NPC name from ObjectID
            if (clientPacket.Header.Opcode == 4)
            {
                if (EntityManager.QueryForEntity(interactTarget, out Entity npc))
                {
                    mySession.MyCharacter.MyDialogue.npcName = npc.CharName;
                }
            }
            //Call the event manager to pass everything to Lua
            EventManager.GetNPCDialogue(dialogueType, mySession);
        }
    }
}
