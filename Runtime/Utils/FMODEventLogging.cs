using FMOD.Studio;
using UnityEngine;
using static Depra.Spine.FMOD.Runtime.Common.Constants;

namespace Depra.Spine.FMOD.Runtime.Utils
{
	[AddComponentMenu(MENU_NAME, DEFAULT_ORDER)]
	internal sealed class FMODEventLogging : FMODEventDecorator
	{
		private const string MENU_NAME = MODULE_PATH + "/" + nameof(FMODEventFollowingRigidbody);

		public override void Decorate(string eventName, EventInstance eventInstance) =>
			Debug.Log($"FMOD Event was triggered: {eventName}");
	}
}