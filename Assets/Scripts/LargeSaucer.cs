using System.Collections;
using UnityEngine;

public class LargeSaucer : MonoBehaviour
{
    public const float Velocity = 100f;
    public const float MinimumSpawnTime = 5.0f;
    public const float MaximumSpawnTime = 10.0f;
    public const float ScreenWrapBufferTime = 5.0f;

    private void Start()
    {
        Camera camera = Camera.main;
        
        //generate a random y-axis value, then ensure it is on the commonly-used z position
        Vector3 randomPosition = new Vector3(-0.1f, Random.Range(0f, 1f), -1f);
        randomPosition = camera.ViewportToWorldPoint(randomPosition);
        randomPosition.z = -1f;
        GetComponent<Rigidbody2D>().position = randomPosition;

        StartCoroutine(Spawn());
    }

    private IEnumerator Spawn()
    {
        GetComponent<ScreenWrap>().enabled = false;
        gameObject.GetComponent<PolygonCollider2D>().enabled = false;
        yield return new WaitForSeconds(Random.Range(MinimumSpawnTime, MaximumSpawnTime));
        
        GetComponent<Rigidbody2D>().AddForce(transform.right * Velocity);
        gameObject.GetComponent<PolygonCollider2D>().enabled = true;
        yield return new WaitForSeconds(ScreenWrapBufferTime);
        
        GetComponent<ScreenWrap>().enabled = true;
    }

    private void OnTriggerEnter2D(Collider2D c)
    {
        if (c.CompareTag("ShipMissile"))
        {
            Destroy(gameObject);
        }
    }
}