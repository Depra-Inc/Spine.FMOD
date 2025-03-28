﻿// SPDX-License-Identifier: Apache-2.0
// © 2023-2025 Depra <n.melnikov@depra.org>

using System;
using FMOD;
using FMOD.Studio;
using UnityEngine;
using UnityEngine.Events;
using static Depra.Spine.FMOD.Constants;

namespace Depra.Spine.FMOD
{
	[AddComponentMenu(MENU_PATH + nameof(FMODEventCallbacks), DEFAULT_ORDER)]
	internal sealed class FMODEventCallbacks : FMODEventExtension
	{
		[SerializeField] private Pair[] _callbacks;

		public override void Apply(string eventName, EventInstance eventInstance)
		{
			foreach (var callback in _callbacks)
			{
				callback.Subscribe(eventInstance);
			}
		}

		[Serializable]
		private sealed class Pair
		{
			[SerializeField] private EVENT_CALLBACK_TYPE _eventType;
			[SerializeField] private UnityEvent<RESULT> _callback;

			public void Subscribe(EventInstance eventInstance)
			{
				eventInstance.setCallback((_, _, _) => Callback(), _eventType);
				return;

				RESULT Callback()
				{
					_callback?.Invoke(RESULT.OK);
					return RESULT.OK;
				}
			}
		}
	}
}