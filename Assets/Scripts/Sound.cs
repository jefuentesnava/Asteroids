using UnityEngine.Audio;
using UnityEngine;

//Sound class to hold parameters for the AudioManager
[System.Serializable]
public class Sound
{
    public string name;
    public AudioClip clip;
    [Range(0f,1f)]
    public float volume;
    [Range(.1f,3f)]
    public float pitch;


    [HideInInspector]
    public AudioSource source;
}
