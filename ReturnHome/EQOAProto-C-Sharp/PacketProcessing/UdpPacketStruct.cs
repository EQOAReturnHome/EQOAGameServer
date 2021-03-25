// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the MIT license.

using System;
using System.Net;

namespace ReturnHome.PacketProcessing
{
    public readonly struct UdpPacketStruct
    {
        public readonly Memory<byte> PacketMemory;
        public readonly IPEndPoint PacketIP;
        public readonly long TimeStamp;

        public UdpPacketStruct(Memory<byte> packetMemory, IPEndPoint packetIP)
        {
            PacketMemory = packetMemory;
            PacketIP = packetIP;
            TimeStamp = DateTimeOffset.Now.ToUnixTimeMilliseconds();
        }
    }
}
