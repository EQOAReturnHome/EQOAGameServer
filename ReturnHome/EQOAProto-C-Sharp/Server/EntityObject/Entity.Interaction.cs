
using System;
using System.Collections.Generic;
using System.Numerics;
using System.Text;
using ReturnHome.Opcodes;
using ReturnHome.Server.EntityObject.Player;
using ReturnHome.Server.Managers;
using ReturnHome.Server.Network;
using ReturnHome.Utilities;

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

            switch(difference)
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

        public void BankTunar(Session mySession, PacketMessage clientPacket)
        {
            ReadOnlySpan<byte> IncMessage = clientPacket.Data.Span;

            Console.WriteLine("4693");
            int offset = 0;
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
                newPlayerAmt = mySession.MyCharacter.Tunar - transferAmount;
                mySession.MyCharacter.Tunar = newPlayerAmt;
                newBankAmt = mySession.MyCharacter.BankTunar + transferAmount;
                mySession.MyCharacter.BankTunar = newBankAmt;
            }
            //withdraw
            else if (giveOrTake == 1)
            {
                Console.WriteLine("Withdraw");
                newPlayerAmt = mySession.MyCharacter.Tunar + transferAmount;
                mySession.MyCharacter.Tunar = newPlayerAmt;
                newBankAmt = mySession.MyCharacter.BankTunar - transferAmount;
                mySession.MyCharacter.BankTunar = newBankAmt;
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

            SessionQueueMessages.PackMessage(mySession, playerTemp, MessageOpcodeTypes.ShortReliableMessage);
            SessionQueueMessages.PackMessage(mySession, bankTemp, MessageOpcodeTypes.ShortReliableMessage);
            return;
        }
        public void ProcessDialogue(Session mySession, PacketMessage clientPacket)
        {
            int offset = 0;
            //Read the incoming message and get the objectID that was interacted with
            ReadOnlySpan<byte> IncMessage = clientPacket.Data.Span;
            uint interactTarget = IncMessage.GetLEUInt(ref offset);

            //Create new instance of the event manager
            EventManager eManager = new EventManager();
            Entity npcEntity = new Entity(false);
            Dialogue dialogue = mySession.MyCharacter.MyDialogue;
            ushort dialogueType = (ushort)GameOpcode.DialogueBox;

            //Reset offset for outgoing message
            offset = 0;
            //Length of choices
            uint choicesLength = 0;
            //Dialogue at top of box
            string TextboxMessage;
            //The number of text choices that exist
            //uint textChoicesNum = 0;
            //List of dialogue options
            List<string> textChoices = new List<string>();
            //Counter to keep track of how many
            //int choiceCounter = IncMessage.GetLEInt(ref offset);
            uint choiceCounter = mySession.MyCharacter.MyDialogue.counter;
            byte textOptions = 0;
            Console.WriteLine("choice is: " + choiceCounter);
            //Gets NPC name from ObjectID
            if (choiceCounter == 0)
            {
                if (EntityManager.QueryForEntity(interactTarget, out Entity npc))
                {
                    Console.WriteLine("In the query: " + npc.CharName);
                    dialogue.npcName = npc.CharName;
                }
            }


            dialogue = eManager.GetNPCDialogue(GameOpcode.DialogueBox, mySession.MyCharacter);
            if (dialogue.diagOptions != null)
            {
                textChoices = dialogue.diagOptions;
                choiceCounter = (uint)textChoices.Count;

                //Length of choices
                foreach (string choice in textChoices)
                {
                    choicesLength += (uint)choice.Length;
                    Console.WriteLine(choice);
                }
                textOptions = (byte)textChoices.Count;
                dialogueType = (ushort)GameOpcode.OptionBox;
                Console.WriteLine("OptionBox");
            }
            else if (dialogue.diagOptions == null)
            {
                dialogueType = (ushort)GameOpcode.DialogueBox;
                Console.WriteLine("Dialogue Box");
            }

            TextboxMessage = dialogue.dialogue;

            Memory<byte> temp = new Memory<byte>(new byte[11 + (TextboxMessage.Length * 2) + (choiceCounter * 4) + 1 + (choicesLength * 2)]);
            Span<byte> Message = temp.Span;

            Message.Write(dialogueType, ref offset);
            Message.Write(choiceCounter, ref offset);
            Message.Write(TextboxMessage.Length, ref offset);
            Message.Write(Encoding.Unicode.GetBytes(TextboxMessage), ref offset);
            if (dialogueType == (ushort)GameOpcode.OptionBox)
            {
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
            }



            //Send Message
            SessionQueueMessages.PackMessage(mySession, temp, MessageOpcodeTypes.ShortReliableMessage);

        }
    }
}
