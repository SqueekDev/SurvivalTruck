using System;
using UnityEngine;

public class WeaponDamageUpgradeButton : UpgradeButton
{
    [SerializeField] private Weapon _weapon;

    private int _startDamage;

    public event Action DamageUpgraded;

    protected override void OnEnable()
    {
        base.OnEnable();
        UpgradeabilityCheck(PlayerPrefsKeys.WeaponDamage, _startDamage);
        _startDamage = _weapon.Damage;
        Renew(PlayerPrefsKeys.WeaponDamage, _startDamage, PlayerPrefsKeys.UpgradeWeaponDamagePrice);
    }

    protected override void OnUpgradeButtonClick()
    {
        BuyUpgrade(PlayerPrefsKeys.WeaponDamage, _startDamage, PlayerPrefsKeys.UpgradeWeaponDamagePrice);
        DamageUpgraded?.Invoke();
    }

    protected override void OnPurchaseSuccsessed()
    {
        UpgradeabilityCheck(PlayerPrefsKeys.WeaponDamage, _startDamage);
    }
}
