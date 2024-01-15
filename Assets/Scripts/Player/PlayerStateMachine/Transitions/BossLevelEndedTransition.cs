using Base;
using Level;
using UnityEngine;

namespace Player
{
    public class BossLevelEndedTransition : Transition
    {
        [SerializeField] private LevelChanger _levelChanger;

        protected override void OnEnable()
        {
            base.OnEnable();
            _levelChanger.BossLevelEnded += OnBossLevelEnded;
        }

        private void OnDisable()
        {
            _levelChanger.BossLevelEnded -= OnBossLevelEnded;
        }

        private void OnBossLevelEnded()
        {
            NeedTransit = true;
        }
    }
}