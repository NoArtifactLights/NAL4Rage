using System.Drawing;
using Rage;

namespace NALRage.Engine.Modification.API.Events.Integrated
{
    /// <summary>
    /// The example <see cref="ArmedPed"/> event, featuring a random ped equipped with a weapon.
    /// </summary>
    public class ArmedPed : Event
    {
        private Blip _blip;

        /// <inheritdoc />
        public override void OnStart()
        {
            if (!Entry.ArmedIds.Contains(Ped.Handle))
            {
                _blip = Ped.AttachBlip();
                _blip.Sprite = BlipSprite.Enemy;
                _blip.Color = Color.Red;
                _blip.Scale = 0.5f;
                Functions.MarkBlipDeletion(_blip);
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
            if (_blip)
            {
                _blip.Delete();
            }
            base.Finally();
        }
    }
}