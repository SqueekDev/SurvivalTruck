using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ZombieHealth : Health
{
    [SerializeField] private LevelChanger _levelChanger;
    [SerializeField] private Mover _mover;
    [SerializeField] private int _reward;

    public int Reward => _reward;

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
        AddHealthMultiplier = _levelChanger.CurrentLevelNumber / _levelChanger.BossLevelNumber;
    }
}
