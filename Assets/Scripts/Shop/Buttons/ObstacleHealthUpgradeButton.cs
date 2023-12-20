using System;
using UnityEngine;

public class ObstacleHealthUpgradeButton : UpgradeButton
{
    [SerializeField] private Obstacle _obstacle;

    private int _startHealth;

    public event Action HealthUpgraded;

    protected override void OnEnable()
    {
        base.OnEnable();
        UpgradeabilityCheck(PlayerPrefsKeys.ObstacleHealth, _startHealth);
        _startHealth = _obstacle.MaxHealth;
        Renew(PlayerPrefsKeys.ObstacleHealth, _startHealth, PlayerPrefsKeys.UpgradeObstacleHealthPrice);
    }

    protected override void OnUpgradeButtonClick()
    {
        BuyUpgrade(PlayerPrefsKeys.ObstacleHealth, _startHealth, PlayerPrefsKeys.UpgradeObstacleHealthPrice);
        HealthUpgraded?.Invoke();
    }

    protected override void OnPurchaseSuccsessed()
    {
        UpgradeabilityCheck(PlayerPrefsKeys.ObstacleHealth, _startHealth);
    }
}
