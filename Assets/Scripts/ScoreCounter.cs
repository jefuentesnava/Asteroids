using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class ScoreCounter : MonoBehaviour
{
    private Ship ship;
    private TMP_Text textComponent;

    // Start is called before the first frame update
    void Start()
    {
        //get access to Ship functions to get current score
        GameObject UI = transform.parent.gameObject;
        GameObject shipObject = UI.transform.parent.Find("Ship").gameObject;
        if (shipObject != null)
        {
            ship = shipObject.GetComponent<Ship>();
        }

        textComponent = GetComponent<TMP_Text>();
    }

    // Update is called once per frame
    void Update()
    {
        if (textComponent != null)
        {
            textComponent.SetText(ship.getScore().ToString());
        }
    }
}
