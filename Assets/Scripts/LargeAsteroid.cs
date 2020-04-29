using UnityEngine;

public class LargeAsteroid : MonoBehaviour
{
    public const float InitialVelocity = 50f;
    
    public bool IsCollided { get; private set; } = false;
    public GameObject MediumAsteroidPrefab; //leave as public field

    private void Start()
    {
        transform.eulerAngles = new Vector3(0f, 0f, Random.Range(0, 360));
        GetComponent<Rigidbody2D>().AddForce(transform.up * InitialVelocity);
    }

    private void OnTriggerEnter2D(Collider2D c)
    {
        if (c.CompareTag("ShipMissile"))
        {
            //prevent future calls
            if (IsCollided)
            {
                return;
            }

            IsCollided = true;

            Instantiate(
                MediumAsteroidPrefab,
                transform.position,
                Random.rotation,
                transform.parent);
            Instantiate(
                MediumAsteroidPrefab,
                transform.position,
                Random.rotation,
                transform.parent);
            FindObjectOfType<AudioManager>().Play("AsteroidDeath");
            Destroy(gameObject);

        }
    }
}