namespace NALRage.Entities
{
    /// <summary>
    /// Represents an item.
    /// </summary>
    public abstract class Item
    {
        /// <summary>
        /// Gets the maximum stack size of this item.
        /// </summary>
        public abstract int MaxStackSize { get; }

        /// <summary>
        /// Gets the name of this item.
        /// </summary>
        public abstract string Name { get; }

        /// <summary>
        /// Uses this item.
        /// </summary>
        /// <returns>The result of usage.</returns>
        public abstract ItemResult Use();
    }
}