using UnityEngine;

namespace UI
{
    public class CloseButton : GameButton
    {
        [SerializeField] private GamePanel _panelToClose;

        protected override void OnButtonClick()
        {
            base.OnButtonClick();
            _panelToClose.gameObject.SetActive(false);
        }
    }
}