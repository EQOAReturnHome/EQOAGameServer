﻿using System;
using System.Collections.Generic;

namespace Auctions
{
    public class Auction
    {
        private List<byte> ourMessage = new List<byte> { };

        public Auction()
        {

        }

        public byte[] PullAuction()
        {
            ourMessage.Clear();

            return ourMessage.ToArray();
        }
    }
}
