using Rage;

namespace NALRage.Engine.Modification.API.Events
{
    /// <summary>
    /// Represents an event.
    /// </summary>
    public abstract class Event
    {
        internal GameFiber Fiber;

        public abstract void Start(Ped p);
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
