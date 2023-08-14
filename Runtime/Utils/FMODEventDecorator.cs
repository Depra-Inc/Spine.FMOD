// Copyright © 2023 Nikolay Melnikov. All rights reserved.
// SPDX-License-Identifier: Apache-2.0

using FMOD.Studio;
using UnityEngine;

namespace Depra.Spine.FMOD.Runtime.Utils
{
	internal abstract class FMODEventDecorator : MonoBehaviour
	{
		public abstract void Decorate(string eventName, EventInstance eventInstance);
	}
}