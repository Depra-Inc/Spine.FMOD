// Copyright © 2023 Nikolay Melnikov. All rights reserved.
// SPDX-License-Identifier: Apache-2.0

using System.Collections.Generic;
using System.Linq;
using Depra.Spine.FMOD.Runtime.Binds;

namespace Depra.Spine.FMOD.Runtime.Extensions
{
	internal static class SoundEventEnumerableExtensions
	{
		public static Dictionary<string, ISoundEvent> Flatten<TSoundEvent>(this IEnumerable<TSoundEvent> self)
			where TSoundEvent : ISoundEvent => self.ToDictionary(
			soundEvent => soundEvent.Key,
			soundEvent => (ISoundEvent) soundEvent);
	}
}