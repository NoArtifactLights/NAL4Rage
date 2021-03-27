// Copyright (C) Hot Workshop & contributors 2020, 2021.
// Licensed under GNU General Public License version 3.

namespace NALRage.Engine.Modification.API
{
    /// <summary>
    /// Represents a plug-in.
    /// </summary>
    public abstract class Plugin : IProcessable
    {
        /// <summary>
        /// Called when this plug-in finishes it's process.
        /// </summary>
        public abstract void Finally();

        /// <summary>
        /// Called when this plug-in gets started.
        /// </summary>
        public abstract void OnStart();
        
        /// <summary>
        /// Called each NAL tick.
        /// </summary>
        public abstract void Process();
    }
}
