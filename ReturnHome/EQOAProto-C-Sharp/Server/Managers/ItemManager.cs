using System;
using System.Collections.Concurrent;
using ReturnHome.Server.EntityObject.Items;
using ReturnHome.Server.EntityObject.Player;
using ReturnHome.Server.Network;
using ReturnHome.Database.SQL;

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
            Item newItem = CreateItem(itemID, qty);
            mySession.MyCharacter.Inventory.AddItem(newItem);
            CharacterSQL sql = new CharacterSQL();
            sql.AddPlayerItem(mySession.MyCharacter, newItem);
        }


        public static Item CreateItem(int itemID, int qty)
        {
            Item newItem = new(qty, itemList[itemID].Maxhp, 0, (int)EquipSlot.NotEquipped, 1, 0, itemList[itemID], nextItemID);
            nextItemID++;

            return newItem;
        }


        public static ItemPattern GetItemPattern(int itemID) => itemList[itemID];

        //Really should be called remove quantity
        public static void UpdateQuantity(Session mySession, int itemID, int qty)
        {
            if (Character.CheckIfItemInInventory(mySession, itemID, out byte key, out Item newItem))
                mySession.MyCharacter.Inventory.UpdateQuantity(key, qty);


        }
    }
}
