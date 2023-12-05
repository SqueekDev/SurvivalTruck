using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SettingsPanel : GamePanel
{
    [SerializeField] private SettingButton _settingsButton;

    private void OnEnable()
    {
        _settingsButton.gameObject.SetActive(false);
    }

    private void OnDisable()
    {
        _settingsButton.gameObject.SetActive(true);
    }
}
