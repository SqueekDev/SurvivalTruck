using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CloseButton : GameButton
{
    [SerializeField] private GamePanel _loginPanel;

    protected override void OnButtonClick()
    {
        base.OnButtonClick();
        _loginPanel.gameObject.SetActive(false);
    }
}
