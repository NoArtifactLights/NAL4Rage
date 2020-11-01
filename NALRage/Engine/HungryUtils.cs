using NALRage.Engine.Modification.GameFibers;
using Rage;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NALRage.Engine
{
    internal static class HungryUtils
    {
        internal static GameFiber Fiber;

        internal static void StartFiber()
        {
            Fiber = GameFiber.ExecuteNewFor(HungryManager.Fiber, -1);
        }
    }
}
