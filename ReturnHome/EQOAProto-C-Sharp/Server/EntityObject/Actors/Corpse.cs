using System;
using System.Collections.Generic;
using ReturnHome.Server.EntityObject.Items;
using ReturnHome.Server.EntityObject.Player;
using ReturnHome.Server.Network;
using ReturnHome.Server.EntityObject.Grouping;
using ReturnHome.Server.Opcodes.Messages.Server;
using ReturnHome.Database.SQL;
using ReturnHome.Server.Managers;

namespace ReturnHome.Server.EntityObject.Actors
{
    public class Corpse
    {
        //Corpse will be locked to the player who killed it for 5 minute's, then anyone can loot it
        private static readonly int _timeToOpenCorpse = 5000;
        //When someone opens the loot table, locks the corpse
        private Character _lootee;
        private Group _lootingGroup;
        private Entity _npc;
        //To track entity Death
        private long _timeOfDeath;
        //To track when someone entered the loot window, important as this "refreshes" the corpse
        private long _lootingTime;
        public List<ClientItemWrapper> Loot { private set; get; }

        //When we create the corpse, entity has died and we toss a time stamp to it
        public Corpse(Entity npc) => _npc = npc;

        public void UpdateCorpseOnDeath(List<ClientItemWrapper> loot)
        {
            Loot = loot;
            _timeOfDeath = DateTime.UtcNow.Millisecond;
        }
        public void LootCorpse(Session session)
        {
            //TODO: Add code to account for the owner of the corpse, Player/Group
            if (_lootee != null && session.MyCharacter != _lootee)
            {
                ServerLoot.ServerLootOptions(session, LootOptions.CorpseBeingLootedByAnotherPlayer);
                //Send corpse is already being looted opcode to this client
                return;
            }

            //Designate looter request and send loot to client
            _lootee = session.MyCharacter;

            //Change Server Lootbox to take a corpse object
            ServerLoot.ServerLootBox(session, Loot);
        }

        public void LootItems(Session session, byte key, int itemQty)
        {
            Item existingItem = null;
            CharacterSQL sql = new();



            for (byte i = 0; i < Loot.Count; ++i)
                if (Loot[i].key == key)
                {
                    foreach (ClientItemWrapper invItem in session.MyCharacter.Inventory.itemContainer)
                    {
                        Console.WriteLine(invItem.item.Pattern.ItemID);
                        if (invItem.item.Pattern.ItemID == Loot[i].item.Pattern.ItemID)
                        {
                            Console.WriteLine($"Found item {invItem.item.Pattern.ItemID} by using {Loot[i].item.Pattern.ItemID}");
                            existingItem = invItem.item;
                            break;
                        }
                        existingItem = null;
                    }
                    //If Character can add it to Inventory, remove from Corpse
                    if (session.MyCharacter.Inventory.AddItem(Loot[i].item, true))
                    {

                        if (existingItem != null)
                        {
                            Console.WriteLine("Updating item quantity");
                            sql.UpdatePlayerItem(session.MyCharacter, existingItem.StackLeft + itemQty, existingItem.ID);
                        }
                        else
                        {
                            //Save the item to the players DB entries immediately
                            Loot[i].item.ID = ItemManager.nextItemID;
                            ItemManager.nextItemID++;
                            sql.AddPlayerItem(session.MyCharacter, Loot[i].item);
                        }

                        Loot.RemoveAt(i);
                        ServerLoot.ServerLootItem(session, i, itemQty);
                    }
                }
        }

        public void ExitCorpse(Session session)
        {
            _lootee = null;

            if (Loot.Count <= 0)
            {
                //Destroy corpse somehow
            }
        }
    }
}
