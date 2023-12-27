using UnityEngine;
using Agava.WebUtility;

public class StartMenuSoundMuter : MonoBehaviour
{
    protected const int DisabledValue = 0;
    protected const int EnabledValue = 1;

    [SerializeField] private SoundButton _soundButton;

    protected virtual void OnEnable()
    {
        _soundButton.Muted += OnMuted;
        Application.focusChanged += OnInBackgroundChangeApp;
        WebApplication.InBackgroundChangeEvent += OnInBackgroundChangeWeb;
        bool isMuted = PlayerPrefs.GetInt(PlayerPrefsKeys.Sound, DisabledValue) == EnabledValue;
        OnMuted(isMuted);
    }

    protected virtual void OnDisable()
    {
        _soundButton.Muted -= OnMuted;        
        Application.focusChanged -= OnInBackgroundChangeApp;
        WebApplication.InBackgroundChangeEvent -= OnInBackgroundChangeWeb;
    }

    protected void PauseGame(bool isPaused)
    {
        Time.timeScale = isPaused ? DisabledValue : EnabledValue;
    }
    
    protected void PauseSound(bool isPaused)
    {
        AudioListener.pause = isPaused;
    }

    protected virtual void OnInBackgroundChangeApp(bool inApp)
    {
        PauseGame(!inApp);
        PauseSound(!inApp);
    }

    protected virtual void OnInBackgroundChangeWeb(bool inBackground)
    {
        PauseGame(inBackground);
        PauseSound(inBackground);
    }

    private void OnMuted(bool isMuted)
    {
        AudioListener.volume = isMuted ? DisabledValue : EnabledValue;
    }
}
