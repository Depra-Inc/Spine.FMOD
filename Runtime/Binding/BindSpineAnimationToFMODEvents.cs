// Copyright © 2023 Nikolay Melnikov. All rights reserved.
// SPDX-License-Identifier: Apache-2.0

using System;
using System.Collections.Generic;
using Depra.Spine.FMOD.Runtime.Extensions;
using Depra.Spine.FMOD.Runtime.Utils;
using FMOD.Studio;
using FMODUnity;
using JetBrains.Annotations;
using Spine;
using Spine.Unity;
using UnityEngine;
using StopMode = FMOD.Studio.STOP_MODE;
using static Depra.Spine.FMOD.Runtime.Common.Constants;

namespace Depra.Spine.FMOD.Runtime.Binding
{
	[AddComponentMenu(MODULE_PATH + SEPARATOR + nameof(BindSpineAnimationToFMODEvents), DEFAULT_ORDER)]
	internal sealed class BindSpineAnimationToFMODEvents : MonoBehaviour
	{
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

		[UsedImplicitly]
		public void Stop(string eventName)
		{
			if (_eventsMap.TryGetValue(eventName, out var soundEvent))
			{
				soundEvent.Stop();
			}
		}

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
			[SerializeField] private StopMode _stopMode;

			[Tooltip("Optional extensions for the event instance.")]
			[SerializeField] private FMODEventExtension[] _decorators;

			string ISoundEvent.Key => _spineAnimation;

			private EventInstance _eventInstance;

			void ISoundEvent.Play(string animationName)
			{
				_eventInstance = RuntimeManager.CreateInstance(_fmodEvent);
				_eventInstance.start();
				_decorators.Decorate(animationName, _eventInstance);
			}

			void ISoundEvent.Stop() => _eventInstance.stop(_stopMode);
		}
	}
}