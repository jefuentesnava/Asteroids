using UnityEngine;

public class SmallAsteroid : MonoBehaviour
{
    public bool IsCollided { get; private set; } = false;
    public float MinimumVelocity { get; private set; } = 125;
    public float MaximumVelocity { get; private set; } = 150;
    private void Start()
    {
        transform.eulerAngles = new Vector3(0f, 0f, Random.Range(0, 360));
        var initialVelocty = Random.Range(MinimumVelocity, MaximumVelocity);
        GetComponent<Rigidbody2D>().AddForce(transform.up * initialVelocty);
    }

    private void OnTriggerEnter2D(Collider2D c)
    {
        if (c.CompareTag("ShipMissile"))
        {
            //prevent future calls
            if (IsCollided)
            {
                return;
            }

            IsCollided = true;
            FindObjectOfType<AudioManager>().Play("Death");
            Destroy(gameObject);
        }
    }
}