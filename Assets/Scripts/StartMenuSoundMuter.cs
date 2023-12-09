using UnityEngine;
using Agava.WebUtility;

public class StartMenuSoundMuter : MonoBehaviour
{
    protected const int SoundOff = 0;
    protected const int SoundOn = 1;

    [SerializeField] private SoundButton _soundButton;

    protected virtual void OnEnable()
    {
        _soundButton.Muted += OnMuted;
        WebApplication.InBackgroundChangeEvent += OnInBackgroundChange;
        bool isMuted = PlayerPrefs.GetInt(PlayerPrefsKeys.Sound, SoundOff) == SoundOn;
        OnMuted(isMuted);
    }

    protected virtual void OnDisable()
    {
        _soundButton.Muted -= OnMuted;        
        WebApplication.InBackgroundChangeEvent -= OnInBackgroundChange;
    }

    protected virtual void OnInBackgroundChange(bool inBackground)
    {
        if (inBackground)
            AudioListener.pause = inBackground;
        else
            AudioListener.pause = inBackground;
    }

    private void OnMuted(bool isMuted)
    {
        AudioListener.volume = isMuted == true ? SoundOff : SoundOn;
    }
}
