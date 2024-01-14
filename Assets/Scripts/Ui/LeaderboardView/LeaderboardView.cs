using System.Collections.Generic;
using UnityEngine;
using YandexSDK;

namespace UI
{
    public class LeaderboardView : MonoBehaviour
    {
        [SerializeField] private LeaderboardPlayerView _template;
        [SerializeField] private LeaderboardDataChanger _leaderboardDataChanger;
        [SerializeField] private GamePanel _loginPanel;

        private List<LeaderboardPlayerView> _leaderboardPlayerViews = new List<LeaderboardPlayerView>();

        private void OnEnable()
        {
            _leaderboardDataChanger.Created += OnCreated;
            _loginPanel.gameObject.SetActive(false);
        }

        private void OnDisable()
        {
            _leaderboardDataChanger.Created -= OnCreated;
        }

        private void Clear()
        {
            foreach (var playerView in _leaderboardPlayerViews)
            {
                Destroy(playerView.gameObject);
            }

            _leaderboardPlayerViews.Clear();
        }

        private void OnCreated(List<LeaderboardPlayer> leaderboardPlayers)
        {
            Clear();

            foreach (var player in leaderboardPlayers)
            {
                LeaderboardPlayerView leaderboardPlayerView = Instantiate(_template, transform);
                leaderboardPlayerView.Init(player.Number, player.Name, player.Score);
                _leaderboardPlayerViews.Add(leaderboardPlayerView);
            }
        }
    }
}