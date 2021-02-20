using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NALRage.Entities
{
    /// <summary>
    /// Represents the inventory of the player.
    /// </summary>
    public class PlayerInventory
    {
        internal List<ItemStack> Items = new List<ItemStack>();

        internal PlayerInventory()
        {
        }

        /// <summary>
        /// Adds an item to this inventory.
        /// </summary>
        /// <param name="item">The item to add.</param>
        /// <param name="amount">The amount.</param>
        public void Add(Item item, int amount = 1)
        {
            if (item == null)
            {
                throw new ArgumentNullException(nameof(item));
            }

            foreach (var stack in Items)
            {
                if (stack.Item == item)
                {
                    stack.Add(amount);
                    return;
                }
            }

            var newStack = new ItemStack(item);
            newStack.Add(amount);
            Items.Add(newStack);
        }
    }
}
