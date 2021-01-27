﻿using Rage;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NALRage.Entities.Serialization
{
    /// <summary>
    /// Represents a saved weapon.
    /// </summary>
    public struct SaveWeaponDescriptor
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="SaveWeaponDescriptor"/> class.
        /// </summary>
        /// <param name="wd">The weapon to save.</param>
        /// <exception cref="ArgumentNullException">The weapon is null.</exception>
        public SaveWeaponDescriptor(WeaponDescriptor wd)
        {
            if(wd == null)
            {
                throw new ArgumentNullException();
            }
            Hash = wd.Hash;
            Ammo = wd.Ammo;
            LoadedAmmo = wd.LoadedAmmo;
        }

        /// <summary>
        /// Adds this weapon to player.
        /// </summary>
        public void AddToPlayer()
        {
            Game.LocalPlayer.Character.Inventory.GiveNewWeapon(Hash, Ammo, false);
        }

        /// <summary>
        /// The type of the weapon.
        /// </summary>
        public WeaponHash Hash { get; set; }
        /// <summary>
        /// The ammo amount of the weapon.
        /// </summary>
        public short Ammo { get; set; }
        /// <summary>
        /// The loaded amount amount of the weapon.
        /// </summary>
        public short LoadedAmmo { get; set; }
        

    }
}
