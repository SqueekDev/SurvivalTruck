using Base;
using Truck;
using UI;
using UnityEngine;

namespace Enemy
{
    public class AttackObstacleState : AttackState
    {
        [SerializeField] private Obstacle _obstacle;
        [SerializeField] private Health _head;
        [SerializeField] private HealthBar _headHealthBar;
        [SerializeField] private Collider _bossCollider;

        protected override void OnEnable()
        {
            base.OnEnable();
            _bossCollider.enabled = false;
            _headHealthBar.gameObject.SetActive(true);
            _head.gameObject.SetActive(true);
        }

        protected override void Attack()
        {
            _obstacle.ApplyDamade(Boss.Damage);
        }
    }
}