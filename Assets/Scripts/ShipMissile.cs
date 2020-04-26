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
    public Rigidbody2D RigidBody;
    private PlayerState playerState;

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
            }
        }
        playerState = playerStateObject.GetComponent<PlayerState>();
    }

    private void FixedUpdate()
    {
        Timer += Time.fixedDeltaTime;

        if (Timer > Timeout)
        {
            Destroy(gameObject);
        }
    }

    //missile-asteroid collision behavior; called when missile collides with asteroid
    private void OnTriggerEnter2D(Collider2D c)
    {
        if (c.gameObject.CompareTag("LargeAsteroid"))
        {
            playerState.Score += LargeAsteroidPointValue;
            playerState.ExtraLifeScore += LargeAsteroidPointValue;
            Destroy(gameObject);
        }

        if (c.gameObject.CompareTag("MediumAsteroid"))
        {
            playerState.Score += MediumAsteroidPointValue;
            playerState.ExtraLifeScore += MediumAsteroidPointValue;
            Destroy(gameObject);
        }

        if (c.gameObject.CompareTag("SmallAsteroid"))
        {
            playerState.Score += SmallAsteroidPointValue;
            playerState.ExtraLifeScore += SmallAsteroidPointValue;
            Destroy(gameObject);
        }

        if (c.gameObject.CompareTag("LargeSaucer"))
        {
            playerState.Score += LargeSaucerPointValue;
            playerState.ExtraLifeScore += LargeSaucerPointValue;
            Destroy(gameObject);
        }

        if (c.gameObject.CompareTag("SmallSaucer"))
        {
            playerState.Score += SmallSaucerPointValue;
            playerState.ExtraLifeScore += SmallSaucerPointValue;
            Destroy(gameObject);
        }

        playerState.Save();
    }
}