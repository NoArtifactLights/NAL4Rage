using NALRage.Engine.Modification.GameFibers;
using Rage;

namespace NALRage.Engine
{
    internal static class HungryUtils
    {
        internal static GameFiber Fiber;
        internal static GameFiber Drawing;

        internal static void StartFiber()
        {
            Fiber = GameFiber.StartNew(HungryManager.FiberNew);
            Drawing = GameFiber.StartNew(HungryManager.DrawingFiber);
        }
    }
}