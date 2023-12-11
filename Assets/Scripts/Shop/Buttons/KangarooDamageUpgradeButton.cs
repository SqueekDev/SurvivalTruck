using UnityEngine;
using UnityEngine.Events;

public class KangarooDamageUpgradeButton : UpgradeButton
{
    [SerializeField] private Kangaroo _kangaroo;

    private int _startDamage;

    public event UnityAction DamageUpgraded;

    protected override void OnEnable()
    {
        base.OnEnable();
        UpgradeabilityCheck(PlayerPrefsKeys.KangarooDamage, _startDamage);
        _startDamage = _kangaroo.Damage;
        Renew(PlayerPrefsKeys.KangarooDamage, _startDamage, PlayerPrefsKeys.UpgradeKangarooDamagePrice);
    }

    protected override void OnUpgradeButtonClick()
    {
        BuyUpgrade(PlayerPrefsKeys.KangarooDamage, _startDamage, PlayerPrefsKeys.UpgradeKangarooDamagePrice);
        DamageUpgraded?.Invoke();
    }

    protected override void OnPurchaseSuccsessed()
    {
        UpgradeabilityCheck(PlayerPrefsKeys.KangarooDamage, _startDamage);
    }
}
