// SPDX-License-Identifier: Apache-2.0
// © 2023-2025 Depra <n.melnikov@depra.org>

using FMOD.Studio;
using UnityEngine;
using static Depra.Spine.FMOD.Constants;

namespace Depra.Spine.FMOD
{
	[AddComponentMenu(MENU_PATH + nameof(FMODEventLogging), DEFAULT_ORDER)]
	internal sealed class FMODEventLogging : FMODEventExtension
	{
		[SerializeField] private string _format = "FMOD Event was triggered: {0}";

		public override void Apply(string eventName, EventInstance eventInstance) =>
			Debug.Log(string.Format(_format, eventName));
	}
}