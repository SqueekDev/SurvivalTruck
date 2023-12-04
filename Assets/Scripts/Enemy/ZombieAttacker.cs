using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class ZombieAttacker : MonoBehaviour
{
    [SerializeField] private float _timeBetweenAttacks;
    [SerializeField] private int _startDamage;
    [SerializeField] private LevelChanger _levelChanger;

    private Coroutine _obstacleAttacking;
    private Coroutine _attacking;
    private int _currentDamage;
    private int _damageModifier;
    private int _midDamageModifierValue = 2;
    private int _maxDamageModifierValue = 6;
    [SerializeField]private bool _isAttacking;

    public bool IsAttacking=>_isAttacking;

    public event UnityAction OnObstacleDestroyed;

    private void Awake()
    {
        _damageModifier = Random.Range(_midDamageModifierValue, _maxDamageModifierValue);
    }

    private void OnEnable()
    {
        _isAttacking = false;
        _levelChanger.Changed += OnLevelChanged;
        OnLevelChanged(_levelChanger.CurrentLevelNumber);
    }

    private void OnDisable()
    {
        _levelChanger.Changed -= OnLevelChanged;        
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

    private void OnLevelChanged(int levelNumber)
    {
        _currentDamage = _startDamage + (levelNumber / _damageModifier);
    }

    private IEnumerator Attacking(Obstacle obstacle)
    {
        _isAttacking = true;
        WaitForSeconds waitForSeconds = new WaitForSeconds(_timeBetweenAttacks);
        obstacle.ApplyDamade(_currentDamage);
        yield return waitForSeconds;

        if (obstacle.IsDestroyed)
        {
            OnObstacleDestroyed?.Invoke();
        }

        _obstacleAttacking = null;
        _isAttacking = false;
    }

    private IEnumerator Attacking(Health playerHealth)
    {
        _isAttacking = true;
        WaitForSeconds waitForSeconds = new WaitForSeconds(_timeBetweenAttacks);
        playerHealth.TakeDamage(_currentDamage);
        yield return waitForSeconds;
        _attacking = null;
        _isAttacking = false;
    }
}
