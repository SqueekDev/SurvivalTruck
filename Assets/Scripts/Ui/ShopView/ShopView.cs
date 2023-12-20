using System;
using UnityEngine;

public class ShopView : MonoBehaviour
{
    [SerializeField] private GameObject[] _buttonGroups;
    [SerializeField] private UpgradeButton[] _buttons;
    [SerializeField] private GameButton _nextButton;
    [SerializeField] private GameButton _previousButton;

    private int _currentGroupIndex;

    public event Action PurchaseSuccsessed;
    public event Action Closed;

    private void OnEnable()
    {
        foreach (var button in _buttons)
        {
            button.SkillUpgraded += OnSkillUpgraded;
        }

        _nextButton.Clicked += OnNextButtonClicked;
        _previousButton.Clicked += OnPreviousButtonClicked;
        ShowFirstButtons();
    }

    private void OnDisable()
    {
        foreach (var button in _buttons)
        {
            button.SkillUpgraded -= OnSkillUpgraded;
        }

        _nextButton.Clicked -= OnNextButtonClicked;
        _previousButton.Clicked -= OnPreviousButtonClicked;

        foreach (var buttonGroup in _buttonGroups)
        {
            buttonGroup.gameObject.SetActive(false);
        }

        Closed?.Invoke();
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

    private void OnNextButtonClicked()
    {
        if (_currentGroupIndex == _buttonGroups.Length - GlobalValues.ListIndexCorrection)
        {
            _buttonGroups[_currentGroupIndex].gameObject.SetActive(false);
            ShowFirstButtons();
            _currentGroupIndex = GlobalValues.Zero;
        }
        else 
        {
            _buttonGroups[_currentGroupIndex].gameObject.SetActive(false);
            _currentGroupIndex++;
            _buttonGroups[_currentGroupIndex].gameObject.SetActive(true);
        }
    }

    private void OnPreviousButtonClicked()
    {
        if (_currentGroupIndex == GlobalValues.Zero)
        {
            _buttonGroups[_currentGroupIndex].gameObject.SetActive(false);
            ShowLastButtons();
            _currentGroupIndex = _buttonGroups.Length - GlobalValues.ListIndexCorrection;
        }
        else
        {
            _buttonGroups[_currentGroupIndex].gameObject.SetActive(false);
            _currentGroupIndex--;
            _buttonGroups[_currentGroupIndex].gameObject.SetActive(true);
        }
    }

    private void OnSkillUpgraded()
    {
        PurchaseSuccsessed?.Invoke();
    }
}
