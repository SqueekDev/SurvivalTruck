using UnityEngine;

public class RestartButton : GameButton
{
    [SerializeField] private SettingsPanel _settingsPanel;

    protected override void OnButtonClick()
    {
        base.OnButtonClick();
        _settingsPanel.gameObject.SetActive(false);
    }
}
