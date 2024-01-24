using System;
using Base;
using Enemy;
using UI;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace Level
{
    public class LevelChanger : MonoBehaviour
    {
        [SerializeField] private Boss _boss;
        [SerializeField] private GameButton _nextLevelButton;
        [SerializeField] private GameButton _restartLevelButton;
        [SerializeField] private GameButton _settingsRestartLevelButton;
        [SerializeField] private GameButton _finishLevelButton;
        [SerializeField] private int _bossLevelNumber;

        private int _playerPrefsSavedLevelNumber = 1;
        private bool _isInWaveState = false;
        private Scene _currentScene;

        public event Action<int> Changed;

        public event Action Finished;

        public event Action NormalLevelStarted;

        public event Action BossLevelStarted;

        public event Action BossLevelEnded;

        public int BossLevelNumber => _bossLevelNumber;

        public bool IsInWaveState => _isInWaveState;

        public int CurrentLevelNumber { get; private set; } = 1;

        private void Awake()
        {
            _currentScene = SceneManager.GetActiveScene();
            _playerPrefsSavedLevelNumber = PlayerPrefs.GetInt(PlayerPrefsKeys.LevelNumber, CurrentLevelNumber);
            SyncLevelNumber();
        }

        private void OnEnable()
        {
            _boss.Died += OnBossDied;
            _nextLevelButton.Clicked += OnNextLevelButtonClick;
            _restartLevelButton.Clicked += OnRestartLevelButtonClick;
            _settingsRestartLevelButton.Clicked += OnRestartLevelButtonClick;
            _finishLevelButton.Clicked += OnFinishLevelButtonClick;
        }

        private void OnDisable()
        {
            _boss.Died -= OnBossDied;
            _nextLevelButton.Clicked -= OnNextLevelButtonClick;
            _restartLevelButton.Clicked -= OnRestartLevelButtonClick;
            _settingsRestartLevelButton.Clicked -= OnRestartLevelButtonClick;
            _finishLevelButton.Clicked -= OnFinishLevelButtonClick;
        }

        private void SyncLevelNumber()
        {
            if (_playerPrefsSavedLevelNumber > CurrentLevelNumber)
            {
                CurrentLevelNumber = _playerPrefsSavedLevelNumber;
            }
            else
            {
                _playerPrefsSavedLevelNumber = CurrentLevelNumber;
                PlayerPrefs.SetInt(PlayerPrefsKeys.LevelNumber, _playerPrefsSavedLevelNumber);
                PlayerPrefs.Save();
            }
        }

        private void ChangeLevel()
        {
            if (CurrentLevelNumber % _bossLevelNumber == GlobalValues.Zero)
            {
                BossLevelStarted?.Invoke();
            }
            else
            {
                NormalLevelStarted?.Invoke();
            }

            _isInWaveState = true;
            Changed?.Invoke(CurrentLevelNumber);
        }

        private void OnBossDied(Health boss)
        {
            BossLevelEnded?.Invoke();
        }

        private void OnNextLevelButtonClick()
        {
            ChangeLevel();
        }

        private void OnRestartLevelButtonClick()
        {
            SceneManager.LoadScene(_currentScene.name);
        }

        private void OnFinishLevelButtonClick()
        {
            _isInWaveState = false;
            CurrentLevelNumber++;

            if (CurrentLevelNumber != _playerPrefsSavedLevelNumber)
            {
                SyncLevelNumber();
            }

            Finished?.Invoke();
        }
    }
}