using UnityEngine;
using UnityEngine.Events;
using UnityEngine.SceneManagement;

public class LevelChanger : MonoBehaviour
{
    [SerializeField] private WaveController _waveController;
    [SerializeField] private Player _player;
    [SerializeField] private Boss _boss;
    [SerializeField] private ChangeLevelArea _changeLevelArea;
    [SerializeField] private GamePanel _lostPanel;
    [SerializeField] private GameButton _nextLevelButton;
    [SerializeField] private GameButton _restartLevelButton;
    [SerializeField] private GameButton _settingsRestartLevelButton;
    [SerializeField] private int _bossLevelNubmerDivider;

    private int _playerPrefsSavedLevelNumber = 1;
    private bool _isWave = false;
    private Scene _currentScene;

    public int BossLevelNumber => _bossLevelNubmerDivider;
    public bool IsWave => _isWave;
    public int CurrentLevelNumber { get; private set; } = 1;

    public event UnityAction<int> Changed;
    public event UnityAction LevelFinished;
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
        _settingsRestartLevelButton.Clicked += OnRestartLevelButtonClick;
    }

    private void OnDisable()
    {
        _waveController.WaveEnded -= OnWaveEnded;
        _player.Died -= OnPlayerDied;
        _boss.Died -= OnBossDied;
        _nextLevelButton.Clicked -= OnNextLevelButtonClick;
        _restartLevelButton.Clicked -= OnRestartLevelButtonClick;
        _settingsRestartLevelButton.Clicked -= OnRestartLevelButtonClick;
    }

    private void Start()
    {
        _changeLevelArea.gameObject.SetActive(true);
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

        _isWave = true;
        Changed?.Invoke(CurrentLevelNumber);
    }

    private void OnWaveEnded()
    {
        _isWave = false;
        _changeLevelArea.gameObject.SetActive(true);
        CurrentLevelNumber++;

        if (CurrentLevelNumber != _playerPrefsSavedLevelNumber)
            SyncLevelNumber();

        LevelFinished?.Invoke();
    }

    private void OnBossDied(Health boss)
    {
        BossLevelEnded?.Invoke();
        OnWaveEnded();
    }

    private void OnPlayerDied(Health player)
    {
        _lostPanel.gameObject.SetActive(true);
    }

    private void OnNextLevelButtonClick()
    {
        _changeLevelArea.gameObject.SetActive(false);
        ChangeLevel();
    }

    private void OnRestartLevelButtonClick()
    {
        _lostPanel.gameObject.SetActive(false);
        SceneManager.LoadScene(_currentScene.name);
    }
}
