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
        }

        internal static void Fiber()
        {
            if(hungry <= 2.5f)
            {
                Game.LocalPlayer.Character.Health--;
            }

            float offset = Game.LocalPlayer.Character.IsSprinting ? 0.05f : 0.01f;
            hungry -= offset;

            hungryBar.Percentage = hungry / 10;
        }
    }
}
