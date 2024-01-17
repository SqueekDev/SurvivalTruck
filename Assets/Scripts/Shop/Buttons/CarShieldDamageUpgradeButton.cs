using Base;
using Truck;
using UnityEngine;

namespace Shop
{
    public class CarShieldDamageUpgradeButton : UpgradeButton
    {
        [SerializeField] private CarShield _carShield;

        private void Awake()
        {
            PlayerPrefsCurrentValue = PlayerPrefsKeys.CarShieldDamage;
            PlayerPrefsPrice = PlayerPrefsKeys.UpgradeCarShieldDamagePrice;
        }

        protected override void OnEnable()
        {
            StartValue = _carShield.Damage;
            base.OnEnable();
        }
    }
}