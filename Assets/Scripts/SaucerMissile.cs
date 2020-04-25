using UnityEngine;

public class SaucerMissile : MonoBehaviour
{
    public const float Timeout = 2.5f;
    public const float MissileSpeed = 5f;
    public float Timer { get; private set; } = 0.0f;
    public Rigidbody2D RigidBody;

    void Start()
    {

        RigidBody.velocity = transform.up * MissileSpeed;
    }

    void FixedUpdate()
    {
        Timer += Time.fixedDeltaTime;

        if (Timer > Timeout)
        {
            Destroy(gameObject);
        }
    }

    //missile-ship collision behavior; called when saucer missile collides with ship
    private void OnTriggerEnter2D(Collider2D c)
    {
        if (c.gameObject.CompareTag("Ship"))
        {
            Destroy(gameObject);
        }
    }
}