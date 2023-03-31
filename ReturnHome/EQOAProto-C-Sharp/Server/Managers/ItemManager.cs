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

namespace ReturnHome.Server.Managers
{
    public static class ItemManager
    {

        public static int nextItemID { get; set; }
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
            //Console.WriteLine($"Activating item with itemID {itemID}");
            ItemPattern item = GetItemPattern(itemID);
            Console.WriteLine($"This should be {item.ItemName}");

            //Find Lua script recursively through scripts directory by class
            string[] file = Directory.GetFiles("../../../Scripts", item.ItemName + ".lua", SearchOption.AllDirectories);


            //TODO: work around for a spell with no scripts etc? Investigate more eventually
            if (file.Length < 1 || file == null)
                return;

            //Create new lua object
            //Lua lua = new Lua();

            //load lua CLR library

            //Create handles for the lua script to access some c# variables and methods
            LuaState.State["CastSpell"] = ServerCastSpell.CastSpell;
            LuaState.State["Damage"] = ServerDamage.Damage;
            LuaState.State["CoolDown"] = ServerSpellCoolDown.SpellCoolDown;
            LuaState.State["session"] = session;
            LuaState.State["target"] = session.MyCharacter.Target;
            LuaState.State["TeleportPlayer"] = ServerTeleportPlayer.TeleportPlayer;
            LuaState.State["LearnSpell"] = ServerLearnSpell.LearnSpell;
            LuaState.State["GetSpell"] = SpellManager.GetSpellPattern;


            //Call the Lua script found by the Directory Find above
            LuaState.State.DoFile(file[0]);

            //Call Lua function for initial interaction
            LuaFunction callFunction = LuaState.State.GetFunction("useItem");
            callFunction.Call();

        }
    }
}
