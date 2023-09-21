using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Boss))]
public class AttackObstacleState : AttackState
{
    [SerializeField] private Obstacle _obstacle;
    [SerializeField] private Health _head;

    protected override void OnEnable()
    {
        base.OnEnable();
        _head.gameObject.SetActive(true);
    }

    protected override void Attack()
    {
        base.Attack();
        _obstacle.ApplyDamade(Stats.Damage);
    }
}
