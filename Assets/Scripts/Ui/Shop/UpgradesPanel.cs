using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UpgradesPanel : MonoBehaviour
{
    [SerializeField] private GameObject[] _buttonGroups;

    private int _currentGroupIndex;

    private void OnEnable()
    {
        Time.timeScale = 0f;
        ShowFirstButtons();
    }

    private void OnDisable()
    {
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
}
