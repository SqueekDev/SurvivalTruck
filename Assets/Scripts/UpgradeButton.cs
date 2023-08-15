using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class UpgradeButton : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI _descriptionText;
    [SerializeField] private int _upgradeValue;
    [SerializeField] private int _price;
    [SerializeField] private int _startValue;
    [SerializeField] private string _savingName;
    [SerializeField] private string _savingPrice;
    [SerializeField] private CoinCounter _counter;
    [SerializeField] private TextMeshProUGUI _coinsPriceText;

    private string _startDescription;
    private string _startPriceText;

    private void Start()
    {
        _startDescription = _descriptionText.text;
        _startPriceText = _coinsPriceText.text;
    }

    public void Renew()
    {
        int savingName;
        if (PlayerPrefs.HasKey(_savingName))
        {
            savingName = PlayerPrefs.GetInt(_savingName);
        }
        else
        {
            savingName = _startValue;
        }
        if (PlayerPrefs.HasKey(_savingPrice))
        {
            _price = PlayerPrefs.GetInt(_savingPrice);
        }
        else
        {
            int number;
            if (int.TryParse(_coinsPriceText.text,out number))
            {
                number += _price;
                _coinsPriceText.text =number.ToString();
                PlayerPrefs.SetInt(_savingPrice, number);
            }
        }



        _descriptionText.text = "+" + _upgradeValue.ToString() + " " + _startDescription + savingName.ToString();
    }

    public void BuyUpgrade()
    {
        if (_counter.Count >= _price)
        {
            if (PlayerPrefs.HasKey(_savingName))
            {
                PlayerPrefs.SetInt(_savingName, PlayerPrefs.GetInt(_savingName) + _upgradeValue);
            }
            else
            {
                PlayerPrefs.SetInt(_savingName, _startValue + _upgradeValue);
            }

            _counter.RemoveCoins(_price);
            _descriptionText.text = "+" + _upgradeValue.ToString() + " " + _startDescription + PlayerPrefs.GetInt(_savingName).ToString();
            _price += (int)((float)_price * 0.1f);
            int number;
            if (int.TryParse(_startPriceText,out number))
            {
                _coinsPriceText.text =(number +_price).ToString();
            }
            PlayerPrefs.SetInt(_savingPrice, _price);
        }
    }
}
