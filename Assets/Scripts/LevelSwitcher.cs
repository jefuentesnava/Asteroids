using UnityEngine;
using UnityEngine.SceneManagement;

public class LevelSwitcher : MonoBehaviour
{
    private int asteroidCount;

    void Update()
    {
        asteroidCount = 0;

        foreach (Transform child in transform)
        {
            if (child.CompareTag("LargeAsteroid"))
            {
                asteroidCount++;
            }
            else if (child.CompareTag("MediumAsteroid"))
            {
                asteroidCount++;
            }
            else if (child.CompareTag("SmallAsteroid"))
            {
                asteroidCount++;
            }
        }

        if (asteroidCount == 0)
        {
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
