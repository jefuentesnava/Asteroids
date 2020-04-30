using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SaucerBullet : MonoBehaviour
{
    public AudioSource Saucer_Bullet;

    //plays saucer bullet sound when gameObject is visible
    private void OnBecameVisible()
    {
        Saucer_Bullet.Play();
    }
}
