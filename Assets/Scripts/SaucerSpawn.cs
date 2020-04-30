using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SaucerSpawn : MonoBehaviour
{
    public AudioSource Saucer_Spawn;

    //plays approapriate sound when Saucer Spawns
    private void OnBecameVisible()
    {
        enabled = true;
        Saucer_Spawn.Play();
    }
}
