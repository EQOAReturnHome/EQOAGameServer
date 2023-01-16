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

        private byte type = 0;

        public int Tunar
        {
            get { return _tunar; }
            private set
            {
                _tunar = value;
                if (_e.isPlayer)
                    ServerUpdateTunar.UpdateTunar(((Character)_e).characterSession, Inventory ? GameOpcode.PlayerTunar : GameOpcode.DepositBankTunar, _tunar);
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
                type = 1;
            else
                type = 2;
        }

        public void AddTunar(int tunar) => _tunar += tunar;

        public void RemoveTunar(int tunar) => _tunar -= tunar;

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
                //if(itemToBeAdded.Lore == true)

                _itemContainer.Add(new ClientItemWrapper(itemToBeAdded, _counter++));
                itemToBeAdded.Location = type;
                itemToBeAdded.ClientIndex = (byte)_itemContainer.Count;
                if (_e.isPlayer && !loot)
                    if (((Character)_e).characterSession.inGame)
                    {
                        if (Inventory)
                            ServerAddInventoryItemQuantity.AddInventoryItemQuantity(((Character)_e).characterSession, itemToBeAdded);

                        else
                            ServerAddBankItemQuantity.AddBankItemQuantity(((Character)_e).characterSession, itemToBeAdded);
                    }

                return true;
            }

            return false;
        }

        public void SaveItem(Item itemToBeSaved, bool loot = false)
        {
            CharacterSQL sql = new();
            //Save the item to the players DB entries immediately
            sql.AddPlayerItem((Character)_e, itemToBeSaved);
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
                        {
                            if (Inventory)
                                ServerRemoveInventoryItemQuantity.RemoveInventoryItemQuantity(((Character)_e).characterSession, item.StackLeft, (byte)i);

                            else
                                ServerRemoveBankItemQuantity.RemoveBankItemQuantity(((Character)_e).characterSession, item.StackLeft, (byte)i);
                        }
                    return true;
                }
            }

            return false;
        }

        public void UpdateQuantity(byte key, int quantityToDestroy)
        {
            CharacterSQL sql = new CharacterSQL();
            for (int i = 0; i < _itemContainer.Count; ++i)
            {
                if (_itemContainer[i].key == key)
                {
                    if (_itemContainer[i].item.StackLeft >= quantityToDestroy)
                    {
                        if (_itemContainer[i].item.StackLeft == quantityToDestroy)
                        {
                            Console.WriteLine($"Deleting Item {_itemContainer[i].item.ID}");
                            sql.DeletePlayerItem(_itemContainer[i].item.ID);
                            RemoveItem(key);
                        }



                        else
                        {
                            _itemContainer[i].item.StackLeft -= quantityToDestroy;
                            if (_e.isPlayer)
                                if (((Character)_e).characterSession.inGame)
                                {
                                    if (Inventory)
                                    {
                                        Console.WriteLine($"Updating item instead of deleting it");
                                        ServerRemoveInventoryItemQuantity.RemoveInventoryItemQuantity(((Character)_e).characterSession, _itemContainer[i].item.StackLeft, (byte)i);
                                        sql.UpdatePlayerItem(((Character)_e).characterSession.MyCharacter, quantityToDestroy, _itemContainer[i].item.ID);
                                    }
                                    else
                                        ServerRemoveBankItemQuantity.RemoveBankItemQuantity(((Character)_e).characterSession, _itemContainer[i].item.StackLeft, (byte)i);
                                }
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
        }
    }
