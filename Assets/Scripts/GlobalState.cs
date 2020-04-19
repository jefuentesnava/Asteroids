using UnityEngine;

public class GlobalState : MonoBehaviour
{
    public const int defaultNumberOfExtraLives = 3;

    public static GlobalState instance;
    public int Score { get; set; }
    public int ExtraLifeScore { get; set; }
    public int ExtraLives { get; set; }

    void Awake()
    {
        if (instance == null)
        {
            DontDestroyOnLoad(gameObject);
            instance = this;

            ExtraLives = defaultNumberOfExtraLives;
        }
        else if (instance != this)
        {
            Destroy(gameObject);
        }
    }

    public void reset()
    {
        Score = 0;
        ExtraLifeScore = 0;
        ExtraLives = defaultNumberOfExtraLives;
    }
}
