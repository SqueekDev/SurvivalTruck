using Base;
using Player;
using UnityEngine;

namespace Shop
{
    public class WeaponShootDelayUpgradeButton : UpgradeButton
    {
        [SerializeField] private Weapon _weapon;

        private void Awake()
        {
            PlayerPrefsCurrentValue = PlayerPrefsKeys.WeaponShootDelay;
            PlayerPrefsPrice = PlayerPrefsKeys.UpgradeWeaponShootDelayPrice;
        }

        protected override void OnEnable()
        {
            StartValue = (int)_weapon.TimeBetweenShoot;
            base.OnEnable();
        }
    }
}