using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class CoinsModifierUpgradeButton : UpgradeButton
{
    private int _startValue;

    public event UnityAction CoinsModifierUpgraded;

    protected override void OnEnable()
    {
        base.OnEnable();
        UpgradeabilityCheck(PlayerPrefsKeys.CoinsModifier, _startValue);
    }

    private void Start()
    {
        _startValue = CoinCounter.EarnModifier;
        Renew(PlayerPrefsKeys.CoinsModifier, _startValue, PlayerPrefsKeys.UpgradeCoinsModifierPrice);
    }

    protected override void OnUpgradeButtonClick()
    {
        BuyUpgrade(PlayerPrefsKeys.CoinsModifier, _startValue, PlayerPrefsKeys.UpgradeCoinsModifierPrice);
        CoinsModifierUpgraded?.Invoke();
    }

    protected override void OnPurchaseSuccsessed()
    {
        UpgradeabilityCheck(PlayerPrefsKeys.CoinsModifier, _startValue);
    }
}
