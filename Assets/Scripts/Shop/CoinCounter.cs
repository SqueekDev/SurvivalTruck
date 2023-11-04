using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.Events;

public class CoinCounter : MonoBehaviour
{
    private const int _bossEarnModifier = 10;

    [SerializeField] private List<RageArea> _rageAreas;
    [SerializeField] private Boss _boss;
    [SerializeField] private CoinsModifierUpgradeButton _coinsModifierUpgradeButton;

    private int _count;
    private int _totalEarnedCoins;
    private int _currentZombieReward;

    public int Count => _count;
    public int TotalEarnedCoins => _totalEarnedCoins;

    public int EarnModifier { get; private set; }

    public event UnityAction<int> CoinsAmountChanged;

    private void OnEnable()
    {
        _boss.Died += OnBossDied;
        _coinsModifierUpgradeButton.CoinsModifierUpgraded += OnCoinsModifierUpgraded;

        foreach (var rageArea in _rageAreas)
            rageArea.ZombieAttacked += OnZombieAttacked;
    }

    private void Start()
    {
        OnCoinsModifierUpgraded();
        _totalEarnedCoins = PlayerPrefs.GetInt(PlayerPrefsKeys.TotalEarnedCoins, 0);
        int currentCoins = PlayerPrefs.GetInt(PlayerPrefsKeys.CurrentCoinsCount, 0);
        AddCoins(currentCoins);
    }

    private void OnDisable()
    {
        _boss.Died -= OnBossDied;
        _coinsModifierUpgradeButton.CoinsModifierUpgraded -= OnCoinsModifierUpgraded;

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
        PlayerPrefs.SetInt(PlayerPrefsKeys.CurrentCoinsCount, _count);
        CoinsAmountChanged?.Invoke(_count);
    }

    private void AddCoins(int count)
    {
        _count += count;
        PlayerPrefs.SetInt(PlayerPrefsKeys.CurrentCoinsCount, _count);
        _totalEarnedCoins += count;
        PlayerPrefs.SetInt(PlayerPrefsKeys.TotalEarnedCoins, _totalEarnedCoins);
        CoinsAmountChanged?.Invoke(_count);
    }

    private void OnZombieAttacked(ZombieHealth zombie)
    {
        zombie.Died += OnZombieDied;
        _currentZombieReward = zombie.Reward;
    }

    private void OnZombieDied(Health zombie)
    {
        zombie.Died -= OnZombieDied;
        AddCoins(_currentZombieReward + EarnModifier);
    }

    private void OnBossDied(Health boss)
    {
        AddCoins(_boss.Reward + EarnModifier * _bossEarnModifier);
    }

    private void OnCoinsModifierUpgraded()
    {
        EarnModifier = PlayerPrefs.GetInt(PlayerPrefsKeys.CoinsModifier, 0);
    }
}
