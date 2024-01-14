using UnityEngine;
using Base;
using Level;

namespace Enemy
{
    public class Boss : Health
    {
        private const int Correction = 1;

        [SerializeField] private LevelChanger _levelChanger;
        [SerializeField] private int _reward;
        [SerializeField] private int _damage;
        [SerializeField] private float _attackDelayTime;

        public int Reward => _reward;
        public int Damage => _damage;
        public float AttackDelayTime => _attackDelayTime;

        protected override void OnEnable()
        {
            ChangeHealthMultiplier();
            base.OnEnable();
        }

        private void ChangeHealthMultiplier()
        {
            AddHealthMultiplier = _levelChanger.CurrentLevelNumber / (_levelChanger.BossLevelNumber + Correction);
        }
    }
}
