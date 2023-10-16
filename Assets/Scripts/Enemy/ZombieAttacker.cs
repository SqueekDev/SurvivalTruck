using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class ZombieAttacker : MonoBehaviour
{
    [SerializeField] private float _timeBetweenAttacks;
    [SerializeField] private int _damage;

    private Coroutine _obstacleAttacking;
    private Coroutine _attacking;

<<<<<<< Updated upstream
    private void Start()
    {
        _animator = GetComponent<Animator>();
    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.TryGetComponent(out Obstacle obstacle))
        {
            Attack(obstacle);
        }
        if (other.TryGetComponent(out Health playerHealth))
        {
            Attack(obstacle);
        }
    }
=======
    public event UnityAction OnObstacleDestroyed;
>>>>>>> Stashed changes

    public void Attack(Obstacle obstacle)
    {
        if (_obstacleAttacking==null)
        {
            _obstacleAttacking=StartCoroutine(Attacking(obstacle));
        }
    }
    public void Attack(Player player)
    {
        if (_attacking == null)
        {

<<<<<<< Updated upstream
    private IEnumerator Attacking(Obstacle obstacle)
    {
        WaitForSeconds waitForSeconds = new WaitForSeconds(_timeBetweenAttacks);
        _animator.SetBool("Attack", true);
        while (obstacle.gameObject.activeSelf)
        {
=======
            _attacking = StartCoroutine(Attacking(player));
        }
    }

    private IEnumerator Attacking(Obstacle obstacle)
    {
        WaitForSeconds waitForSeconds = new WaitForSeconds(_timeBetweenAttacks);

>>>>>>> Stashed changes
            obstacle.ApplyDamade(_damage);
            yield return waitForSeconds;
        if (obstacle.IsDestroyed)
        {
            OnObstacleDestroyed?.Invoke();
        }
        _obstacleAttacking = null;
<<<<<<< Updated upstream
        _zombieMover.SetNeedMoveToTarget();
        _animator.SetTrigger("Jump");

=======
>>>>>>> Stashed changes
    }
    private IEnumerator Attacking(Health playerHealth)
    {
        WaitForSeconds waitForSeconds = new WaitForSeconds(_timeBetweenAttacks);
<<<<<<< Updated upstream
        _animator.SetBool("Attack", true);
        while (playerHealth.gameObject.activeSelf)
        {
            playerHealth.TakeDamage(_damage);
            yield return null;
            yield return waitForSeconds;
        }
        _attacking = null;
        _zombieMover.SetNeedMoveToTarget();
        _animator.SetBool("Attack", false);
=======
>>>>>>> Stashed changes

            playerHealth.TakeDamage(_damage);
            Debug.Log("za");

            yield return waitForSeconds;
        
        _attacking = null;
    }
}
