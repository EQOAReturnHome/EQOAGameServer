using System.Collections.Generic;

namespace MessageStruct
{
    public struct Message
    {
        public List<byte> ThisMessage { private set; get; }
        public ushort ThisMessagenumber { private set; get; }

        public  Message(ushort num, List<byte> MyMessage)
        {
            this.ThisMessage = MyMessage;
            this.ThisMessagenumber = num;
        }
    }
}
