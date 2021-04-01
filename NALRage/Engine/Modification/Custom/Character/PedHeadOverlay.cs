// Copyright (C) Hot Workshop & contributors 2020, 2021.
// Licensed under GNU General Public License version 3.

#pragma warning disable S1104, CA1815

namespace NALRage.Engine.Modification.Custom.Character
{
    /// <summary>
    /// Represents a head overlay.
    /// </summary>
    public struct PedHeadOverlay
    {
        /// <summary>
        /// The index.
        /// </summary>
        public int Index;
        /// <summary>
        /// The color.
        /// </summary>
        public int Color;
        /// <summary>
        /// The secondary color.
        /// </summary>
        public int SecondColor;
        /// <summary>
        /// The opacity.
        /// </summary>
        public int Opacity;
    }
}
