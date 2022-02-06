
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

        public void BankItem(Session mySession, PacketMessage clientPacket)
        {
            int offset = 0;
            ReadOnlySpan<byte> IncMessage = clientPacket.Data.Span;

            uint targetNPC = IncMessage.GetLEUInt(ref offset);
            uint giveOrTake = IncMessage[offset++];
            uint itemToTransfer = IncMessage.GetLEUInt(ref offset);
            Console.WriteLine(targetNPC);
            Console.WriteLine(giveOrTake);
            Console.WriteLine(itemToTransfer);

            //Deposit Item
            if(giveOrTake == 0)
            {

            }else if(giveOrTake == 1){

            }



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

        public void ProcessDialogue(Session mySession, PacketMessage clientPacket)
        {
            //set offset
            int offset = 0;
            uint interactTarget = 0;
            //Read the incoming message and get the objectID that was interacted with
            ReadOnlySpan<byte> IncMessage = clientPacket.Data.Span;
            if (clientPacket.Header.Opcode == (ushort)GameOpcode.Interact)
            {
                interactTarget = IncMessage.GetLEUInt(ref offset);
            }
            //if option choice incoming
            if (clientPacket.Header.Opcode == 53)
            {
                //try to pull the option counter and players choice out of message
                try
                {
                    offset = 0;
                    uint optionCounter = IncMessage.GetLEUInt(ref offset);
                    mySession.MyCharacter.MyDialogue.choice = IncMessage.GetByte(ref offset);
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
            if (clientPacket.Header.Opcode == 53)
            {
                dialogueType = GameOpcode.DialogueBoxOption;
            }
            //else this is just a regular interaction with only dialogue
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
                    dialogue.npcName = npc.CharName;
                }
            }
            //call the event manager to process dialogue from here. Passes to Lua scripts.
            EventManager.GetNPCDialogue(dialogueType, mySession);
        }
    }
}
