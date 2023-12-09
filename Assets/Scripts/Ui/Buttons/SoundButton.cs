using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

public class SoundButton : GameButton
{
    private const int FalseValue = 0;
    private const int TrueValue = 1;

    [SerializeField] private Sprite _soundOnIcon;
    [SerializeField] private Sprite _soundOffIcon;

    private bool _isMuted;

    public event UnityAction<bool> Muted;

    private void Awake()
    {
        _isMuted = PlayerPrefs.GetInt(PlayerPrefsKeys.Sound, FalseValue) == TrueValue;
        Button.image.sprite = _isMuted ? _soundOffIcon : _soundOnIcon;
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

        SaveData(_isMuted);
        Muted?.Invoke(_isMuted);
    }

    private void SaveData(bool isMuted)
    {
        int value = isMuted ? TrueValue : FalseValue;
        PlayerPrefs.SetInt(PlayerPrefsKeys.Sound, value);
        PlayerPrefs.Save();
    }
}
