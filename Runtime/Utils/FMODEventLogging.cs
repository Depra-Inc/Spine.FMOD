// Copyright © 2023 Nikolay Melnikov. All rights reserved.
// SPDX-License-Identifier: Apache-2.0

using FMOD.Studio;
using UnityEngine;
using static Depra.Spine.FMOD.Runtime.Common.Constants;

namespace Depra.Spine.FMOD.Runtime.Utils
{
	[AddComponentMenu(MODULE_PATH + SEPARATOR + nameof(FMODEventLogging), DEFAULT_ORDER)]
	internal sealed class FMODEventLogging : FMODEventExtension
	{
		[SerializeField] private string _format = "FMOD Event was triggered: {0}";

		public override void Apply(string eventName, EventInstance eventInstance) =>
			Debug.Log(string.Format(_format, eventName));
	}
}