using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ZombieAttacker : MonoBehaviour
{
    [SerializeField] private float _timeBetweenAttacks;
    [SerializeField] private int _damage;
    [SerializeField] private ZombieMover _zombieMover;

    private Coroutine _obstacleAttacking;
    private Coroutine _attacking;
    private Animator _animator;

    private void Start()
    {
        _animator = GetComponent<Animator>();
    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.TryGetComponent(out Obstacle obstacle))
        {
            if (obstacle.IsDestroyed==false)
            {
                _zombieMover.StopMoveTo();
                Attack(obstacle);
            }
            else
            {
                _zombieMover.Jump();
            }

        }

    }
    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.TryGetComponent(out Health playerHealth))
        {
            _zombieMover.StopMoveTo();
            Attack(playerHealth);
        }
    }

    private void OnCollisionExit(Collision collision)
    {
        if (collision.gameObject.TryGetComponent(out Health playerHealth))
        {
            StopAttack();
            _zombieMover.MoveTo(playerHealth.transform);
        }
    }
    public void Attack(Obstacle obstacle)
    {
        if (_obstacleAttacking==null)
        {
            StartCoroutine(Attacking(obstacle));
        }
    }
    public void Attack(Health playerHealth)
    {
        if (_attacking == null)
        {
            StartCoroutine(Attacking(playerHealth));
        }
    }

    private  void StopAttack()
    {
        if (_attacking != null)
        {
            StopCoroutine(_attacking);
            _attacking = null;
            _animator.SetBool("Attack", false);
        }
    }

    private IEnumerator Attacking(Obstacle obstacle)
    {
        WaitForSeconds waitForSeconds = new WaitForSeconds(_timeBetweenAttacks);
        _animator.SetBool("Attack", true);

        while (obstacle.IsDestroyed==false)
        {
            obstacle.ApplyDamade(_damage);
            yield return null;
            yield return waitForSeconds;
        }
        _obstacleAttacking = null;
        _zombieMover.Jump();
    }
    private IEnumerator Attacking(Health playerHealth)
    {
        WaitForSeconds waitForSeconds = new WaitForSeconds(_timeBetweenAttacks);
        _animator.SetBool("Attack", true);
        while (playerHealth.gameObject.activeSelf)
        {
            playerHealth.TakeDamage(_damage);
            yield return null;
            yield return waitForSeconds;
        }
        _attacking = null;
        _animator.SetBool("Attack", false);

    }
}
