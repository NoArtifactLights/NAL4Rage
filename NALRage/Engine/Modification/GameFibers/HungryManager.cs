using Rage;
using RAGENativeUI.Elements;

namespace NALRage.Engine.Modification.GameFibers
{
    // TODO test
    internal static class HungryManager
    {
        private static float hungry;

        private static TimerBarPool pool = new TimerBarPool();
        private static BarTimerBar hungryBar;

        internal static void Init()
        {
            hungryBar = new BarTimerBar("Hungry");
            pool.Add(hungryBar);
        }

        internal static void FiberNew()
        {
            Init();
            while(true)
            {
                GameFiber.Yield();
                pool.Draw();
            }
        }

        internal static float Precentage => hungry * 10;

        internal static void Fiber()
        {
            GameFiber.Sleep(1499);
            if (hungry < 10f) hungry = 10f;
            
            if(hungry <= 2.5f)
            {
                Game.LocalPlayer.Character.Health--;
            }

            float offset = Game.LocalPlayer.Character.IsSprinting ? 0.05f : 0.01f;
            hungry -= offset;

            hungryBar.Percentage = hungry / 10;
            GameFiber.Yield();
        }
    }
}
