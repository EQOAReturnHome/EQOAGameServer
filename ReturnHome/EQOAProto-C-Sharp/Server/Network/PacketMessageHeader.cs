using System;
using System.IO;
using ReturnHome.Utilities;

namespace ReturnHome.Server.Network
{
    public class PacketMessageHeader
    {
        public int HeaderSize { get; set; }

        public byte MessageType { get; set; }
        public ushort MessageNumber { get; set; }
        public ushort Opcode { get; private set; }
        public ushort Size { get; set; }
		public int Count { get; set; }
        public int Index { get; set; }
        public bool Split { get; set; }

        public void Unpack(ReadOnlyMemory<byte> buffer, ref int offset)
        {
            //Read first byte, if it is FF, read an additional byte (Indicates >255byte message
            byte temp= buffer.GetByte(ref offset);
			if (temp == 0xFF)
			{
				MessageType   = buffer.GetByte(ref offset);
                Size          = (ushort)(buffer.GetLEUShort(ref offset) - 2);
				MessageNumber = buffer.GetLEUShort(ref offset);

                //Message is split across 2+ packets
				//Does the client even utilize this? Don't think it does/will can maybe remove
                if (MessageType == 0xFA)
					Split = true;
			}
			
			//Single byte message type, may be unreliable/reliable
			else
			{
				if (temp == 0xFA || temp == 0xFB || temp == 0xF9 || temp == 0xFC)
				{
                    MessageType = temp;

                    //Check if split
                    if (MessageType == 0xFA)
						Split = true;

                    //Subtract 2 because we will read the opcode off
					Size = (ushort)(buffer.GetByte(ref offset) - 2);

                    //FC type is of an unreliable nature and does not have a message#
                    if (!(MessageType == 0xFC))
						MessageNumber = buffer.GetLEUShort(ref offset);
                }
				
				//Eventually check for unreliable messages "Character updates" from client
			}

            Opcode = buffer.GetLEUShort(ref offset);
        }
    }
}
