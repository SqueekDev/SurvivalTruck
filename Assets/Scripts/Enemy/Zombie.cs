using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Zombie : Health
{
    private const int MultiplyHealthDivider = 10;

    [SerializeField] private LevelChanger _levelChanger;
    [SerializeField] private Mover _mover;

    protected override void OnEnable()
    {
        ChangeHealthMultiplier();
        base.OnEnable();
        _mover.SetStartSpeed();
    }

    protected override void Die()
    {
        _mover.SetNoSpeed();
        base.Die();
    }

    private void ChangeHealthMultiplier()
    {
        AddHealthMultiplier = _levelChanger.CurrentLevelNumber / MultiplyHealthDivider;
    }
}
