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

            RuntimeManager.PlayOneShot(animationSound.FmodEvent, transform.position);

            if (animationSound.Verbose)
            {
                Debug.Log($"{nameof(BindSpineAnimationToFmodEvents)} Animation: {animationName}");
            }
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
            [Tooltip("Insert Spine animation name here")]
            [SpineAnimation(dataField: nameof(_skeletonAnimation), fallbackToTextField: true)]
            [SerializeField] private string _spineAnimation;

            [Tooltip("Insert FMOD event here")]
            [SerializeField] private EventReference _fmodEvent;

            [field: SerializeField] public bool Verbose { get; private set; }

            public string SpineAnimation => _spineAnimation;

            public EventReference FmodEvent => _fmodEvent;
        }
    }
}