using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class CoinCounter : MonoBehaviour
{
    private const int BossEarnModifier = 10;
    private const int AdReward = 100;

    [SerializeField] private List<RageArea> _rageAreas;
    [SerializeField] private Boss _boss;
    [SerializeField] private CoinsModifierUpgradeButton _coinsModifierUpgradeButton;
    [SerializeField] private AdShower _adShower;
    [SerializeField] private GameButton _addCoinsButton;
    [SerializeField] private GamePanel _addCoinsPanel;

    private int _count;
    private int _totalEarnedCoins;
    private int _currentZombieReward;

    public int Count => _count;
    public int TotalEarnedCoins => _totalEarnedCoins;
    public int EarnModifier { get; private set; }

    public event UnityAction<int> CoinsAmountIncrease;
    public event UnityAction<int> CoinsAmountDecrease;
    public event UnityAction TotalCoinsAmountChanged;

    private void Awake()
    {
        OnCoinsModifierUpgraded();
        _totalEarnedCoins = PlayerPrefs.GetInt(PlayerPrefsKeys.TotalEarnedCoins, 0);
        int currentCoins = PlayerPrefs.GetInt(PlayerPrefsKeys.CurrentCoinsCount, 0);
        _count = currentCoins;
    }

    private void OnEnable()
    {
        _boss.Died += OnBossDied;
        _coinsModifierUpgradeButton.CoinsModifierUpgraded += OnCoinsModifierUpgraded;
        _adShower.VideoAdShowed += OnVideoAdShowed;
        _addCoinsButton.Clicked += OnAddCoinsButtonClicked;

        foreach (var rageArea in _rageAreas)
            rageArea.ZombieAttacked += OnZombieAttacked;
    }

    private void OnDisable()
    {
        _boss.Died -= OnBossDied;
        _coinsModifierUpgradeButton.CoinsModifierUpgraded -= OnCoinsModifierUpgraded;
        _adShower.VideoAdShowed -= OnVideoAdShowed;
        _addCoinsButton.Clicked -= OnAddCoinsButtonClicked;

        foreach (var rageArea in _rageAreas)
            rageArea.ZombieAttacked -= OnZombieAttacked;
    }

    private void OnAddCoinsButtonClicked()
    {
        _addCoinsPanel.gameObject.SetActive(true);
    }

    private void OnVideoAdShowed()
    {
        AddCoins(AdReward);
        _addCoinsPanel.gameObject.SetActive(false);
    }

    private void AddCoins(int count)
    {
        _count += count;
        PlayerPrefs.SetInt(PlayerPrefsKeys.CurrentCoinsCount, _count);
        _totalEarnedCoins += count;
        PlayerPrefs.SetInt(PlayerPrefsKeys.TotalEarnedCoins, _totalEarnedCoins);
        CoinsAmountIncrease?.Invoke(count);
        TotalCoinsAmountChanged?.Invoke();
    }

    private IEnumerator RemovingCoins(int count)
    {
        _count -= count;
        PlayerPrefs.SetInt(PlayerPrefsKeys.CurrentCoinsCount, _count);
        yield return new WaitForSeconds(0.1f);
        CoinsAmountDecrease?.Invoke(count);
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
        AddCoins(_boss.Reward + EarnModifier * BossEarnModifier);
    }

    private void OnCoinsModifierUpgraded()
    {
        EarnModifier = PlayerPrefs.GetInt(PlayerPrefsKeys.CoinsModifier, 0);
    }
    public void RemoveCoins(int count)
    {
        StartCoroutine(RemovingCoins(count));
    }
}
