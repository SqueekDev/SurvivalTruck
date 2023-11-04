using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class WeaponDamageUpgradeButton : UpgradeButton
{
    [SerializeField] private Weapon _weapon;

    private int _startDamage;

    public event UnityAction DamageUpgraded;

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
}