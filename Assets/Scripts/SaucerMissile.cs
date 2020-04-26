using UnityEngine;

public class SaucerMissile : MonoBehaviour
{
    public const float Timeout = 2.5f;
    public const float MissileSpeed = 5f;
    public float Timer { get; private set; } = 0.0f;
    public Rigidbody2D RigidBody;   //leave as public field

    private void Start()
    {
        RigidBody.velocity = transform.up * MissileSpeed;
    }

    private void FixedUpdate()
    {
        Timer += Time.fixedDeltaTime;

        if (Timer > Timeout)
        {
            Destroy(gameObject);
        }
    }

    private void OnTriggerEnter2D(Collider2D c)
    {
        if (c.gameObject.CompareTag("Ship"))
        {
            Destroy(gameObject);
        }
    }
}