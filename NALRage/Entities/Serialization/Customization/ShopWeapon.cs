using System;

namespace NALRage.Entities.Serialization.Customization
{
    /// <summary>
    /// Represents a weapon available in a weapon shop.
    /// </summary>
    [Serializable]
    public struct ShopWeapon
    {
        /// <summary>
        /// The name of the weapon.
        /// </summary>
        public string WeaponName { get; set; }

        /// <summary>
        /// The price of the weapon.
        /// Beware of that the price of buying a new weapon and buying ammos of the weapons are the same.
        /// </summary>
        public int Price { get; set; }

        /// <summary>
        /// The amount of the ammo.
        /// </summary>
        public int AmmoAmount { get; set; }
    }
}
