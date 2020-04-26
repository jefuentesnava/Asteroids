using System.Collections;
using UnityEngine;

public class LargeSaucerTurret : MonoBehaviour
{
    public const float TurretRotationTime = 1.5f;
    public const float ShootingTime = 2.0f;
    
    public Transform FiringPoint;   //leave as public field
    public Transform Ship;  //leave as public field
    public GameObject SaucerMissilePrefab;  //leave as public field

    private void Start()
    {
        StartCoroutine("RotateTurret");
        StartCoroutine("Shoot");
    }


    private IEnumerator RotateTurret()
    {
        while (true)
        {
            Vector3 targetPosition = Ship.position;
            Vector3 flattenedTargetPosition = new Vector3(
                targetPosition.x, 
                targetPosition.y, 
                0.0f);
            FiringPoint.transform.LookAt(flattenedTargetPosition);
            FiringPoint.transform.Rotate(90.0f, 0, 0);  //hacky
            yield return new WaitForSeconds(TurretRotationTime);
        }
    }

    private IEnumerator Shoot()
    {
        while (true)
        {
            yield return new WaitForSeconds(ShootingTime);
            Instantiate(SaucerMissilePrefab, FiringPoint.position, FiringPoint.rotation);
        }
    }
}