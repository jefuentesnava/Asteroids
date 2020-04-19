using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class Leaderboard : MonoBehaviour
{
    private TMP_Text textComponent;
    private SortedList<string, string> leaderboard;
    string leaderboardStringFinal;
    void Start()
    {
        textComponent = GetComponent<TMP_Text>();

        string leaderboardString = PlayerPrefs.GetString("leaderboard");
        string[] leaderboardStrings = leaderboardString.Split(',');

        leaderboard = new SortedList<string, string>();

        for (int i = 0; i < leaderboardStrings.Length; i += 2)
        {
            leaderboard.Add(leaderboardStrings[i], leaderboardStrings[i + 1]);
        }

        leaderboardStringFinal = "High Scores\n";

        foreach (KeyValuePair<string, string> kvp in leaderboard)
        {
            leaderboardStringFinal += $"{kvp.Key}: {kvp.Value}\n";
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (textComponent != null)
        {
            textComponent.SetText(leaderboardStringFinal);
        }
    }
}
