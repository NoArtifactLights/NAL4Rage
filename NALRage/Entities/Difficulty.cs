// Copyright (C) Hot Workshop & contributors 2020, 2021.
// Licensed under GNU General Public License version 3.

using NALRage.Engine;

namespace NALRage.Entities
{
    /// <summary>
    /// Represents the difficulty.
    /// </summary>
    /// <remarks>
    /// Difficulties are increased when the player kill count (accessible via <see cref="Common.Kills"/> property) has matched a specified amount. The default kill amount is displayed
    /// as follows:
    /// <list type="table">
    /// <header>
    ///     <term>Difficulty</term>
    ///     <description>The difficulty.</description>
    /// </header>
    /// <header>
    ///     <term>Kill Amount</term>
    ///     <description>Kill Amount required.</description>
    /// </header>
    /// <item>
    ///     <term>Difficulty</term>
    ///     <description>Easy</description>
    /// </item>
    /// <item>
    ///     <term>Kill Amount</term>
    ///     <description>100</description>
    /// </item>
    /// <item>
    ///     <term>Difficulty</term>
    ///     <description>Normal</description>
    /// </item>
    /// <item>
    ///     <term>Kill Amount</term>
    ///     <description>300</description>
    /// </item>
    /// <item>
    ///     <term>Difficulty</term>
    ///     <description>Hard</description>
    /// </item>
    /// <item>
    ///     <term>Kill Amount</term>
    ///     <description>700</description>
    /// </item>
    /// <item>
    ///     <term>Difficulty</term>
    ///     <description>Extreme</description>
    /// </item>
    /// <item>
    ///     <term>Kill Amount</term>
    ///     <description>1500</description>
    /// </item>
    /// </list>
    /// </remarks>
    public enum Difficulty
    {
        /// <summary>
        /// The difficulty of the beginning of the game.
        /// </summary>
        Initial,
        /// <summary>
        /// The difficulty of the early status of the game.
        /// </summary>
        Easy,
        /// <summary>
        /// The difficulty of the mid-game.
        /// </summary>
        Normal,
        /// <summary>
        /// The difficulty of the late stage of the game.
        /// </summary>
        Hard,
        /// <summary>
        /// The difficulty of the end-game.
        /// </summary>
        Extreme
    }
}
