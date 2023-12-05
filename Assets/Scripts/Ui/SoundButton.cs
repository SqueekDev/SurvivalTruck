using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundButton : MonoBehaviour
{
    [SerializeField] private GameObject _soundOnButton;
    [SerializeField] private GameObject _soundOffButton;
    public void SoundOn()
    {
        AudioListener.volume = 1.0f;
        _soundOffButton.SetActive(false);
        _soundOnButton.SetActive(true);
    }
    
    public void SoundOff()
    {
        AudioListener.volume = 0f;
        _soundOnButton.SetActive(false);
        _soundOffButton.SetActive(true);
    }
}
