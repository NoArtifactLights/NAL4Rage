// Copyright (C) Hot Workshop & contributors 2020, 2021.
// Licensed under GNU General Public License version 3.

using System.Drawing;
using LemonUI;
using LemonUI.Elements;
using Rage;
using Rage.Native;
using Font = LemonUI.Rage.Font;

namespace NALRage.Engine.UI
{
    internal static class MoneyRenderManager
    {
        private static readonly ScaledText moneyText = new ScaledText(PointF.Empty, "$0", 0.65f, Font.Pricedown)
        {
            Alignment = Alignment.Right,
            Outline = true
        };

        private static void UpdateMoneyRender()
        {
            Screen.SetElementAlignment(GFXAlignment.Right, GFXAlignment.Top);
            var pos = Screen.GetRealPosition(0, 0);
            Screen.ResetElementAlignment();

            if (Hud.IsComponentActive(HudComponent.WantedStars))
            {
                pos.Y += 35;
            }

            moneyText.Position = pos;
            moneyText.Text = $"{Common.Cash}";
        }

        internal static void Fiber()
        {
            GameFiber.Yield();
            if (NativeFunction.Natives.THEFEED_IS_PAUSED<bool>() && Game.IsControlJustPressed(0, GameControl.CharacterWheel))
            {
                Hud.HideComponentThisFrame(HudComponent.Cash);
                Hud.HideComponentThisFrame(HudComponent.CashChange);
                UpdateMoneyRender();
                moneyText.Draw();
            }
        }
    }
}
