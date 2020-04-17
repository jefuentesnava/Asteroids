using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Turret : MonoBehaviour
{
    public Transform firingPoint;
    public GameObject missilePrefab;
    public GameObject root;

    private bool inputEnabled;
    private Ship ship;

    void Start()
    {
        root = transform.parent.gameObject;

        //get access to Ship functions to check if input is enabled or not
        GameObject shipObject = transform.gameObject;
        if (shipObject != null)
        {
            ship = shipObject.GetComponent<Ship>();
        }
    }

    // Update is called once per frame
    void Update()
    {
        inputEnabled = ship.getInputEnabled();
        
        if (inputEnabled)
        {
            if (Input.GetButtonDown("Fire1"))
            {
                Shoot();
            }
        }
    }

    void Shoot()
    {
        Instantiate(missilePrefab, firingPoint.position, firingPoint.rotation, root.transform);
    }
}
