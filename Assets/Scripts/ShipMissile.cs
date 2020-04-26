using UnityEngine;
using UnityEngine.SceneManagement;

public class ShipMissile : MonoBehaviour
{
    public const float Timeout = 0.75f;
    public const float MissileSpeed = 20f;
    public const int LargeAsteroidPointValue = 20;
    public const int MediumAsteroidPointValue = 50;
    public const int SmallAsteroidPointValue = 100;
    public const int LargeSaucerPointValue = 200;
    public const int SmallSaucerPointValue = 1000;

    public float Timer { get; private set; } = 0.0f;
    public Rigidbody2D RigidBody;   //leave as public field
    
    private PlayerState PlayerState;

    private void Start()
    {
        RigidBody.velocity = transform.up * MissileSpeed;

        //get access to player state
        GameObject playerStateObject = null;
        GameObject[] rootGameObjects = SceneManager.GetActiveScene().GetRootGameObjects();

        foreach (GameObject g in rootGameObjects)
        {
            if (g.transform.CompareTag("PlayerState"))
            {
                playerStateObject = g;
                break;
            }
        }

        PlayerState = playerStateObject.GetComponent<PlayerState>();
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
        if (c.gameObject.CompareTag("LargeAsteroid"))
        {
            PlayerState.Score += LargeAsteroidPointValue;
            PlayerState.ExtraLifeScore += LargeAsteroidPointValue;
            Destroy(gameObject);
        }

        if (c.gameObject.CompareTag("MediumAsteroid"))
        {
            PlayerState.Score += MediumAsteroidPointValue;
            PlayerState.ExtraLifeScore += MediumAsteroidPointValue;
            Destroy(gameObject);
        }

        if (c.gameObject.CompareTag("SmallAsteroid"))
        {
            PlayerState.Score += SmallAsteroidPointValue;
            PlayerState.ExtraLifeScore += SmallAsteroidPointValue;
            Destroy(gameObject);
        }

        if (c.gameObject.CompareTag("LargeSaucer"))
        {
            PlayerState.Score += LargeSaucerPointValue;
            PlayerState.ExtraLifeScore += LargeSaucerPointValue;
            Destroy(gameObject);
        }

        if (c.gameObject.CompareTag("SmallSaucer"))
        {
            PlayerState.Score += SmallSaucerPointValue;
            PlayerState.ExtraLifeScore += SmallSaucerPointValue;
            Destroy(gameObject);
        }

        PlayerState.Save();
    }
}