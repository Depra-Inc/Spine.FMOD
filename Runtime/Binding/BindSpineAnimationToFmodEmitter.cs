using System;
using System.Collections.Generic;
using FMODUnity;
using Spine;
using Spine.Unity;
using UnityEngine;
using static Depra.Spine.FMOD.Runtime.Common.Constants;

namespace Depra.Spine.FMOD.Runtime.Binding
{
	[AddComponentMenu(MENU_NAME, DEFAULT_ORDER)]
	public sealed class BindSpineAnimationToFmodEmitter : MonoBehaviour
	{
		private const string MENU_NAME = MODULE_PATH + "/" + nameof(BindSpineAnimationToFmodEmitter);

		[SerializeField] private Transform _sourcePoint;
		[SerializeField] private SkeletonAnimation _animation;
		[SerializeField] private SoundEventDefinition[] _soundEvents;

		private Dictionary<string, ISoundEvent> _eventsMap;

		private void Awake() => _eventsMap = _soundEvents.Flatten();

		private void Start()
		{
			if (_animation.AnimationState.Data.SkeletonData.Animations.Count > 0)
			{
				OnAnimationStarted(_animation.AnimationState.GetCurrent(0));
			}
		}

		private void OnEnable() =>
			_animation.AnimationState.Start += OnAnimationStarted;

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

			[Tooltip("Insert FMOD Studio Event Emitter")]
			[SerializeField] private StudioEventEmitter _emitter;

			[Tooltip("Enable logging.")]
			[SerializeField] public bool _verbose;

			string ISoundEvent.Key => _spineAnimation;

			void ISoundEvent.Play(string animationName, Transform sourcePoint)
			{
				_emitter.Play();

				if (_verbose)
				{
					Debug.Log($"{nameof(BindSpineEventsToFmodEvents)} Event: {animationName}");
				}
			}
		}
	}
}