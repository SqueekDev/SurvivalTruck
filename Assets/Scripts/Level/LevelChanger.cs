using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class LevelChanger : MonoBehaviour
{
    private const string LevelNumberKey = "Level";
    private const int BossLevelNubmerDivider = 2;

    [SerializeField] private WaveController _waveController;
    [SerializeField] private Player _player;
    [SerializeField] private Boss _boss;
    [SerializeField] private ChangeLevelArea _changeLevelArea;
    [SerializeField] private LostPanel _lostPanel;
    [SerializeField] private NextLevelButton _nextLevelButton;
    [SerializeField] private RestartLevelButton _restartLevelButton;

    private int _playerPrefsSavedLevelNumber = 1;

    public int CurrentLevelNumber { get; private set; } = 1;

    public event UnityAction<int> Changed;
    public event UnityAction BossLevelStarted;
    public event UnityAction BossLevelEnded;

    private void Awake()
    {
        _playerPrefsSavedLevelNumber = PlayerPrefs.GetInt(LevelNumberKey, 1);
    }

    private void OnEnable()
    {
        _waveController.WaveEnded += OnWaveEnded;
        _player.Died += OnPlayerDied;
        _boss.Died += OnBossDied;
        _nextLevelButton.Clicked += OnNextLevelButtonClick;
        _restartLevelButton.Clicked += OnRestartLevelButtonClick;
    }

    private void OnDisable()
    {
        _waveController.WaveEnded -= OnWaveEnded;
        _player.Died -= OnPlayerDied;
        _boss.Died -= OnBossDied;
        _nextLevelButton.Clicked -= OnNextLevelButtonClick;
        _restartLevelButton.Clicked -= OnRestartLevelButtonClick;
    }

    private void Start()
    {
        SyncLevelNumber();
        ChangeLevel();
    }

    private void SyncLevelNumber()
    {
        if (_playerPrefsSavedLevelNumber > CurrentLevelNumber)
            CurrentLevelNumber = _playerPrefsSavedLevelNumber;
        else
        {
            _playerPrefsSavedLevelNumber = CurrentLevelNumber;
            PlayerPrefs.SetInt(LevelNumberKey, _playerPrefsSavedLevelNumber);
            PlayerPrefs.Save();
        }
    }

    private void ChangeLevel()
    {
        if (CurrentLevelNumber % BossLevelNubmerDivider == 0)
            BossLevelStarted?.Invoke();

        Changed?.Invoke(CurrentLevelNumber);
    }

    private void OnWaveEnded()
    {
        _changeLevelArea.gameObject.SetActive(true);
    }

    private void OnBossDied(Health boss)
    {
        BossLevelEnded?.Invoke();
        OnWaveEnded();
    }

    private void OnPlayerDied(Health player)
    {
        Time.timeScale = 0;
        _lostPanel.gameObject.SetActive(true);
    }

    private void OnNextLevelButtonClick()
    {
        CurrentLevelNumber++;
        _changeLevelArea.gameObject.SetActive(false);

        if (CurrentLevelNumber != _playerPrefsSavedLevelNumber)
            SyncLevelNumber();

        ChangeLevel();
    }

    private void OnRestartLevelButtonClick()
    {
        Time.timeScale = 1;
        _lostPanel.gameObject.SetActive(false);
        ChangeLevel();
    }
}