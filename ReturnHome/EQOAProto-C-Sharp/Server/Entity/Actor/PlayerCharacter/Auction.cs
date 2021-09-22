using System.Collections.Generic;

namespace ReturnHome.Playercharacter.Actor
{
    public class Auction
    {
        private List<byte> ourMessage = new List<byte> { };

        public Auction()
        {

        }

        public byte[] DumpAuction()
        {
            ourMessage.Clear();

            return ourMessage.ToArray();
        }

        public int GetSize()
        {
            int size = 0;

            return size;
        }
    }
}
