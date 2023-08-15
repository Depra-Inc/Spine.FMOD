// Copyright © 2023 Nikolay Melnikov. All rights reserved.
// SPDX-License-Identifier: Apache-2.0

using FMOD.Studio;
using FMODUnity;
using UnityEngine;
using static Depra.Spine.FMOD.Runtime.Common.Constants;

namespace Depra.Spine.FMOD.Runtime.Utils
{
	/// <summary>
	/// Adds sound following the target <see cref="Transform"/>.
	/// If not set, the sound will be played at the transform position.
	/// </summary>
	[AddComponentMenu(MENU_NAME, DEFAULT_ORDER)]
	internal sealed class FMODEventFollowingTransform : FMODEventExtension
	{
		private const string FILE_NAME = nameof(FMODEventFollowingTransform);
		private const string MENU_NAME = MODULE_PATH + SEPARATOR + FILE_NAME;

		[SerializeField] private Transform _sourcePoint;

		public override void Apply(string eventName, EventInstance eventInstance) =>
			RuntimeManager.AttachInstanceToGameObject(eventInstance, _sourcePoint);

		private void OnValidate() => _sourcePoint ??= transform;
	}
}