public class LeaderboardEntry
{
    public string Username { get; set; }
    public int Score { get; set; }

    public LeaderboardEntry(string username, int score)
    {
        this.Username = username;
        this.Score = score;
    }

    public override string ToString()
    {
        return $"username: {Username}, score: {Score}";
    }
}