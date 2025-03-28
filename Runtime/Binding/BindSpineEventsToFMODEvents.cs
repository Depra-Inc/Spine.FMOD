﻿// SPDX-License-Identifier: Apache-2.0
// © 2023-2025 Depra <n.melnikov@depra.org>

using System;
using System.Collections.Generic;
using FMOD.Studio;
using FMODUnity;
using JetBrains.Annotations;
using Spine;
using Spine.Unity;
using UnityEngine;
using Event = Spine.Event;
using StopMode = FMOD.Studio.STOP_MODE;
using static Depra.Spine.FMOD.Constants;

namespace Depra.Spine.FMOD
{
	[AddComponentMenu(MENU_PATH + nameof(BindSpineEventsToFMODEvents), DEFAULT_ORDER)]
	internal sealed class BindSpineEventsToFMODEvents : MonoBehaviour
	{
		[SerializeField] private SkeletonAnimation _animation;
		[SerializeField] private SoundEventDefinition[] _soundEvents;

		private Dictionary<string, ISoundEvent> _eventsMap;

		private void Awake() => _eventsMap = _soundEvents.Flatten();

		private void OnEnable() => _animation.AnimationState.Event += OnEvent;

		private void OnDisable() => _animation.AnimationState.Event += OnEvent;

		private void OnValidate() => _animation ??= GetComponent<SkeletonAnimation>();

		[UsedImplicitly]
		public void Stop(string eventName)
		{
			if (_eventsMap.TryGetValue(eventName, out var animationSound))
			{
				animationSound.Stop();
			}
		}

		private void OnEvent(TrackEntry track, Event @event)
		{
			var eventName = @event.Data.Name;
			if (_eventsMap.TryGetValue(eventName, out var animationSound))
			{
				animationSound.Play(eventName);
			}
		}

		[Serializable]
		private sealed class SoundEventDefinition : ISoundEvent
		{
			[Tooltip("Insert Spine audio event name here")]
			[SpineEvent(dataField: nameof(_animation), fallbackToTextField: true)]
			[SerializeField] private string _spineEvent;

			[Tooltip("Insert FMOD Event here")]
			[SerializeField] public EventReference _fmodEvent;
			[SerializeField] private StopMode _stopMode;

			[Tooltip("Optional extensions for the event instance.")]
			[SerializeField] private FMODEventExtension[] _decorators;

			private EventInstance _eventInstance;

			string ISoundEvent.Key => _spineEvent;

			void ISoundEvent.Play(string eventName)
			{
				_eventInstance = RuntimeManager.CreateInstance(_fmodEvent);
				_decorators.Decorate(eventName, _eventInstance);
				_eventInstance.start();
			}

			void ISoundEvent.Stop() => _eventInstance.stop(_stopMode);
		}
	}
}