using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SettingButton : GameButton
{
    [SerializeField] private GamePanel _settingPanel;

    protected override void OnButtonClick()
    {
        base.OnButtonClick();
        _settingPanel.gameObject.SetActive(true);
    }
}
