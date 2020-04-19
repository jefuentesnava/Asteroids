using UnityEngine;

public class LargeAsteroid : MonoBehaviour
{
    public const float InitialVelocity = 50f;
    public bool isCollided { get; private set; } = false;
    public GameObject mediumAsteroidPrefab;

    // Start is called before the first frame update
    void Start()
    {
        this.transform.eulerAngles = new Vector3(0f, 0f, Random.Range(0, 360));
        GetComponent<Rigidbody2D>().AddForce(transform.up * InitialVelocity);
    }

    //asteroid-missle collision behavior
    private void OnTriggerEnter2D(Collider2D collider)
    {
        if (collider.tag == "Missile")
        {
            //prevent future calls after initial collision
            if (isCollided)
            {
                return;
            }

            isCollided = true;

            //instantiate two medium asteroids
            Instantiate(
                mediumAsteroidPrefab,
                this.transform.position,
                Random.rotation,
                this.transform.parent);
            Instantiate(
                mediumAsteroidPrefab,
                this.transform.position,
                Random.rotation,
                this.transform.parent);
            Destroy(gameObject);
        }
    }
}