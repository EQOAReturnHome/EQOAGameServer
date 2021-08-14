using System;

namespace ReturnHome.Server.Network
{
	//Contains bundle header flags along with BundleTypeFlags
    public enum PacketBundleFlags  : byte
    {
        NewProcessMessages      = 0x00,
		NewProcessReport        = 0x03,
		ProcessMessageAndReport = 0x0D,
		ProcessMessages         = 0x20,
		ProcessReport           = 0x23,
		ProcessAll              = 0x63
    }
}
