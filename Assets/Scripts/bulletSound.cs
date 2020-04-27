using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class bulletSound : MonoBehaviour
{
    public AudioSource bullet_sound;

    // Play bullet sound when ship fires projectile
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            bullet_sound.Play();
        }
    }
}
