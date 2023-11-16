using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Animator))]
public class WinningState : BossState
{
    private const string WinAnimationName = "Win";

    [SerializeField] private Car _car;

    private Vector3 _offset;
    private Animator _animator;

    private void Awake()
    {
        _animator = GetComponent<Animator>();
    }

    private void OnEnable()
    {
        _offset = _car.transform.position - transform.position;
        _animator.SetTrigger(WinAnimationName);
    }

    private void FixedUpdate()
    {
        transform.position = _car.transform.position - _offset;
    }
}
