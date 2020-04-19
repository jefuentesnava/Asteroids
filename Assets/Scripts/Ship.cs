using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Ship : MonoBehaviour
{
    private const float ThrusterSpeed = 12f;
    private const float RotationSpeed = 8f;
    private const int ExtraLifeAwardingThreshold = 10000;

    private static int extraLives = 9999999;
    private static int score = 0;
    private static int extraLifeScore;

    private bool inputEnabled = true;
    private bool isWrappingHorizontally = false;
    private bool isWrappingVertically = false;

    void Start()
    {
        extraLifeScore = score;
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

    //basic getters
    public int getExtraLives()
    {
        return extraLives;
    }

    public void addScore(int points)
    {
        score += points;
    }

    public void addExtraLifeScore(int points)
    {
        extraLifeScore += points;
    }

    public int getScore()
    {
        return score;
    }

    public bool getInputEnabled()
    {
        return inputEnabled;
    }

    //movement keybinds
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
            if (extraLives > 0)
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
        extraLives--;

        //disable input
        inputEnabled = false;

        //hide sprite
        gameObject.GetComponent<SpriteRenderer>().enabled = false;

        //reset ship to original position
        gameObject.transform.localPosition = new Vector3(0f, 0f, 0f);

        //reset rigidbody physics
        Rigidbody2D rb = gameObject.GetComponent<Rigidbody2D>();
        rb.velocity = new Vector3(0f, 0f, 0f);

        //disable collider
        gameObject.GetComponent<PolygonCollider2D>().enabled = false;

        yield return new WaitForSeconds(1.0f);

        inputEnabled = true;
        gameObject.GetComponent<SpriteRenderer>().enabled = true;
        gameObject.GetComponent<PolygonCollider2D>().enabled = true;
    }

    //extra life awarding functionality
    private void awardExtraLifeCheck()
    {
        if (extraLifeScore > ExtraLifeAwardingThreshold)
        {
            extraLives++;
            extraLifeScore -= ExtraLifeAwardingThreshold;
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
