// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the MIT license.

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ReturnHome.Server.EntityObject.Items;
using ReturnHome.Server.EntityObject.Player;
using ReturnHome.Server.Network;
using ReturnHome.Server.Opcodes.Messages.Server;

namespace ReturnHome.Server.Managers
{
    public static class ItemManager
    {

        private static readonly List<Item> itemList = new();

        public static void AddItem(Item item)
        {
            itemList.Add(item);
        }

        public static void GrantItem(Session mySession, int itemID, int qty)
        {
            Item myItem = itemList.Find(item => item.ItemID == itemID);
            Item newItem = myItem.AcquireItem(qty);
            mySession.MyCharacter.Inventory.AddItem(newItem);
        }


        //Really should be called remove quantity
        public static void UpdateQuantity(Session mySession, int itemID, int qty)
        {
            if (Character.CheckIfItemInInventory(mySession, itemID, out byte key, out Item newItem))
                mySession.MyCharacter.Inventory.UpdateQuantity(key, qty);



        }
    }
}
