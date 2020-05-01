using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Ship : MonoBehaviour
{
    public const float ThrusterSpeed = 12f;
    public const float RotationSpeed = 8f;
    public const float RespawnTime = 1.0f;
    public const float TeleportationTime = 0.25f;
    public const float InvulnerabilityTime = 1.0f;

    public bool InputEnabled { get; private set; } = true;

    private PlayerState PlayerState;

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
                break;
            }
        }

        PlayerState = playerStateObject.GetComponent<PlayerState>();
    }

    private void FixedUpdate()
    {
        if (InputEnabled)
        {
            GetUserInput();
        }

        AwardExtraLifeCheck();
    }

    private void GetUserInput()
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
            if (!PlayerState.HasTeleported)
            {
                StartCoroutine(Teleport());
                PlayerState.HasTeleported = true;
            }
        }
    }

    private void AwardExtraLifeCheck()
    {
        if (PlayerState.ExtraLifeScore > PlayerState.ExtraLifeAwardingThreshold)
        {
            FindObjectOfType<AudioManager>().Play("ExtraLife");
            PlayerState.ExtraLives++;
            PlayerState.ExtraLifeScore -= PlayerState.ExtraLifeAwardingThreshold;
            PlayerState.Save();
        }
    }

    private IEnumerator Teleport()
    {
        InputEnabled = false;
        gameObject.GetComponent<PolygonCollider2D>().enabled = false;

        Camera camera = Camera.main;

        //generate a random position, then ensure it is on the commonly-used z position
        Vector3 randomPosition = new Vector3(Random.Range(0f, 1f), Random.Range(0f, 1f), -1f);
        randomPosition = camera.ViewportToWorldPoint(randomPosition);
        randomPosition.z = -1f;
        gameObject.transform.position = randomPosition;

        yield return new WaitForSeconds(TeleportationTime);
        InputEnabled = true;

        yield return new WaitForSeconds(InvulnerabilityTime);
        gameObject.GetComponent<PolygonCollider2D>().enabled = true;
    }

    private void OnTriggerEnter2D(Collider2D c)
    {
        if (c.gameObject.CompareTag("LargeAsteroid") ||
            c.gameObject.CompareTag("MediumAsteroid") ||
            c.gameObject.CompareTag("SmallAsteroid") ||
            c.gameObject.CompareTag("LargeSaucer") ||
            c.gameObject.CompareTag("SmallSaucer") ||
            c.gameObject.CompareTag("SaucerMissile"))
        {
            FindObjectOfType<AudioManager>().Play("Death");
            if (PlayerState.ExtraLives > 0)
            {
                StartCoroutine(Respawn());
            }
            else
            {
                SceneManager.LoadScene("GameOver", LoadSceneMode.Single);
            }
        }
    }

    private IEnumerator Respawn()
    {
        PlayerState.ExtraLives--;

        //disable input, hide sprite, disable collision, reset physics
        InputEnabled = false;
        gameObject.GetComponent<SpriteRenderer>().enabled = false;
        gameObject.GetComponent<PolygonCollider2D>().enabled = false;
        gameObject.transform.localPosition = Vector3.zero;
        gameObject.GetComponent<Rigidbody2D>().velocity = Vector3.zero;
        gameObject.GetComponent<Rigidbody2D>().angularVelocity = 0f;

        yield return new WaitForSeconds(RespawnTime);

        //reenable input, show sprite, reenable collision
        InputEnabled = true;
        gameObject.GetComponent<SpriteRenderer>().enabled = true;
        gameObject.GetComponent<PolygonCollider2D>().enabled = true;
    }
}