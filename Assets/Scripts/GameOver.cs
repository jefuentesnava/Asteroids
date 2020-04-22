using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameOver : MonoBehaviour
{
    public void GetUserName()
    {
        var inputFieldComponent = transform.Find("Name Input").GetComponent<TMP_InputField>();
        var username = inputFieldComponent.text.ToUpper();
        var score = GlobalState.instance.Score.ToString();
        var leaderboardString = PlayerPrefs.GetString("leaderboard");
        string entry;

        if (leaderboardString == "")
        {
            entry = $"{username},{score}";
        }
        else
        {
            entry = $"{leaderboardString},{username},{score}";
        }

        PlayerPrefs.SetString("leaderboard", entry);
        PlayerPrefs.Save();

        SceneManager.LoadScene("MainMenu", LoadSceneMode.Single);
    }
}