// Copyright (C) Hot Workshop & contributors 2020, 2021.
// Licensed under GNU General Public License version 3.

using NALRage.Engine.Modification.GameFibers;
using Rage;

namespace NALRage.Engine
{
    internal static class HungryUtils
    {
#pragma warning disable S2223 // Non-constant static fields should not be visible
        internal static GameFiber Fiber;
        internal static GameFiber Drawing;
#pragma warning restore S2223 // Non-constant static fields should not be visible

        internal static void StartFiber()
        {
            Fiber = GameFiber.StartNew(HungryManager.FiberNew);
            Drawing = GameFiber.StartNew(HungryManager.DrawingFiber);
        }
    }
}