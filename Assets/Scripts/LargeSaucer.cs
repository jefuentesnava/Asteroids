using System.Collections;
using UnityEngine;

public class LargeSaucer : MonoBehaviour
{
    public const float Velocity = 100f;
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
        yield return new WaitForSeconds(5.0f);
        GetComponent<Rigidbody2D>().AddForce(transform.right * Velocity);
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
