// Copyright (C) Hot Workshop & contributors 2020, 2021.
// Licensed under GNU General Public License version 3.

using System;
using System.Xml.Serialization;

namespace NALRage.Entities.Serialization.Customization
{
    /// <summary>
    /// Represents an item that a food shop sells.
    /// </summary>
    [Serializable]
    public struct FoodItem
    {
        /// <summary>
        /// Gets or sets the display name of this <see cref="FoodItem"/>.
        /// </summary>
        [XmlAttribute]
        public string Name { get; set; }

        /// <summary>
        /// Gets or sets the amount of hungry that this <see cref="FoodItem"/> increases when used.
        /// </summary>
        [XmlAttribute]
        public float Amount { get; set; }

        /// <summary>
        /// Gets or sets the amount of NAL$ that this <see cref="FoodItem"/> costs.
        /// </summary>
        [XmlAttribute]
        public int Price { get; set; }
    }
}
