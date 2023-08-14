// Copyright © 2023 Nikolay Melnikov. All rights reserved.
// SPDX-License-Identifier: Apache-2.0

using FMOD.Studio;
using FMODUnity;
using UnityEngine;
using static Depra.Spine.FMOD.Runtime.Common.Constants;

namespace Depra.Spine.FMOD.Runtime.Utils
{
	/// <summary>
	/// Adds sound following the target <see cref="Rigidbody"/>.
	/// If not set, the sound will be played at the transform position.
	/// </summary>
	[AddComponentMenu(MENU_NAME, DEFAULT_ORDER)]
	internal sealed class FMODEventFollowingRigidbody : FMODEventDecorator
	{
		private const string MENU_NAME = MODULE_PATH + "/" + nameof(FMODEventFollowingRigidbody);

		[SerializeField] private Rigidbody _rigidbody;

		public override void Decorate(string eventName, EventInstance eventInstance) =>
			RuntimeManager.AttachInstanceToGameObject(eventInstance, _rigidbody.transform, _rigidbody);

		private void OnValidate() => _rigidbody ??= GetComponent<Rigidbody>();
	}
}