/*
recast4j copyright (c) 2021 Piotr Piastucki piotr@jtilia.org

This software is provided 'as-is', without any express or implied
warranty.  In no event will the authors be held liable for any damages
arising from the use of this software.
Permission is granted to anyone to use this software for any purpose,
including commercial applications, and to alter it and redistribute it
freely, subject to the following restrictions:
1. The origin of this software must not be misrepresented; you must not
 claim that you wrote the original software. If you use this software
 in a product, an acknowledgment in the product documentation would be
 appreciated but is not required.
2. Altered source versions must be plainly marked as such, and must not be
 misrepresented as being the original software.
3. This notice may not be removed or altered from any source distribution.
*/

using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading;
using DotRecast.Core;

namespace DotRecast.Recast
{
    public class Telemetry
    {
        private readonly ThreadLocal<Dictionary<string, RcAtomicLong>> timerStart = new ThreadLocal<Dictionary<string, RcAtomicLong>>(() => new Dictionary<string, RcAtomicLong>());
        private readonly ConcurrentDictionary<string, RcAtomicLong> timerAccum = new ConcurrentDictionary<string, RcAtomicLong>();

        public void StartTimer(string name)
        {
            timerStart.Value[name] = new RcAtomicLong(RcFrequency.Ticks);
        }

        public void StopTimer(string name)
        {
            timerAccum
                .GetOrAdd(name, _ => new RcAtomicLong(0))
                .AddAndGet(RcFrequency.Ticks - timerStart.Value?[name].Read() ?? 0);
        }

        public void Warn(string @string)
        {
            Console.WriteLine(@string);
        }

        public List<Tuple<string, long>> ToList()
        {
            return timerAccum
                .Select(x => Tuple.Create(x.Key, x.Value.Read() / TimeSpan.TicksPerMillisecond))
                .ToList();
        }
    }
}