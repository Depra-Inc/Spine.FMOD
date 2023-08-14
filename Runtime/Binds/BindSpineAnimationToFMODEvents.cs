// Copyright © 2023 Nikolay Melnikov. All rights reserved.
// SPDX-License-Identifier: Apache-2.0

using System;
using System.Collections.Generic;
using Depra.Spine.FMOD.Runtime.Extensions;
using Depra.Spine.FMOD.Runtime.Utils;
using FMODUnity;
using Spine;
using Spine.Unity;
using UnityEngine;
using static Depra.Spine.FMOD.Runtime.Common.Constants;

namespace Depra.Spine.FMOD.Runtime.Binds
{
	[AddComponentMenu(MENU_NAME, DEFAULT_ORDER)]
	internal sealed class BindSpineAnimationToFMODEvents : MonoBehaviour
	{
		private const string FILE_NAME = nameof(BindSpineAnimationToFMODEvents);
		private const string MENU_NAME = MODULE_PATH + SEPARATOR + FILE_NAME;

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

		private void OnDisable() => _animation.AnimationState.Start -= OnAnimationStarted;

		private void OnValidate() => _animation ??= GetComponent<SkeletonAnimation>();

		private void OnAnimationStarted(TrackEntry trackEntry)
		{
			var animationName = trackEntry.Animation.Name;
			if (_eventsMap.TryGetValue(animationName, out var soundEvent))
			{
				soundEvent.Play(animationName);
			}
		}

		[Serializable]
		private sealed class SoundEventDefinition : ISoundEvent
		{
			[Tooltip("Insert Spine animation name here.")]
			[SpineAnimation(dataField: nameof(_animation), fallbackToTextField: true)]
			[SerializeField] private string _spineAnimation;

			[Tooltip("Insert FMOD event here.")]
			[SerializeField] private EventReference _fmodEvent;

			[Tooltip("Optional extensions for the event instance.")]
			[SerializeField] private FMODEventDecorator[] _decorators;

			string ISoundEvent.Key => _spineAnimation;

			void ISoundEvent.Play(string animationName)
			{
				var eventInstance = RuntimeManager.CreateInstance(_fmodEvent);
				eventInstance.start();
				_decorators.Decorate(animationName, eventInstance);
			}
		}
	}
}