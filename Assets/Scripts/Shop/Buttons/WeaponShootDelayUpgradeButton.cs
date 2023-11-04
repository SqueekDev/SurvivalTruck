using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class WeaponShootDelayUpgradeButton : UpgradeButton
{
    [SerializeField] private Weapon _weapon;

    private int _startShootDelay;

    public event UnityAction ShootDelayUpgraded;

    private void Start()
    {
        _startShootDelay = (int)_weapon.TimeBetweenShoot;
        Renew(PlayerPrefsKeys.WeaponShootDelay, _startShootDelay, PlayerPrefsKeys.UpgradeWeaponShootDelayPrice);
    }

    protected override void OnUpgradeButtonClick()
    {
        BuyUpgrade(PlayerPrefsKeys.WeaponShootDelay, _startShootDelay, PlayerPrefsKeys.UpgradeWeaponDamagePrice);
        ShootDelayUpgraded?.Invoke();
    }
}
