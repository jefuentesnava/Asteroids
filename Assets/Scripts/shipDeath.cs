using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class shipDeath : MonoBehaviour
{
    public AudioSource ship_Death;

    //plays explosion audio when ship collides with another gameObject
    private void OnTriggerEnter2D(Collider2D c)
    {
        if (c.gameObject.CompareTag("LargeAsteroid") ||
            c.gameObject.CompareTag("MediumAsteroid") ||
            c.gameObject.CompareTag("SmallAsteroid") ||
            c.gameObject.CompareTag("LargeSaucer") ||
            c.gameObject.CompareTag("SmallSaucer") ||
            c.gameObject.CompareTag("SaucerMissile"))
        {
            ship_Death.Play();
        }
    }
}

