using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Policy;
using System.Text;
using System.Threading.Tasks;

namespace NALRage.Entities
{
    /// <summary>
    /// Represents a stack of items.
    /// </summary>
    public class ItemStack
    {
        private int _count;

        /// <summary>
        /// Occurs when the <see cref="Count"/> is changed.
        /// </summary>
        public event EventHandler CountChanged;

        /// <summary>
        /// Initializes a new instance of the <see cref="ItemStack"/> class.
        /// </summary>
        /// <param name="item">The item.</param>
        public ItemStack(Item item)
        {
            Item = item;
        }

        /// <summary>
        /// Gets the count of the items of this instance.
        /// </summary>
        public int Count
        {
            get => _count;
            set
            {
                _count = value;
                CountChanged?.Invoke(this, EventArgs.Empty);
            }
        }

        /// <summary>
        /// Gets the item that this instance stores.
        /// </summary>
        public Item Item { get; }

        /// <summary>
        /// Determines whether this instance can be consumed.
        /// </summary>
        /// <returns>If <c>true</c>, you can now consume the amounts.</returns>
        public bool IsConsumeable()
        {
            return Count != 0;
        }

        /// <summary>
        /// Adds the numbers of items to the total numbers of item. If it exceeds the limit, it will be set to the maximum stack size of items.
        /// </summary>
        /// <param name="amount">The amount to add.</param>
        public void Add(int amount = 1)
        {
            var temp = Count + amount;
            if (temp > Item.MaxStackSize)
            {
                Count = Item.MaxStackSize;
                return;
            }

            Count = temp;
        }

        /// <summary>
        /// Consumes one of this item.
        /// </summary>
        public void Consume()
        {
            if (!IsConsumeable()) return;
            Count--;
        }
    }
}
