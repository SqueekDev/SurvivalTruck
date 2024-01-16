using Base;
using Level;
using UnityEngine;

namespace Enemy
{
    public class ZombieHealth : Health
    {
        [SerializeField] private LevelChanger _levelChanger;
        [SerializeField] private Mover _mover;
        [SerializeField] private int _reward;

        private bool _isAngry = false;

        public int Reward => _reward;

        public bool IsAngry => _isAngry;

        protected override void OnEnable()
        {
            ChangeHealthMultiplier();
            base.OnEnable();
            _mover.SetStartSpeed();
            _isAngry = false;
        }

        private void Update()
        {
            if (transform.position.y < 0)
            {
                gameObject.SetActive(false);
            }
        }

        public void SetAngry()
        {
            _isAngry = true;
        }

        protected override void Die()
        {
            _mover.SetZeroSpeed();
            base.Die();
        }

        private void ChangeHealthMultiplier()
        {
            AddHealthMultiplier = _levelChanger.CurrentLevelNumber / _levelChanger.BossLevelNumber;
        }
    }
}