using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ship : MonoBehaviour
{
    private int extraLives = 3;
    private float thrusterSpeed = 1f;
    private float rotationSpeed = 1f;

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKey(KeyCode.W))
        {
            GetComponent<Rigidbody2D>().AddForce(transform.up * thrusterSpeed);
        }

        if (Input.GetKey(KeyCode.A))
        {
            transform.Rotate(Vector3.forward * rotationSpeed);
        }

        if (Input.GetKey(KeyCode.D))
        {
            transform.Rotate(-Vector3.forward * rotationSpeed);
        }

    }

    public int getExtraLives()
    {
        return extraLives;
    }

    //called when Ship collides with an asteroid
    private void OnTriggerEnter2D(Collider2D collider)
    {
        if (collider.tag == "Asteroid" && extraLives > 0)
        {
            Debug.Log("Asteroid collision detected");
            StartCoroutine(respawn());
        }
    }

    private IEnumerator respawn()
    {
        Debug.Log("Respawning...");
        extraLives--;

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

        gameObject.GetComponent<SpriteRenderer>().enabled = true;
        gameObject.GetComponent<PolygonCollider2D>().enabled = true;
        Debug.Log("Respawned");
    }
}
