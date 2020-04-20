using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using TMPro;

public class Leaderboard : MonoBehaviour
{
    private TMP_Text textComponent;

    string leaderboardStringFinal = "High Scores\n";

    void Start()
    {
        textComponent = GetComponent<TMP_Text>();

        string leaderboardString = PlayerPrefs.GetString("leaderboard");
        string[] leaderboardStrings = leaderboardString.Split(',');
        List<LeaderboardEntry> leaderboardEntries = new List<LeaderboardEntry>();

        //populate leaderboardEntries
        for (int i = 0; i < leaderboardStrings.Length; i += 2)
        {
            string username = leaderboardStrings[i];
            int score = int.Parse(leaderboardStrings[i + 1]);

            LeaderboardEntry entry = new LeaderboardEntry(username, score);
            leaderboardEntries.Add(entry);
        }

        //sort entries in ascending order
        leaderboardEntries.Sort((a, b) => b.score.CompareTo(a.score));

        //build string to display top ten scores
        if (leaderboardEntries.Count < 10)
        {
            foreach (LeaderboardEntry e in leaderboardEntries)
            {
                leaderboardStringFinal += $"{e.username}: {e.score.ToString()}\n";
            }
        }
        else
        {
            for (int i = 0; i < 10; i++)
            {
                LeaderboardEntry entry = leaderboardEntries.ElementAt(i);
                string username = entry.username;
                int score = entry.score;

                leaderboardStringFinal += $"{username}: {score.ToString()}\n";
            }
        }
    }

    void Update()
    {
        if (textComponent != null)
        {
            textComponent.SetText(leaderboardStringFinal);
        }
    }
}