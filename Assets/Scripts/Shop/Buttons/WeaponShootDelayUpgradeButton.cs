using System;
using UnityEngine;

public class WeaponShootDelayUpgradeButton : UpgradeButton
{
    [SerializeField] private Weapon _weapon;

    private int _startShootDelay;

    public event Action ShootDelayUpgraded;

    protected override void OnEnable()
    {
        base.OnEnable();
        UpgradeabilityCheck(PlayerPrefsKeys.WeaponShootDelay, _startShootDelay);
        _startShootDelay = (int)_weapon.TimeBetweenShoot;
        Renew(PlayerPrefsKeys.WeaponShootDelay, _startShootDelay, PlayerPrefsKeys.UpgradeWeaponShootDelayPrice);
    }

    protected override void OnUpgradeButtonClick()
    {
        BuyUpgrade(PlayerPrefsKeys.WeaponShootDelay, _startShootDelay, PlayerPrefsKeys.UpgradeWeaponShootDelayPrice);
        ShootDelayUpgraded?.Invoke();
    }

    protected override void OnPurchaseSuccsessed()
    {
        UpgradeabilityCheck(PlayerPrefsKeys.WeaponShootDelay, _startShootDelay);
    }
}
