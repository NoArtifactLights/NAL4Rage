using System.Drawing;
using Rage;

namespace NALRage.Engine.Modification.API.Events.Integrated
{
    public class ArmedPed : Event
    {
        public override void Start(Ped p)
        {
            if (!Entry.ArmedIds.Contains(p.Handle))
            {
                Blip b = p.AttachBlip();
                b.Color = Color.Red;
                b.Sprite = BlipSprite.Enemy;
                b.Scale = 0.5f;
                Functions.MarkBlipDeletion(b);
                Functions.EquipPedWeapon(p);
                Functions.AddBountyToPed(p);
            }
        }

        public override void Process()
        {
            throw new System.NotImplementedException();
        }
    }
}