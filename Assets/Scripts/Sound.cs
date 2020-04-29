using UnityEngine.Audio;
using UnityEngine;

//Sound class to hold parameters for the AudioManager
[System.Serializable]
public class Sound
{
    public string name;
    public AudioClip clip;

    [HideInInspector]
    public AudioSource source;
}
