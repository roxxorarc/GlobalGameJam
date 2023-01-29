using System;
using System.Collections;
using UnityEngine.Audio;
using UnityEngine;
using Random = UnityEngine.Random;
public class BackGroundAudioManager : MonoBehaviour
{
    public Sound[] _Sounds;
    public static BackGroundAudioManager s_Instance;
    [SerializeField] private bool m_PlaySound;

    private void Awake()
    {
        if (s_Instance == null)
        {
            s_Instance = this;
        }
        else
        {
            Destroy(gameObject);
        }

        foreach (Sound s in _Sounds)
        {
            s._Source = gameObject.AddComponent<AudioSource>();
            s._Source.clip = s._Clip[Random.Range(0, s._Clip.Length)];
            s._Source.outputAudioMixerGroup = s._AudioMixerGroup;
            s._Source.playOnAwake = s._PlayOnAwake;
            s._Source.spatialBlend = s._SpatialBlend;
            s._Source.volume = s._Volume;
            s._Source.pitch = s._Pitch;
            s._Source.loop = s._Loop;
        }

    }

    private void Start()
    {
        Play("Musique");
    }

    private void Update()
    {



        if (!m_PlaySound)
        {
            StartCoroutine(WaitToPlaySound(15, 60));
            m_PlaySound = true;
            PlayRandom("WoodRandom");
            StartCoroutine(WaitToPlaySound(25, 60));


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


    IEnumerator WaitToPlaySound(float random1, float random2)
    {
        yield return new WaitForSeconds(Random.Range(random1, random2));
        m_PlaySound = false;

    }

}
