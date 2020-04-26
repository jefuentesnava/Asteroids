using UnityEngine;
using UnityEngine.SceneManagement;

public class LevelSwitcher : MonoBehaviour
{
    private PlayerState PlayerState;

    private void Start()
    {
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
        var asteroidCount = 0;
        GameObject[] rootGameObjects = SceneManager.GetActiveScene().GetRootGameObjects();

        foreach (GameObject g in rootGameObjects)
        {
            if (g.CompareTag("LargeAsteroid") ||
                g.CompareTag("MediumAsteroid") ||
                g.CompareTag("SmallAsteroid"))
            {
                asteroidCount++;
            }
        }

        if (asteroidCount == 0)
        {
            PlayerState.Save();

            var sceneName = SceneManager.GetActiveScene().name;

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