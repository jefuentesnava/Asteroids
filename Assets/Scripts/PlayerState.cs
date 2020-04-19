using UnityEngine;

public class PlayerState : MonoBehaviour
{
    //constants
    public const int ExtraLifeAwardingThreshold = 10000;

    //properties
    public int Score { get; set; }
    public int ExtraLifeScore { get; set; }
    public int ExtraLives { get; set; }

    void Start()
    {
        //load state
        Score = GlobalState.instance.Score;
        ExtraLifeScore = GlobalState.instance.ExtraLifeScore; ;
        ExtraLives = GlobalState.instance.ExtraLives;
    }

    public void save()
    {
        GlobalState.instance.Score = Score;
        GlobalState.instance.ExtraLifeScore = ExtraLifeScore;
        GlobalState.instance.ExtraLives = ExtraLives;
    }
}
