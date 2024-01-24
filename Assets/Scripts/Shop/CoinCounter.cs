using System;
using System.Collections.Generic;
using Base;
using Enemy;
using Level;
using Truck;
using UnityEngine;
using YandexSDK;

namespace Shop
{
    public class CoinCounter : MonoBehaviour
    {
        private const int BossEarnModifier = 10;
        private const int AdReward = 100;
        private const int AdRewardLevelModifier = 10;
        private const int LevelModifierCorrection = 1;

        [SerializeField] private List<RageArea> _rageAreas;
        [SerializeField] private CarShield _carShield;
        [SerializeField] private Boss _boss;
        [SerializeField] private CoinsModifierUpgradeButton _coinsModifierUpgradeButton;
        [SerializeField] private AdShower _adShower;
        [SerializeField] private LevelChanger _levelChanger;

        private int _count;
        private int _totalEarnedCoins;
        private int _currentZombieReward;

        public event Action<int> CoinsAmountChanged;

        public event Action VideoBonusAdded;

        public int Count => _count;

        public int EarnModifier { get; private set; }

        public int CurrentAdReward { get; private set; }

        private void Awake()
        {
            OnLevelChanged(_levelChanger.CurrentLevelNumber);
            OnCoinsModifierUpgraded();
            _totalEarnedCoins = PlayerPrefs.GetInt(PlayerPrefsKeys.TotalEarnedCoins, GlobalValues.Zero);
            int currentCoins = PlayerPrefs.GetInt(PlayerPrefsKeys.CurrentCoinsCount, GlobalValues.Zero);
            _count = currentCoins;
        }

        private void OnEnable()
        {
            _levelChanger.Changed += OnLevelChanged;
            _boss.Died += OnBossDied;
            _coinsModifierUpgradeButton.SkillUpgraded += OnCoinsModifierUpgraded;
            _adShower.VideoAdShowed += OnVideoAdShowed;
            _carShield.ZombieHited += OnZombieAttacked;

            foreach (var rageArea in _rageAreas)
            {
                rageArea.ZombieAttacked += OnZombieAttacked;
            }
        }

        private void OnDisable()
        {
            _levelChanger.Changed -= OnLevelChanged;
            _boss.Died -= OnBossDied;
            _coinsModifierUpgradeButton.SkillUpgraded -= OnCoinsModifierUpgraded;
            _adShower.VideoAdShowed -= OnVideoAdShowed;
            _carShield.ZombieHited -= OnZombieAttacked;

            foreach (var rageArea in _rageAreas)
            {
                rageArea.ZombieAttacked -= OnZombieAttacked;
            }
        }

        public void RemoveCoins(int count)
        {
            _count -= count;
            PlayerPrefs.SetInt(PlayerPrefsKeys.CurrentCoinsCount, _count);
            CoinsAmountChanged?.Invoke(-count);
        }

        private void AddCoins(int count)
        {
            _count += count;
            PlayerPrefs.SetInt(PlayerPrefsKeys.CurrentCoinsCount, _count);
            _totalEarnedCoins += count;
            PlayerPrefs.SetInt(PlayerPrefsKeys.TotalEarnedCoins, _totalEarnedCoins);
            CoinsAmountChanged?.Invoke(count);
        }

        private void OnLevelChanged(int level)
        {
            CurrentAdReward = AdReward + AdRewardLevelModifier * (level - LevelModifierCorrection);
        }

        private void OnVideoAdShowed()
        {
            AddCoins(CurrentAdReward);
            VideoBonusAdded?.Invoke();
        }

        private void OnZombieAttacked(ZombieHealth zombie)
        {
            zombie.Died -= OnZombieDied;
            zombie.Died += OnZombieDied;
            _currentZombieReward = zombie.Reward;
        }

        private void OnZombieDied(Health zombie)
        {
            zombie.Died -= OnZombieDied;
            AddCoins(_currentZombieReward + EarnModifier);
        }

        private void OnBossDied(Health boss)
        {
            AddCoins(_boss.Reward + EarnModifier * BossEarnModifier);
        }

        private void OnCoinsModifierUpgraded()
        {
            EarnModifier = PlayerPrefs.GetInt(PlayerPrefsKeys.CoinsModifier, GlobalValues.Zero);
        }
    }
}