using System;

namespace NALRage.Entities.Serialization.Customization
{
    /// <summary>
    /// Represents a collection of positions.
    /// </summary>
    [Serializable]
    public struct PositionsFile
    {
        /// <summary>
        /// Gets or sets the collection of positions.
        /// </summary>
        public SerializablePosition[] Positions;
    }
}
