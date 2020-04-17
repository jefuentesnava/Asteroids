using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Missile : MonoBehaviour
{
    private const float TimeOut = 0.75f;
    private const float MissileSpeed = 20f;
    private const int LargeAsteroidPointValue = 20;
    private const int MediumAsteroidPointValue = 50;
    private const int SmallAsteroidPointValue = 100;

    private bool isWrappingHorizontally = false;
    private bool isWrappingVertically = false;
    private float timer = 0.0f;

    public Rigidbody2D rigidBody;
    private Ship ship;

    // Start is called before the first frame update
    void Start()
    {
        rigidBody.velocity = transform.up * MissileSpeed;

        //get access to Ship functions to get current score
        GameObject shipObject = transform.parent.Find("Ship").gameObject;
        if (shipObject != null)
        {
            ship = shipObject.GetComponent<Ship>();
        }
    }

    // Update is called once per frame
    void Update()
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
            ship.addScore(LargeAsteroidPointValue);
            Destroy(gameObject);
        }
        if (collider.gameObject.CompareTag("MediumAsteroid"))
        {
            ship.addScore(MediumAsteroidPointValue);
            Destroy(gameObject);
        }
        if (collider.gameObject.CompareTag("SmallAsteroid"))
        {
            ship.addScore(SmallAsteroidPointValue);
            Destroy(gameObject);
        }
    }

    //screen-wrapping functions
    private bool isVisible()
    {
        Renderer renderer = GetComponentInChildren<Renderer>();

        if (renderer.isVisible)
        {
            return true;
        }
        else
        {
            return false;
        }
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
