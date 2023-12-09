using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GamePauser : MonoBehaviour
{
    [SerializeField] private GamePanel _lostPanel;
    [SerializeField] private GamePanel _loginPanel;
    [SerializeField] private GamePanel _leaderboardPanel;
    [SerializeField] private GamePanel _settingsPanel;
    [SerializeField] private GamePanel _addCoinsPanel;

    private void OnEnable()
    {
        _loginPanel.Opened += OnOpened;
        _lostPanel.Opened += OnOpened;
        _leaderboardPanel.Opened += OnOpened;
        _settingsPanel.Opened += OnOpened;
        _addCoinsPanel.Opened += OnOpened;
        _loginPanel.Closed += OnClosed;
        _lostPanel.Closed += OnClosed;
        _leaderboardPanel.Closed += OnClosed;
        _settingsPanel.Closed += OnClosed;
        _addCoinsPanel.Closed += OnClosed;
    }

    private void OnDisable()
    {
        _loginPanel.Opened -= OnOpened;
        _lostPanel.Opened -= OnOpened;
        _leaderboardPanel.Opened -= OnOpened;
        _settingsPanel.Opened -= OnOpened;
        _addCoinsPanel.Opened -= OnOpened;
        _loginPanel.Closed -= OnClosed;
        _lostPanel.Closed -= OnClosed;
        _leaderboardPanel.Closed -= OnClosed;
        _settingsPanel.Closed -= OnClosed;
        _addCoinsPanel.Closed -= OnClosed;
    }

    private void OnOpened()
    {
        Time.timeScale = 0;
    }

    private void OnClosed()
    {
        Time.timeScale = 1;
    }
}
