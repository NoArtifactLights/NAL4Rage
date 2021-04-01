// Copyright (C) Hot Workshop & contributors 2020, 2021.
// Licensed under GNU General Public License version 3.

#pragma warning disable S1104, CA1815

namespace NALRage.Engine.Modification.Custom.Character
{
    /// <summary>
    /// Represents the head blend data.
    /// </summary>
    public struct PedHeadBlend
    {
        /// <summary>
        /// The head shape inherited from father.
        /// </summary>
        public int FatherShape;
        /// <summary>
        /// The head shape inherited from mother.
        /// </summary>
        public int MotherShape;
        /// <summary>
        /// Unknown.
        /// </summary>
        public int OverrideShape;
        /// <summary>
        /// The tone inherited from father.
        /// </summary>
        public int FatherTone;
        /// <summary>
        /// The tone inherited from mother.
        /// </summary>
        public int MotherTone;
        /// <summary>
        /// Unknown.
        /// </summary>
        public int OverrideTone;
        /// <summary>
        /// The value.
        /// </summary>
        public float ShapeVal;
        /// <summary>
        /// The value.
        /// </summary>
        public float ToneVal;
        public float OverrideVal;
        public bool IsParent;
    }
}
