// SPDX-License-Identifier: Apache-2.0
// © 2023-2025 Depra <n.melnikov@depra.org>

using System;
using System.Collections.Generic;
using Spine;
using Spine.Unity;
using UnityEngine;
using UnityEngine.Events;
using Event = Spine.Event;
using static Depra.Spine.FMOD.Constants;

namespace Depra.Spine.FMOD
{
	[AddComponentMenu(MENU_PATH + nameof(BindSpineEventsToUnityEvents), DEFAULT_ORDER)]
	internal sealed class BindSpineEventsToUnityEvents : MonoBehaviour
	{
		[SerializeField] private SkeletonAnimation _animation;
		[SerializeField] private SoundEventDefinition[] _soundEvents;

		private Dictionary<string, ISoundEvent> _eventsMap;

		private void Awake() => _eventsMap = _soundEvents.Flatten();

		private void OnEnable() => _animation.AnimationState.Event += OnEvent;

		private void OnDisable() => _animation.AnimationState.Event -= OnEvent;

		private void OnValidate() => _animation ??= GetComponent<SkeletonAnimation>();

		private void OnEvent(TrackEntry entry, Event @event)
		{
			var eventName = @event.Data.Name;
			if (_eventsMap.TryGetValue(eventName, out var soundEvent))
			{
				soundEvent.Play(eventName);
			}
		}

		[Serializable]
		private sealed class SoundEventDefinition : ISoundEvent
		{
			[Tooltip("Insert Spine audio event name here")]
			[SpineEvent(dataField: nameof(_animation), fallbackToTextField: true)]
			[SerializeField]
			private string _spineEvent;

			[SerializeField] private UnityEvent<string> _unityEvent;

			string ISoundEvent.Key => _spineEvent;

			void ISoundEvent.Play(string eventName) => _unityEvent.Invoke(eventName);

			void ISoundEvent.Stop() { }
		}
	}
}