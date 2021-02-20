namespace NALRage
{
    /// <summary>
    /// An enumeration of results of usage of an item.
    /// </summary>
    public enum ItemResult
    {
        /// <summary>
        /// The item has been successfully used. Does not consume.
        /// </summary>
        Success,
        /// <summary>
        /// The item has been successfully consumed.
        /// </summary>
        Consume,
        /// <summary>
        /// The item is failed to be used.
        /// </summary>
        Failed,
        /// <summary>
        /// The item usage has been dismissed/canceled.
        /// </summary>
        Dismiss
    }
}