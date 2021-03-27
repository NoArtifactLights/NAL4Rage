// Copyright (C) Hot Workshop & contributors 2020, 2021.
// Licensed under GNU General Public License version 3.

using System;

namespace NALRage.Entities.Serialization.Customization
{
    /// <summary>
    /// Represents a shop of food.
    /// </summary>
    [Serializable]
    public struct FoodShop
    {
        /// <summary>
        /// Gets or sets the position of this <see cref="FoodShop"/>.
        /// </summary>
        public SerializablePosition Position { get; set; }

        /// <summary>
        /// Gets or sets the items of this <see cref="FoodShop"/>.
        /// </summary>
#pragma warning disable CA1819 // Properties should not return arrays
        public FoodItem[] Items { get; set; }
#pragma warning restore CA1819 // Properties should not return arrays
    }
}
