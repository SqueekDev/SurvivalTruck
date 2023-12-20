using System.Collections;
using UnityEngine;

[RequireComponent(typeof(Boss))]
public class AttackState : BossState
{
    private const int DelayDivider = 2;
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
        _animator.SetBool(AttackAnimationName, true);
    }

    private void FixedUpdate()
    {
        transform.position = _car.transform.position - _offset;
    }

    private void OnDisable()
    {
        StopAttackCorutine();
        _animator.SetBool(AttackAnimationName, false);
    }

    protected virtual void Attack() {}

    private IEnumerator StartAttack()
    {
        WaitForSeconds delay = new WaitForSeconds(Stats.AttackDelayTime/DelayDivider);

        while (enabled)
        {
            yield return delay;
            Attack();
            yield return delay;
        }
    }

    private void StopAttackCorutine()
    {
        if (_attackCorutine != null)
        {
            StopCoroutine(StartAttack());
        }
    }
}
