using UnityEngine;

public class MediumAsteroid : MonoBehaviour
{
    public bool IsCollided { get; private set; } = false;
    public GameObject smallAsteroidPrefab;

    private void Start()
    {
        transform.eulerAngles = new Vector3(0f, 0f, Random.Range(0, 360));
        var initialVelocty = Random.Range(75f, 100f);
        GetComponent<Rigidbody2D>().AddForce(transform.up * initialVelocty);
    }

    //asteroid-missle collision behavior
    private void OnTriggerEnter2D(Collider2D c)
    {
        if (c.CompareTag("Missile"))
        {
            //prevent future calls after initial collision
            if (IsCollided) return;

            IsCollided = true;
            //instantiate two small asteroids
            Instantiate(
                smallAsteroidPrefab,
                transform.position,
                Random.rotation,
                transform.parent);
            Instantiate(
                smallAsteroidPrefab,
                transform.position,
                Random.rotation,
                transform.parent);
            Destroy(gameObject);
        }
    }
}