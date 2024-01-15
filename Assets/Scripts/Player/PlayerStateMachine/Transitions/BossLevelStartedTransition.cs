using Base;
using Level;
using UnityEngine;

namespace Player
{
    public class BossLevelStartedTransition : Transition
    {
        [SerializeField] private LevelChanger _levelChanger;

        protected override void OnEnable()
        {
            base.OnEnable();
            _levelChanger.BossLevelStarted += OnBossLevelStarted;
        }

        private void OnDisable()
        {
            _levelChanger.BossLevelStarted -= OnBossLevelStarted;
        }

        private void OnBossLevelStarted()
        {
            NeedTransit = true;
        }
    }
}