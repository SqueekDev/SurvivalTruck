using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class KangarooDamageUpgradeButton : UpgradeButton
{
    [SerializeField] private Kangaroo _kangaroo;

    private int _startDamage;

    public event UnityAction DamageUpgraded;

    private void Start()
    {
        _startDamage = _kangaroo.Damage;
        Renew(PlayerPrefsKeys.KangarooDamage, _startDamage, PlayerPrefsKeys.UpgradeKangarooDamagePrice);
    }

    protected override void OnUpgradeButtonClick()
    {
        BuyUpgrade(PlayerPrefsKeys.KangarooDamage, _startDamage, PlayerPrefsKeys.UpgradeKangarooDamagePrice);
        DamageUpgraded?.Invoke();
    }
}
