using System;
using System.Collections.Generic;
using Spine;
using Spine.Unity;
using UnityEngine;
using Event = Spine.Event;

namespace Depra.Spine.FMOD.Runtime.Binding
{
	public sealed partial class BindSpineEventsToFmodEvents
	{
		[Serializable]
		private sealed record EventSource : ISpineEventSource, IDisposable
		{
			[SerializeField] private Transform _sourcePoint;
			[SerializeField] private SkeletonAnimation _animation;
			private readonly Dictionary<string, ISoundEvent> _eventsMap;

			public EventSource(Dictionary<string, ISoundEvent> soundEvents) =>
				_eventsMap = soundEvents ?? throw new ArgumentNullException(nameof(soundEvents));

			public void Dispose() => Unsubscribe();

			public void Subscribe() => _animation.AnimationState.Event += OnEvent;

			public void Unsubscribe() => _animation.AnimationState.Event += OnEvent;

			private void OnEvent(TrackEntry track, Event @event)
			{
				var eventName = @event.Data.Name;
				if (_eventsMap.TryGetValue(eventName, out var animationSound))
				{
					animationSound.Play(eventName, _sourcePoint);
				}
			}

			public void Reset(Transform transform)
			{
				_sourcePoint ??= transform;
				_animation ??= transform.GetComponent<SkeletonAnimation>();
			}
		}
	}
}