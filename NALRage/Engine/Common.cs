using System;
using System.Collections.Generic;
using LemonUI.Scaleform;
using NALRage.Entities;

namespace NALRage.Engine
{
    /// <summary>
    /// The shared information of the Mod.
    /// </summary>
    public static class Common
    {
        /// <summary>
        /// Gets the amount of cash that the player has.
        /// </summary>
        public static int Cash { get; internal set; }
        
        /// <summary>
        /// Gets the total amount of kills of the player.
        /// </summary>
        public static int Kills { get; internal set; }

        /// <summary>
        /// Gets a value indicating whether the San Andreas is in blackout.
        /// You may use <see cref="Modification.API.Functions.BlackoutStatus"/> to set this property.
        /// </summary>
        public static bool Blackout { get; internal set; }

        /// <summary>
        /// Gets a value indicating whether the pedestrians will fight each other.
        /// </summary>
        public static bool Riot { get; internal set; }

        /// <summary>
        /// Gets a value indicating whether the mod should be ticking. 
        /// For example, if your plug-in have a loop, please make sure your plug-in uses <see langword="while"/> loop 
        /// and use this property as the condition.
        /// </summary>
        public static bool InstanceRunning { get; internal set; } = true;
        
        /// <summary>
        /// Gets the current difficulty.
        /// </summary>
        public static Difficulty Difficulty { get; internal set; }

        /// <summary>
        /// Gets the bonus of having a ped with bounty killed.
        /// Use <see cref="Modification.API.Functions.AddBountyToPed(Rage.Ped)"/> to add bounty to a ped.
        /// </summary>
        public const int BountyBonus = 30;
    }
}