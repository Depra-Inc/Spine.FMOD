# Depra.Spine.FMOD

<div align="center">
    <strong><a href="README.md">English</a> | <a href="README.RU.md">Русский</a></strong>
</div>

<details>
<summary>Table of Contents</summary>

- [Introduction](#introduction)
    - [Prerequisites](#prerequisites)
    - [Check this out](#check-this-out)
- [Features](#features)
- [Integration](#integration)
- [Dependencies](#dependencies)
- [Support](#support)
- [License](#license)

</details>

## Introduction

The module allows you to synchronize **Spine** events with **FMOD** events.<br>

### Prerequisites

To use `Depra.Spine.FMOD`, you'll need the following:

- [Spine Runtime For Unity](http://it.esotericsoftware.com/spine-unity-download)
- [FMOD-Unity Integration package](https://www.fmod.com/unity)

### Check this out

- [Spine Forum Thread](http://esotericsoftware.com/forum/Free-FMOD-Wwise-Audio-Integration-Tool-14845)<br>
- [FMOD page on Unity Asset Store](https://assetstore.unity.com/packages/tools/audio/spine2fmod-181263)<br>

## Features

- Flexible configuration;
- Support for multiple types of synchronization:

| **Spine** \ **FMOD** | `EventReference`'s                | `StudioEventEmitter`                |
|----------------------|-----------------------------------|-------------------------------------|
| Animation Start      | ✅`BindSpineAnimationToFMODEvents` | ✅ `BindSpineAnimationToFMODEmitter` |
| Animation Event      | ✅`BindSpineEventsToFMODEvents`    | ✅ `BindSpineEventsToFMODEmitter`    |

- Support multiple `EventInstance` extensions:
    - `FMODEventLogging` - Logs the event name in the console.
    - `FMODEventCallbacks` - Adds callbacks to the event.
    - `FMODEventFollowingTransform` - Adds sound position following `UnityEngine.Transform`.
    - `FMODEventFollowingRigidbody` - Adds sound position following `UnityEngine.Rigidbody`.
    - `FMODEventFollowingRigidbody2D` - Adds sound position following `UnityEngine.Rigidbody2D`.

## Integration

1. Download and Integrate the latest [Spine Runtime For Unity](http://it.esotericsoftware.com/spine-unity-download);
2. Download and Integrate the latest [FMOD-Unity Integration package](https://www.fmod.com/unity);
3. Add events in your **Spine** project:

<p>
<img src="https://i.ibb.co/3ycvLRK/spine-event.png" alt="Spine events">
</p>

4. Ensure that events are included in your Spine animation export in Unity:

<p>
<img src="https://i.ibb.co/B3YRpxW/unity-animation-preview.png" alt="Spine events export">
</p>

5. Add one of the binding scripts as a component to the **Spine** GameObject and use the size to select the number of
   animations.
6. Choose the **Spine** event/animation and the **FMOD** event/emitter you want to synchronize.
7. Add FMOD event extensions as necessary.

## Dependencies

| Dependency  | Supported versions |
|-------------|--------------------|
| Unity       | 2018.4 and upper   |
| Spine Unity | 4.1 (2023-06-27)   |
| FMOD Studio | 2.02.15            |

## Support

I am an independent developer,
and most of the development on this project is done in my spare time.
If you're interested in collaboration or hiring me for a project,
please explore [my portfolio](https://github.com/Depression-aggression) and [reach out](mailto:g0dzZz1lla@yandex.ru)!

## License

**Apache-2.0**

Copyright (c) 2022-2023 Nikolay Melnikov
[g0dzZz1lla@yandex.ru](mailto:g0dzZz1lla@yandex.ru)