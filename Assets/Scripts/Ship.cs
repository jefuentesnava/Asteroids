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
}