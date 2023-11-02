using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using TMPro;
using UnityEngine;

public class UpgradeButton : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI _descriptionText;
    [SerializeField] private int _upgradeValue;
    [SerializeField] private int _price;
    [SerializeField] private int _startValue;
    [SerializeField] private string _savingPrice;
    [SerializeField] private CoinCounter _coins;
    [SerializeField] private TextMeshProUGUI _coinsPriceText;
    [SerializeField] private Savings _savings;

    private string _startDescription;
    private string _startPriceText;


    private void Start()
    {
        _startDescription = _descriptionText.text;
        _startPriceText = _coinsPriceText.text;
        Renew();
    }

    public void Renew()
    {
        int currentValue;

        if (PlayerPrefs.HasKey(_savings.ToString()))
        {
            currentValue = PlayerPrefs.GetInt(_savings.ToString());
        }
        else
        {
            currentValue = _startValue;
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
                _coinsPriceText.text = number.ToString();
                PlayerPrefs.SetInt(_savingPrice, number);
            }
        }

        _descriptionText.text = "+" + _upgradeValue.ToString() + " " + _startDescription + currentValue.ToString();
        _coinsPriceText.text= _startPriceText + _price.ToString();
    }

    public void BuyUpgrade()
    {
        if (_coins.Count >= _price)
        {
            if (PlayerPrefs.HasKey(_savings.ToString()))
            {
                PlayerPrefs.SetInt(_savings.ToString(), PlayerPrefs.GetInt(_savings.ToString()) + _upgradeValue);
            }
            else
            {
                PlayerPrefs.SetInt(_savings.ToString(), _startValue + _upgradeValue);
            }

            _coins.RemoveCoins(_price);
            _descriptionText.text = "+" + _upgradeValue.ToString() + " " + _startDescription + PlayerPrefs.GetInt(_savings.ToString());
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
