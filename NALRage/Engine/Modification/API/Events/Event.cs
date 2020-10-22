using Rage;

namespace NALRage.Engine.Modification.API.Events
{
    /// <summary>
    /// Represents an event.
    /// </summary>
    public abstract class Event
    {
        internal GameFiber Fiber;

        public abstract void Start();
        public abstract void Process();

        public virtual bool IsEnded { get; set; }

        internal void FiberThread()
        {
            Start();
            while(!IsEnded)
            {
                GameFiber.Yield();
                Process();
            }
        }
    }
}
