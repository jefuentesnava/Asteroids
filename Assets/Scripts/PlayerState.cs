using UnityEngine;

public class PlayerState : MonoBehaviour
{
    public const int ExtraLifeAwardingThreshold = 10000;

    public bool HasTeleported { get; set; }
    public int Score { get; set; }
    public int ExtraLifeScore { get; set; }
    public int ExtraLives { get; set; }

    private void Start()
    {
        //load state
        HasTeleported = GlobalState.Instance.HasTeleported;
        Score = GlobalState.Instance.Score;
        ExtraLifeScore = GlobalState.Instance.ExtraLifeScore; ;
        ExtraLives = GlobalState.Instance.ExtraLives;
    }

    public void Save()
    {
        GlobalState.Instance.HasTeleported = HasTeleported;
        GlobalState.Instance.Score = Score;
        GlobalState.Instance.ExtraLifeScore = ExtraLifeScore;
        GlobalState.Instance.ExtraLives = ExtraLives;
    }
}