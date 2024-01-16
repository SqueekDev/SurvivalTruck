using System;
using System.Collections.Generic;
using Agava.YandexGames;
using Lean.Localization;
using Level;
using Shop;
using UI;
using UnityEngine;

namespace YandexSDK
{
    public class LeaderboardDataChanger : MonoBehaviour
    {
        private const string LeaderboardName = "Leaderboard";
        private const int MinPlayersCount = 1;
        private const int MaxPlayersCount = 5;

        [SerializeField] private GamePanel _leaderboardPanel;
        [SerializeField] private LeaderboardButton _leaderboardButton;
        [SerializeField] private CoinCounter _coinCounter;
        [SerializeField] private GameButton _loginAcceptButton;
        [SerializeField] private LeanPhrase _anonymousText;
        [SerializeField] private LevelChanger _levelChanger;

        private List<LeaderboardPlayer> _leaderboardPlayers = new List<LeaderboardPlayer>();

        public Action<List<LeaderboardPlayer>> Created;

        private void OnEnable()
        {
            _levelChanger.Finished += OnScoreChanged;
            _coinCounter.VideoBonusAdded += OnScoreChanged;
            _leaderboardButton.AutorizationCompleted += OnLeaderboardButtonClicked;
            _loginAcceptButton.Clicked += OnLeaderboardButtonClicked;
        }

        private void OnDisable()
        {
            _levelChanger.Finished -= OnScoreChanged;
            _coinCounter.VideoBonusAdded -= OnScoreChanged;
            _leaderboardButton.AutorizationCompleted -= OnLeaderboardButtonClicked;
            _loginAcceptButton.Clicked -= OnLeaderboardButtonClicked;
        }

        private void FillTable()
        {
            if (PlayerAccount.IsAuthorized == false)
            {
                return;
            }

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
                    {
                        playerName = LeanLocalization.GetTranslationText(_anonymousText.name);
                    }

                    _leaderboardPlayers.Add(new LeaderboardPlayer(number, playerName, level));
                }

                Created?.Invoke(_leaderboardPlayers);
            });
        }

        private void OnLeaderboardButtonClicked()
        {
            PlayerAccount.Authorize();

            if (PlayerAccount.IsAuthorized)
            {
                PlayerAccount.RequestPersonalProfileDataPermission();
            }

            if (PlayerAccount.IsAuthorized == false)
            {
                return;
            }

            _leaderboardPanel.gameObject.SetActive(true);
            FillTable();
        }

        private void OnScoreChanged()
        {
#if UNITY_WEBGL && !UNITY_EDITOR
        if (PlayerAccount.IsAuthorized == false)
        {
            return;
        }

        int totalCoins = UnityEngine.PlayerPrefs.GetInt(PlayerPrefsKeys.TotalEarnedCoins, 0);
        Leaderboard.GetPlayerEntry(LeaderboardName, (result) =>
        {
            if (result == null || result.score < totalCoins)
            {
                Leaderboard.SetScore(LeaderboardName, totalCoins);
            }
        });
#endif
        }
    }
}