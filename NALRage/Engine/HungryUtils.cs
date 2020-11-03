using NALRage.Engine.Modification.GameFibers;
using Rage;

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