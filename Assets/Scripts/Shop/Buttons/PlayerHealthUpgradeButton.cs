using Base;
using Player;
using UnityEngine;

namespace Shop
{
    public class PlayerHealthUpgradeButton : UpgradeButton
    {
        [SerializeField] private PlayerHealth _player;

        private void Awake()
        {
            PlayerPrefsCurrentValue = PlayerPrefsKeys.PlayerHealth;
            PlayerPrefsPrice = PlayerPrefsKeys.UpgradePlayerHealthPrice;
        }

        protected override void OnEnable()
        {
            StartValue = _player.MaxHealth;
            base.OnEnable();
        }
    }
}