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
        yield return waitForSeconds;
        _attacking = null;
    }

    public void Attack(Obstacle obstacle)
    {
        if (obstacle.transform.parent.position.x>transform.position.x)
        {
            transform.localEulerAngles = new Vector3(0,90,0);
        }
        if (obstacle.transform.parent.position.x < transform.position.x)
        {
            transform.localEulerAngles = new Vector3(0, -90, 0);
        }
        if (_obstacleAttacking == null)
        {
            _obstacleAttacking = StartCoroutine(Attacking(obstacle));
        }
    }

    public void Attack(Player player)
    {
        transform.LookAt(player.transform);

        if (_attacking == null)
        {
            _attacking = StartCoroutine(Attacking(player));
        }
    }
}
