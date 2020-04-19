using UnityEngine;

public class SmallAsteroid : MonoBehaviour
{
    public bool IsCollided { get; private set; } = false;

    // Start is called before the first frame update
    void Start()
    {
        this.transform.eulerAngles = new Vector3(0f, 0f, Random.Range(0, 360));
        float initialVelocty = Random.Range(125f, 150f);
        GetComponent<Rigidbody2D>().AddForce(transform.up * initialVelocty);
    }

    //asteroid-missle collision behavior
    private void OnTriggerEnter2D(Collider2D collider)
    {
        if (collider.tag == "Missile")
        {
            //prevent future calls after initial collision
            if (IsCollided)
            {
                return;
            }

            IsCollided = true;
            Destroy(gameObject);
        }
    }
}
