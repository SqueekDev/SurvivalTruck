using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using Agava.YandexGames;

public class LeaderboardDataChanger : MonoBehaviour
{
    private const string LeaderboardName = "Leaderboard";
    private const int MinPlayersCount = 1;
    private const int MaxPlayersCount = 5;

    [SerializeField] private GamePanel _leaderboardPanel;
    [SerializeField] private LeaderboardButton _leaderboardButton;
    [SerializeField] private CoinCounter _coinCounter;
    [SerializeField] private GameButton _loginAcceptButton;


    private List<LeaderboardPlayer> _leaderboardPlayers = new List<LeaderboardPlayer>();

    public UnityAction<List<LeaderboardPlayer>> Created;

    private void OnEnable()
    {
#if UNITY_WEBGL && !UNITY_EDITOR
        _coinCounter.TotalCoinsAmountChanged += OnTotalCoinsChanged;
#endif
        _leaderboardButton.AutorizationCompleted += TryOpenPanel;
        _loginAcceptButton.Clicked += TryOpenPanel;
    }

    private void OnDisable()
    {
#if UNITY_WEBGL && !UNITY_EDITOR
        _coinCounter.TotalCoinsAmountChanged -= OnTotalCoinsChanged;
#endif
        _leaderboardButton.AutorizationCompleted -= TryOpenPanel;
        _loginAcceptButton.Clicked -= TryOpenPanel;
    }

#if UNITY_WEBGL && !UNITY_EDITOR
    private void OnTotalCoinsChanged()
    {
        if (PlayerAccount.IsAuthorized == false)
            return;

        int totalCoins = UnityEngine.PlayerPrefs.GetInt(PlayerPrefsKeys.TotalEarnedCoins, 0);
        Leaderboard.GetPlayerEntry(LeaderboardName, (result) =>
        {
            if (result == null || result.score < totalCoins)
                Leaderboard.SetScore(LeaderboardName, totalCoins);
        });
    }
#endif

    private void TryOpenPanel()
    {
        PlayerAccount.Authorize();

        if (PlayerAccount.IsAuthorized)
            PlayerAccount.RequestPersonalProfileDataPermission();

        if (PlayerAccount.IsAuthorized == false)
            return;

        _leaderboardPanel.gameObject.SetActive(true);
        FillTable();
    }

    private void FillTable()
    {
        if (PlayerAccount.IsAuthorized == false)
            return;

        _leaderboardPlayers.Clear();
        Leaderboard.GetEntries(LeaderboardName, result =>
        {
            int results = result.entries.Length;
            results = Mathf.Clamp(results, MinPlayersCount, MaxPlayersCount);

            for (int i = 0; i < results; i++)
            {
                string number = (i + 1).ToString();
                string level = result.entries[i].score.ToString();
                string playerName = result.entries[i].player.publicName;

                if (string.IsNullOrEmpty(playerName))
                    playerName = "Anonymous";

                _leaderboardPlayers.Add(new LeaderboardPlayer(number, playerName, level));
            }

            Created?.Invoke(_leaderboardPlayers);
        });
    }
}
