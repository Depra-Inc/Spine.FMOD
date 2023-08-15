// Copyright © 2023 Nikolay Melnikov. All rights reserved.
// SPDX-License-Identifier: Apache-2.0

using FMOD.Studio;
using UnityEngine;

namespace Depra.Spine.FMOD.Runtime.Utils
{
	internal abstract class FMODEventExtension : MonoBehaviour
	{
		public abstract void Apply(string eventName, EventInstance eventInstance);
	}
}