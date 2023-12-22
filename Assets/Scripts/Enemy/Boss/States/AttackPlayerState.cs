using UnityEngine;

public class AttackPlayerState : AttackState
{
    private const int DamageMultiplier = 3;

    [SerializeField] private Player _player;

    protected override void Attack()
    {
        _player.TakeDamage(Stats.Damage * DamageMultiplier);
    }
}
