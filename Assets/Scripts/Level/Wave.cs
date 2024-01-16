using System;
using System.Collections.Generic;
using Base;
using Enemy;
using Truck;
using UnityEngine;

namespace Level
{
    public class Wave : MonoBehaviour
    {
        private const int LevelToAddedZombieMultiplier = 3;
        private const int MaxZombiesInWaveCount = 14;

        [SerializeField] private LevelChanger _levelChanger;
        [SerializeField] private List<RageArea> _rageAreas;
        [SerializeField] private Boss _boss;
        [SerializeField] private int _startZombieinWaveCount;

        private int _attackingZombiesCount = 0;
        private int _ragedZombieCount = 0;
        private int _zombiesInWaveCount;
        private bool _isMaxZombieCount = false;

        public event Action Ended;
        public event Action AllZombiesAttacked;
        public event Action<int, int> ZombieCountChanged;
        public event Action<ZombieHealth> ZombieAttacked;

        private void OnEnable()
        {
            _levelChanger.Changed += OnlevelChanged;
            _boss.Died += OnBossDied;

            foreach (var rageArea in _rageAreas)
            {
                rageArea.ZombieAttacked += OnZombieAttacked;
            }
        }

        private void OnDisable()
        {
            _levelChanger.Changed -= OnlevelChanged;
            _boss.Died -= OnBossDied;

            foreach (var rageArea in _rageAreas)
            {
                rageArea.ZombieAttacked -= OnZombieAttacked;
            }
        }

        private void ChangeZombiesCount(int levelNumber)
        {
            _zombiesInWaveCount = _startZombieinWaveCount + levelNumber / LevelToAddedZombieMultiplier;

            if (_zombiesInWaveCount >= MaxZombiesInWaveCount)
            {
                _zombiesInWaveCount = MaxZombiesInWaveCount;
                _isMaxZombieCount = true;
            }
        }

        private void OnlevelChanged(int levelNumber)
        {
            if (_isMaxZombieCount == false)
            {
                ChangeZombiesCount(levelNumber);
            }

            _attackingZombiesCount = _zombiesInWaveCount;
            ZombieCountChanged?.Invoke(levelNumber, _zombiesInWaveCount);
        }

        private void OnBossDied(Health boss)
        {
            Ended?.Invoke();
        }

        private void OnZombieAttacked(ZombieHealth zombie)
        {
            zombie.Died += OnZombieDied;
            _ragedZombieCount++;
            ZombieAttacked?.Invoke(zombie);

            if (_ragedZombieCount >= _zombiesInWaveCount)
            {
                AllZombiesAttacked?.Invoke();
            }
        }

        private void OnZombieDied(Health zombie)
        {
            zombie.Died -= OnZombieDied;
            _attackingZombiesCount--;

            if (_attackingZombiesCount <= GlobalValues.Zero)
            {
                _ragedZombieCount = GlobalValues.Zero;
                Ended?.Invoke();
            }
        }
    }
}