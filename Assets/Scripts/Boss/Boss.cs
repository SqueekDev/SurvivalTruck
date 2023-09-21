using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class Boss : Health
{
    [SerializeField] private int _startReward;
    [SerializeField] private int _damage;
    [SerializeField] private int _attackDelayTime;

    public int Damage => _damage;
    public float AttackDelayTime => _attackDelayTime;
}
