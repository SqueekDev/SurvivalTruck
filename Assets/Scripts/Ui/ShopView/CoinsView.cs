using System.Collections;
using UnityEngine;
using TMPro;

public class CoinsView : MonoBehaviour
{
    private const float TimeBetweenShowingCount = 0.001f;

    [SerializeField] private CoinCounter _counter;
    [SerializeField] private TextMeshProUGUI _text;

    private Coroutine _showing;
    private int _currentCount;
    private int _trueCount;
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
        _trueCount = _currentCount;
        ShowCount(_currentCount);
    }

    private void ShowCount(int coinsCount)
    {
        _text.text = coinsCount.ToString();
    }

    private IEnumerator ShowingCount()
    {

        while (_currentCount != _trueCount)
        {
            if (_currentCount > _trueCount)
            {
                _currentCount--;
            }

            if (_currentCount < _trueCount)
            {
                _currentCount++;
            }

            ShowCount(_currentCount);
            yield return _showingDelay;
        }

        _showing = null;
    }

    private void OnCoinsAmountIncrease(int coinsCount)
    {
        _trueCount += coinsCount;

        if (_showing == null)
        {
            _showing = StartCoroutine(ShowingCount());
        }
    }

    private void OnCoinsAmountDecrease(int coinsCount)
    {
        _trueCount -= coinsCount;

        if (_showing == null)
        {
            _showing = StartCoroutine(ShowingCount());
        }
    }
}
