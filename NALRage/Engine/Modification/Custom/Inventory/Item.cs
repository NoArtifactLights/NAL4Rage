// Copyright (C) Hot Workshop & contributors 2020, 2021.
// Licensed under GNU General Public License version 3.

// FOR EDITORS - THIS FILE IS NOT COMPILED ANYMORE

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using LemonUI.Elements;
using Newtonsoft.Json;

namespace NALRage.Engine.Modification.Custom.Inventory
{
    /// <summary>
    /// Represents an item.
    /// </summary>
    [JsonConverter(typeof(ItemConverter))]
    public abstract class Item
    {
        /// <summary>
        /// The usage result of the item.
        /// </summary>
        public enum UseResult
        {
            /// <summary>
            /// Done using. Should not consume.
            /// </summary>
            Normal,
            /// <summary>
            /// Consume one in the inventory.
            /// </summary>
            Consume
        }

        private PlayerInventory inventory;

        /// <summary>
        /// Gets the name of this instance.
        /// </summary>
        public abstract string Name { get; }

        /// <summary>
        /// Gets the description of this instance. If not set, will display "No Description Available" in the inventory.
        /// </summary>
        public virtual string Description => string.Empty;

        /// <summary>
        /// Gets the icon of this instance. By default, it will display nothing.
        /// </summary>
        public virtual ScaledTexture Icon => new ScaledTexture("", "");

        /// <summary>
        /// Gets the price of this instance.
        /// </summary>
        public virtual int Price => 0;

        /// <summary>
        /// Uses this instance.
        /// </summary>
        /// <returns>The result.</returns>
        public abstract UseResult Use();
    }
}
