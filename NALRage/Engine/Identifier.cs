// Copyright (C) Hot Workshop & contributors 2020, 2021.
// Licensed under GNU General Public License version 3.

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;

namespace NALRage.Engine
{
    /// <summary>
    /// Represents a Minecraft-like item identifier.
    /// </summary>
    public struct Identifier
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="Identifier"/> structure.
        /// </summary>
        /// <param name="space">The name space.</param>
        /// <param name="id">The identifier.</param>
        public Identifier(string space, string id)
        {
            Namespace = space;
            Id = id;
        }

        /// <summary>
        /// Gets or sets the names pace of this instance.
        /// </summary>
        [JsonProperty("name")]
        public string Namespace { get; set; }
        
        /// <summary>
        /// Gets or sets the identifier of this instance.
        /// </summary>
        [JsonProperty("identifier")]
        public string Id { get; set; }
    }
}
