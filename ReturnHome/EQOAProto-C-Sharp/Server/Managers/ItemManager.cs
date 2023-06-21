using System;
using System.Collections.Concurrent;
using ReturnHome.Server.EntityObject.Items;
using ReturnHome.Server.EntityObject.Player;
using ReturnHome.Server.Network;
using ReturnHome.Database.SQL;
using ReturnHome.Server.EntityObject;
using ReturnHome.Server.EntityObject.Spells;
using ReturnHome.Server.Opcodes.Messages.Server;
using System.IO;
using NLua;
using System.ServiceModel.Channels;
using System.Collections.Generic;
using ReturnHome.Server.Items;
using ReturnHome.Server.EntityObject.Actors;

namespace ReturnHome.Server.Managers
{
    public static class ItemManager
    {

        public static int nextItemID { get; set; }
        public static List<LootTable> lootTables = new List<LootTable>();
        //TODO: Add a field to our Item Pattern class to hold a... "standard" quatity for item's? So merchant consumable's would come in qwuantities of 3?
        private static readonly ConcurrentDictionary<int, ItemPattern> itemList = new();

        public static void AddItem(ItemPattern item) => itemList.TryAdd(item.ItemID, item);

        public static void GrantItem(Session mySession, int itemID, int qty)
        {
            Item newItem = CreateItem(itemID, qty, mySession.MyCharacter);
            mySession.MyCharacter.Inventory.AddItem(newItem);
        }


        public static Item CreateItem(int itemID, int qty, Entity entity)
        {
            Item newItem;
            if (entity is Character)
            {
                newItem = new(qty, itemList[itemID].Maxhp, 0, (int)EquipSlot.NotEquipped, ItemLocation.Inventory, 0, itemList[itemID], nextItemID);
                nextItemID++;
                
            }

            //Create npc one here
            else
            {
                newItem = new(qty, itemList[itemID].Maxhp, 0, (int)EquipSlot.NotEquipped, ItemLocation.Inventory, 0, itemList[itemID], 0);
            }
            return newItem;
        }

        public static ItemPattern GetItemPattern(int itemID) => itemList[itemID];

        //Really should be called remove quantity
        public static void UpdateQuantity(Session mySession, int itemID, int qty)
        {
            if (Character.CheckIfItemInInventory(mySession, itemID, out byte key, out Item newItem))
                mySession.MyCharacter.Inventory.UpdateQuantity(key, qty);


        }

        public static void UseItem(int itemID, Session session)
        {
            ItemPattern item = GetItemPattern(itemID);

            string itemName = item.ItemName.Replace(" ", "_");


            //Find Lua script recursively through scripts directory by class
            string[] file = Directory.GetFiles("../../../Scripts", itemName + ".lua", SearchOption.AllDirectories);


            //TODO: work around for a spell with no scripts etc? Investigate more eventually
            if (file.Length < 1 || file == null)
                return;

            //Create new lua object
            //Lua lua = new Lua();

            //load lua CLR library

            //Create handles for the lua script to access some c# variables and methods
            LuaState.State["session"] = session;
            LuaState.State["target"] = session.MyCharacter.Target;
            LuaState.State["AddStatusEffect"] = session.MyCharacter.AddStatusEffect;


            //Call the Lua script found by the Directory Find above
            LuaState.State.DoFile(file[0]);

            //Call Lua function for initial interaction
            LuaFunction callFunction = LuaState.State.GetFunction("useItem");
            callFunction.Call();

        }

        public static void GetMobLoot(Actor a)
        {
            //No loot table exists with value 0(default for int) so just return.
            if (a.lootTableID == 0) return;
            //Find the loot table entries based on the item pool
            List<LootTable> lt = lootTables.FindAll(e => e._itempool_id == a.lootTableID);
            List<int> lootedItems = new List<int> { };

            //define totalWeight for randomizing spawner
            int totalWeight = 0;

            //Generate new instance of random
            Random _rnd = new Random();

            //For every entry in spawnentries for that group, total up the combined weights
            foreach (LootTable entry in lt)
            {
                totalWeight += entry._item_rate;
            }

            //Find a random number between 0 and the total combined weight for the group
            int randomNumber = _rnd.Next(0, totalWeight);

            //For each entry in spawn entries for that group, determine which one is the one to spawn.
            //based on the randomized weight.
            foreach (LootTable entry in lt)
            {
                if (entry._item_rate == 100)
                {
                    lootedItems.Add(entry._itemid);
                }
                else if (randomNumber < entry._item_rate)
                {
                    //if the random number is smaller than the entries chance, select this for the mob to spawn
                    lootedItems.Add(entry._itemid);
                    Console.WriteLine($"Rolled item with item ID {entry._itemid}");
                    //break out out of the loop if an npc is chosen
                    break;
                }
                //deduct the entries chance from the random number every loop until one is chosen
                randomNumber = randomNumber - entry._item_rate;
            }

            foreach (int itemID in lootedItems)
            {
                //Create the new item from the item ID and quantity
                Item newItem = CreateItem(itemID, 1, a);

                //Make sure the NPC has an inventory before trying to assign
                //loot to it, if not then create it first
                if (a.Inventory == null)
                {
                    a.Inventory = new(0, a);
                }
                //Add item to NPCs inventory.
                a.Inventory.AddItem(newItem);
            }
        }
    }
}
