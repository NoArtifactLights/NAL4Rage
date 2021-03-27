// Copyright (C) Hot Workshop & contributors 2020, 2021.
// Licensed under GNU General Public License version 3.

using System;

namespace NALRage.Entities.Serialization.Customization
{
    /// <summary>
    /// Represents a weapon shop.
    /// </summary>
    [Serializable]
    public struct WeaponShop
    {
        /// <summary>
        /// Gets or sets the name of this instance.
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// Gets or sets the position of this instance.
        /// </summary>
        public SerializablePosition Position { get; set; }

        /// <inheritdoc />
        public override bool Equals(object obj)
        {
            return obj is WeaponShop weaponShop && (weaponShop).Name == this.Name && ((WeaponShop)obj).Position == this.Position;
        }

        /// <summary>
        /// Do not use.
        /// </summary>
        /// <inheritdoc />
        [Obsolete("Does nothing")]
#pragma warning disable CS0809 
        public override int GetHashCode()
#pragma warning restore CS0809
        {
            throw new NotImplementedException();
        }

        /// <inheritdoc />
        public static bool operator ==(WeaponShop left, WeaponShop right)
        {
            return left.Equals(right);
        }

        /// <inheritdoc />
        public static bool operator !=(WeaponShop left, WeaponShop right)
        {
            return !(left == right);
        }
    }
}
