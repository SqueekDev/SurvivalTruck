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


    public event UnityAction OnObstacleDestroyed;

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

            _attacking = StartCoroutine(Attacking(player));
        }
    }

    private IEnumerator Attacking(Obstacle obstacle)
    {
        WaitForSeconds waitForSeconds = new WaitForSeconds(_timeBetweenAttacks);

            obstacle.ApplyDamade(_damage);
            yield return waitForSeconds;
        if (obstacle.IsDestroyed)
        {
            OnObstacleDestroyed?.Invoke();
        }
        _obstacleAttacking = null;

    }
    private IEnumerator Attacking(Health playerHealth)
    {
        WaitForSeconds waitForSeconds = new WaitForSeconds(_timeBetweenAttacks);


            playerHealth.TakeDamage(_damage);
            Debug.Log("za");

            yield return waitForSeconds;
        
        _attacking = null;
    }
}
