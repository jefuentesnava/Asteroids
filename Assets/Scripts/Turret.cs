using UnityEngine;

public class Turret : MonoBehaviour
{
    public Transform firingPoint;
    public GameObject missilePrefab;

    private Ship ship;

    void Start()
    {
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
        if (ship.inputEnabled)
        {
            if (Input.GetButtonDown("Fire1"))
            {
                Shoot();
            }
        }
    }

    void Shoot()
    {
        Instantiate(missilePrefab, firingPoint.position, firingPoint.rotation);
    }
}