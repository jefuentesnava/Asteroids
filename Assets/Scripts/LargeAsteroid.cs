using UnityEngine;

public class LargeAsteroid : MonoBehaviour
{
    private const float InitialVelocity = 50f;
    private const float WrappingTimeOut = 2f;

    private bool isCollided = false;
    private bool isWrappingHorizontally = false;
    private bool isWrappingVertically = false;
    private float wrappingTimer = 0.0f;

    public GameObject mediumAsteroidPrefab;

    // Start is called before the first frame update
    void Start()
    {
        this.transform.eulerAngles = new Vector3(0f, 0f, Random.Range(0, 360));
        GetComponent<Rigidbody2D>().AddForce(transform.up * InitialVelocity);
    }

    // Update is called once per frame
    void Update()
    {
        screenWrap();
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

    private void screenWrap()
    {
        Camera camera = Camera.main;
        Vector3 viewportPosition = camera.WorldToViewportPoint(transform.position);
        Vector3 newPosition = transform.position;

        //if asteroid is visible, no wrapping is necessary
        if (isVisible())
        {
            isWrappingHorizontally = false;
            isWrappingVertically = false;
            return;
        }

        //if asteroid is currently wrapping, check for timeout
        if (isWrappingHorizontally && isWrappingVertically)
        {
            wrappingTimer += Time.deltaTime;

            if (wrappingTimer > WrappingTimeOut)
            {
                //return asteroid to viewport
                transform.position = Camera.main.ViewportToWorldPoint(new Vector3(1, 1, 1f));

                //reset timer
                wrappingTimer = 0f;
            }

            return;
        }


        //detect whether asteroid has disappeared horizontally...
        if (!isWrappingHorizontally && (viewportPosition.x > 1 || viewportPosition.x < 0))
        {
            newPosition.x = -newPosition.x;
            isWrappingHorizontally = true;
        }

        //...or vertically
        if (!isWrappingVertically && (viewportPosition.y > 1 || viewportPosition.y < 0))
        {
            newPosition.y = -newPosition.y;
            isWrappingVertically = true;
        }

        transform.position = newPosition;
    }
}
