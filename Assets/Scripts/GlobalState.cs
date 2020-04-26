using UnityEngine;
using UnityEngine.SceneManagement;

public class GlobalState : MonoBehaviour
{
    public const int defaultNumberOfExtraLives = 3;

    public static GlobalState instance;

    public bool hasTeleported;
    public int Score { get; set; }
    public int ExtraLifeScore { get; set; }
    public int ExtraLives { get; set; }

    private void Awake()
    {
        if (instance == null)
        {
            DontDestroyOnLoad(gameObject);
            instance = this;

            hasTeleported = false;
            ExtraLives = defaultNumberOfExtraLives;
        }
        else if (instance != this)
        {
            Destroy(gameObject);
        }
    }

    public void Reset()
    {
        Score = 0;
        ExtraLifeScore = 0;
        ExtraLives = defaultNumberOfExtraLives;
        hasTeleported = false;
    }
}