using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Level01 : MonoBehaviour
{
    private int asteroidCount;

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
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
            SceneManager.LoadScene("Level02", LoadSceneMode.Single);
        }
    }
}
