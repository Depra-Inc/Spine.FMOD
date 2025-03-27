// SPDX-License-Identifier: Apache-2.0
// © 2023-2025 Depra <n.melnikov@depra.org>

using FMOD.Studio;
using FMODUnity;
using UnityEngine;
using static Depra.Spine.FMOD.Constants;

namespace Depra.Spine.FMOD
{
	/// <summary>
	/// Adds sound following the target <see cref="Transform"/>.
	/// If not set, the sound will be played at the transform position.
	/// </summary>
	[AddComponentMenu(MENU_PATH + nameof(FMODEventFollowingTransform), DEFAULT_ORDER)]
	internal sealed class FMODEventFollowingTransform : FMODEventExtension
	{
		[SerializeField] private Transform _target;

		public override void Apply(string eventName, EventInstance eventInstance) =>
			RuntimeManager.AttachInstanceToGameObject(eventInstance, _target.gameObject);

		private void OnValidate() => _target ??= transform;
	}
}