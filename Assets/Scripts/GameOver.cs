using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;

public class GameOver : MonoBehaviour
{
    public void getUserName()
    {
        print(transform.Find("Name Input").GetComponent<TMP_InputField>().text);
        SceneManager.LoadScene("MainMenu", LoadSceneMode.Single);
    }
}