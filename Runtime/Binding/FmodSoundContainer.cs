using System;
using System.Collections.Generic;
using FMOD.Studio;
using FMODUnity;
using UnityEngine;

namespace Depra.Spine.FMOD.Runtime.Binding
{
    public sealed class FmodSoundContainer : IDisposable
    {
        private readonly List<EventInstance> _events = new();

        public void Dispose()
        {
            foreach (var @event in _events)
            {
                @event.release();
            }
        }

        public void PlaySound(EventReference eventReference, Transform source, bool attachToSource, bool followToSource)
        {
            if (attachToSource)
            {
                PlayAttached(eventReference, source, followToSource);
            }
            else
            {
                Play(eventReference, source, followToSource);
            }
        }

        public void Play(EventReference eventReference, Transform source, bool followToSource)
        {
            if (followToSource)
            {
                RuntimeManager.PlayOneShotAttached(eventReference, source.gameObject);
            }
            else
            {
                RuntimeManager.PlayOneShot(eventReference, source.position);
            }
        }

        private void PlayAttached(EventReference eventReference, Transform source, bool followToSource)
        {
            var eventInstance = RuntimeManager.CreateInstance(eventReference);
            eventInstance.start();

            if (followToSource)
            {
                RuntimeManager.AttachInstanceToGameObject(eventInstance, source);
            }

            _events.Add(eventInstance);
        }
    }
}