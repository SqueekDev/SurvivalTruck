using System;
using UnityEngine;

public class PlayerHealthUpgradeButton : UpgradeButton
{
    [SerializeField] private Player _player;

    private int _startHealth;

    public event Action HealthUpgraded;

    protected override void OnEnable()
    {
        base.OnEnable();
        UpgradeabilityCheck(PlayerPrefsKeys.PlayerHealth, _startHealth);
        _startHealth = _player.MaxHealth;
        Renew(PlayerPrefsKeys.PlayerHealth, _startHealth, PlayerPrefsKeys.UpgradePlayerHealthPrice);
    }

    protected override void OnUpgradeButtonClick()
    {
        BuyUpgrade(PlayerPrefsKeys.PlayerHealth, _startHealth, PlayerPrefsKeys.UpgradePlayerHealthPrice);
        HealthUpgraded?.Invoke();
    }

    protected override void OnPurchaseSuccsessed()
    {
        UpgradeabilityCheck(PlayerPrefsKeys.PlayerHealth, _startHealth);
    }
}
