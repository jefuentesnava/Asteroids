using System.Collections.Generic;
using System.Linq;
using TMPro;
using UnityEngine;

public class Leaderboard : MonoBehaviour
{
    private const int MaxNumberOfDisplayedScores = 10;

    private string leaderboardStringFinal = "High Scores\n";

    private void Start()
    {
        var textComponent = GetComponent<TMP_Text>();
        var leaderboard = PlayerPrefs.GetString("leaderboard");

        //if leaderboard isn't empty...
        if (!leaderboard.Equals(""))
        {
            string[] leaderboardStrings = leaderboard.Split(',');
            var leaderboardEntries = new List<LeaderboardEntry>();

            //...populate leaderboardEntries...
            for (var i = 0; i < leaderboardStrings.Length; i += 2)
            {
                var username = leaderboardStrings[i];
                var score = int.Parse(leaderboardStrings[i + 1]);

                var entry = new LeaderboardEntry(username, score);
                leaderboardEntries.Add(entry);
            }

            //...sort entries in ascending order...
            leaderboardEntries.Sort((a, b) => b.score.CompareTo(a.score));

            var numberOfScoresToDisplay = leaderboardEntries.Count();
            if (MaxNumberOfDisplayedScores < numberOfScoresToDisplay)
            {
                numberOfScoresToDisplay = MaxNumberOfDisplayedScores;
            }

            //...then build string to display top ten scores
            for (var i = 0; i < numberOfScoresToDisplay; i++)
            {
                var entry = leaderboardEntries.ElementAt(i);
                var username = entry.username;
                var score = entry.score;

                leaderboardStringFinal += $"{username}: {score}\n";
            }
        }

        textComponent.SetText(leaderboardStringFinal);
    }
}