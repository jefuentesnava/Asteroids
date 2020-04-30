using UnityEngine;

public class MediumAsteroid : MonoBehaviour
{
    public bool IsCollided { get; private set; } = false;
    public float MinimumVelocity { get; private set; } = 75f;
    public float MaximumVelocity { get; private set; } = 100f;
    public GameObject SmallAsteroidPrefab;  //leave as public field

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
            
            Instantiate(
                SmallAsteroidPrefab,
                transform.position,
                Random.rotation,
                transform.parent);
            Instantiate(
                SmallAsteroidPrefab,
                transform.position,
                Random.rotation,
                transform.parent);
            FindObjectOfType<AudioManager>().Play("Death");
            Destroy(gameObject);
        }
    }
}