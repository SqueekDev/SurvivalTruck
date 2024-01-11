using UnityEngine;

public class AttackPlayerState : AttackState
{
    private const int DamageMultiplier = 3;

    [SerializeField] private PlayerHealth _player;

    protected override void Attack()
    {
        _player.TakeDamage(Stats.Damage * DamageMultiplier);
    }
}
