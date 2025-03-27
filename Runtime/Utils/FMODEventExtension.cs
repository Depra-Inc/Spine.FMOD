// SPDX-License-Identifier: Apache-2.0
// © 2023-2025 Depra <n.melnikov@depra.org>

using FMOD.Studio;
using UnityEngine;

namespace Depra.Spine.FMOD
{
	internal abstract class FMODEventExtension : MonoBehaviour
	{
		public abstract void Apply(string eventName, EventInstance eventInstance);
	}
}