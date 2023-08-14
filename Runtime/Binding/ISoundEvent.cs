using UnityEngine;
using AnimationState = Spine.AnimationState;

namespace Depra.Spine.FMOD.Runtime.Binding
{
	internal interface ISpineEventSource
	{
		void Subscribe();

		void Unsubscribe();
	}

	internal interface ISoundEvent
	{
		string Key { get; }

		void Play(string key, Transform sourcePoint = null);
	}
}