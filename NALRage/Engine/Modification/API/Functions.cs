using System;
using System.Linq;
using NALRage.Entities;
using Rage;

namespace NALRage.Engine.Modification.API
{
    /// <summary>
    /// Public functions.
    /// </summary>
    public static class Functions
    {
        public static void SetPedAsWeaponed(Ped ped)
        {
            if(!ped.Exists()) throw new ArgumentNullException(nameof(ped));
            if (Entry.ArmedIds.Contains(ped.Handle)) return;
            
            Entry.ArmedIds.Add(ped.Handle);
        }

        public static void EquipPedWeapon(Ped ped)
        {
            WeaponHash wp;

            if (ped.IsInAnyVehicle(true))
            {
                ped.Tasks.LeaveVehicle(LeaveVehicleFlags.BailOut);
            }

            switch (Common.Difficulty)
            {
                default:
                    wp = WeaponHash.Pistol; 
                    break;
                case Difficulty.Easy:
                    wp = WeaponHash.PumpShotgun;
                    break;
                case Difficulty.Normal:
                    wp = WeaponHash.MicroSMG;
                    break;
                case Difficulty.Hard:
                    wp = WeaponHash.CarbineRifle;
                    break;
                case Difficulty.Nether:
                    wp = WeaponHash.RPG;
                    break;
            }

            if (!ped.Inventory.Weapons.Contains(wp))
            {
                WeaponDescriptor weapon = ped.Inventory.Weapons.Add(wp);
                weapon.Ammo = 10000;
            }
            
           
        }
    }
}