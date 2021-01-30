// NALRage
// Copyright (C) RelaperCrystal 2020-2021.

using System.Drawing;
using Rage;
using Rage.Attributes;
using RAGENativeUI.Elements;

namespace NALRage.Engine.Modification.GameFibers
{
    internal static class HungryManager
    {
        internal static float Hungry = 10f;

        private static readonly TimerBarPool Pool = new TimerBarPool();
        private static BarTimerBar hungryBar;

        [ConsoleCommand("Refills the hungry value.")]
        public static void RefillHungry()
        {
            Hungry = 10f;
        }

        [ConsoleCommand("Prints the raw value of the hungry on the game console.")]
        public static void HungryRaw() => Game.Console.Print(Hungry.ToString("F"));

        private static void Init()
        {
            hungryBar = new BarTimerBar("Hungry");
            Pool.Add(hungryBar);
        }

        internal static void DrawingFiber()
        {
            while (Common.InstanceRunning)
            {
                GameFiber.Yield();
                Pool.Draw();
            }
        }

        internal static void FiberNew()
        {
            Init();
            while (Common.InstanceRunning)
            {
                Fiber();
            }
        }

        private static void Fiber()
        {
            GameFiber.Sleep(1500);
            if (Hungry > 10f) Hungry = 10f;
            
            if (Hungry <= 2.5f)
            {
                Game.LocalPlayer.Character.Health--;
            }

            float offset = 0.01f;
            if (Game.LocalPlayer.Character.IsSprinting)
            {
                offset = 0.1f;
                hungryBar.ForegroundColor = Color.Red;
                hungryBar.BackgroundColor = Color.DarkRed;
            }
            else
            {
                hungryBar.ForegroundColor = Color.White;
                hungryBar.BackgroundColor = Color.Gray;
            }

            Hungry = Hungry - offset;

            hungryBar.Percentage = Hungry / 10;
            GameFiber.Yield();
        }
    }
}
