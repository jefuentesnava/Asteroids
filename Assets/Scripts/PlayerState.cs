using UnityEngine;

public class PlayerState : MonoBehaviour
{
    public const int ExtraLifeAwardingThreshold = 10000;

    public int Score { get; set; }
    public int ExtraLifeScore { get; set; }
    public int ExtraLives { get; set; }

    private void Start()
    {
        //load state
        Score = GlobalState.instance.Score;
        ExtraLifeScore = GlobalState.instance.ExtraLifeScore; ;
        ExtraLives = GlobalState.instance.ExtraLives;
    }

    public void Save()
    {
        GlobalState.instance.Score = Score;
        GlobalState.instance.ExtraLifeScore = ExtraLifeScore;
        GlobalState.instance.ExtraLives = ExtraLives;
    }
}