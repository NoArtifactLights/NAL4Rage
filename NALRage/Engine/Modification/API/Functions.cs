using NALRage.Engine.Modification.API.Events;
using NALRage.Entities;
using Rage;
using Rage.Exceptions;
using Rage.Native;
using System;

namespace NALRage.Engine.Modification.API
{
    /// <summary>
    /// Public functions.
    /// </summary>
    public static class Functions
    {
        /// <summary>
        /// Gets or sets a value indicating whether the game is in blackout.
        /// </summary>
        /// <value>
        /// <see langword="true"/> if the San Andreas is currently in blackout; otherwise, <see langword="false"/>.
        /// </value>
        /// <remarks>
        /// <note type="important">Do <b>not</b> use <c>SET_ARTIFICIAL_LIGHTS_STATE</c> for setting blackout status. You should use this property instead.
        /// This allows the mod to process the game environment according to the lights state, and save the lights state.</note>
        /// </remarks>
        public static bool BlackoutStatus
        {
            get => Common.Blackout;
            set
            {
                NativeFunction.Natives.x1268615ACE24D504(value);
                Common.Blackout = value;
            }
        }

        /// <summary>
        /// Adds a bounty status of $100 to the specified <see cref="Ped"/>.
        /// </summary>
        /// <param name="ped">The ped.</param>
        public static void AddBountyToPed(Ped ped)
        {
            if (!ped.Exists()) throw new ArgumentNullException(nameof(ped));
            if (Entry.ArmedIds.Contains(ped.Handle)) return;

            Entry.ArmedIds.Add(ped.Handle);
        }

        /// <summary>
        /// Registers the specified event into the event manager.
        /// </summary>
        /// <param name="type">The type of the event. Must be inherited from <see cref="Event" />.</param>
        /// <exception cref="ArgumentNullException">The <paramref name="type"/> argument is a <see langword="null"/> (or <see langword="Nothing" />) value.</exception>
        /// <exception cref="ArgumentException">The <see cref="Type"/> passed to <paramref name="type"/> argument is not inherited from <see cref="Event"/>.</exception>
        public static void RegisterEvent(Type type)
        {
            if (type is null) throw new ArgumentNullException(nameof(type));
            if (!type.IsAssignableFrom(typeof(Event))) throw new ArgumentException("The type of the argument is invalid. It must inherit Event.", nameof(type));
            EventManager.RegisterEvent(type);
        }

        /// <summary>
        /// Marks a <see cref="Blip"/> to be deleted when the mod is unloading.
        /// </summary>
        /// <param name="blip">The blip.</param>
        public static void MarkBlipDeletion(Blip blip)
        {
            if (blip == null) throw new ArgumentNullException(nameof(blip));
            if (!blip.IsValid()) throw new InvalidHandleableException(blip);
            if (Entry.Blips.Contains(blip)) return;
            Entry.Blips.Add(blip);
        }

        /// <summary>
        /// Equips a weapon on the specified ped, according to the current difficulty.
        /// To give player NAL$ when the ped is killed by player, use <see cref="AddBountyToPed(Ped)" />.
        /// </summary>
        /// <param name="ped">The ped.</param>
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