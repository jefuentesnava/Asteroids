using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Level02 : MonoBehaviour
{
    int asteroidCount;

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
            if (child.tag == "Asteroid")
            {
                asteroidCount++;
            }
        }

        if (asteroidCount == 0)
        {
            SceneManager.LoadScene("Level03", LoadSceneMode.Single);
        }
    }
}
