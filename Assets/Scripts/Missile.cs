using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Missile : MonoBehaviour
{
    private float TimeOut = 0.75f;
    
    public float missileSpeed = 20f;
    public Rigidbody2D rigidBody;
    bool isWrappingHorizontally = false;
    bool isWrappingVertically = false;
    private float timer = 0.0f;
    
    // Start is called before the first frame update
    void Start()
    {
        rigidBody.velocity = transform.up * missileSpeed;
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
        if (collider.tag == "Asteroid")
        {
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
