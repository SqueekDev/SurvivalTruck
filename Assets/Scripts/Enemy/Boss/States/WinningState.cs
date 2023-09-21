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
        _offset = _car.transform.position - transform.position;
    }

    private void OnEnable()
    {
        //_animator.SetBool(WinAnimationName, true);
    }

    private void OnDisable()
    {
        //_animator.SetBool(WinAnimationName, false);
    }

    private void FixedUpdate()
    {
        transform.position = _car.transform.position - _offset;
    }
}
