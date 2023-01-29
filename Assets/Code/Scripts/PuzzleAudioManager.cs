using Random = UnityEngine.Random;
using UnityEngine;
using UnityEngine.Audio;
using System.Collections;
using System.Collections.Generic;
using System;
public class PuzzleAudioManager : MonoBehaviour
{
    public Sound[] _Sounds;


    private void Awake()
    {

        DontDestroyOnLoad(gameObject);

        foreach (Sound s in _Sounds)
        {
            s._Source = gameObject.AddComponent<AudioSource>();
            s._Source.clip = s._Clip[Random.Range(0, s._Clip.Length)];

            s._Source.volume = s._Volume;
            s._Source.pitch = s._Pitch;
            s._Source.loop = s._Loop;
        }

    }

    public void Play(string name)
    {
        Sound s = Array.Find(_Sounds, sound => sound._Name == name);
        s._Source.Play();


    }

    public void PlayRandom(string name)
    {
        Sound s = Array.Find(_Sounds, sound => sound._Name == name);
        s._Source.clip = s._Clip[Random.Range(0, s._Clip.Length)];
        s._Source.PlayOneShot(s._Source.clip);
    }

}
