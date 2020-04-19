using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Ship : MonoBehaviour
{
    //constants/readonlys
    public const float ThrusterSpeed = 12f;
    public const float RotationSpeed = 8f;
    public readonly Vector3 DefaultPosition = new Vector3(0f, 0f, 1f);
    public readonly Vector3 DefaultVelocity = new Vector3(0f, 0f, 1f);

    //properties
    public bool inputEnabled { get; private set; } = true;
    public bool isWrappingHorizontally { get; private set; } = false;
    public bool isWrappingVertically { get; private set; } = false;
    
    private PlayerState playerState;

    void Start()
    {
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

    void FixedUpdate()
    {
        if (inputEnabled)
        {
            getUserInput();
        }
        awardExtraLifeCheck();
        screenWrap();
    }

    public void getUserInput()
    {
        if (Input.GetKey(KeyCode.W))
        {
            GetComponent<Rigidbody2D>().AddForce(transform.up * ThrusterSpeed);
        }

        if (Input.GetKey(KeyCode.A))
        {
            transform.Rotate(Vector3.forward * RotationSpeed);
        }

        if (Input.GetKey(KeyCode.D))
        {
            transform.Rotate(-Vector3.forward * RotationSpeed);
        }
    }

    //ship-asteroid collision behavior; called when ship collides with asteroid
    private void OnTriggerEnter2D(Collider2D collider)
    {
        if (collider.gameObject.CompareTag("LargeAsteroid") ||
            collider.gameObject.CompareTag("MediumAsteroid") ||
            collider.gameObject.CompareTag("SmallAsteroid"))
        {
            if (playerState.ExtraLives > 0)
            {
                StartCoroutine(respawn());
            }
            else
            {
                SceneManager.LoadScene("GameOver", LoadSceneMode.Single);
            }
        }
    }

    //coroutine for OnTriggerEnter2D()
    private IEnumerator respawn()
    {
        playerState.ExtraLives--;

        inputEnabled = false;
        gameObject.GetComponent<SpriteRenderer>().enabled = false;
        gameObject.GetComponent<PolygonCollider2D>().enabled = false;
        gameObject.transform.localPosition = DefaultPosition;
        gameObject.GetComponent<Rigidbody2D>().velocity = DefaultVelocity;

        yield return new WaitForSeconds(1.0f);

        inputEnabled = true;
        gameObject.GetComponent<SpriteRenderer>().enabled = true;
        gameObject.GetComponent<PolygonCollider2D>().enabled = true;
    }


    private void awardExtraLifeCheck()
    {
        if (playerState.ExtraLifeScore > PlayerState.ExtraLifeAwardingThreshold)
        {
            playerState.ExtraLives++;
            playerState.ExtraLifeScore -= PlayerState.ExtraLifeAwardingThreshold;
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
