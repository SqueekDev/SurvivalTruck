using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SoundButton : GameButton
{
    private const int FalseValue = 0;
    private const int TrueValue = 1;

    [SerializeField] private Sprite _soundOnIcon;
    [SerializeField] private Sprite _soundOffIcon;

    private bool _isMuted;

    private void Awake()
    {
        Button.image.sprite = PlayerPrefs.GetInt(PlayerPrefsKeys.Sound, FalseValue) == TrueValue ? _soundOnIcon : _soundOnIcon;
        _isMuted = PlayerPrefs.GetInt(PlayerPrefsKeys.Sound, FalseValue) == TrueValue;
        Mute(_isMuted);
    }

    protected override void OnButtonClick()
    {
        base.OnButtonClick();

        if (_isMuted == false)
        {
            Button.image.sprite = _soundOffIcon;
            _isMuted = true;
        }
        else
        {
            Button.image.sprite = _soundOnIcon;
            _isMuted = false;
        }

        Mute(_isMuted);
        SaveData(_isMuted);
    }

    private void SaveData(bool isMuted)
    {
        int value = isMuted ? TrueValue : FalseValue;
        PlayerPrefs.SetInt(PlayerPrefsKeys.Sound, value);
        PlayerPrefs.Save();
    }

    private void Mute(bool isMuted)
    {
        if (isMuted)
            AudioListener.volume = 0;
        else
            AudioListener.volume = 1;
    }
}
