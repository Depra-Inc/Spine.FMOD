// Copyright © 2023 Nikolay Melnikov. All rights reserved.
// SPDX-License-Identifier: Apache-2.0

using System;
using System.Collections.Generic;
using System.Linq;
using FMODUnity;
using Spine;
using Spine.Unity;
using UnityEngine;
using Event = Spine.Event;

namespace Depra.Spine.Integration.FMOD.Runtime
{
    public sealed class BindSpineEventsToFmodEvents : MonoBehaviour
    {
        [SerializeField] private Transform _soundPoint;
        [SerializeField] private SkeletonAnimation _skeletonAnimation;
        [SerializeField] private List<AnimationSound> _animationSounds;

        private Dictionary<string, AnimationSound> _eventsMap;

        private void Awake() =>
            _eventsMap = _animationSounds
                .ToDictionary(x => x.SpineEvent, x => x);

        private void OnEnable() =>
            _skeletonAnimation.AnimationState.Event += OnEvent;

        private void OnDisable() =>
            _skeletonAnimation.AnimationState.Event -= OnEvent;

        private void OnEvent(TrackEntry trackEntry, Event @event)
        {
            var eventName = @event.Data.Name;
            if (_eventsMap.TryGetValue(eventName, out var animationSound) == false)
            {
                return;
            }

            PlaySound(animationSound);

            if (animationSound.Verbose)
            {
                Debug.Log($"{nameof(BindSpineEventsToFmodEvents)} Event: {eventName}");
            }
        }

        private void PlaySound(AnimationSound sound)
        {
            if (sound.Attached)
            {
                RuntimeManager.PlayOneShotAttached(sound.FmodEvent, _soundPoint.gameObject);
            }
            else
            {
                RuntimeManager.PlayOneShot(sound.FmodEvent, _soundPoint.position);
            }
        }

        private void OnValidate()
        {
            _soundPoint ??= transform;
            _skeletonAnimation ??= GetComponent<SkeletonAnimation>();
        }

        [Serializable]
        private sealed class AnimationSound
        {
            [field: Tooltip("Insert Spine audio event name here")]
            [field: SpineEvent(dataField: nameof(_skeletonAnimation), fallbackToTextField: true)]
            [field: SerializeField] public string SpineEvent { get; private set; }

            [field: Tooltip("Insert FMOD Event here")]
            [field: SerializeField] public EventReference FmodEvent { get; private set; }

            [field: Tooltip("Follows the given game object or not. " +
                            "If false, the sound will be played at the transform position.")]
            [field: SerializeField] public bool Attached { get; private set; }

            [field: SerializeField] public bool Verbose { get; private set; }
        }
    }
}