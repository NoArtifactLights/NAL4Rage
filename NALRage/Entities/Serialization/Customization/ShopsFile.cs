// Copyright (C) Hot Workshop & contributors 2020, 2021.
// Licensed under GNU General Public License version 3.

using System;

namespace NALRage.Entities.Serialization.Customization
{
    /// <summary>
    /// Represents a food shop file.
    /// </summary>
    [Serializable]
#pragma warning disable CA1815 // Override equals and operator equals on value types
    public struct ShopsFile
#pragma warning restore CA1815 // Override equals and operator equals on value types
    {
        /// <summary>
        /// Represents a list of all food shops defined in this file.
        /// </summary>
#pragma warning disable CA1819 // Properties should not return arrays
        public FoodShop[] Shops { get; set; }
#pragma warning restore CA1819 // Properties should not return arrays
    }
}