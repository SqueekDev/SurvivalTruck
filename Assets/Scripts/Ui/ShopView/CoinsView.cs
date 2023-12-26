using System.Collections;
using UnityEngine;
using TMPro;

public class CoinsView : MonoBehaviour
{
    private const float TimeBetweenShowingCount = 0.05f;
    private const int NumberToConvert = 999;
    private const int ThousandDivider = 1000;
    private const int TenDivider = 10;
    private const string Dot = ".";

    [SerializeField] private CoinCounter _counter;
    [SerializeField] private TextMeshProUGUI _text;

    private Coroutine _showing;
    private int _currentCount;
    private WaitForSeconds _showingDelay = new WaitForSeconds(TimeBetweenShowingCount);

    private void OnEnable()
    {
        _counter.CoinsAmountIncrease += OnCoinsAmountIncrease;
        _counter.CoinsAmountDecrease += OnCoinsAmountDecrease;

    }

    private void OnDisable()
    {
        _counter.CoinsAmountIncrease -= OnCoinsAmountIncrease;
        _counter.CoinsAmountDecrease -= OnCoinsAmountDecrease;
    }

    private void Start()
    {
        _currentCount = PlayerPrefs.GetInt(PlayerPrefsKeys.CurrentCoinsCount, GlobalValues.Zero);
        ShowCount(_currentCount);
    }

    private void StartShowingIncrease(int coinsCount)
    {
        _showing = StartCoroutine(ShowingIncreseCount(coinsCount));
    }
    
    private void StartShowingDecrease(int coinsCount)
    {
        _showing = StartCoroutine(ShowingDecreseCount(coinsCount));
    }

    private void ShowCount(int coinsCount)
    {
        if (coinsCount > NumberToConvert)
        {
            int count = coinsCount / ThousandDivider;
            int remainder = (coinsCount - (ThousandDivider * count));

            if (remainder == GlobalValues.Zero)
            {
                _text.text = count.ToString();
            }
            else
            {
                if (remainder<TenDivider)
                {
                    _text.text = count.ToString() + Dot + "0" + remainder;
                }
                else
                {
                    _text.text = count.ToString() + Dot + remainder;
                }
            }
        }
        else
        {
            _text.text = coinsCount.ToString();
        }
    }

    private IEnumerator ShowingIncreseCount(int coinsCount)
    {
        for (int i = 0; i < coinsCount; i++)
        {
            _currentCount++;
            ShowCount(_currentCount);
            yield return _showingDelay;
        }
    }
    
    private IEnumerator ShowingDecreseCount(int coinsCount)
    {
        for (int i = 0; i < coinsCount; i++)
        {
            _currentCount--;
            ShowCount(_currentCount);
            yield return _showingDelay;
        }
    }

    private void OnCoinsAmountIncrease(int coinsCount)
    {
        StartShowingIncrease(coinsCount);
    }

    private void OnCoinsAmountDecrease(int coinsCount)
    {
        StartShowingDecrease(coinsCount);
    }
}
