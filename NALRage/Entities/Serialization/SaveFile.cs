namespace NALRage.Entities.Serialization
{
    /// <summary>
    /// Represents the save file structure.
    /// </summary>
    public struct SaveFile
    {
        /// <summary>
        /// The version of the save file.
        /// </summary>
        public int Version { get; set; }
        
        /// <summary>
        /// The X component of the position of the player.
        /// </summary>
        public float PlayerX { get; set; }
        /// <summary>
        /// The Y component of the position of the player.
        /// </summary>
        public float PlayerY { get; set; }
        /// <summary>
        /// The Z component of the position of the player.
        /// </summary>
        public float PlayerZ { get; set; }
        /// <summary>
        /// The status of the world.
        /// </summary>
        public WorldStatus Status { get; set; }
        /// <summary>
        /// Whether the game is in blackout.
        /// </summary>
        public bool Blackout { get; set; }
        /// <summary>
        /// The current difficulty of the game.
        /// </summary>
        public Difficulty CurrentDifficulty { get; set; }
        /// <summary>
        /// The total kills of the game.
        /// </summary>
        public int Kills { get; set; }
        /// <summary>
        /// The cash amount of the player.
        /// </summary>
        public int Cash { get; set; }
        /// <summary>
        /// An array of all weapons.
        /// </summary>
#pragma warning disable CA1819 // Properties should not return arrays
        public SaveWeaponDescriptor[] Weapons { get; set; }
#pragma warning restore CA1819 // Properties should not return arrays
        /// <summary>
        /// The armor amount of the player.
        /// </summary>
        public int PlayerArmor { get; set; }
        /// <summary>
        /// The health amount of the player.
        /// </summary>
        public int PlayerHealth { get; set; }
    }
}
