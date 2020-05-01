using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Background : MonoBehaviour
{
    public AudioSource Background_Sound;

    // Generates an increasing background thump sound after some delays
    void Start()
    {
        Invoke("Background_Pitch_1",8f);
        Invoke("Background_Pitch_2", 18f);
        Invoke("Background_Pitch_1", 28f);

    }

    void Background_Pitch_1()
    {
        Background_Sound.Play();
    }

    void Background_Pitch_2()
    {
        Background_Sound.pitch = Mathf.Lerp(0.5f,.75f,Time.time);
        Background_Sound.Play();
    }

    void Background_Pitch_3()
    {
        Background_Sound.pitch = Mathf.Lerp(0.75f, 1f,Time.time);
        Background_Sound.Play();
    }

}
