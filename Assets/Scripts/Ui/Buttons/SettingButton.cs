using UnityEngine;

namespace UI
{
    public class SettingButton : GameButton
    {
        [SerializeField] private GamePanel _settingPanel;

        protected override void OnButtonClick()
        {
            base.OnButtonClick();
            _settingPanel.gameObject.SetActive(true);
        }
    }
}