// SPDX-License-Identifier: Apache-2.0
// © 2023-2025 Depra <n.melnikov@depra.org>

using FMOD.Studio;
using FMODUnity;
using UnityEngine;
using static Depra.Spine.FMOD.Constants;

namespace Depra.Spine.FMOD
{
	/// <summary>
	/// Adds sound following the target <see cref="Rigidbody2D"/>.
	/// If not set, the sound will be played at the transform position.
	/// </summary>
	[AddComponentMenu(MENU_PATH + nameof(FMODEventFollowingRigidbody2D), DEFAULT_ORDER)]
	internal sealed class FMODEventFollowingRigidbody2D : FMODEventExtension
	{
		[SerializeField] private Rigidbody2D _target;

		public override void Apply(string eventName, EventInstance eventInstance) =>
			RuntimeManager.AttachInstanceToGameObject(eventInstance, _target.gameObject, _target);

		private void OnValidate() => _target ??= GetComponent<Rigidbody2D>();
	}
}