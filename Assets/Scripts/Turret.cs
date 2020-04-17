using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Turret : MonoBehaviour
{
    public Transform firingPoint;
    public GameObject missilePrefab;
    public GameObject root;

    void Start()
    {
        root = transform.parent.gameObject;
    }
    
    // Update is called once per frame
    void Update()
    {
        if (Input.GetButtonDown("Fire1"))
        {
            Shoot();
        }
    }

    void Shoot()
    {
        Instantiate(missilePrefab, firingPoint.position, firingPoint.rotation, root.transform);
    }
}
