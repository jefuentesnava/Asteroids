using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LargeSaucerTurret : MonoBehaviour
{
    public Transform FiringPoint;
    public Transform Ship;
    public GameObject SaucerMissilePrefab;

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
            Vector3 flattenedTargetPosition = new Vector3(targetPosition.x, targetPosition.y, 0.0f);
            FiringPoint.transform.LookAt(flattenedTargetPosition);
            FiringPoint.transform.Rotate(90.0f, 0, 0);
            yield return new WaitForSeconds(1.5f);
        }
    }

    private IEnumerator Shoot()
    {
        while (true)
        {
            yield return new WaitForSeconds(2.0f);
            Instantiate(SaucerMissilePrefab, FiringPoint.position, FiringPoint.rotation);
        }
    }
}