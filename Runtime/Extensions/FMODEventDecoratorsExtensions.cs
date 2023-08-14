using System.Collections.Generic;
using Depra.Spine.FMOD.Runtime.Utils;
using FMOD.Studio;

namespace Depra.Spine.FMOD.Runtime.Extensions
{
	internal static class FMODEventDecoratorsExtensions
	{
		public static void Decorate(this IEnumerable<FMODEventDecorator> self, string eventName,
			EventInstance eventInstance)
		{
			foreach (var decorator in self)
			{
				decorator.Decorate(eventName, eventInstance);
			}
		}
	}
}