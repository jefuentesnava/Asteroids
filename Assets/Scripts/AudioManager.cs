using UnityEngine.Audio;
using System;
using UnityEngine;

public class AudioManager : MonoBehaviour
{
    public Sound[] sounds;
  
    //Array to hold a parameters about gmaeObject sounds
    void Awake()
    {
        foreach (Sound s in sounds)
        {
            s.source = gameObject.AddComponent<AudioSource>();
            s.source.clip = s.clip;
        }
    }

    //play the sound denoted by a name
    public void Play(string name)
    {
        Sound s = Array.Find(sounds, sound => sound.name == name);
        if(name == null)
        {
            return;
        }
        s.source.Play();
    }
}