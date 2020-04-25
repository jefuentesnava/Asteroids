using UnityEngine;

public class ShipTurret : MonoBehaviour
{
    public Transform firingPoint;
    public GameObject missilePrefab;

    private Ship ship;

    private void Start()
    {
        //get access to Ship functions to check if input is enabled or not
        var shipObject = transform.gameObject;
        ship = shipObject.GetComponent<Ship>();
    }

    // Update is called once per frame
    private void Update()
    {
        if (ship.InputEnabled)
        {
            if (Input.GetButtonDown("Fire1"))
            {
                Shoot();
            }
        }
    }

    private void Shoot()
    {
        Instantiate(missilePrefab, firingPoint.position, firingPoint.rotation);
    }
}