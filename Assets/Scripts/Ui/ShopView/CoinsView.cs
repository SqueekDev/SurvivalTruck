using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class CoinsView : MonoBehaviour
{
    [SerializeField] private CoinCounter _counter;
    [SerializeField] private TextMeshProUGUI _text;

    private void OnEnable()
    {
        _counter.CoinsAmountChanged += OnCoinsAmountChanged;
    }

    private void OnDisable()
    {
        _counter.CoinsAmountChanged -= OnCoinsAmountChanged;        
    }

    private void OnCoinsAmountChanged(int coinsCount)
    {
        if (coinsCount > 999)
        {
            int count = coinsCount / 1000;
            int reminder = (coinsCount - (1000 * count)) / 10;

            if (reminder == 0)
                _text.text = count.ToString() + "K";
            else
                _text.text = count.ToString() + "." + reminder + "K";
        }
        else
        {
            _text.text = coinsCount.ToString();
        }
    }
}
