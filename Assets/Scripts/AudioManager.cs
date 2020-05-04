using UnityEngine.Audio;
using System;
using UnityEngine;
using System.Threading;
using UnityEngine.UIElements;

public class AudioManager : MonoBehaviour
{
    public Sound[] sounds;

    //Array to hold a parameters about gmaeObject sounds
    //Comment to allow to commit to Master
    void Awake()
    {
        foreach (Sound s in sounds)
        {
            s.source = gameObject.AddComponent<AudioSource>();
            s.source.clip = s.clip;
            s.source.volume = s.volume;
            s.source.pitch = s.pitch;
        }
    }

    //play the sound denoted by a name
    public void Play(string name)
    {
        Sound s = Array.Find(sounds, sound => sound.name == name);
        s.source.Play();
    }
}