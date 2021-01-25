using System.Drawing;
using Rage;

namespace NALRage.Engine.Modification.API.Events.Integrated
{
    /// <summary>
    /// The example <see cref="ArmedPed"/> event, featuring a random ped equipped with a weapon.
    /// </summary>
    public class ArmedPed : Event
    {
        private Blip blip;

        /// <inheritdoc />
        public override void OnStart()
        {
            if (!Entry.ArmedIds.Contains(Ped.Handle))
            {
                Blip b = Ped.AttachBlip();
                b.Sprite = BlipSprite.Enemy;
                b.Color = Color.Red;
                b.Scale = 0.5f;
                Functions.MarkBlipDeletion(b);
                Functions.EquipPedWeapon(Ped);
                Functions.AddBountyToPed(Ped);
            }
        }

        /// <inheritdoc />
        public override void Process()
        {
            if (!Ped.Exists() || Ped.IsDead)
            {
                Finally();
            }
        }

        /// <inheritdoc />
        public override void Finally()
        {
            if (blip)
            {
                blip.Delete();
            }
            base.Finally();
        }
    }
}