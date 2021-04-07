using System;
using System.Threading.Channels;
using ReturnHome.Utilities;
using ReturnHome.Opcodes;

namespace ReturnHome.PacketProcessing
{
    public class HandleIncPacket
    {
        private readonly SessionManager _sessionManager;

        public HandleIncPacket(SessionManager sessionManager)
        {
            _sessionManager = sessionManager;
        }

        public async void AcceptPacket(ChannelReader<UdpPacketStruct> ChannelReader)
        {
            while (await ChannelReader.WaitToReadAsync())
                while (ChannelReader.TryRead(out UdpPacketStruct item))
                    ProcPacket(item);
        }

        public void ProcPacket(UdpPacketStruct myObject)
        {
            int offset = 0;
            ReadOnlyMemory<byte> ClientPacket = myObject.PacketMemory;
            
            Logger.Info("Grabbed item from queue");

            ///Grab Client Endpoint
            ushort ClientEndpoint = BinaryPrimitiveWrapper.GetLEUShort(ClientPacket, ref offset);

            ///Grab destination endpoint
            ushort EPDest = BinaryPrimitiveWrapper.GetLEUShort(ClientPacket, ref offset);
                
            ///Check if server transfer
            if (!(MessageOpcodeTypes.serverTransfer == EPDest))
            {
                Logger.Info("No Server transfer, processing");

                ///If CRC passes, continue
                if (ClientPacket.Span.Slice(ClientPacket.Length - 4, 4).SequenceEqual(CRC.calculateCRC(ClientPacket.Span.Slice(0, ClientPacket.Length - 4))))
                {
                    Logger.Info("CRC Passed, processing");

                    uint val = Utility_Funcs.Unpack(ClientPacket.Span, ref offset);

                    _sessionManager.ProcessSession(ClientPacket, offset, ClientEndpoint, myObject.PacketIP, val);
                    ///SessionManager.ProcessSession(myPacket, false);
                }

                ///CRC Failed, drop packet
                else
                {
                    ///Break for now
                    ///Probably need to log for time being
                    Logger.Err("Dropping Packet, CRC failed");
                    return;
                }
            }

            else
            {
                ///If server transfer, do server transfer stuff
                Logger.Info("Received server Transfer");
                return;
            }
        }
    }

    ///Outbound CommManager
    public class HandleOutPacket
    {
        private readonly UDPListener _udpListener;
        public HandleOutPacket(UDPListener udpListener)
        {
            _udpListener = udpListener;
        }
        ///Final touches
        ///Adds Endpoints to front and appends CRC
        public void AddEndPoints(PacketCreator packetCreator, Session MySession, byte[] SessionHeader)
        {
            Memory<byte> Packet = new byte[4 + SessionHeader.Length + packetCreator.ReadBytes + 4];

            ///Insert Client Endpoint
            Logger.Info("Adding Client Endpoint");
            BitConverter.GetBytes(UDPListener.ServerEndPoint).CopyTo(Packet[0..2]);

            Logger.Info("Adding Server Endpoint");
            BitConverter.GetBytes(MySession.ClientEndpoint).CopyTo(Packet[2..4]);
            SessionHeader.CopyTo(Packet[4..(4 + SessionHeader.Length)]);
            ReadOnlyMemory<byte> MyPacket = packetCreator.PacketReader();
            MyPacket.CopyTo(Packet.Slice(4 + SessionHeader.Length, MyPacket.Length));

            Logger.Info("Calculating CRC");
            Logger.Info("Appending CRC to packet");
            ///Append CRC now
            CRC.calculateCRC(Packet.Span[0..(Packet.Length - 4)]).CopyTo(Packet[(Packet.Length - 4)..Packet.Length]);

            _udpListener.SendPacket(Packet, MySession.MyIPEndPoint);
        }     
    }
}
