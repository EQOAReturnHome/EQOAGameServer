using System;

namespace ReturnHome.Server.Items
{
    public readonly struct LootTable
    {

        public readonly int _itempool_id;
        public readonly int _itemid;
        public readonly int _item_rate;


        public LootTable(int itempool_id, int itemID, int item_rate)
        {
            _itempool_id = itempool_id;
            _itemid = itemID;
            _item_rate = item_rate;
        }
    }
}
