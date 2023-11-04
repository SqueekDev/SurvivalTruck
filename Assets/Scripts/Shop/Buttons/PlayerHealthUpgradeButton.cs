using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class PlayerHealthUpgradeButton : UpgradeButton
{
    [SerializeField] private Player _player;

    private int _startHealth;

    public event UnityAction HealthUpgraded;

    protected override void OnEnable()
    {
        base.OnEnable();
        UpgradeabilityCheck(PlayerPrefsKeys.PlayerHealth, _startHealth);
    }

    private void Start()
    {
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
