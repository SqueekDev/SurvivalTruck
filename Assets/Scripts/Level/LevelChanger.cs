using System;
using System.Collections;
using Base;
using Enemy;
using Player;
using Truck;
using UI;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace Level
{
    public class LevelChanger : MonoBehaviour
    {
        private const float FinishLevelDelayTime = 1.5f;

        [SerializeField] private Wave _waveController;
        [SerializeField] private PlayerHealth _player;
        [SerializeField] private Boss _boss;
        [SerializeField] private ChangeLevelArea _changeLevelArea;
        [SerializeField] private GamePanel _lostPanel;
        [SerializeField] private GamePanel _finishLevelPanel;
        [SerializeField] private GameButton _nextLevelButton;
        [SerializeField] private GameButton _restartLevelButton;
        [SerializeField] private GameButton _settingsRestartLevelButton;
        [SerializeField] private GameButton _finishLevelButton;
        [SerializeField] private int _bossLevelNumber;

        private int _playerPrefsSavedLevelNumber = 1;
        private bool _isWave = false;
        private Scene _currentScene;
        private Coroutine _finishCorutine;
        private WaitForSeconds _finishLevelDelay = new WaitForSeconds(FinishLevelDelayTime);

        public event Action<int> Changed;
        public event Action Finished;
        public event Action BossLevelStarted;
        public event Action BossLevelEnded;

        public int BossLevelNumber => _bossLevelNumber;
        public bool IsWave => _isWave;
        public int CurrentLevelNumber { get; private set; } = 1;

        private void Awake()
        {
            _currentScene = SceneManager.GetActiveScene();
            _playerPrefsSavedLevelNumber = PlayerPrefs.GetInt(PlayerPrefsKeys.LevelNumber, CurrentLevelNumber);
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
            _finishLevelButton.Clicked += OnFinishLevelButtonClick;
        }

        private void OnDisable()
        {
            _waveController.WaveEnded -= OnWaveEnded;
            _player.Died -= OnPlayerDied;
            _boss.Died -= OnBossDied;
            _nextLevelButton.Clicked -= OnNextLevelButtonClick;
            _restartLevelButton.Clicked -= OnRestartLevelButtonClick;
            _settingsRestartLevelButton.Clicked -= OnRestartLevelButtonClick;
            _finishLevelButton.Clicked -= OnFinishLevelButtonClick;
        }

        private void Start()
        {
            _changeLevelArea.gameObject.SetActive(true);
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

            _isWave = true;
            Changed?.Invoke(CurrentLevelNumber);
        }

        private IEnumerator FinishLevelCorutine()
        {
            yield return _finishLevelDelay;
            _finishLevelPanel.gameObject.SetActive(true);
        }

        private void OnWaveEnded()
        {
            if (_finishCorutine != null)
            {
                StopCoroutine(_finishCorutine);
            }

            _finishCorutine = StartCoroutine(FinishLevelCorutine());
        }

        private void OnBossDied(Health boss)
        {
            BossLevelEnded?.Invoke();
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

        private void OnFinishLevelButtonClick()
        {
            _isWave = false;
            _changeLevelArea.gameObject.SetActive(true);
            CurrentLevelNumber++;

            if (CurrentLevelNumber != _playerPrefsSavedLevelNumber)
            {
                SyncLevelNumber();
            }

            Finished?.Invoke();
        }
    }
}