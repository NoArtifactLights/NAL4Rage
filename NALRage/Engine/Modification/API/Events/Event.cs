using System;
using System.Collections.Generic;
using Rage;

namespace NALRage.Engine.Modification.API.Events
{
    /// <summary>
    /// Represents an event.
    /// </summary>
    public abstract class Event : IProcessable
    {
        // internal GameFiber Fiber;

        /// <summary>
        /// Gets or sets the ped allocated to this <see cref="Event"/>.
        /// </summary>
        /// <value>
        /// A <see cref="Ped"/> allocated to this event. The <see cref="Finally"/> method will release this instance.
        /// By default, an ambient ped is passed to this property.
        /// </value>
        public Ped Ped { get; protected set; }

        internal void SetPed(Ped ped)
        {
            Ped = ped;
        }

        public abstract void Process();

        /// <summary>
        /// Called when the event is started.
        /// </summary>
        /// <remarks>
        /// <note type="warning">
        /// Do <b>not</b> use any other <see cref="Ped" /> other than the specified one in the <see cref="Ped"/> property.
        /// </note>
        /// </remarks>
        public abstract void OnStart();

        /// <summary>
        /// Finalizes the in-game resources of this <see cref="Event"/>.
        /// </summary>
        /// <remarks>
        /// In the default implementation, only the <see cref="Rage.Ped"/> defined in <see cref="Ped"/> property is
        /// dismissed. To free other <see cref="Entity"/>-es, you <b>must</b> override this method and then dismisses them. 
        /// You can call this to end this <see cref="Event"/>.
        /// <para>
        /// <note type="implement">
        /// Do <b>not</b> put long blocking calls and infinite loops here; However, the action of freeing a lot of <see cref="Entity"/>-es
        /// in a collection like <see cref="List{T}"/>, or <see cref="Array"/> is allowed. <br />
        /// Also, make sure you don't cause or <see langword="throw"/> <see cref="Exception"/>s here, as it will crash the plugin.
        /// </note>
        /// </para>
        /// </remarks>
        public virtual void Finally()
        {
            IsEnded = true;
            if (Ped)
            {
                Ped.Dismiss();
            }
        }

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