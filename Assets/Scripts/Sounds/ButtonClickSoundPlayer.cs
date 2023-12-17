using System.Collections.Generic;
using UnityEngine;

public class ButtonClickSoundPlayer : MonoBehaviour
{
    [SerializeField] private AudioSource _audioSource;
    [SerializeField] private AudioClip _clickSound;
    [SerializeField] private List<GameButton> _buttons;

    private void OnEnable()
    {
        foreach (var button in _buttons)
        {
            button.Clicked += OnButtonClick;
        }
    }

    private void OnDisable()
    {
        foreach (var button in _buttons)
        {
            button.Clicked -= OnButtonClick;
        }
    }

    protected void OnButtonClick()
    {
        _audioSource.PlayOneShot(_clickSound);
    }
}
