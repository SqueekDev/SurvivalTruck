using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class LeaderboardView : MonoBehaviour
{
    [SerializeField] private LeaderboardPlayerView _template;
    [SerializeField] private LeaderboardDataChanger _leaderboardDataChanger;
    [SerializeField] private GamePanel _loginPanel;

    private List<LeaderboardPlayerView> _leaderboardPlayerViews = new List<LeaderboardPlayerView>();

    public event UnityAction Opened;
    public event UnityAction Closed;

    private void OnEnable()
    {
        _leaderboardDataChanger.Created += OnCreated;
        _loginPanel.gameObject.SetActive(false);
        Opened?.Invoke();
    }

    private void OnDisable()
    {
        _leaderboardDataChanger.Created -= OnCreated;
        Closed?.Invoke();
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

    private void Clear()
    {
        foreach (var playerView in _leaderboardPlayerViews)
            Destroy(playerView.gameObject);

        _leaderboardPlayerViews.Clear();
    }
}
