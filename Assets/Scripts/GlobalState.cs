using UnityEngine;

public class GlobalState : MonoBehaviour
{
    public const int DefaultNumberOfExtraLives = 3;

    public static GlobalState Instance;

    public bool HasTeleported { get; set; }
    public int Score { get; set; }
    public int ExtraLifeScore { get; set; }
    public int ExtraLives { get; set; }

    private void Awake()
    {
        if (Instance == null)
        {
            DontDestroyOnLoad(gameObject);
            Instance = this;

            Reset();
        }
        else if (Instance != this)
        {
            Destroy(gameObject);
        }
    }

    public void Reset()
    {
        Score = 0;
        ExtraLifeScore = 0;
        ExtraLives = DefaultNumberOfExtraLives;
        HasTeleported = false;
    }
}