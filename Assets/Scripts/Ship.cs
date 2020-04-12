using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ship : MonoBehaviour
{
    private int extraLives = 3;
    private float thrusterSpeed = 5f;
    private float rotationSpeed = 1f;
    Vector3 originalPosition;

    // Start is called before the first frame update
    void Start()
    {
        originalPosition = gameObject.transform.position;
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKey(KeyCode.W))
        {
            GetComponent<Rigidbody2D>().AddForce(transform.up);
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
        if (collider.tag == "Asteroid")
        {
            Debug.Log("Asteroid collision detected");
        }

        if (extraLives > 0)
        {
            Debug.Log("Respawning...");
            respawn();
        }
        
    }

    private IEnumerator respawn()
    {
        extraLives--;

        gameObject.SetActive(false);
        yield return new WaitForSeconds(1);

        gameObject.transform.position = originalPosition;
        gameObject.SetActive(true);
    }
}
