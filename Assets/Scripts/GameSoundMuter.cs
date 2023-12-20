using UnityEngine;

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

    protected override void OnInBackgroundChange(bool inBackground)
    {
        if (inBackground)
        {
            AudioListener.pause = inBackground;
        }
        else if (_adShowing == false)
        {
            AudioListener.pause = inBackground;
        }
    }

    private void OnAdShowed(bool showing)
    {
        _adShowing = showing;
        AudioListener.pause = showing;
    }
}
