namespace ReturnHome.Server.Network
{
    public class SessionConnectionData
    {
		public ushort lastReceivedMessageSequence { get; set; }
		public ushort lastReceivedPacketSequence { get; set; }
        public ushort lastSentMessageSequence { get; set; }
        public ushort lastSentPacketSequence { get; set; }


        public SessionConnectionData()
        {
            lastReceivedMessageSequence = 0;
            lastReceivedPacketSequence = 0;
			lastSentMessageSequence = 1;
			lastSentPacketSequence = 1;
        }
    }
}