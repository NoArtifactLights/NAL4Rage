#if DEBUG
using System;
using Rage;

namespace NALRage.Engine.Modification.Character
{
    internal static class NextGenCharacter
    {
        internal static void ApplyNextGenCharFeatures(Ped ped)
        {
            if (ped.Model.Name != "mp_f_freemode_01")
            {
                throw new ArgumentException("Ped must be free mode female!", nameof(ped));
            }

            ped.Metadata.IsNextGenChar = true; // unused
            ped.SetPedAimingAsInjuredAnim();
            ped.MovementAnimationSet = new AnimationSet("move_f@injured");
            ped.SetPedHeadBlendData(21, 17, 0, 21, 17, 0, 0.239999995F, 0, 0, false);
            ped.SetPedEyeColor(1);
            ped.SetPedHairColor(52, 11);
            ped.SetPedHeadOverlayData(2, 21, 1);
            ped.SetPedHeadOverlayData(8, 4, 1);
            ped.SetVariation(2, 42, 0);
            ped.SetVariation(3, 5, 0);
            ped.SetVariation(4, 12, 7);
            ped.SetVariation(6, 9, 0);
            ped.SetVariation(8, 60, 1);
            ped.SetVariation(10, -1, 1);
            ped.SetVariation(11, 1, 2);
            ped.SetPedPropIndex(1, 31, 0, true);
        }
    }
}
#endif