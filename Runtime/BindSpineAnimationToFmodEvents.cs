// Copyright © 2023 Nikolay Melnikov. All rights reserved.
// SPDX-License-Identifier: Apache-2.0

using System;
using System.Collections.Generic;
using System.Linq;
using FMODUnity;
using Spine;
using Spine.Unity;
using UnityEngine;

namespace Depra.Spine.Integration.FMOD.Runtime
{
    public sealed class BindSpineAnimationToFmodEvents : MonoBehaviour
    {
        [SerializeField] private Transform _soundPoint;
        [SerializeField] private SkeletonAnimation _skeletonAnimation;
        [SerializeField] private List<AnimationSound> _animationSounds;

        private Dictionary<string, AnimationSound> _eventsMap;

        private void Awake() =>
            _eventsMap = _animationSounds
                .ToDictionary(x => x.SpineAnimation, x => x);

        private void Start()
        {
            if (_skeletonAnimation.AnimationState.Data.SkeletonData.Animations.Count > 0)
            {
                OnAnimationStarted(_skeletonAnimation.AnimationState.GetCurrent(0));
            }
        }

        private void OnEnable() =>
            _skeletonAnimation.AnimationState.Start += OnAnimationStarted;

        private void OnDisable() =>
            _skeletonAnimation.AnimationState.Start -= OnAnimationStarted;

        private void OnAnimationStarted(TrackEntry trackEntry)
        {
            var animationName = trackEntry.Animation.Name;
            if (_eventsMap.TryGetValue(animationName, out var animationSound) == false)
            {
                return;
            }

            PlaySound(animationSound);

            if (animationSound.Verbose)
            {
                Debug.Log($"{nameof(BindSpineAnimationToFmodEvents)} Animation: {animationName}");
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
            [field: Tooltip("Insert Spine animation name here.")]
            [field: SpineAnimation(dataField: nameof(_skeletonAnimation), fallbackToTextField: true)]
            [field: SerializeField] public string SpineAnimation { get; private set; }

            [field: Tooltip("Insert FMOD event here.")]
            [field: SerializeField] public EventReference FmodEvent { get; private set; }

            [field: Tooltip("Follows the given game object or not. " +
                            "If false, the sound will be played at the transform position.")]
            [field: SerializeField] public bool Attached { get; private set; }

            [field: SerializeField] public bool Verbose { get; private set; }
        }
    }
}