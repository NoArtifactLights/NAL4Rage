// // NALRage
// // Copyright (C) RelaperCrystal 2021-2021.

using Rage;

namespace NALRage.Engine.Modification.GameFibers
{
    internal static class RespawnManager
    {
        private static bool isRespawning;
        
        internal static void Loop()
        {
            while (Common.InstanceRunning)
            {
                GameFiber.Yield();
                if (!isRespawning && Game.LocalPlayer.Character.IsDead)
                {
                    isRespawning = true;
                }

                if (isRespawning && !Game.IsScreenFadingOut && Game.IsScreenFadedOut)
                {
                    GameFiber.Sleep(3000);
                    Game.FadeScreenIn(100);
                }
            }
        }
    }
}