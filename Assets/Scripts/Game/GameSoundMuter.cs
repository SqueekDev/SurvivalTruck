using StartMenu;
using UnityEngine;
using YandexSDK;

namespace Game
{
    public class GameSoundMuter : StartMenuSoundMuter
    {
        [SerializeField] private AdShower _adShower;

        private bool _adShowing = false;

        protected override void OnEnable()
        {
            base.OnEnable();
            _adShower.AdShowing += OnAdShowed;
        }

        protected override void OnDisable()
        {
            base.OnDisable();
            _adShower.AdShowing -= OnAdShowed;
        }

        protected override void OnInBackgroundChangeApp(bool inApp)
        {
            if (inApp == false)
            {
                PauseGame(!inApp);
                PauseSound(!inApp);
            }
            else if (_adShowing == false)
            {
                PauseGame(!inApp);
                PauseSound(!inApp);
            }
        }

        protected override void OnInBackgroundChangeWeb(bool inBackground)
        {
            if (inBackground)
            {
                PauseGame(inBackground);
                PauseSound(inBackground);
            }
            else if (_adShowing == false)
            {
                PauseGame(inBackground);
                PauseSound(inBackground);
            }
        }

        private void OnAdShowed(bool showing)
        {
            _adShowing = showing;
            AudioListener.pause = showing;
        }
    }
}