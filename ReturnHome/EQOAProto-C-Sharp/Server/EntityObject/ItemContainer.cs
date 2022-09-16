using System.Collections.Concurrent;
using ReturnHome.Server.EntityObject.Items;

namespace ReturnHome.Server.EntityObject
{
    //TODO: Rewrite ItemContainer class to handle sending messages if a player
    public class ItemContainer
    {
        private Entity _e;
        private ConcurrentDictionary<byte, Item> _itemContainer;
        private byte _counter = 0;
        private int _tunar;

        //Indicates if inventory or bank
        public bool Inventory;

        private byte type = 0;

        public int Tunar => _tunar;

        public int Count => _itemContainer.Count;

        public ConcurrentDictionary<byte, Item> itemContainer => _itemContainer;

        public ItemContainer(int tunar, Entity e, bool inventory = true)
        {
            _e = e;
            _tunar = tunar;
            _itemContainer = new();
            Inventory = inventory;
            if (inventory)
                type = 1;
            else
                type = 2;
        }

        public void AddTunar(int tunar)
        {
            _tunar += tunar;
        }

        public void RemoveTunar(int tunar)
        {
            _tunar -= tunar;
        }

        public int GetTunar()
        {
            return _tunar;
        }

        public bool Exists(byte key)
        {
            return _itemContainer.ContainsKey(key);
        }

        public bool AddItem(Item itemToBeAdded)
        {
            if (_itemContainer.TryAdd(_counter, itemToBeAdded))
            {
                itemToBeAdded.ServerKey = _counter;
                itemToBeAdded.Location = type;
                itemToBeAdded.ClientIndex = (byte)(_itemContainer.Count - 1);

                _counter++;
                return true;
            }

            return false;
        }

        public bool RemoveItem(byte index, out Item item, out byte clientIndex)
        {
            item = default;
            clientIndex = 0;
            if (_itemContainer.TryRemove(index, out item))
            {
                clientIndex = item.ClientIndex;

                //Adjust Client index's for items
                AdjustClientIndex(clientIndex);

                return true;
            }

            return false;
        }

        public bool UpdateQuantity(byte index, int quantityToDestroy, out Item item)
        {
            item = default;
            if (_itemContainer.TryGetValue(index, out Item temp))
            {
                if (temp.StackLeft >= quantityToDestroy)
                {
                    if(temp.StackLeft == quantityToDestroy)
                        RemoveItem(index, out _, out _);

                    else
                        temp.StackLeft -= quantityToDestroy;

                    item = temp;
                    return true;
                }
            }

            return false;
        }

        public bool ArrangeItems(byte itemSlot1, byte itemSlot2, out byte clientItem1, out byte clientItem2)
        {
            clientItem1 = 0;
            clientItem2 = 0;
            //If both item's exist...
            if (_itemContainer.ContainsKey(itemSlot1) && _itemContainer.ContainsKey(itemSlot2))
            {
                //Get both client index's
                if (_itemContainer.TryGetValue(itemSlot1, out Item temp) && _itemContainer.TryGetValue(itemSlot2, out Item temp2))
                {
                    //Client1 old index
                    clientItem1 = temp.ClientIndex;
                    //New index
                    clientItem2 = temp2.ClientIndex;

                    AdjustClientIndex(clientItem1, clientItem2);

                    return true;
                }
            }

            return false;
        }

        public bool RetrieveItem(byte itemToTransfer, out Item item)
        {
            if (_itemContainer.TryGetValue(itemToTransfer, out Item temp))
            {
                item = temp;
                return true;
            }

            item = default;
            return false;
        }

        private void AdjustClientIndex(byte clientIndexToAdjust, byte newIndex = 0xFF)
        {
            //Means we deleted an item, and reorganize
            if (newIndex == 0xFF)
            {
                foreach (var item in _itemContainer.Values)
                {
                    //Lower index value by 1
                    if (item.ClientIndex > clientIndexToAdjust)
                        item.ClientIndex--;
                }
            }

            //Need to shuffle items around to fit an arrange
            else
            {
                if (clientIndexToAdjust > newIndex)
                {
                    foreach (var item in _itemContainer.Values)
                    {
                        if (item.ClientIndex == clientIndexToAdjust)
                            item.ClientIndex = newIndex;

                        else if (item.ClientIndex >= newIndex && item.ClientIndex < clientIndexToAdjust)
                            item.ClientIndex++;
                    }
                }

                else
                {
                    foreach (var item in _itemContainer.Values)
                    {
                        if (item.ClientIndex == clientIndexToAdjust)
                            item.ClientIndex = newIndex;

                        else if (item.ClientIndex <= newIndex && item.ClientIndex > clientIndexToAdjust)
                            item.ClientIndex--;
                    }
                }
            }
        }
    }
}
