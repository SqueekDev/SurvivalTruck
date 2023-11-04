using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

public class UpgradeButton : MonoBehaviour
{
    private const float PriceIncreace = 0.1f;

    [SerializeField] private int _upgradeValue;
    [SerializeField] private int _price;
    [SerializeField] private int _maxValue;
    [SerializeField] private CoinCounter _coins;
    [SerializeField] private Button _button;
    [SerializeField] private GameButton _gameButton;
    [SerializeField] private UpgradesPanel _upgradesPanel;

    protected CoinCounter CoinCounter => _coins;

    public int MaxValue => _maxValue;

    public event UnityAction<int, int, int> ValuesChanged;
    public event UnityAction SkillUpgraded;

    protected virtual void OnEnable()
    {
        _gameButton.Clicked += OnUpgradeButtonClick;
        _upgradesPanel.PurchaseSuccsessed += OnPurchaseSuccsessed;
    }

    private void OnDisable()
    {
        _gameButton.Clicked -= OnUpgradeButtonClick;
        _upgradesPanel.PurchaseSuccsessed -= OnPurchaseSuccsessed;
    }

    protected virtual void OnUpgradeButtonClick() {}

    protected void Renew(string playerPrefsCurrentValue, int defaultValue, string playerPrefsPrice)
    {
        int currentValue = PlayerPrefs.GetInt(playerPrefsCurrentValue, defaultValue);
        _price = PlayerPrefs.GetInt(playerPrefsPrice, _price);
        UpgradeabilityCheck(playerPrefsCurrentValue, defaultValue);
        ValuesChanged?.Invoke(_upgradeValue, currentValue, _price);
    }

    protected void BuyUpgrade(string playerPrefsCurrentValue, int defaultValue, string playerPrefsPrice)
    {
        if (_coins.Count >= _price)
        {
            if (PlayerPrefs.HasKey(playerPrefsCurrentValue))
                PlayerPrefs.SetInt(playerPrefsCurrentValue, PlayerPrefs.GetInt(playerPrefsCurrentValue) + _upgradeValue);
            else
                PlayerPrefs.SetInt(playerPrefsCurrentValue, defaultValue + _upgradeValue);

            _coins.RemoveCoins(_price);
            _price += (int)((float)_price * PriceIncreace);
            PlayerPrefs.SetInt(playerPrefsPrice, _price);
            SkillUpgraded?.Invoke();
            ValuesChanged?.Invoke(_upgradeValue, PlayerPrefs.GetInt(playerPrefsCurrentValue), _price);
        }
    }

    protected void UpgradeabilityCheck(string playerPrefsCurrentValue, int defaultValue)
    {
        int currentValue = PlayerPrefs.GetInt(playerPrefsCurrentValue, defaultValue);

        if (_coins.Count >= _price && currentValue < _maxValue)
            _button.interactable = true;
        else
            _button.interactable = false;
    }

    protected virtual void OnPurchaseSuccsessed() {}
}
