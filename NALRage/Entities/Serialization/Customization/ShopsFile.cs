using System;

namespace NALRage.Entities.Serialization.Customization
{
    /// <summary>
    /// Represents a food shop file.
    /// </summary>
    [Serializable]
    public struct ShopsFile
    {
        /// <summary>
        /// Represents a list of all food shops defined in this file.
        /// </summary>
        public FoodShop[] Shops;
    }
}