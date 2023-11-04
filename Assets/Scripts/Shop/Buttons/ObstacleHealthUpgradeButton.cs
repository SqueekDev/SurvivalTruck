using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class ObstacleHealthUpgradeButton : UpgradeButton
{
    [SerializeField] private Obstacle _obstacle;

    private int _startHealth;

    public event UnityAction HealthUpgraded;

    protected override void OnEnable()
    {
        base.OnEnable();
        UpgradeabilityCheck(PlayerPrefsKeys.ObstacleHealth, _startHealth);
    }

    private void Start()
    {
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
