using System.Collections.Generic;
using System.Linq;

namespace Depra.Spine.FMOD.Runtime.Binding
{
	internal static class SoundEventEnumerableExtensions
	{
		public static Dictionary<string, ISoundEvent> Flatten<TSoundEvent>(this IEnumerable<TSoundEvent> self)
			where TSoundEvent : ISoundEvent => self.ToDictionary(
			soundEvent => soundEvent.Key,
			soundEvent => (ISoundEvent) soundEvent);
	}
}