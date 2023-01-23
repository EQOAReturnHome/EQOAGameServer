using System.Collections.Generic;
using ReturnHome.Server.EntityObject.Items;
using ReturnHome.Server.EntityObject.Player;
using ReturnHome.Server.Opcodes;
using ReturnHome.Server.Opcodes.Messages.Server;
using ReturnHome.Database.SQL;
using ReturnHome.Server.Managers;
using System;

namespace ReturnHome.Server.EntityObject
{
    //TODO: Rewrite ItemContainer class to handle sending messages if a player
    public class ItemContainer
    {
        private Entity _e;
        private List<ClientItemWrapper> _itemContainer;
        private byte _counter = 0;
        private int _tunar;

        //Indicates if inventory or bank
        public bool Inventory;

        private ItemLocation _type;

        public int Tunar
        {
            get { return _tunar; }
            private set
            {
                _tunar = value;
                if (_e.isPlayer)
                    ServerUpdateTunar.UpdateTunar(((Character)_e).characterSession, Inventory ? GameOpcode.PlayerTunar : GameOpcode.ConfirmBankTunar, Tunar);
            }
        }

        public int Count => _itemContainer.Count;

        public List<ClientItemWrapper> itemContainer => _itemContainer;

        public ItemContainer(int tunar, Entity e, bool inventory = true)
        {
            _e = e;
            _tunar = tunar;
            _itemContainer = new();
            Inventory = inventory;
            if (inventory)
                _type = ItemLocation.Inventory;
            else
                _type = ItemLocation.Bank;
        }

        public void AddTunar(int tunar) => Tunar += tunar;

        public void RemoveTunar(int tunar) => Tunar -= tunar;

        public bool Exists(byte key)
        {
            for (int i = 0; i < _itemContainer.Count; ++i)
                if (_itemContainer[i].key == key)
                    return true;
            return false;
        }

        //TODO: Need to add check's in place for item's like "Lore", where we can only have 1 in our inventory at a time.
        public bool AddItem(Item itemToBeAdded, bool loot = false)
        {
            if (_itemContainer.Count < 40)
            {

                //TODO fix item stacking, specifically stacking of overflow stacks
                //if(itemToBeAdded.Lore == true)


                if (_e.isPlayer)
                {
                    //Make sure this isn't a freshly created item, if it is, update the itemID before adding to inventory and save it to the players inventory
                    if (itemToBeAdded.ID == 0)
                    {
                        CharacterSQL sql = new();

                        foreach (ClientItemWrapper item in ((Character)_e).characterSession.MyCharacter.Inventory.itemContainer)
                        {
                            Console.WriteLine($"item ID is {item.item.Pattern.ItemID} and item to be added is {itemToBeAdded.Pattern.ItemID}");
                            if (item.item.Pattern.ItemID == itemToBeAdded.Pattern.ItemID)
                            {
                                if ((itemToBeAdded.StackLeft + item.item.StackLeft) <= itemToBeAdded.Pattern.Maxstack)
                                {
                                    Console.WriteLine($"Would not be overflow stack, updating existing item {itemToBeAdded.ID}");
                                    Console.WriteLine($"The stack to be added is {itemToBeAdded.StackLeft} to the existing stack of {item.item.StackLeft}");

                                    //Update internally on an already held item's stack count, we should be adding here
                                    UpdateQuantity(item.key, itemToBeAdded.StackLeft);
                                    itemToBeAdded.StackLeft = 0;
                                }
                                else
                                {
                                    int qtyToAdd = itemToBeAdded.Pattern.Maxstack - item.item.StackLeft;
                                    Console.WriteLine($"Would be overflow stack, adding {qtyToAdd} to existing stack for a total of {qtyToAdd + itemToBeAdded.StackLeft}.");

                                    //Update internally on an already held item's stack count, we should be adding here
                                    UpdateQuantity(item.key, qtyToAdd);

                                    //Subject the quantity added to currently held stack from new item
                                    itemToBeAdded.StackLeft -= qtyToAdd;
                                }
                            }

                            //If at the end of this loop the new items stackleft is 0, let it GC and return
                            if (itemToBeAdded.StackLeft == 0)
                                return true;
                        }

                        //Finished loop, if we still have an item with stacks, give it an ID
                        if (itemToBeAdded.StackLeft > 0)
                        {
                            itemToBeAdded.ID = ItemManager.nextItemID;
                            ItemManager.nextItemID++;
                        }
                    }
                }

                _itemContainer.Add(new ClientItemWrapper(itemToBeAdded, _counter++));
                itemToBeAdded.Location = _type;
                itemToBeAdded.ClientIndex = (byte)_itemContainer.Count;
                if (_e.isPlayer && !loot)
                {
                    if (((Character)_e).characterSession.inGame)
                    {
                        CharacterSQL sql = new CharacterSQL();
                        //Do we need a SQL call here to add the item to the database?
                        ServerAddItemOrQuantity.AddItemOrQuantity(((Character)_e).characterSession, Inventory ? GameOpcode.AddInvItem : GameOpcode.AddBankItem, itemToBeAdded);
                        sql.AddPlayerItem(((Character)_e).characterSession.MyCharacter, itemToBeAdded);
                    }
                }
                return true;
            }
            return false;
        }

