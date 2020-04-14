using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Missile : MonoBehaviour
{
    public float missileSpeed = 20f;
    public Rigidbody2D rigidBody;
    
    // Start is called before the first frame update
    void Start()
    {
        rigidBody.velocity = transform.up * missileSpeed;
    }
}
