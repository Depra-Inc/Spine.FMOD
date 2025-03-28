﻿// SPDX-License-Identifier: Apache-2.0
// © 2023-2025 Depra <n.melnikov@depra.org>

using System;
using System.Collections.Generic;
using FMODUnity;
using JetBrains.Annotations;
using Spine;
using Spine.Unity;
using UnityEngine;
using Event = Spine.Event;
using static Depra.Spine.FMOD.Constants;

namespace Depra.Spine.FMOD
{
	[AddComponentMenu(MENU_PATH + nameof(BindSpineEventsToFMODEmitter), DEFAULT_ORDER)]
	internal sealed class BindSpineEventsToFMODEmitter : MonoBehaviour
	{
		[SerializeField] private SkeletonAnimation _animation;
		[SerializeField] private SoundEventDefinition[] _soundEvents;

		private Dictionary<string, ISoundEvent> _eventsMap;

		private void Awake() => _eventsMap = _soundEvents.Flatten();

		private void OnEnable() => _animation.AnimationState.Event += OnEvent;

		private void OnDisable() => _animation.AnimationState.Event -= OnEvent;

		private void OnValidate() => _animation ??= GetComponent<SkeletonAnimation>();

		[UsedImplicitly]
		public void Stop(string eventName)
		{
			if (_eventsMap.TryGetValue(eventName, out var soundEvent))
			{
				soundEvent.Stop();
			}
		}

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
			[SerializeField] private string _spineEvent;

			[Tooltip("Insert FMOD Studio Event Emitter")]
			[SerializeField] private StudioEventEmitter _emitter;

			[Tooltip("Optional extensions for the event instance.")]
			[SerializeField] private FMODEventExtension[] _decorators;

			string ISoundEvent.Key => _spineEvent;

			void ISoundEvent.Play(string eventName)
			{
				_emitter.Play();
				_decorators.Decorate(eventName, _emitter.EventInstance);
			}

			void ISoundEvent.Stop() => _emitter.Stop();
		}
	}
}