// Copyright © 2023 Nikolay Melnikov. All rights reserved.
// SPDX-License-Identifier: Apache-2.0

using System;
using System.Collections.Generic;
using FMODUnity;
using Spine;
using Spine.Unity;
using UnityEngine;
using Event = Spine.Event;

namespace Depra.Spine.Integration.FMOD.Runtime
{
    public sealed class BindSpineEventsToFmodEvents : MonoBehaviour
    {
        [SerializeField] private SkeletonAnimation _skeletonAnimation;
        [SerializeField] private List<AnimationSound> _animationSounds;

        private Dictionary<string, AnimationSound> _eventsMap;

        private void Awake() => _eventsMap = MapSounds();

        private void OnEnable() =>
            _skeletonAnimation.AnimationState.Event += OnEvent;

        private void OnDisable() =>
            _skeletonAnimation.AnimationState.Event -= OnEvent;

        private void OnEvent(TrackEntry trackEntry, Event @event)
        {
            if (_eventsMap.TryGetValue(@event.Data.Name, out var animationSound) == false)
            {
                return;
            }

            RuntimeManager.PlayOneShot(animationSound.FmodEvent, transform.position);

            if (animationSound.Verbose)
            {
                Debug.Log($"{nameof(BindSpineEventsToFmodEvents)} Event: {@event.Data.Name}");
            }
        }

        private Dictionary<string, AnimationSound> MapSounds()
        {
            var map = new Dictionary<string, AnimationSound>(_animationSounds.Count);
            foreach (var sound in _animationSounds)
            {
                map.Add(sound.SpineEvent, sound);
            }

            return map;
        }

        private void OnValidate()
        {
            if (_skeletonAnimation == null)
            {
                _skeletonAnimation = GetComponent<SkeletonAnimation>();
            }
        }

        [Serializable]
        private sealed class AnimationSound
        {
            [Tooltip("Insert Spine Audio Event Name here")]
            [SpineEvent(dataField: nameof(_skeletonAnimation), fallbackToTextField: true)]
            [SerializeField] private string _spineEvent;

            [Tooltip("Insert Fmod Event here")]
            [SerializeField] private EventReference _fmodEvent;

            [field: SerializeField] public bool Verbose { get; private set; }

            public string SpineEvent => _spineEvent;

            public EventReference FmodEvent => _fmodEvent;
        }
    }
}