using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Boss))]
public class AttackState : BossState
{
    [SerializeField] private Car _car;

    private Vector3 _offset;
    private Coroutine _attackCorutine;

    public Boss Stats { get; private set; }

    protected virtual void OnEnable()
    {
        StopAttackCorutine();
        _attackCorutine = StartCoroutine(StartAttack());
    }

    private void Start()
    {
        Stats = GetComponent<Boss>();
        _offset = _car.transform.position - transform.position;
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
