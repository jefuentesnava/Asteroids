using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MediumAsteroid : MonoBehaviour
{
    private float initialVelocty;
    private bool isCollided = false;
    bool isWrappingHorizontally = false;
    bool isWrappingVertically = false;

    // Start is called before the first frame update
    void Start()
    {
        initialVelocty = Random.Range(75f, 100f);
        this.transform.eulerAngles = new Vector3(0f, 0f, Random.Range(0, 360));
        GetComponent<Rigidbody2D>().AddForce(transform.up * initialVelocty);
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

        //if asteroid is visible, no wrapping is necessary
        if (isVisible())
        {
            isWrappingHorizontally = false;
            isWrappingVertically = false;
            return;
        }

        //if asteroid is currently wrapping, do nothing
        if (isWrappingHorizontally && isWrappingVertically)
        {
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
