// Copyright (C) Hot Workshop & contributors 2020, 2021.
// Licensed under GNU General Public License version 3.

using System;

namespace NALRage.Entities.Serialization.Customization
{
    /// <summary>
    /// Represents a collection of positions.
    /// </summary>
    [Serializable]
    public struct PositionsFile
    {
        /// <summary>
        /// Gets or sets the collection of positions.
        /// </summary>
#pragma warning disable CA1819 // Properties should not return arrays
        public SerializablePosition[] Positions { get; set; }
#pragma warning restore CA1819 // Properties should not return arrays
    }
}
