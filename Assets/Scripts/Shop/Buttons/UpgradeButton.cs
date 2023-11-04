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
    public event UnityAction<string, int> Upgraded;

    private void OnEnable()
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
            Upgraded?.Invoke(playerPrefsCurrentValue, defaultValue);
            ValuesChanged?.Invoke(_upgradeValue, PlayerPrefs.GetInt(playerPrefsCurrentValue), _price);
        }
    }

    private void UpgradeabilityCheck(string playerPrefsCurrentValue, int defaultValue)
    {
        int currentValue = PlayerPrefs.GetInt(playerPrefsCurrentValue, defaultValue);
        Debug.Log($"coins = {_coins.Count} price = {_price} coins>price {_coins.Count >= _price} currentValue = {currentValue} maxValue = {_maxValue} currentValue<maxValue {currentValue < _maxValue}");

        if (_coins.Count >= _price && currentValue < _maxValue)
        {
            _button.interactable = true;
            Debug.Log("True");
        }
        else
        {
            _button.interactable = false;
            Debug.Log("False");
        }
    }

    private void OnPurchaseSuccsessed(string playerPrefsCurrentValue, int defaultValue)
    {
        UpgradeabilityCheck(playerPrefsCurrentValue, defaultValue);
        Debug.Log($"{gameObject} PurchaseSuscessed!");
    }
}
