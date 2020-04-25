using UnityEngine;

public class LargeAsteroid : MonoBehaviour
{
    public const float InitialVelocity = 50f;
    public bool IsCollided { get; private set; } = false;

    public GameObject mediumAsteroidPrefab;

    private void Start()
    {
        transform.eulerAngles = new Vector3(0f, 0f, Random.Range(0, 360));
        GetComponent<Rigidbody2D>().AddForce(transform.up * InitialVelocity);
    }

    //asteroid-missle collision behavior
    private void OnTriggerEnter2D(Collider2D c)
    {
        if (c.CompareTag("ShipMissile"))
        {
            //prevent future calls after initial collision
            if (IsCollided) return;

            IsCollided = true;

            //instantiate two medium asteroids
            Instantiate(
                mediumAsteroidPrefab,
                transform.position,
                Random.rotation,
                transform.parent);
            Instantiate(
                mediumAsteroidPrefab,
                transform.position,
                Random.rotation,
                transform.parent);
            Destroy(gameObject);
        }
    }
}