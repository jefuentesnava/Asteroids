using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class thrustSound : MonoBehaviour
{
    public AudioSource sound_Thrust;

    //Plays audio file when ship accelerates forward
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.W))
        {
            if (!sound_Thrust.isPlaying)
            {
                sound_Thrust.Play();
            }
        }

        if (Input.GetKeyUp(KeyCode.W))
        {
            sound_Thrust.Stop();
        }
    }
}
