using System.Collections.Concurrent;
using ReturnHome.Server.EntityObject.Items;
using ReturnHome.Server.EntityObject.Player;
using ReturnHome.Server.Network;

namespace ReturnHome.Server.Managers
{
    public static class ItemManager
    {

        private static readonly ConcurrentDictionary<int, ItemPattern> itemList = new();

        public static void AddItem(ItemPattern item) => itemList.TryAdd(item.ItemID, item);

        public static void GrantItem(Session mySession, int itemID, int qty)
        {
            ItemPattern itemPattern = itemList[itemID];
            Item newItem = new(qty, itemPattern.Maxhp ,0, (int)EquipSlot.NotEquipped, 0, 0, itemPattern);
            mySession.MyCharacter.Inventory.AddItem(newItem);
        }

        public static ItemPattern GetItemPattern(int itemID) => itemList[itemID];

        //Really should be called remove quantity
        //What is this?
        public static void UpdateQuantity(Session mySession, int itemID, int qty)
        {
            if (Character.CheckIfItemInInventory(mySession, itemID, out byte key, out Item newItem))
                mySession.MyCharacter.Inventory.UpdateQuantity(key, qty);



        }
    }
}
