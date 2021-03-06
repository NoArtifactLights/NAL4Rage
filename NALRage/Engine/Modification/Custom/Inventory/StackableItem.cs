// Copyright (C) Hot Workshop & contributors 2020, 2021.
// Licensed under GNU General Public License version 3.

// FOR EDITORS - THIS FILE IS NOT COMPILED ANYMORE

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NALRage.Engine.Modification.Custom.Inventory
{
    /// <summary>
    /// Represents an item that can be stacked.
    /// </summary>
    public abstract class StackableItem : Item
    {
        /// <summary>
        /// Gets or sets the current amount of this item.
        /// </summary>
        public int Count { get; set; }

        /// <summary>
        /// Gets the maximum count of this item.
        /// </summary>
        public virtual int MaxCount => 64;
    }
}
