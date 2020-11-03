using Rage;
using System;
using System.Collections.Generic;

namespace NALRage.Engine.Modification.API.Events
{
    internal static class EventManager
    {
        private static readonly List<Type> Events = new List<Type>();

        internal static void RegisterEvent(Type @event)
        {
            if (@event is null) return;
            if (!@event.IsAssignableFrom(typeof(Event))) return;
            Game.LogTrivial("Registering event " + @event.Name + " from assembly " + @event.Assembly.GetName().CodeBase);
            Events.Add(@event);
        }

        internal static bool IsDisabled { private get; set; } = false;

        internal static void StartRandomEvent(Ped p)
        {
            Logger.Info("EventManager", "Random event start triggered");
            Logger.Debug("EventManager", "There are total of " + Events.Count + " events available");
            if (Events.Count == 0) return;
            if (IsDisabled) return;
            Logger.Trace("EventManager", "Picking event");
            var r = new Random();
            var result = r.Next(0, Events.Count + 1); // this may prevent picker picking the last event
            if (p.IsInAnyVehicle(false)) p.Tasks.LeaveVehicle(LeaveVehicleFlags.BailOut);
            var obj = Activator.CreateInstance(Events[result]);
            var instance = (Event)obj;
            Logger.Trace("EventManager", "Starting " + Events[result].Name + " event");
            try
            {
                instance.Start(p);
                //processing.Add(instance);
            }
            catch (Exception ex)
            {
                Logger.Error("EventManager", "Error while starting event");
                Logger.Error("EventManager", ex.ToString());
            }
        }
    }
}