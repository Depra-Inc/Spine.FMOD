using System;
using System.Collections.Generic;
using FMODUnity;
using Spine;
using Spine.Unity;
using UnityEngine;
using Event = Spine.Event;

namespace Depra.Spine.Integration.FMOD.Runtime
{
    public sealed class BindSpineEventToFmodEvent : MonoBehaviour
    {
        [SerializeField] private SkeletonAnimation _skeletonAnimation;

        [Tooltip("Use 'Size' to choose animations number")]
        [SerializeField] private List<AnimationSound> _animations;

        private void OnEnable() =>
            _skeletonAnimation.AnimationState.Event += OnEvent;

        private void OnDisable() =>
            _skeletonAnimation.AnimationState.Event -= OnEvent;

        private void OnEvent(TrackEntry trackEntry, Event @event)
        {
            foreach (var entry in _animations)
            {
                if (entry.Verbose)
                {
                    Debug.Log($"{nameof(BindSpineEventToFmodEvent)} Event: {@event.Data.Name}");
                }

                // If the Date name of the object that triggered the event is the same
                // to the name of the temporary object event I'm evaluating.
                if (@event.Data.Name != entry.SpineEvent)
                {
                    continue;
                }

                //FMOD One Shot Sound with the variable of our list in the inspector.
                RuntimeManager.PlayOneShot(entry.FMODEvent, GetComponent<Transform>().position);

                // Warning: if it is sufficient for this condition to occur only once, and therefore
                // it is not necessary to continue checking the other elements if one has already been found
                // corresponding to the search, then you can end the for loop
                // and avoid subsequent unnecessary checks.
                break;
            }
        }

        [Serializable]
        public class AnimationSound
        {
            [Tooltip("Insert Spine Audio Event Name here")]
            [SpineEvent(dataField: "skeletonAnimation", fallbackToTextField: true)]
            public string SpineEvent;

            [Tooltip("Insert Fmod Event here")]
            public EventReference FMODEvent;

            public bool Verbose;
        }
    }
}