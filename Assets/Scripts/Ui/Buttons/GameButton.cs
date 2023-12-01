using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

public class GameButton : MonoBehaviour
{
    [SerializeField] private Button _button;

    public event UnityAction Clicked;

    private void OnEnable()
    {
        _button.onClick.AddListener(OnButtonClick);
    }

    private void OnDisable()
    {
        _button.onClick.RemoveListener(OnButtonClick);
    }

    protected virtual void OnButtonClick()
    {
        Clicked?.Invoke();
    }
}
