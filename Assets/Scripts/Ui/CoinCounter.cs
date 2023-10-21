using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class CoinCounter : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI _text;
    [SerializeField] private List<RageArea> _rageAreas;
    [SerializeField] private Boss _boss;

    private int _count;
    private int _totalEarnedCoins;
    private int _currentZombieReward;
    private int _earnModifier;

    public int Count => _count;
    public int TotalEarnedCoins => _totalEarnedCoins;

    private void OnEnable()
    {
        _boss.Died += OnBossDied;

        foreach (var rageArea in _rageAreas)
            rageArea.ZombieAttacked += OnZombieAttacked;
    }

    private void Start()
    {
        _totalEarnedCoins = PlayerPrefs.GetInt(PlayerPrefsKeys.TotalEarnedCoinsName, 0);
        int currentCoins = PlayerPrefs.GetInt(PlayerPrefsKeys.CurrentCoinsCountName, 0);
        AddCoins(currentCoins);
    }

    private void OnDisable()
    {
        _boss.Died -= OnBossDied;

        foreach (var rageArea in _rageAreas)
            rageArea.ZombieAttacked -= OnZombieAttacked;
    }

    public void AddCoinslsForAd(int count)
    {
        AddCoins(count);
    }

    public void RemoveCoins(int count)
    {
        _count -= count;
        PlayerPrefs.SetInt(PlayerPrefsKeys.CurrentCoinsCountName, _count);
        ShowCount();
    }

    private void AddCoins(int count)
    {
        _count += count;
        PlayerPrefs.SetInt(PlayerPrefsKeys.CurrentCoinsCountName, _count);
        _totalEarnedCoins += count;
        PlayerPrefs.SetInt(PlayerPrefsKeys.TotalEarnedCoinsName, _totalEarnedCoins);
        ShowCount();
    }

    private void OnZombieAttacked(ZombieHealth zombie)
    {
        zombie.Died += OnZombieDied;
        _currentZombieReward = zombie.Reward;
    }

    private void OnZombieDied(Health zombie)
    {
        zombie.Died -= OnZombieDied;
        AddCoins(_currentZombieReward + _earnModifier);
    }

    private void OnBossDied(Health boss)
    {
        AddCoins(_boss.Reward + _earnModifier);
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
