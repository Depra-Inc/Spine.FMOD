// Copyright © 2023 Nikolay Melnikov. All rights reserved.
// SPDX-License-Identifier: Apache-2.0

using System;
using System.Collections.Generic;
using Depra.Spine.FMOD.Runtime.Extensions;
using FMODUnity;
using Spine;
using Spine.Unity;
using UnityEngine;
using static Depra.Spine.FMOD.Runtime.Common.Constants;

namespace Depra.Spine.FMOD.Runtime.Binding
{
	[AddComponentMenu(MENU_NAME, DEFAULT_ORDER)]
	internal sealed class BindSpineAnimationToFMODEvents : MonoBehaviour
	{
		private const string MENU_NAME = MODULE_PATH + "/" + nameof(BindSpineAnimationToFMODEvents);

		[SerializeField] private Transform _sourcePoint;
		[SerializeField] private SkeletonAnimation _animation;
		[SerializeField] private SoundEventDefinition[] _soundEvents;

		private Dictionary<string, ISoundEvent> _eventsMap;

		private void Awake() => _eventsMap = _soundEvents.Flatten();

		private void OnEnable()
		{
			if (_animation.AnimationState.Data.SkeletonData.Animations.Count > 0)
			{
				OnAnimationStarted(_animation.AnimationState.GetCurrent(0));
			}

			_animation.AnimationState.Start += OnAnimationStarted;
		}

		private void OnDisable() =>
			_animation.AnimationState.Start -= OnAnimationStarted;

		private void OnValidate()
		{
			_sourcePoint ??= transform;
			_animation ??= GetComponent<SkeletonAnimation>();
		}

		private void OnAnimationStarted(TrackEntry trackEntry)
		{
			var animationName = trackEntry.Animation.Name;
			if (_eventsMap.TryGetValue(animationName, out var soundEvent))
			{
				soundEvent.Play(animationName, _sourcePoint);
			}
		}

		[Serializable]
		private sealed record SoundEventDefinition : ISoundEvent
		{
			[Tooltip("Insert Spine animation name here.")]
			[SpineAnimation(dataField: nameof(_animation), fallbackToTextField: true)]
			[SerializeField] private string _spineAnimation;

			[Tooltip("Insert FMOD event here.")]
			[SerializeField] private EventReference _fmodEvent;

			[Tooltip("Follows the given transform for 3D pointing or not. " +
			         "If false, the sound will be played at the transform position.")]
			[SerializeField] private bool _followToSource;

			[SerializeField] public bool _attachToSource;

			[Tooltip("Enable logging.")]
			[SerializeField] public bool _verbose;

			string ISoundEvent.Key => _spineAnimation;

			void ISoundEvent.Play(string animationName, Transform sourcePoint)
			{
				if (_attachToSource)
				{
					var @event = RuntimeManager.CreateInstance(_fmodEvent);
					@event.start();
					// @event.setCallback(() => , )
					if (_followToSource)
					{
						RuntimeManager.AttachInstanceToGameObject(@event, sourcePoint);
					}
				}
				else
				{
					if (_followToSource)
					{
						RuntimeManager.PlayOneShotAttached(_fmodEvent, sourcePoint.gameObject);
					}
					else
					{
						RuntimeManager.PlayOneShot(_fmodEvent, sourcePoint.position);
					}
				}

				if (_verbose)
				{
					Debug.Log($"{nameof(BindSpineEventsToFMODEvents)} Event: {animationName}");
				}
			}
		}
	}
}