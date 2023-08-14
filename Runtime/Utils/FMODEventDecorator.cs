using FMOD.Studio;
using UnityEngine;

namespace Depra.Spine.FMOD.Runtime.Utils
{
	internal abstract class FMODEventDecorator : MonoBehaviour
	{
		public abstract void Decorate(string eventName, EventInstance eventInstance);
	}
}