using UnityEngine;

public class MediumAsteroid : MonoBehaviour
{
    public bool IsCollided { get; private set; } = false;
    public GameObject smallAsteroidPrefab;

    // Start is called before the first frame update
    void Start()
    {
        this.transform.eulerAngles = new Vector3(0f, 0f, Random.Range(0, 360));
        float initialVelocty = Random.Range(75f, 100f);
        GetComponent<Rigidbody2D>().AddForce(transform.up * initialVelocty);
    }

    //asteroid-missle collision behavior
    private void OnTriggerEnter2D(Collider2D collider)
    {
        if (collider.tag == "Missile")
        {
            //prevent future calls after initial collision
            if (IsCollided)
            {
                return;
            }

            IsCollided = true;
            //instantiate two small asteroids
            Instantiate(
                smallAsteroidPrefab,
                this.transform.position,
                Random.rotation,
                this.transform.parent);
            Instantiate(
                smallAsteroidPrefab,
                this.transform.position,
                Random.rotation,
                this.transform.parent);
            Destroy(gameObject);
        }
    }

    //screen-wrapping functions
    private bool isVisible()
    {
        Renderer[] renderers = GetComponentsInChildren<Renderer>();

        foreach (Renderer r in renderers)
        {
            if (r.isVisible)
            {
                return true;
            }
        }

        return false;
    }
}