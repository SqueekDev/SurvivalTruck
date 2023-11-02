using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.SceneManagement;

public class LevelChanger : MonoBehaviour
{
    [SerializeField] private WaveController _waveController;
    [SerializeField] private Player _player;
    [SerializeField] private Boss _boss;
    [SerializeField] private ChangeLevelArea _changeLevelArea;
    [SerializeField] private LostPanel _lostPanel;
    [SerializeField] private GameButton _nextLevelButton;
    [SerializeField] private GameButton _restartLevelButton;
    [SerializeField] private int _bossLevelNubmerDivider;

    private int _playerPrefsSavedLevelNumber = 1;
    private Scene _currentScene;

    public int BossLevelNumber => _bossLevelNubmerDivider;

    public int CurrentLevelNumber { get; private set; } = 1;

    public event UnityAction<int> Changed;
    public event UnityAction BossLevelStarted;
    public event UnityAction BossLevelEnded;

    private void Awake()
    {
        _currentScene = SceneManager.GetActiveScene();
        _playerPrefsSavedLevelNumber = PlayerPrefs.GetInt(PlayerPrefsKeys.LevelNumber, 1);
        SyncLevelNumber();
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
        ChangeLevel();
    }

    private void SyncLevelNumber()
    {
        if (_playerPrefsSavedLevelNumber > CurrentLevelNumber)
            CurrentLevelNumber = _playerPrefsSavedLevelNumber;
        else
        {
            _playerPrefsSavedLevelNumber = CurrentLevelNumber;
            PlayerPrefs.SetInt(PlayerPrefsKeys.LevelNumber, _playerPrefsSavedLevelNumber);
            PlayerPrefs.Save();
        }
    }

    private void ChangeLevel()
    {
        if (CurrentLevelNumber % _bossLevelNubmerDivider == 0)
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
        SceneManager.LoadScene(_currentScene.name);
    }
}
