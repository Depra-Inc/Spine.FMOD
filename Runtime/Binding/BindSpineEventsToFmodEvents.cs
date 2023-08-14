// Copyright © 2023 Nikolay Melnikov. All rights reserved.
// SPDX-License-Identifier: Apache-2.0

using System;
using System.Collections.Generic;
using FMODUnity;
using Spine;
using Spine.Unity;
using UnityEngine;
using static Depra.Spine.FMOD.Runtime.Common.Constants;
using AnimationState = Spine.AnimationState;
using Event = Spine.Event;

namespace Depra.Spine.FMOD.Runtime.Binding
{
	[AddComponentMenu(MENU_NAME, DEFAULT_ORDER)]
	public sealed partial class BindSpineEventsToFmodEvents : MonoBehaviour
	{
		private const string MENU_NAME = MODULE_PATH + "/" + nameof(BindSpineEventsToFmodEvents);

		[SerializeField] private Transform _sourcePoint;
		[SerializeField] private SkeletonAnimation _animation;
		[SerializeField] private SoundEventDefinition[] _soundEvents;

		private Dictionary<string, ISoundEvent> _eventsMap;

		private void Awake() =>_eventsMap = _soundEvents.Flatten();

		private void OnEnable() =>_animation.AnimationState.Event += OnEvent;

		private void OnDisable() =>_animation.AnimationState.Event += OnEvent;

		private void OnEvent(TrackEntry track, Event @event)
		{
			var eventName = @event.Data.Name;
			if (_eventsMap.TryGetValue(eventName, out var animationSound))
			{
				animationSound.Play(eventName, _sourcePoint);
			}
		}

		private void OnValidate()
		{
			_sourcePoint ??= transform;
			_animation ??= GetComponent<SkeletonAnimation>();
		}

		[Serializable]
		private sealed record SoundEventDefinition : ISoundEvent
		{
			[Tooltip("Insert Spine audio event name here")]
			[SpineEvent(dataField: nameof(_animation), fallbackToTextField: true)]
			[SerializeField] private string _spineEvent;

			[Tooltip("Insert FMOD Event here")]
			[SerializeField] public EventReference _fmodEvent;

			[Tooltip("Follows the given game object or not. " +
			         "If false, the sound will be played at the transform position.")]
			[SerializeField] private bool _attached;

			[SerializeField] private bool _verbose;

			string ISoundEvent.Key => _spineEvent;

			void ISoundEvent.Play(string eventName, Transform sourcePoint)
			{
				if (_attached)
				{
					RuntimeManager.PlayOneShotAttached(_fmodEvent, sourcePoint.gameObject);
				}
				else
				{
					RuntimeManager.PlayOneShot(_fmodEvent, sourcePoint.position);
				}

				if (_verbose)
				{
					Debug.Log($"{nameof(BindSpineEventsToFmodEvents)} Event: {eventName}");
				}
			}
		}
	}
}