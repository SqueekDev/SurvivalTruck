using System;
using Agava.YandexGames;
using Level;
using UI;
using UnityEngine;

namespace YandexSDK
{
    public class AdShower : MonoBehaviour
    {
        private const int NumberToShowAd = 2;

        [SerializeField] private LevelChanger _levelChanger;
        [SerializeField] private GameButton _videoButton;
        [SerializeField] private GamePanel _finishLevelPanel;
        [SerializeField] private GamePanel _addCoinsPanel;

        private int _counter;

        public event Action<bool> AdShowing;

        public event Action VideoAdShowed;

        private void OnEnable()
        {
            _levelChanger.Finished += OnLevelFinished;
            _videoButton.Clicked += OnVideoButtonClick;
        }

        private void OnDisable()
        {
            _levelChanger.Finished -= OnLevelFinished;
            _videoButton.Clicked -= OnVideoButtonClick;
        }

        private void OnLevelFinished()
        {
            if (_counter < NumberToShowAd)
            {
                _counter++;
                _finishLevelPanel.gameObject.SetActive(false);
            }
            else
            {
#if UNITY_WEBGL && !UNITY_EDITOR
            InterstitialAd.Show(OnOpenCallBack, OnCloseCallBack, OnErrorCallBack);
#endif
                _counter = 0;
            }
        }

        private void OnVideoButtonClick()
        {
#if UNITY_WEBGL && !UNITY_EDITOR
        VideoAd.Show(OnOpenCallBack, OnRewardCallBack, OnCloseCallback);
#endif
        }

        private void OnOpenCallBack()
        {
            Time.timeScale = 0;
            AdShowing?.Invoke(true);
        }

        private void OnRewardCallBack()
        {
            VideoAdShowed?.Invoke();
        }

        private void OnCloseCallback()
        {
            Time.timeScale = 1;
            AdShowing?.Invoke(false);
            _addCoinsPanel.gameObject.SetActive(false);
        }

        private void OnCloseCallBack(bool state)
        {
            Time.timeScale = 1;
            AdShowing?.Invoke(false);
            _finishLevelPanel.gameObject.SetActive(false);
        }

        private void OnErrorCallBack(string error)
        {
            _finishLevelPanel.gameObject.SetActive(false);
        }
    }
}