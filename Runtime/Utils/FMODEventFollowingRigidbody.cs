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
	[AddComponentMenu(MODULE_PATH + SEPARATOR + nameof(FMODEventFollowingRigidbody), DEFAULT_ORDER)]
	internal sealed class FMODEventFollowingRigidbody : FMODEventExtension
	{
		[SerializeField] private Rigidbody _rigidbody;

		public override void Apply(string eventName, EventInstance eventInstance) =>
			RuntimeManager.AttachInstanceToGameObject(eventInstance, _rigidbody.transform, _rigidbody);

		private void OnValidate() => _rigidbody ??= GetComponent<Rigidbody>();
	}
}