        public void SaveItem(Character e, Item itemToBeSaved)
        {

        }

        public bool RemoveItem(byte key)
        {
            for (int i = 0; i < _itemContainer.Count; ++i)
            {
                if (_itemContainer[i].key == key)
                {
                    Item item = _itemContainer[i].item;
                    _itemContainer.RemoveAt(i);
                    if (_e.isPlayer)
                        if (((Character)_e).characterSession.inGame)
                            ServerRemoveItemOrQuantity.RemoveItemOrQuantity(((Character)_e).characterSession, Inventory ? GameOpcode.RemoveInvItem : GameOpcode.RemoveBankItem, item.StackLeft, (byte)i);

                    return true;
                }
            }

            return false;
        }

        /// <summary>
        /// <paramref name="key"/> is specific to client unique item keys.
        /// Ensure <paramref name="quantity"/> is negative if removing stacks/item, or positive if adding.
        /// </summary>
        /// 
        /// //TODO: Update references to this method to make sure they are appropriately passing a positive or negative value
        public void UpdateQuantity(byte key, int quantity)
        {
            for (int i = 0; i < _itemContainer.Count; ++i)
            {
                if (_itemContainer[i].key == key)
                {
                    CharacterSQL sql = new CharacterSQL(); //Moved to here so we don't needlessly create a sql connection every call incase item doesn't exist?

                    //CVheck if we are subtracting stack and if it will take our total to 0
                    if ((_itemContainer[i].item.StackLeft + quantity) == 0)
                    {
                        Console.WriteLine($"Deleting Item {_itemContainer[i].item.ID}");
                        if (_e.isPlayer)
                            sql.DeletePlayerItem(_itemContainer[i].item.ID);//Should this be moved to RemoveItem? Other classes/Methods call remove Item outside of UpdateQuantity

                        RemoveItem(key);
                    }

                    else
                    {
                        //We always add quantity here, onus on us as developers to pass a positive or negative value depending on request
                        _itemContainer[i].item.StackLeft += quantity;
                        if (_e.isPlayer)
                            if (((Character)_e).characterSession.inGame)
                            {
                                Console.WriteLine($"Updating item instead of deleting it");
                                if (quantity < 0)
                                    ServerRemoveItemOrQuantity.RemoveItemOrQuantity(((Character)_e).characterSession, Inventory ? GameOpcode.RemoveInvItem : GameOpcode.RemoveBankItem, _itemContainer[i].item.StackLeft, (byte)i);
                                else
                                    ServerAddItemOrQuantity.AddItemOrQuantity(((Character)_e).characterSession, Inventory ? GameOpcode.AddInvItem : GameOpcode.AddBankItem, _itemContainer[i].item);
                                //Does this method update database to add the quantity, or set the stack to quantity? If last one, need to pass _itemContainer[i].item.StackLeft
                                sql.UpdatePlayerItem(((Character)_e).characterSession.MyCharacter, quantity, _itemContainer[i].item.ID); //Should this be outside of our if else? See 1.

                                //1. Here?
                            }
                    }

                }
            }
        }

        public bool ArrangeItems(byte key1, byte key2)
        {
            //If both item's exist...
            for (byte i = 0; i < _itemContainer.Count; ++i)
            {
                //Get both client index's
                if (_itemContainer[i].key == key1)
                {
                    for (byte j = 0; j < _itemContainer.Count; ++j)
                    {
                        if (_itemContainer[j].key == key2)
                        {
                            ClientItemWrapper temp = _itemContainer[i];
                            _itemContainer.RemoveAt(i);
                            _itemContainer.Insert(j, temp);

                            if (_e.isPlayer)
                                if (((Character)_e).characterSession.inGame)
                                    ServerInventoryItemArrange.InventoryItemArrange(((Character)_e).characterSession, i, j);

                            return true;
                        }
                    }
                    //if we found one item, but not the other break out and return false?
                    break;
                }
            }

            return false;
        }

        //Retrieve's our object reference
        public bool TryRetrieveItem(byte key, out Item item, out byte index)
        {
            for (byte i = 0; i < _itemContainer.Count; ++i)
            {
                if (_itemContainer[i].key == key)
                {
                    item = _itemContainer[i].item;
                    index = i;
                    return true;
                }
            }

            item = default;
            index = 0xFF;
            return false;
        }

        //TODO: Figure out how pulling out items should work. Should we pull the item wrapper out with any item so we can get the relevant key if needed?
        public bool TryRetrieveItem(byte key, out ClientItemWrapper item, out byte index)
        {
            for (byte i = 0; i < _itemContainer.Count; ++i)
            {
                if (_itemContainer[i].key == key)
                {
                    item = _itemContainer[i];
                    index = i;
                    return true;
                }
            }

            item = default;
            index = 0xFF;
            return false;
        }
    }
}
