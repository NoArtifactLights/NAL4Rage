using NALRage.Engine.Modification.API.Events;

namespace NALRage.Entities.Serialization
{
    /// <summary>
    /// Represents the configuration structure.
    /// </summary>
    public struct Configuration
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="Configuration"/> class.
        /// </summary>
        /// <param name="version">The configuration format version.</param>
        public Configuration(int version)
        {
            Version = version;
            EventRequirement = 59;
            EventMax = 89;
            EventMinimal = 9;
            ProcessInterval = 100;
            DefaultDifficulty = Difficulty.Initial;
            Riot = true;
        }

        /// <summary>
        /// Gets or sets the format version of this <see cref="Configuration"/>.
        /// </summary>
        public int Version { get; set; }
        /// <summary>
        /// Gets or sets a value between <see cref="EventMax"/> and <see cref="EventMinimal"/> to let event engine randomizer determine whether to start an event.
        /// </summary>
        /// <remarks>
        /// The event engine uses a randomizer to determine whether to create ambient <see cref="Event"/>s. The randomizer is limited by the <see cref="EventMinimal"/> and the
        /// <see cref="EventMax"/> values, then it checks if the generated number matches this property, and if match, creates an event.
        /// </remarks>
        public int EventRequirement { get; set; }
        /// <summary>
        /// Gets or sets the maximum value of the randomizer of the event engine. Should be the same or bigger than <see cref="EventRequirement"/>.
        /// </summary>
        public int EventMax { get; set; }
        /// <summary>
        /// Gets or sets the minimum value of the randomizer of the event engine. Should be smaller than <see cref="EventRequirement"/>.
        /// </summary>
        public int EventMinimal { get; set; }
        /// <summary>
        /// Gets or sets the interval between two NAL ticks.
        /// </summary>
        /// <remarks>
        /// The modification does most kind of looping things in NAL ticks. By default, NAL ticks are triggered each 100 milliseconds. Events get fired and processed
        /// in NAL ticks, and also processing internal engine changes, process game entities, etc. The reason of not to run loop in each game tick is to prevent lagging and also let
        /// the event trigger randomizer to work correctly in a desired chance to fire an event in the default values.
        /// <para>
        /// Notes that there are some loops did not get processed in NAL ticks:
        /// <list type="bullet">
        ///     <item><description>Hungry is set to tick each one and a half second to reduce the speed of hungry dropping.</description></item>
        ///     <item><description>All in-game rendering loops are set to tick each game tick to let rendering work correctly, such as menus.</description></item>
        ///     <item><description>All external rendering loops are set to tick each frame to let rendering work correctly.</description></item>
        /// </list>
        /// </para>
        /// </remarks>
        public int ProcessInterval { get; set; }
        /// <summary>
        /// Gets or sets the default difficulty of the modification.
        /// </summary>
        public Difficulty DefaultDifficulty { get; set; }
        public bool Riot { get; internal set; }
    }
}
