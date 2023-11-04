using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Boss))]
public class AttackState : BossState
{
    private const string AttackAnimationName = "Attack";

    [SerializeField] private Car _car;

    private Animator _animator;
    private Vector3 _offset;
    private Coroutine _attackCorutine;

    public Boss Stats { get; private set; }

    private void Awake()
    {
        _animator = GetComponent<Animator>();
        Stats = GetComponent<Boss>();
    }

    protected virtual void OnEnable()
    {
        _offset = _car.transform.position - transform.position;
        StopAttackCorutine();
        _attackCorutine = StartCoroutine(StartAttack());
    }

    private void FixedUpdate()
    {
        transform.position = _car.transform.position - _offset;
    }

    private void OnDisable()
    {
        StopAttackCorutine();
    }

    protected virtual void Attack()
    {
        //_animator.SetTrigger(AttackAnimationName);
    }

    private IEnumerator StartAttack()
    {
        WaitForSeconds delay = new WaitForSeconds(Stats.AttackDelayTime);

        while (enabled)
        {
            yield return delay;
            Attack();
        }
    }

    private void StopAttackCorutine()
    {
        if (_attackCorutine != null)
            StopCoroutine(StartAttack());
    }
}
