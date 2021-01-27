using System.Drawing;
using Rage;
using Rage.Attributes;
using RAGENativeUI.Elements;

namespace NALRage.Engine.Modification.GameFibers
{
    // TODO test
    internal static class HungryManager
    {
        private static float hungry = 10f;

        private static readonly TimerBarPool Pool = new TimerBarPool();
        private static BarTimerBar hungryBar;

        [ConsoleCommand("Refills the hungry value.")]
        public static void RefillHungry()
        {
            hungry = 10f;
        }

        [ConsoleCommand("Prints the raw value of the hungry on the game console.")]
        public static void HungryRaw() => Game.Console.Print(hungry.ToString("F"));

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
            if (hungry > 10f) hungry = 10f;
            
            if (hungry <= 2.5f)
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

            hungry = hungry - offset;

            hungryBar.Percentage = hungry / 10;
            GameFiber.Yield();
        }
    }
}
