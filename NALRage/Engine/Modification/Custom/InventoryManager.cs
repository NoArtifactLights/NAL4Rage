// Copyright (C) Hot Workshop & contributors 2020, 2021.
// Licensed under GNU General Public License version 3.

// FOR EDITORS - THIS FILE IS NOT COMPILED ANYMORE

using System;
using System.Collections.Generic;
using NALRage.Engine.Modification.Custom.Inventory;

namespace NALRage.Engine.Modification.Custom
{
    internal static class InventoryManager
    {
        private static readonly List<Type> items = new List<Type>();
        private static PlayerInventory currentInventory;
        
        internal static void LoadInventory(Item[] items)
        {
            // TODO complete inventory feature
        }
    }
}
