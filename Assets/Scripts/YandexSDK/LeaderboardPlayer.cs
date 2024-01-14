namespace YandexSDK
{
    public class LeaderboardPlayer
    {
        public LeaderboardPlayer(string number, string name, string score)
        {
            Number = number;
            Name = name;
            Score = score;
        }

        public string Number { get; private set; }
        public string Name { get; private set; }
        public string Score { get; private set; }
    }
}