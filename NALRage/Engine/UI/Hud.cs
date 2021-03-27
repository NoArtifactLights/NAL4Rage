// Copyright (C) Hot Workshop & contributors 2020, 2021.
// Licensed under GNU General Public License version 3.

using Rage.Native;

namespace NALRage.Engine.UI
{
	/// <summary>
	/// Hud Related functions.
	/// </summary>
	public static class Hud
	{
		/// <summary>
		/// Determines whether a given <see cref="HudComponent"/> is active.
		/// </summary>
		/// <param name="component">The <see cref="HudComponent"/> to check</param>
		/// <returns><c>true</c> if the <see cref="HudComponent"/> is active; otherwise, <c>false</c></returns>
		public static bool IsComponentActive(HudComponent component)
		{
			return NativeFunction.Natives.IS_HUD_COMPONENT_ACTIVE<bool>((int)component);
		}

		/// <summary>
		/// Hides the specified <see cref="HudComponent"/> this frame.
		/// </summary>
		/// <param name="component">The <see cref="HudComponent"/> to hide.</param>
		public static void HideComponentThisFrame(HudComponent component)
		{
			NativeFunction.Natives.HIDE_HUD_COMPONENT_THIS_FRAME((int)component);
		}
	}
}
