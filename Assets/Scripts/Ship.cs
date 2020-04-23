using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Ship : MonoBehaviour
{
    public const float ThrusterSpeed = 12f;
    public const float RotationSpeed = 8f;

    public bool InputEnabled { get; private set; } = true;
    private PlayerState playerState;

    private void Start()
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
        playerState = playerStateObject.GetComponent<PlayerState>();
    }

    private void FixedUpdate()
    {
        if (InputEnabled)
        {
            GetUserInput();
        }
        AwardExtraLifeCheck();

    }

    public void GetUserInput()
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

        if (Input.GetKey(KeyCode.LeftShift))
        {
            if (!playerState.hasTeleported)
            {
                StartCoroutine(Teleport());
                playerState.hasTeleported = true;
            }
        }
    }

    //ship-asteroid collision behavior; called when ship collides with asteroid
    private void OnTriggerEnter2D(Collider2D c)
    {
        if (c.gameObject.CompareTag("LargeAsteroid") ||
            c.gameObject.CompareTag("MediumAsteroid") ||
            c.gameObject.CompareTag("SmallAsteroid"))
        {
            if (playerState.ExtraLives > 0)
            {
                StartCoroutine(Respawn());
            }
            else
            {
                SceneManager.LoadScene("GameOver", LoadSceneMode.Single);
            }
        }
    }

    //coroutine for OnTriggerEnter2D()
    private IEnumerator Respawn()
    {
        playerState.ExtraLives--;

        InputEnabled = false;
        gameObject.GetComponent<SpriteRenderer>().enabled = false;
        gameObject.GetComponent<PolygonCollider2D>().enabled = false;
        gameObject.transform.localPosition = Vector3.zero;
        gameObject.GetComponent<Rigidbody2D>().velocity = Vector3.zero;
        gameObject.GetComponent<Rigidbody2D>().angularVelocity = 0f;

        yield return new WaitForSeconds(1.0f);

        InputEnabled = true;
        gameObject.GetComponent<SpriteRenderer>().enabled = true;
        gameObject.GetComponent<PolygonCollider2D>().enabled = true;
    }

    private IEnumerator Teleport()
    {
        InputEnabled = false;
        gameObject.GetComponent<PolygonCollider2D>().enabled = false;

        Camera camera = Camera.main;

        Vector3 randomPosition = new Vector3(Random.Range(0f, 1f), Random.Range(0f, 1f), -1f);
        randomPosition = camera.ViewportToWorldPoint(randomPosition);
        randomPosition.z = -1f;
        gameObject.transform.position = randomPosition;

        yield return new WaitForSeconds(0.25f);
        InputEnabled = true;

        yield return new WaitForSeconds(1.0f);
        gameObject.GetComponent<PolygonCollider2D>().enabled = true;
    }


    private void AwardExtraLifeCheck()
    {
        if (playerState.ExtraLifeScore > PlayerState.ExtraLifeAwardingThreshold)
        {
            playerState.ExtraLives++;
            playerState.ExtraLifeScore -= PlayerState.ExtraLifeAwardingThreshold;
            playerState.Save();
        }
    }
}