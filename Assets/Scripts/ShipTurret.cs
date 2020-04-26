using UnityEngine;

public class ShipTurret : MonoBehaviour
{
    public Transform FiringPoint;   //leave as public field
    public GameObject MissilePrefab;    //leave as public field

    private Ship Ship;

    private void Start()
    {
        var shipObject = transform.gameObject;
        Ship = shipObject.GetComponent<Ship>();
    }

    private void Update()
    {
        if (Ship.InputEnabled)
        {
            if (Input.GetButtonDown("Fire1"))
            {
                Shoot();
            }
        }
    }

    private void Shoot()
    {
        Instantiate(MissilePrefab, FiringPoint.position, FiringPoint.rotation);
    }
}