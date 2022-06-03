using System;
using System.Collections.Generic;
using System.Numerics;
using System.Text;
using System.Linq;
using System.IO;

using ReturnHome.Server.Opcodes;
using ReturnHome.Server.EntityObject.Player;
using ReturnHome.Server.Managers;
using ReturnHome.Server.Network;
using ReturnHome.Utilities;
using ReturnHome.Server.Opcodes.Messages.Server;
using ReturnHome.Server.Opcodes.Chat;

using NLua;
using Newtonsoft.Json;

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
            //If false, ignore? Might need some kind of escalation, why would we target something not known about?
            //If true, prep message
            if (EntityManager.QueryForEntity(_target, out Entity ent))
            {
                //This shouldn't happen, but to be safe? Eventually could be an expired object that was originally target?
                if (ent == null)
                    return;

                ServerPlayerTarget.PlayerTarget(((Character)this).characterSession, 3, GenerateConColor(), _target, _targetCounter++);
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

        //This is wrong... We are just referencing the npc item's state to a character here where the state will be shared to all users
        //Need to some how make a new copy of it, not sure the easiest way to do that
        public void MerchantBuy(byte itemSlot, int itemQty, uint targetNPC)
        {
            if (EntityManager.QueryForEntity(targetNPC, out Entity npc))
            {
                npc.Inventory.RetrieveItem(itemSlot, out Item item);
                Inventory.RemoveTunar((int)(item.ItemCost * itemQty));

                //Adjust player tunar
                ServerUpdatePlayerTunar.UpdatePlayerTunar(((Character)this).characterSession, Inventory.Tunar);

                Item newItem = item.AcquireItem(itemQty);

                Inventory.AddItem(newItem);

                ServerAddInventoryItemQuantity.AddInventoryItemQuantity(((Character)this).characterSession, newItem);
            }
        }

        public void TriggerMerchantMenu(uint targetNPC)
        {
            if (EntityManager.QueryForEntity(targetNPC, out Entity npc))
                ServerTriggerMerchantMenu.TriggerMerchantMenu(((Character)this).characterSession, npc);
        }

        //Method used to send any in game dialogue to player. Works for option box or regular dialogue box
        public void SendDialogue(Session session, string dialogue, LuaTable diagOptions)
        {
            //Clear player's previous dialogue options before adding new ones.
            session.MyCharacter.MyDialogue.diagOptions.Clear();

            //loop over luatable, assigning every value to an element of the players diagOptions list
            //lua table always returns a dict type object of <object,object>
            foreach (KeyValuePair<object, object> k in diagOptions)
                session.MyCharacter.MyDialogue.diagOptions.Add(k.Value.ToString());

            //If it's not a yes/no choice then sort alphabetically.
            //This forces it to return choices the same every time.
            if (!session.MyCharacter.MyDialogue.diagOptions.Contains("Yes"))
                session.MyCharacter.MyDialogue.diagOptions.Sort();

            //Length of choices
            uint choicesLength = 0;
            //set choice counter for player
            uint choiceCounter = session.MyCharacter.MyDialogue.counter;
            byte textOptions = 0;
            GameOpcode dialogueType = GameOpcode.DialogueBoxOption;
            //if dialogue options exist set choice counter update
            if (session.MyCharacter.MyDialogue.diagOptions != null)
            {
                choiceCounter = (uint)session.MyCharacter.MyDialogue.diagOptions.Count;
                //Length of choices
                foreach (string choice in session.MyCharacter.MyDialogue.diagOptions)
                    choicesLength += (uint)choice.Length;

                //count the number of textOptions
                textOptions = (byte)session.MyCharacter.MyDialogue.diagOptions.Count;
                //set dialogue type to option
                dialogueType = GameOpcode.OptionBox;
            }
            //if there are no diagOptions send onlly diag box.
            if (session.MyCharacter.MyDialogue.diagOptions.Count <= 0)
                dialogueType = GameOpcode.DialogueBox;

            //set player dialogue to the incoming dialogue
            session.MyCharacter.MyDialogue.dialogue = dialogue;
            //create variable memory span for sending out dialogue
            Message message = Message.Create(MessageType.ReliableMessage, dialogueType);
            BufferWriter writer = new BufferWriter(message.Span);

            writer.Write(message.Opcode);
            writer.Write(choiceCounter);
            writer.WriteString(Encoding.Unicode, dialogue);
            //if it's an option box iterate through options and write options to message
            if (dialogueType == GameOpcode.OptionBox)
            {
                writer.Write(textOptions);

                if (session.MyCharacter.MyDialogue.diagOptions != null)
                {
                    for (int i = 0; i < session.MyCharacter.MyDialogue.diagOptions.Count; i++)
                        writer.WriteString(Encoding.Unicode, session.MyCharacter.MyDialogue.diagOptions[i]);
                }
            }

            message.Size = writer.Position;
            //Send Message
            session.sessionQueue.Add(message);
            session.MyCharacter.MyDialogue.choice = 1000;
        }

        public void ProcessDialogue(Session session, BufferReader reader, PacketMessage ClientPacket)
        {
            uint interactTarget = 0;
            //Read the incoming message and get the objectID that was interacted with

            if (ClientPacket.Header.Opcode == (ushort)GameOpcode.Interact)
                interactTarget = reader.Read<uint>();

            //if option choice incoming
            if (ClientPacket.Header.Opcode == (ushort)GameOpcode.DialogueBoxOption)
            {
                //try to pull the option counter and players choice out of message
                try
                {
                    uint optionCounter = reader.Read<uint>();
                    session.MyCharacter.MyDialogue.choice = reader.Read<byte>();
                    //if diag message is 255(exit dialogue in client) return immediately without new message
                    //and set choice to incredibly high number to make sure it doesn't retrigger any specific dialogue.
                    if (session.MyCharacter.MyDialogue.choice == 255)
                    {
                        session.MyCharacter.MyDialogue.choice = 1000;
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
            Dialogue dialogue = session.MyCharacter.MyDialogue;
            GameOpcode dialogueType = GameOpcode.DialogueBoxOption;
            //if a diag option choice incoming set outgoing to diag box option
            if (ClientPacket.Header.Opcode == (ushort)GameOpcode.DialogueBoxOption)
                dialogueType = GameOpcode.DialogueBoxOption;

            //else this is just a regular interaction with only dialogue
            else if (ClientPacket.Header.Opcode == (ushort)GameOpcode.Interact)
                dialogueType = GameOpcode.DialogueBox;

            //Gets NPC name from ObjectID
            if (ClientPacket.Header.Opcode == (ushort)GameOpcode.Interact)
            {
                if (EntityManager.QueryForEntity(interactTarget, out Entity npc))
                    dialogue.npcName = npc.CharName;
            }
            //call the event manager to process dialogue from here. Passes to Lua scripts.
            EventManager.GetNPCDialogue(dialogueType, session);
        }

        public static void GrantXP(Session session, int xpAmount)
        {
            int debt = 0;
            int totXP = xpAmount;
            int paidDebt = 0;
            int cm = 0;
            int CMXP = 0;
            if (session.MyCharacter.totalDebt > 0)
            {
                debt = session.MyCharacter.totalDebt;

                //If true, will erase remaining debt
                if ((xpAmount / 2) >= session.MyCharacter.totalDebt)
                {
                    paidDebt = session.MyCharacter.totalDebt;
                    xpAmount -= session.MyCharacter.totalDebt;
                    session.MyCharacter.totalDebt = 0;
                }

                //Otherwise, chop xp amount in half to satisify some debt
                else
                {
                    paidDebt = (xpAmount / 2);
                    session.MyCharacter.totalDebt -= (xpAmount / 2);
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
                    ChatMessage.GenerateClientSpecificChat(session, $"You received {totXP} XP. {paidDebt} was paid towards your debt of {debt}.");

                else
                    ChatMessage.GenerateClientSpecificChat(session, $"You received {xpAmount} XP.");
            }

            //Need to send earned CM opcode with this if any
            if (CMXP > 0)
                ChatMessage.GenerateClientSpecificChat(session, $"You received {CMXP} Mastery XP.");

            session.MyCharacter.TotalXPEarned += xpAmount;

            //Something similar as above for Training points
            ServerUpdatePlayerXPandLevel.UpdatePlayerXPandLevel(session, session.MyCharacter.Level, session.MyCharacter.TotalXPEarned);

            while (session.MyCharacter.TotalXPEarned > CharacterUtilities.CharXPDict[session.MyCharacter.Level])
            {
                session.MyCharacter.Level++;
                ChatMessage.GenerateClientSpecificChat(session, $"You have reached level {session.MyCharacter.Level}");
            }
        }

        public static bool CheckQuestItem(Session session, int itemID, int itemQty)
        {
            if (session.MyCharacter.Inventory.itemContainer.Any(p => p.Value.ItemID == itemID && p.Value.StackLeft >= itemQty))
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
