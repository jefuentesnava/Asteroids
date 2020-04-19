using UnityEngine;
using UnityEngine.SceneManagement;

public class LevelSwitcher : MonoBehaviour
{
    private PlayerState playerState;
    private int asteroidCount;

    void Start()
    {
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

    void Update()
    {
        asteroidCount = 0;

        GameObject[] rootGameObjects = SceneManager.GetActiveScene().GetRootGameObjects();

        foreach (GameObject g in rootGameObjects)
        {
            if (g.transform.CompareTag("LargeAsteroid"))
            {
                asteroidCount++;
            }
            else if (g.transform.CompareTag("MediumAsteroid"))
            {
                asteroidCount++;
            }
            else if (g.transform.CompareTag("SmallAsteroid"))
            {
                asteroidCount++;
            }
        }

        if (asteroidCount == 0)
        {
            playerState.save();

            string sceneName = SceneManager.GetActiveScene().name;

            switch (sceneName)
            {
                case "Level01":
                    SceneManager.LoadScene("Level02", LoadSceneMode.Single);
                    break;
                case "Level02":
                    SceneManager.LoadScene("Level03", LoadSceneMode.Single);
                    break;
                case "Level03":
                    SceneManager.LoadScene("Level04", LoadSceneMode.Single);
                    break;
                case "Level04":
                    SceneManager.LoadScene("Level04", LoadSceneMode.Single);
                    break;
                default:
                    Debug.LogError("Level switching failed");
                    break;
            }
        }
    }
}
