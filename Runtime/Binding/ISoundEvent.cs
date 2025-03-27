// SPDX-License-Identifier: Apache-2.0
// © 2023-2025 Depra <n.melnikov@depra.org>

namespace Depra.Spine.FMOD
{
	internal interface ISoundEvent
	{
		string Key { get; }

		void Play(string key);

		void Stop();
	}
}