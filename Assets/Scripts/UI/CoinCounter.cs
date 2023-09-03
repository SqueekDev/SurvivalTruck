using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class CoinCounter : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI _text;

    private int _count;
    private int _earnedCoins;

    public int Count => _count;
    public int EarnedCoins => _earnedCoins;

    private void Start()
    {
        if (PlayerPrefs.HasKey("CoinsCount"))
        {
            AddCoins(PlayerPrefs.GetInt("CoinsCount"));
        }
    }

    public void AddCoins(int count)
    {
        _count += count;
        ShowCount();
    }

    public void AddCoinslsForAd(int count)
    {
        AddCoins(count);
        PlayerPrefs.SetInt("CoinsCount", _count);
    }

    public void RemoveCoins(int count)
    {
        _count -= count;
        PlayerPrefs.SetInt("CoinsCount", _count);
        ShowCount();
    }

    private void ShowCount()
    {
        if (_count > 999)
        {
            int count = _count / 1000;
            int reminder = (_count - (1000 * count)) / 10;
            if (reminder == 0)
            {
                _text.text = count.ToString() + "K";
            }
            else
            {
                _text.text = count.ToString() + "." + reminder + "K";
            }
        }
        else
        {
            _text.text = _count.ToString();
        }

    }
}
