using Rage;
using System;

namespace NALRage.Entities.Serialization
{
    /// <summary>
    /// Represents the current status of the world.
    /// </summary>
    [Serializable]
    public struct WorldStatus
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="WorldStatus"/> struct.
        /// </summary>
        /// <param name="weather">The weather type.</param>
        /// <param name="hour">Hour of the time.</param>
        /// <param name="minute">Minute of the time.</param>
        public WorldStatus(WeatherType weather, int hour, int minute)
        {
            CurrentWeather = weather;
            Hour = hour;
            Minute = minute;
        }

        /// <summary>
        /// Gets or sets the current weather of this instance.
        /// </summary>
        public WeatherType CurrentWeather { get; set; }
        /// <summary>
        /// Gets or sets the hour time of this instance.
        /// </summary>
        public int Hour { get; set; }
        /// <summary>
        /// Gets or sets the minute time of this instance.
        /// </summary>
        public int Minute { get; set; }
    }
}
