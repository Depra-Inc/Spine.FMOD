using Depra.Spine.FMOD.Runtime.Common;
using FMOD.Studio;
using FMODUnity;
using UnityEngine;

namespace Depra.Spine.FMOD.Runtime.Utils
{
	/// <summary>
	/// Adds sound following the target <see cref="Rigidbody2D"/>.
	/// If not set, the sound will be played at the transform position.
	/// </summary>
	[AddComponentMenu(MENU_NAME, Constants.DEFAULT_ORDER)]
	internal sealed class FMODEventFollowingRigidbody2D : FMODEventDecorator {

		private const string MENU_NAME = Constants.MODULE_PATH + "/" + nameof(FMODEventFollowingRigidbody2D);

		[SerializeField] private Rigidbody2D _rigidbody;

		public override void Decorate(string eventName, EventInstance eventInstance) =>
			RuntimeManager.AttachInstanceToGameObject(eventInstance, _rigidbody.transform, _rigidbody);

		private void OnValidate() =>_rigidbody ??= GetComponent<Rigidbody2D>();
	}
}