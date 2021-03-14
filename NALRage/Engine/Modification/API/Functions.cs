using NALRage.Engine.Modification.API.Events;
using NALRage.Entities;
using Rage;
using Rage.Exceptions;
using Rage.Native;
using System;
using System.Windows.Forms;
using NALRage.Engine.Modification.GameFibers;
using System.Runtime.CompilerServices;

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
        /// Gets or sets a value indicating whether the pedestrians will fight each other.
        /// Randomly assigns weapon to these pedestrians.
        /// </summary>
        public static bool IsInRiot
        {
            get => Common.Riot;
            set
            {
                NativeFunction.Natives.SET_RIOT_MODE_ENABLED(value);
                Common.Riot = value;
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
        /// Increases hungry by specified amount.
        /// </summary>
        /// <param name="value">The hungry amount to increase. If more than <c>10.0F</c>, it will be <c>10.0F</c>.</param>
        public static void IncreaseHungry(float value)
        {
            var tempHungry = HungryManager.Hungry += value;
            tempHungry = tempHungry.LimitRange(0f, 10.0f);
            HungryManager.Hungry = tempHungry;
        }

        /// <summary>
        /// Clamps the value to the limited range, if the value is not in range.
        /// </summary>
        /// <param name="value">The value.</param>
        /// <param name="min">The minimum value.</param>
        /// <param name="max">The maximum value.</param>
        /// <returns>The clamped value.</returns>
        public static float LimitRange(this float value, float min, float max)
        {
            if (value > max) value = max;
            if (value < min) value = min;
            return value;
        }

        /// <summary>
        /// Costs the specified amount of money and returns whether the transaction is successful.
        /// </summary>
        /// <param name="amount">The amount of money to cost.</param>
        /// <returns><c>true</c> if the amount of money is successfully decreased; otherwise, <c>false</c>.</returns>
        public static bool CostMoney(int amount)
        {
            if (Common.Cash < amount)
            {
                Game.DisplaySubtitle("You don't have money to do that.");
                return false;
            }

            Common.Cash -= amount;
            return true;
        }
        
        /// <summary>
        /// Registers a new garage to the specified position, for repairing player's vehicle.
        /// </summary>
        /// <param name="position">The position of the blip.</param>
        /// <param name="addBlip">If <c>true</c>, a blip will be added to the garage.</param>
        /// <exception cref="ArgumentException">The position is <see cref="Vector3.Zero"/>.</exception>
        public static void RegisterGarage(Vector3 position, bool addBlip = true)
        {
            if (position == Vector3.Zero)
            {
                throw new ArgumentException("Position cannot be zero.", nameof(position));
            }
            GameManager.Garages.Add(position);
            if (addBlip)
            {
                var blip = new Blip(position)
                {
                    Sprite = BlipSprite.Garage,
                    Name = "Repair Vehicle"
                };

                Entry.Blips.Add(blip);
            }
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

                case Difficulty.Extreme:
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