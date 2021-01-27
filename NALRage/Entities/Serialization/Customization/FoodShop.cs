using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NALRage.Entities.Serialization.Customization
{
    /// <summary>
    /// Represents a shop of food.
    /// </summary>
    [Serializable]
    public struct FoodShop
    {
        /// <summary>
        /// Gets or sets the position of this <see cref="FoodShop"/>.
        /// </summary>
        public SerializablePosition Position;

        /// <summary>
        /// Gets or sets the items of this <see cref="FoodShop"/>.
        /// </summary>
        public FoodItem[] Items;
    }
}
