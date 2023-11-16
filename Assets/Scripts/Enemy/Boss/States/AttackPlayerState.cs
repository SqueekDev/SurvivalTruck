using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AttackPlayerState : AttackState
{
    [SerializeField] private Player _player;

    protected override void Attack()
    {
        _player.TakeDamage(Stats.Damage);
    }
}
