using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class CoinsView : MonoBehaviour
{
    [SerializeField] private CoinCounter _counter;
    [SerializeField] private TextMeshProUGUI _text;
    [SerializeField] private float _timeBetweenShowingCount;
    [SerializeField] private Animation _animationClip;

    private Coroutine _showing;
    private int _currentCount;

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
        _currentCount = PlayerPrefs.GetInt(PlayerPrefsKeys.CurrentCoinsCount, 0);
        ShowCount(_currentCount);
    }

    private void OnCoinsAmountIncrease(int coinsCount)
    {
        StartShowingIncrease(coinsCount);
    }
    private void OnCoinsAmountDecrease(int coinsCount)
    {
        StartShowingDecrease(coinsCount);
    }

    private void StartShowingIncrease(int coinsCount)
    {
        _animationClip.Play();
        _showing = StartCoroutine(ShowingIncreseCount(coinsCount));
    }
    
    private void StartShowingDecrease(int coinsCount)
    {
        _animationClip.Play();
        _showing = StartCoroutine(ShowingDecreseCount(coinsCount));
    }

    private void ShowCount(int coinsCount)
    {
        if (coinsCount > 999)
        {
            int count = coinsCount / 1000;
            int reminder = (coinsCount - (1000 * count));

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

    private IEnumerator ShowingIncreseCount(int coinsCount)
    {
        for (int i = 0; i < coinsCount; i++)
        {
            _currentCount++;
            ShowCount(_currentCount);
            yield return new WaitForSeconds(_timeBetweenShowingCount);
        }
    }
    
    private IEnumerator ShowingDecreseCount(int coinsCount)
    {
        for (int i = 0; i < coinsCount; i++)
        {
            _currentCount--;
            ShowCount(_currentCount);
            yield return new WaitForSeconds(_timeBetweenShowingCount);
        }
    }
}
