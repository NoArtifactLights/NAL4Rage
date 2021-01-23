using Rage;

namespace NALRage.Engine.Modification.API.Events
{
    /// <summary>
    /// Represents an event.
    /// </summary>
    public abstract class Event
    {
        // internal GameFiber Fiber;

        /// <summary>
        /// Called when the event is started.
        /// </summary>
        /// <remarks>
        /// <note type="warning">
        /// Do <b>not</b> use any other <see cref="Ped" /> other than the specified one passed to you by <paramref name="p"/>.
        /// </note>
        /// </remarks>
        /// <param name="p">The ped to handle the event.</param>
        public abstract void Start(Ped p);

        /// <summary>
        /// Called every NAL tick (each 100 ms).
        /// </summary>
        public abstract void Process();

        public virtual bool IsEnded { get; set; }

        // internal void FiberThread()
        // {
        //     Start(p);
        //     while(!IsEnded)
        //     {
        //         GameFiber.Yield();
        //         Process();
        //     }
        // }
    }
}