using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

public class ShopView : MonoBehaviour
{
    [SerializeField] private GameObject[] _buttonGroups;
    [SerializeField] private UpgradeButton[] _buttons;
    [SerializeField] private GameButton _nextButton;
    [SerializeField] private GameButton _previousButton;

    private int _currentGroupIndex;

    public event UnityAction PurchaseSuccsessed;
    public event UnityAction Closed;

    private void OnEnable()
    {
        foreach (var button in _buttons)
            button.SkillUpgraded += OnSkillUpgraded;

        _nextButton.Clicked += ShowNextButtons;
        _previousButton.Clicked += ShowPreviousButtons;

        ShowFirstButtons();
    }

    private void OnDisable()
    {
        foreach (var button in _buttons)
            button.SkillUpgraded -= OnSkillUpgraded;

        _nextButton.Clicked -= ShowNextButtons;
        _previousButton.Clicked -= ShowPreviousButtons;

        foreach (var buttonGroup in _buttonGroups)
            buttonGroup.gameObject.SetActive(false);

        Closed?.Invoke();
    }

    private void ShowNextButtons()
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

    private void ShowPreviousButtons()
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

    private void OnSkillUpgraded()
    {
        PurchaseSuccsessed?.Invoke();
    }
}
