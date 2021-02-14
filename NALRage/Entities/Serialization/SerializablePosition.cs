using System;
using System.Xml.Serialization;
using Rage;

namespace NALRage.Entities.Serialization
{
    /// <summary>
    /// Represents a <see cref="Rage.Vector3"/> that can be serialized.
    /// </summary>
    [Serializable]
    public class SerializablePosition
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="SerializablePosition"/> class.
        /// Only for serialize-rs.
        /// </summary>
        public SerializablePosition()
        {
        }
        
        /// <summary>
        /// Initializes a new instance of the <see cref="SerializablePosition"/> class.
        /// </summary>
        /// <param name="position">The position.</param>
        public SerializablePosition(Vector3 position)
        {
            X = position.X;
            Y = position.Y;
            Z = position.Z;
        }

        /// <summary>
        /// Gets the X component of this <see cref="SerializablePosition"/>
        /// </summary>
        [XmlAttribute]
        public float X;

        /// <summary>
        /// Gets the Y component of this <see cref="SerializablePosition"/>
        /// </summary>
        [XmlAttribute]
        public float Y;

        /// <summary>
        /// Gets the Z component of this <see cref="SerializablePosition"/>
        /// </summary>
        [XmlAttribute]
        public float Z;

        [NonSerialized] internal Vector3 GeneratedVector3;

        internal void GenerateVector3()
        {
            GeneratedVector3 = this;
        }
        
        /// <inheritdoc />
        public override bool Equals(object obj)
        {
            return obj is SerializablePosition position && this.Equals(position);
        }

        /// <summary>
        /// Determines whether this <see cref="SerializablePosition"/> is equivalent to the specified <see cref="SerializablePosition"/>.
        /// </summary>
        /// <param name="other">The other instance.</param>
        /// <returns><c>true</c> if this instance is equivalent to the specified <see cref="SerializablePosition"/>; otherwise, <c>false</c>.</returns>
        public bool Equals(SerializablePosition other)
        {
            return Math.Abs(other.X - this.X) < 0.05 && Math.Abs(other.Y - this.Y) < 0.05 && Math.Abs(other.Z - this.Z) < 0.05;
        }

        /// <inheritdoc />
        public override int GetHashCode()
        {
            return X.GetHashCode() + Y.GetHashCode();
        }

        /// <summary>
        /// Determines whether the instance on the left equivalent to the instance on the right.
        /// </summary>
        /// <param name="left">The instance on the left.</param>
        /// <param name="right">The instance on the right.</param>
        /// <returns><c>true</c> if this instance is equivalent to the specified <see cref="SerializablePosition"/>; otherwise, <c>false</c>.</returns>
        public static bool operator ==(SerializablePosition left, SerializablePosition right)
        {
            return left.Equals(right);
        }

        /// <summary>
        /// Determines whether the instance on the left isn't equivalent to the instance on the right.
        /// </summary>
        /// <param name="left">The instance on the left.</param>
        /// <param name="right">The instance on the right.</param>
        /// <returns><c>true</c> if this instance isn't equivalent to the specified <see cref="SerializablePosition"/>; otherwise, <c>false</c>.</returns>
        public static bool operator !=(SerializablePosition left, SerializablePosition right)
        {
            return !(left == right);
        }

        /// <summary>
        /// Converts this instance into <see cref="Vector3"/> used in natives.
        /// </summary>
        /// <param name="pos">The instance.</param>
        /// <returns>A Vector3 for method calls.</returns>
        public static implicit operator Vector3(SerializablePosition pos)
        {
            Game.LogTrivial($"{pos.X}, {pos.Y}, {pos.Z}");
            return new Vector3(pos.X, pos.Y, pos.Z);
        }
    }
}
