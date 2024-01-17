using Base;
using Level;
using Shop;
using UnityEngine;

namespace Player
{
    public class PlayerHealth : Health
    {
        [SerializeField] private PlayerHealthUpgradeButton _playerHealthUpgradeButton;
        [SerializeField] private LevelChanger _levelChanger;

        protected override void OnEnable()
        {
            base.OnEnable();
            _playerHealthUpgradeButton.SkillUpgraded += OnMaxHealthUpgraded;
            _levelChanger.Changed += OnLevelChanged;
        }

        private void OnDisable()
        {
            _playerHealthUpgradeButton.SkillUpgraded -= OnMaxHealthUpgraded;
            _levelChanger.Changed -= OnLevelChanged;
        }

        protected override void ChangeMaxHealth()
        {
            MaxHealth = PlayerPrefs.GetInt(PlayerPrefsKeys.PlayerHealth, MaxHealth);
            Heal(MaxHealth);
        }

        private void OnLevelChanged(int level)
        {
            ChangeMaxHealth();
        }

        private void OnMaxHealthUpgraded()
        {
            ChangeMaxHealth();
        }
    }
}