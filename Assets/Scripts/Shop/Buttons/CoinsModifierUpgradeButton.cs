using System;
using Base;

namespace Shop
{
    public class CoinsModifierUpgradeButton : UpgradeButton
    {
        private int _startValue;

        public event Action CoinsModifierUpgraded;

        protected override void OnEnable()
        {
            base.OnEnable();
            UpgradeabilityCheck(PlayerPrefsKeys.CoinsModifier, _startValue);
            _startValue = CoinCounter.EarnModifier;
            Renew(PlayerPrefsKeys.CoinsModifier, _startValue, PlayerPrefsKeys.UpgradeCoinsModifierPrice);
        }

        private void Start()
        {
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
}