# Spine2FMOD 

The module allows you to synchronize Spine events with FMOD events.<br>

### Prerequisites

- [Spine Runtime For Unity](http://it.esotericsoftware.com/spine-unity-download)
- [FMOD-Unity Integration package](https://www.fmod.com/unity)

### Check this out
 - [Unity Asset Store Page](https://assetstore.unity.com/packages/tools/audio/spine2fmod-181263)<br>
 - [Spine Forum Thread](http://esotericsoftware.com/forum/Free-FMOD-Wwise-Audio-Integration-Tool-14845)<br>
 - [How to Setup Video](https://youtu.be/Uds32tKBcsQ)
 - [How to Setup PDF](https://github.com/francescocorsello/Spine2FMOD/blob/main/Spine2FMOD%20-%20HowToSetUp.pdf)

## Integration

1. Download and Integrate the latest [Spine Runtime For Unity](http://it.esotericsoftware.com/spine-unity-download)
2. Download and Integrate the latest [FMOD-Unity Integration package](https://www.fmod.com/unity)
3. Add events in your Spine project.

<p align="left">
<img width="900px" src="https://i.ibb.co/3ycvLRK/spine-event.png">
</p>

4. Make sure to have events in your Spine animation export in Unity

<p align="left">
<img width="600px" src="https://i.ibb.co/B3YRpxW/unity-animation-preview.png">
</p>

5. Add the Spine2FMOD script as component to your Spine game object and use size to
choose your animations number.

<p align="left">
<img width="600px" src="https://i.ibb.co/pbwLb0S/inspector.png">
</p>

6. Choose the Spine event and the Fmod event you want to synchronize.

<p align="left">
<img width="600px" src="https://i.ibb.co/F7vc0dn/inspector-events.png">
</p>

7. Select debug log to print your event name in the console.

<p align="left">
<img width="600px" src="https://i.ibb.co/QbKJmZk/debuglog.png">
</p>

### Tested with:

- Unity 2018.4
- Unity 2019.2
- Unity 2019.3
- Unity 2021.3

- spine-unity-4.1-2023-06-27

- fmodstudio-2.02.15