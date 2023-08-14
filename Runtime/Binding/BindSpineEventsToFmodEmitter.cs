using System;
using System.Collections.Generic;
using Depra.Spine.FMOD.Runtime.Extensions;
using FMODUnity;
using Spine;
using Spine.Unity;
using UnityEngine;
using static Depra.Spine.FMOD.Runtime.Common.Constants;
using Event = Spine.Event;

namespace Depra.Spine.FMOD.Runtime.Binding
{
	[AddComponentMenu(MENU_NAME, DEFAULT_ORDER)]
	internal sealed class BindSpineEventsToFMODEmitter : MonoBehaviour
	{
		private const string MENU_NAME = MODULE_PATH + "/" + nameof(BindSpineEventsToFMODEmitter);

		[SerializeField] private SkeletonAnimation _animation;
		[SerializeField] private SoundEventDefinition[] _soundEvents;

		private Dictionary<string, ISoundEvent> _eventsMap;

		private void Awake() => _eventsMap = _soundEvents.Flatten();

		private void OnEnable() => _animation.AnimationState.Event += OnEvent;

		private void OnDisable() => _animation.AnimationState.Event -= OnEvent;

		private void OnEvent(TrackEntry entry, Event @event)
		{
			var eventName = @event.Data.Name;
			if (_eventsMap.TryGetValue(eventName, out var soundEvent))
			{
				soundEvent.Play(eventName);
			}
		}

		[Serializable]
		private sealed record SoundEventDefinition : ISoundEvent
		{
			[Tooltip("Insert Spine audio event name here")]
			[SpineEvent(dataField: nameof(_animation), fallbackToTextField: true)]
			[SerializeField] private string _spineEvent;

			[Tooltip("Insert FMOD Studio Event Emitter")]
			[SerializeField] private StudioEventEmitter _emitter;

			[Tooltip("Enable logging.")]
			[SerializeField] private bool _verbose;

			string ISoundEvent.Key => _spineEvent;

			void ISoundEvent.Play(string eventName)
			{
				_emitter.Play();

				if (_verbose)
				{
					Debug.Log($"{nameof(BindSpineEventsToFMODEvents)} Event: {eventName}");
				}
			}
		}
	}
}