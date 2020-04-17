using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;

public class GameOver : MonoBehaviour
{
    public void getUserName()
    {
        Debug.Log(transform.Find("Name Input").GetComponent<TMP_InputField>().text);
        SceneManager.LoadScene("MainMenu", LoadSceneMode.Single);
    }
}