using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;

[System.Serializable]
public class Sound
{
    public string _Name;

    public AudioClip[] _Clip;
    [Range(0f,1f)]
    public float _Volume;
    [Range(0.1f,1f)]
    public float _Pitch;
    [Range(0f,1f)]
    public float _SpatialBlend;
    public bool _Loop;
    public bool _PlayOnAwake;

    [HideInInspector]
    public AudioSource _Source;
    public AudioMixerGroup _AudioMixerGroup;

    
}
