using Base;

namespace Shop
{
    public class CoinsModifierUpgradeButton : UpgradeButton
    {
        private void Awake()
        {
            PlayerPrefsCurrentValue = PlayerPrefsKeys.CoinsModifier;
            PlayerPrefsPrice = PlayerPrefsKeys.UpgradeCoinsModifierPrice;
        }

        protected override void OnEnable()
        {
            StartValue = CoinCounter.EarnModifier;
            base.OnEnable();
        }
    }
}