using System;
using System.Collections.Generic;
using Utility;

namespace DNP3
{
    class DNP3Creation
    {
        /// <summary>
        /// Should we just return our DNP3 Byte array here?
        /// </summary>
        /// <returns></returns>
        public static ulong CreateDNP3()
        { 
            ///Gets our time since epoch
            ulong Date1 = DateTime.Now.GetUnixEpoch();

            ///return time * 1000 for milliseconds
            return (ulong)(Date1 * 1000);
        }

        /// <summary>
        /// Create DNP3, but then only return 4 bytes for Session information
        /// </summary>
        /// <returns></returns>
        public static uint DNP3Session()
        {
            ///Gets our time since epoch
            ulong Date1 = DateTime.Now.GetUnixEpoch();

            return (uint)(ByteSwaps.SwapBytes(Date1 * 1000) >> 32);
        }

    }
}
