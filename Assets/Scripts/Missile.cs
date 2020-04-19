using UnityEngine;
using UnityEngine.SceneManagement;

public class Missile : MonoBehaviour
{
    public const float TimeOut = 0.75f;
    public const float MissileSpeed = 20f;
    public const int LargeAsteroidPointValue = 20;
    public const int MediumAsteroidPointValue = 50;
    public const int SmallAsteroidPointValue = 100;

    public float timer { get; private set; } = 0.0f;
    public Rigidbody2D rigidBody;

    private PlayerState playerState;

    // Start is called before the first frame update
    void Start()
    {
        rigidBody.velocity = transform.up * MissileSpeed;

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

        if (playerStateObject != null)
        {
            playerState = playerStateObject.GetComponent<PlayerState>();
        }
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        timer += Time.deltaTime;

        if (timer > TimeOut)
        {
            Destroy(gameObject);
        }
    }

    //missile-asteroid collision behavior; called when missile collides with asteroid
    private void OnTriggerEnter2D(Collider2D collider)
    {
        if (collider.gameObject.CompareTag("LargeAsteroid"))
        {
            playerState.Score += LargeAsteroidPointValue;
            playerState.ExtraLifeScore += LargeAsteroidPointValue;
            Destroy(gameObject);
        }
        if (collider.gameObject.CompareTag("MediumAsteroid"))
        {
            playerState.Score += MediumAsteroidPointValue;
            playerState.ExtraLifeScore += MediumAsteroidPointValue;
            Destroy(gameObject);
        }
        if (collider.gameObject.CompareTag("SmallAsteroid"))
        {
            playerState.Score += SmallAsteroidPointValue;
            playerState.ExtraLifeScore += SmallAsteroidPointValue;
            Destroy(gameObject);
        }
    }
}