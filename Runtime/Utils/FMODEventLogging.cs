// Copyright © 2023 Nikolay Melnikov. All rights reserved.
// SPDX-License-Identifier: Apache-2.0

using FMOD.Studio;
using UnityEngine;
using static Depra.Spine.FMOD.Runtime.Common.Constants;

namespace Depra.Spine.FMOD.Runtime.Utils
{
	[AddComponentMenu(MENU_NAME, DEFAULT_ORDER)]
	internal sealed class FMODEventLogging : FMODEventDecorator
	{
		private const string MENU_NAME = MODULE_PATH + "/" + nameof(FMODEventFollowingRigidbody);

		[SerializeField] private string _format = "FMOD Event was triggered: {0}";

		public override void Decorate(string eventName, EventInstance eventInstance) =>
			Debug.Log(string.Format(_format, eventName));
	}
}