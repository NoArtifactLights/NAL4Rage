#if DEBUG
using Rage;
using Rage.Native;

namespace NALRage.Engine.Modification.Character
{
    internal static class FaceFeature
    {
        internal static void SetPedFaceFeature(this Ped ped, FaceFeatures features, float value)
        {
            NativeFunction.Natives.x71A5C1DBA060049E(ped, (int)features, value);
        }

        internal static void SetPedHeadBlendData(this Ped ped, int shapeFirstID, int shapeSecondID, int shapeThirdID, int skinFirstID, int skinSecondID, int skinThirdID, float shapeMix, float skinMix, float thirdMix, bool isParent)
        {
            NativeFunction.Natives.x9414E18B9434C2FE(ped, shapeFirstID, shapeSecondID, shapeThirdID, skinFirstID, skinSecondID, skinThirdID, shapeMix, skinMix, thirdMix, isParent);
        }

        internal static void SetPedEyeColor(this Ped ped, int color)
        {
            NativeFunction.Natives.x50B56988B170AFDF(ped, color);
        }

        internal static void SetPedHairColor(this Ped ped, int first, int second)
        {
            NativeFunction.Natives.x4CFFC65454C93A49(ped, first, second);
        }

        internal static void SetPedHeadOverlayData(this Ped ped, int id, int index, float opacity)
        {
            NativeFunction.Natives.x48F44967FA05CC1E(ped, id, index, opacity);
        }

        internal static void SetPedHeadOverlayColor(this Ped ped, int id, int type, int colorId, int secondColorId)
        {
            NativeFunction.Natives.x497BF74A7B9CB952(ped, id, type, colorId, secondColorId);
        }

        internal static void SetPedPropIndex(this Ped ped, int index, int drawable, int texture, bool attach)
        {
            NativeFunction.Natives.SetPedPropIndex(ped, index, drawable, texture, attach);
        }
    }
}

namespace NALRage
{
    internal enum FaceFeatures
    {
        Nose_Width,
        Nose_Peak_Hight,
        Nose_Peak_Lenght,
        Nose_Bone_High,
        Nose_Peak_Lowering,
        Nose_Bone_Twist,
        EyeBrown_High,
        EyeBrown_Forward,
        Cheeks_Bone_High,
        Cheeks_Bone_Width,
        Cheeks_Width,
        Eyes_Openning,
        Lips_Thickness,
        Jaw_Bone_Width, // Bone size to sides  
        Jaw_Bone_Back_Lenght, // Bone size to back  
        Chimp_Bone_Lowering, // Go Down  
        Chimp_Bone_Lenght, // Go forward  
        Chimp_Bone_Width,
        Chimp_Hole,
        Neck_Thikness,
    }
}
#endif