using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LivesCounter : MonoBehaviour
{
    private const int DefaultNumberOfExtraLives = 3;
    private int currentNumberOfExtraLives = DefaultNumberOfExtraLives;
    private GameObject UI;
    private GameObject ship;
    private Component shipComponent;
    private GameObject[] lifeIcons;

    // Start is called before the first frame update
    void Start()
    {
        //get access to Ship.cs functions to get current number of extra lives
        UI = transform.parent.gameObject;
        ship = UI.transform.parent.Find("Ship").gameObject;

        //get access to life icons
        lifeIcons = new GameObject[DefaultNumberOfExtraLives];
        for (int i = 0; i < DefaultNumberOfExtraLives; i++)
        {
            lifeIcons[i] = transform.GetChild(i).gameObject;
        }
    }

    // Update is called once per frame
    void Update()
    {
        currentNumberOfExtraLives = ship.GetComponent<Ship>().getExtraLives();

        if (currentNumberOfExtraLives == 2)
        {
            lifeIcons[2].SetActive(false);
        }
        else if (currentNumberOfExtraLives == 1)
        {
            lifeIcons[2].SetActive(false);
            lifeIcons[1].SetActive(false);
        }
        else if (currentNumberOfExtraLives == 0)
        {
            lifeIcons[2].SetActive(false);
            lifeIcons[1].SetActive(false);
            lifeIcons[0].SetActive(false);
        }
    }
}
