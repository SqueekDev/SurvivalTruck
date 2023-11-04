using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class WeaponDamageUpgradeButton : UpgradeButton
{
    [SerializeField] private Weapon _weapon;

    private int _startDamage;

    public event UnityAction DamageUpgraded;

    protected override void OnEnable()
    {
        base.OnEnable();
        UpgradeabilityCheck(PlayerPrefsKeys.WeaponDamage, _startDamage);
    }

    private void Start()
    {
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
