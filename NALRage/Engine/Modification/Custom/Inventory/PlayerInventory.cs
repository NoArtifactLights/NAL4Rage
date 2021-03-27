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
    internal class PlayerInventory
    {
        private readonly List<Item> items = new List<Item>();

        internal void AddItem(Item item)
        {
            items.Add(item);
        }

        internal void AddItem(StackableItem item)
        {
            foreach (var inventoryItem in items)
            {
                if (item.GetType() == inventoryItem.GetType() && ((StackableItem)inventoryItem).MaxCount < ((StackableItem)inventoryItem).Count)
                {
                    ((StackableItem)inventoryItem).Count++;
                    return;
                }
            }

            items.Add(item);
        }
    }
}
