using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AttackObstacleState : AttackState
{
    [SerializeField] private Obstacle _obstacle;
    [SerializeField] private Health _head;
    [SerializeField] private BossHealthBar _headHealthBar;
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
        _obstacle.ApplyDamade(Stats.Damage);
    }
}
