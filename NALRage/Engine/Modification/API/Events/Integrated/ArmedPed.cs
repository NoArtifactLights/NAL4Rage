using Rage;

namespace NALRage.Engine.Modification.API.Events.Integrated
{
    public class ArmedPed : Event
    {
        public override void Start(Ped p)
        {
            if (!Entry.ArmedIds.Contains(p.Handle))
            {
                p.EquipWeapon();
                Entry.ArmedIds.Add(p.Handle);
            }
        }

        public override void Process()
        {
            throw new System.NotImplementedException();
        }
    }
}