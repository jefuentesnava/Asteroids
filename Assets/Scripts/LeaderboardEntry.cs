public class LeaderboardEntry
{
    public string username;
    public int score;

    public LeaderboardEntry(string username, int score)
    {
        this.username = username;
        this.score = score;
    }

    public override string ToString()
    {
        return $"username: {username}, score: {score}";
    }
}
