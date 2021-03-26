#if DEBUG
using Rage;
using Rage.Native;

namespace NALRage.Engine.Modification.Character
{
    internal static class FlagSet
    {
        internal static void SetPedAimingAsInjuredAnim(this Ped ped)
        {
            NativeFunction.Natives.x1913FE4CBF41C463(ped, 187, true);
        }
    }
}
#endif