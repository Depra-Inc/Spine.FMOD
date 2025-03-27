// SPDX-License-Identifier: Apache-2.0
// © 2023-2025 Depra <n.melnikov@depra.org>

using System.Collections.Generic;
using System.Linq;
using FMOD.Studio;

namespace Depra.Spine.FMOD
{
	internal static class EnumerableExtensions
	{
		public static Dictionary<string, ISoundEvent> Flatten<TSoundEvent>(this IEnumerable<TSoundEvent> self)
			where TSoundEvent : ISoundEvent => self.ToDictionary(
			soundEvent => soundEvent.Key,
			soundEvent => (ISoundEvent) soundEvent);
		
		public static void Decorate(this IEnumerable<FMODEventExtension> self, string eventName, EventInstance eventInstance)
		{
			foreach (var decorator in self)
			{
				decorator.Apply(eventName, eventInstance);
			}
		}
	}
}