using System.Collections.Generic;

namespace ReturnHome.Playercharacter.Actor
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
