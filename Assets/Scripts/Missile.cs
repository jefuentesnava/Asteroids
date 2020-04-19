using UnityEngine;
using UnityEngine.SceneManagement;

public class Missile : MonoBehaviour
{
    public const float TimeOut = 0.75f;
    public const float MissileSpeed = 20f;
    public const int LargeAsteroidPointValue = 20;
    public const int MediumAsteroidPointValue = 50;
    public const int SmallAsteroidPointValue = 100;

    public bool isWrappingHorizontally { get; private set; } = false;
    public bool isWrappingVertically { get; private set; } = false;
    private float timer = 0.0f;

    public Rigidbody2D rigidBody;
    private PlayerState playerState;

    // Start is called before the first frame update
    void Start()
    {
        rigidBody.velocity = transform.up * MissileSpeed;

        //get access to player state
        GameObject playerStateObject = null;
        GameObject[] rootGameObjects = SceneManager.GetActiveScene().GetRootGameObjects();

        foreach (GameObject g in rootGameObjects)
        {
            if (g.transform.CompareTag("PlayerState"))
            {
                playerStateObject = g;
            }
        }

        if (playerStateObject != null)
        {
            playerState = playerStateObject.GetComponent<PlayerState>();
        }
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        screenWrap();
        timer += Time.deltaTime;

        if (timer > TimeOut)
        {
            Destroy(gameObject);
        }
    }

    //missile-asteroid collision behavior; called when missile collides with asteroid
    private void OnTriggerEnter2D(Collider2D collider)
    {
        if (collider.gameObject.CompareTag("LargeAsteroid"))
        {
            playerState.Score += LargeAsteroidPointValue;
            playerState.ExtraLifeScore += LargeAsteroidPointValue;
            Destroy(gameObject);
        }
        if (collider.gameObject.CompareTag("MediumAsteroid"))
        {
            playerState.Score += MediumAsteroidPointValue;
            playerState.ExtraLifeScore += MediumAsteroidPointValue;
            Destroy(gameObject);
        }
        if (collider.gameObject.CompareTag("SmallAsteroid"))
        {
            playerState.Score += SmallAsteroidPointValue;
            playerState.ExtraLifeScore += SmallAsteroidPointValue;
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

        //if ship is visible, no wrapping is necessary
        if (isVisible())
        {
            isWrappingHorizontally = false;
            isWrappingVertically = false;
            return;
        }

        //if ship is currently wrapping, do nothing
        if (isWrappingHorizontally && isWrappingVertically)
        {
            return;
        }

        //detect whether ship has disappeared horizontally...
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
