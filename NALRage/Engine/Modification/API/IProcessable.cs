namespace NALRage.Engine.Modification.API
{
    /// <summary>
    /// Represents a processable instance. Similar to a scripting component in LSPD First Response.
    /// </summary>
    public interface IProcessable
    {
        /// <summary>
        /// Called when this <see cref="IProcessable"/> starts.
        /// </summary>
        void OnStart();

        /// <summary>
        /// Called each NAL tick after <see cref="OnStart"/> is called.
        /// </summary>
        void Process();

        /// <summary>
        /// Called when this <see cref="IProcessable"/> finalizes.
        /// </summary>
        void Finally();
    }
}
