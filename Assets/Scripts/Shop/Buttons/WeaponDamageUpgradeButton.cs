using Base;
using Player;
using UnityEngine;

namespace Shop
{
    public class WeaponDamageUpgradeButton : UpgradeButton
    {
        [SerializeField] private Weapon _weapon;

        private void Awake()
        {
            PlayerPrefsCurrentValue = PlayerPrefsKeys.WeaponDamage;
            PlayerPrefsPrice = PlayerPrefsKeys.UpgradeWeaponDamagePrice;
        }

        protected override void OnEnable()
        {
            StartValue = _weapon.Damage;
            base.OnEnable();
        }
    }
}