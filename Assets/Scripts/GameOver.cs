using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;

public class GameOver : MonoBehaviour
{
    public void getUserName()
    {
        string username = transform.Find("Name Input").GetComponent<TMP_InputField>().text.ToUpper();
        string score = GlobalState.instance.Score.ToString();

        string leaderboardString = PlayerPrefs.GetString("leaderboard");
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
        //print($"Added: {username}, {score}");
        PlayerPrefs.Save();

        SceneManager.LoadScene("MainMenu", LoadSceneMode.Single);
    }
}