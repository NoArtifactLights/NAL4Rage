using NALRage.Entities;
using RAGENativeUI.Elements;

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
        /// Gets whether the mod should be ticking. For example, if your plugin have a loop, please make sure you use a <see langword="while"/> loop and use this property as value.
        /// </summary>
        public static bool InstanceRunning { get; internal set; } = true;
        
        /// <summary>
        /// Gets a big message thread that can be used to display big message.
        /// </summary>
        public static BigMessageThread BigMessage { get; internal set; } = new BigMessageThread();
        
        /// <summary>
        /// Gets the current difficulty.
        /// </summary>
        public static Difficulty Difficulty { get; internal set; }

        /// <summary>
        /// Gets the bonus of having a ped with bounty killed.
        /// You may use <see cref="Modification.API.Functions.AddBountyToPed(Rage.Ped)"/> to add bounty to a ped.
        /// </summary>
        public const int BountyBonus = 30;
    }
}