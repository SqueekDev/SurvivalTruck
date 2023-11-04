using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

public class UpgradesPanel : MonoBehaviour
{
    [SerializeField] private GameObject[] _buttonGroups;
    [SerializeField] private UpgradeButton[] _buttons;

    private int _currentGroupIndex;

    public event UnityAction<string, int> PurchaseSuccsessed;

    private void OnEnable()
    {
        foreach (var button in _buttons)
            button.Upgraded += OnUpgraded;

        Time.timeScale = 0f;
        ShowFirstButtons();
    }

    private void OnDisable()
    {
        foreach (var button in _buttons)
            button.Upgraded -= OnUpgraded;

        foreach (var buttonGroup in _buttonGroups)
            buttonGroup.gameObject.SetActive(false);

        Time.timeScale = 1f;
    }

    public void ShowNextButtons()
    {
        if (_currentGroupIndex == _buttonGroups.Length-1)
        {
            _buttonGroups[_currentGroupIndex].gameObject.SetActive(false);
            ShowFirstButtons();
            _currentGroupIndex = 0;
        }
        else 
        {
            _buttonGroups[_currentGroupIndex].gameObject.SetActive(false);
            _currentGroupIndex++;
            _buttonGroups[_currentGroupIndex].gameObject.SetActive(true);
        }
    }

    public void ShowPreviousButtons()
    {
        if (_currentGroupIndex == 0)
        {
            _buttonGroups[_currentGroupIndex].gameObject.SetActive(false);
            ShowLastButtons();
            _currentGroupIndex = _buttonGroups.Length - 1;
        }
        else
        {
            _buttonGroups[_currentGroupIndex].gameObject.SetActive(false);
            _currentGroupIndex--;
            _buttonGroups[_currentGroupIndex].gameObject.SetActive(true);
        }
    }

    private void ShowFirstButtons()
    {
        _buttonGroups[0].gameObject.SetActive(true);
    }

    private void ShowLastButtons()
    {
        _buttonGroups[_currentGroupIndex].gameObject.SetActive(false);
        _buttonGroups[_buttonGroups.Length-1].gameObject.SetActive(true);
    }

    private void OnUpgraded(string playerPrefsCurrentValue, int defaultValue)
    {
        PurchaseSuccsessed?.Invoke(playerPrefsCurrentValue, defaultValue);
    }
}
