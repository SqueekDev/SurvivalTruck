using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class CoinsModifierUpgradeButton : UpgradeButton
{
    private int _startValue;

    public event UnityAction CoinsModifierUpgraded;

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
}
