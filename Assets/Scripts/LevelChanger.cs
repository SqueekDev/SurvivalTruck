using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class LevelChanger : MonoBehaviour
{
    private const string LevelNumberKey = "Level";
    private const int BossLevelNubmerDivider = 10;

    [SerializeField] private WaveController _waveController;
    [SerializeField] private Health _boss;
    [SerializeField] private ChangeLevelArea _changeLevelArea;
    [SerializeField] private NextLevelButton _nextLevelButton;

    private int _playerPrefsSavedLevelNumber = 1;
    private int _currentLevelNumber = 1;

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
        _boss.Died += OnBossDied;
        _nextLevelButton.Clicked += OnNextLevelButtonClick;
    }

    private void OnDisable()
    {
        _waveController.WaveEnded -= OnWaveEnded;        
        _boss.Died -= OnBossDied;
        _nextLevelButton.Clicked -= OnNextLevelButtonClick;
    }

    private void Start()
    {
        SyncLevelNumber();
        ChangeLevel();
    }

    private void SyncLevelNumber()
    {
        if (_playerPrefsSavedLevelNumber > _currentLevelNumber)
            _currentLevelNumber = _playerPrefsSavedLevelNumber;
        else
        {
            _playerPrefsSavedLevelNumber = _currentLevelNumber;
            PlayerPrefs.SetInt(LevelNumberKey, _playerPrefsSavedLevelNumber);
            PlayerPrefs.Save();
        }
    }

    private void ChangeLevel()
    {
        if (_currentLevelNumber % BossLevelNubmerDivider == 0)
            BossLevelStarted?.Invoke();

        Changed?.Invoke(_currentLevelNumber);
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

    private void OnNextLevelButtonClick()
    {
        _currentLevelNumber++;
        _changeLevelArea.gameObject.SetActive(false);

        if (_currentLevelNumber != _playerPrefsSavedLevelNumber)
            SyncLevelNumber();

        ChangeLevel();
    }    
}
