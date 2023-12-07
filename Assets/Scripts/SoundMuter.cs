using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundMuter : MonoBehaviour
{
    private const int MutedValue = 0;
    private const int UnmutedValue = 1;

    [SerializeField] private SoundButton _soundButton;

    private void OnEnable()
    {
        _soundButton.Muted += OnMuted;
        bool isMuted = PlayerPrefs.GetInt(PlayerPrefsKeys.Sound, MutedValue) == UnmutedValue;
        OnMuted(isMuted);
    }

    private void OnDisable()
    {
        _soundButton.Muted -= OnMuted;        
    }

    private void OnMuted(bool isMuted)
    {
        AudioListener.volume = isMuted == true ? MutedValue : UnmutedValue;
    }
}
