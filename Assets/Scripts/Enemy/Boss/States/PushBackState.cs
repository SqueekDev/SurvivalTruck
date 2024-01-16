using System;
using UI;
using UnityEngine;

namespace Enemy
{
    public class PushBackState : JumpingOnCarState
    {
        private const string FallingBoolName = "Fall";

        [SerializeField] private HealthBar _headHealthBar;
        [SerializeField] private Collider _bossCollider;

        public event Action Fell;

        protected override void OnEnable()
        {
            _bossCollider.enabled = true;
            _headHealthBar.gameObject.SetActive(false);
            BossAnimator.SetBool(FallingBoolName, true);
        }

        protected override void OnDisable()
        {
            BossAnimator.SetBool(FallingBoolName, false);
        }

        private void Update()
        {
            if (Target.transform.position.z - transform.position.z > ZOffset)
            {
                Fell?.Invoke();
            }
        }
    }
}