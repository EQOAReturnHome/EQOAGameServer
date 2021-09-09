using System;
using System.IO;

namespace ReturnHome.Server.Network
{
    public class ClientPacket : Packet
    {
        public bool ProcessPacket(ReadOnlyMemory<byte> buffer)
        {
			int offset = 0;
            //Track memory offset as packet is processed
            try
            {
                Header.Unpack(buffer, ref offset);

                //Need a way to identify an additional bundle from client and to process this

                //If crc checksum is false, crc failed...
                //Verify packet is not a transfer, transfers dont have crc
                //Just drop the packet
                if (!(Header.TargetEndPoint == 0xFFFF))
                {
                    //If packet indicates to cancel session, do it, and stop reading.
                    //One off issue... Server select -> Character select, if ip/endpoint is the same, client will bundle old session disconnect
                    //in same packet as the new connection, difficult to process this in current setup. Easy to ignore and let client resend
                    if (Header.CancelSession)
                        return true;

                    //If CRC fails and packet isn't canceling the session
                    if (!Header.CRCChecksum)
                        return false;

                    //If the buffer size is equal to bytes read + 4 (CRC)
                    //just return true as packets been fully broke down
                    if (buffer.Length == offset + 4)
                        //Packet should just be an ack or session cancel
                        return true;

                    //Read messages, if this fails... drop the packet
                    if (!ReadMessages(buffer, ref offset))
                        return false;
                }

                return true;
            }

            catch (Exception ex)
            {
                //Log exception

                return false;
            }
        }

        private bool ReadMessages(ReadOnlyMemory<byte> buffer, ref int offset)
        {
            //If message type is present, break out messages
            if (Header.ProcessMessage)
            {
                while (offset != buffer.Length)
                {
                    try
                    {
                        var message = new ClientPacketMessage();
                        if (!message.Unpack(buffer, ref offset))
                            return false;

                        Messages.TryAdd(message.Header.MessageNumber, message);
                    }

                    catch (Exception)
                    {
                        // corrupt packet? Maybe figure out some additional logging here
                        return false;
                    }
                }
            }

            return true;
        }

        //Nothing needs to be released... for now.
        public void ReleaseBuffer()
        {

        }
    }
}
