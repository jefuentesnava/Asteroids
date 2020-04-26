using System.Collections;
using UnityEngine;

public class SmallSaucer : MonoBehaviour
{
    public const float Velocity = 150f;
    void Start()
    {
        Camera camera = Camera.main;

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
        yield return new WaitForSeconds(Random.Range(15.0f, 20.0f));
        GetComponent<Rigidbody2D>().AddForce(-transform.right * Velocity);
        gameObject.GetComponent<PolygonCollider2D>().enabled = true;
        yield return new WaitForSeconds(5.0f);
        GetComponent<ScreenWrap>().enabled = true;
    }

    //asteroid-missle collision behavior
    private void OnTriggerEnter2D(Collider2D c)
    {
        if (c.CompareTag("ShipMissile"))
        {
            Destroy(gameObject);
        }
    }
}
